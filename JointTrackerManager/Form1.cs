using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;

namespace JointTrackerManager
{
    public partial class MainForm : Form
    {
        private BindingSource data;
        private List<JointConfigure> bones;
        private ProcessStartInfo python;

        private Log log;

        private string cachedFirmwareDirectory;

        private GitHubReleaseCheck github;

        public MainForm()
        {
            InitializeComponent();

            log = new Log();
        }

        private void OpenBones(string path)
        {
            data.Clear();
            if (File.Exists(path))
            {
                using (TextReader tr = new StreamReader(path))
                {
                    string line;
                    while ((line = tr.ReadLine()) != null)
                    {
                        if (line.StartsWith("//") ||
                            line.StartsWith(";") ||
                            line.StartsWith("#") ||
                            line.StartsWith("\"")) continue;

                        string[] row = line.Split(',');
                        try
                        {
                            var joint = new JointConfigure(row);
                            data.Add(joint);
                        }
                        catch (Exception e)
                        {
                            log.WriteLine("ボーン構造読み取りエラー: " + line);
                        }
                    }
                }
            }
        }

        bool tryingModuleInstall = false;

        private void InstallESPTool()
        {
            Task.Run(() => {
                Process p;
                try
                {
                    p = Process.Start(python);
                }
                catch (Exception e)
                {
                    log.WriteLineAsync("Pythonの起動に失敗しました。Firmwareの書き込み機能は利用できません。");
                    log.WriteLineAsync(e.Message);
                    return;
                }

                string t;
                bool hasEsptool = false;
                bool hasPyserial = false;
                while ((t = p.StandardOutput.ReadLine()) != null)
                {
                    string package = t.Split(' ')[0];
                    if (package == "esptool")
                    {
                        log.WriteLineAsync("pythonモジュール 'esptool' が見つかりました。");
                        if ((hasEsptool = true) && hasPyserial)
                        {
                            log.WriteLineAsync("Firmwareの書き込みが利用できます");
                            OnCompleteModuleCheck();
                            break;
                        }
                    } else if (package == "pyserial")
                    {
                        log.WriteLineAsync("pythonモジュール 'pyserial' が見つかりました");
                        if (hasEsptool && (hasPyserial = true))
                        {
                            log.WriteLineAsync("Firmwareの書き込みが利用できます");
                            OnCompleteModuleCheck();
                            break;
                        }
                    }
                }

                if (!hasEsptool || !hasPyserial)
                {
                    if (tryingModuleInstall)
                    {
                        log.WriteLineAsync("必要なモジュールのインストールに失敗しました。次回起動時に再試行されます。");
                        return;
                    }
                    string modules = hasEsptool ? "pyserial" : (hasPyserial ? "esptool" : "esptool pyserial");
                    if (MessageBox.Show(
                        "Firmwareの書き込みを利用するためには、pythonモジュール " + modules + " が必要です。現環境にインストールしますか？",
                        "Ask to install esptool",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        tryingModuleInstall = true;
                        python.Arguments = "/c python -m pip install " + modules;
                        Process.Start(python).WaitForExit();
                        InstallESPTool();
                    }
                }
            });
        }


        private void bParentConfigure_Click(object sender, EventArgs e)
        {
            bParentConfigure.Enabled = false;

            SerialPort port;
            try
            {
                port = new SerialPort(cbParentPort.SelectedItem.ToString(), 115200);
            }
            catch (Exception exc)
            {
                log.WriteLine("ポートを開くのに失敗しました。\n > " + exc.Message);
                return;
            }

            log.WriteLine("ParentデバイスのConfigureを開始します");

            Task configureTask = Task.Run(() =>
            {
                const UInt32 cmd_wifi = 0x128422fd;
                const UInt32 cmd_reboot = 0xff8d91a8;
                const UInt32 cmd_bonecount = 0x38771d81;
                const UInt32 cmd_fixbone = 0xf729adc8;
                const UInt32 cmd_movable = 0xab8cf912;
                const UInt32 cmd_host = 0x431fac89;

                JointConfigure[] fix = bones.Where(x => x.IsMovableBone == JointConfigure.BoneKind.Fix).ToArray();
                JointConfigure[] mov = bones.Where(x => x.IsMovableBone == JointConfigure.BoneKind.Movable).ToArray();

                int fix_count = fix.Length;
                int mov_count = mov.Length;

                bool host_configured = false;
                string[] ipValues = tbIp.Text.Split('.').ToArray();
                if (ipValues.Length != 4)
                {
                    host_configured = true;
                    log.WriteLineAsync("IP addressの書式が間違っています。Configureされません");
                }

                byte[] ip = { 0, 0, 0, 0 };
                if (ipValues.All(x => byte.TryParse(x, out _)))
                {
                    ip = ipValues.Select(x => byte.Parse(x)).ToArray();
                }
                else
                {
                    host_configured = true;
                    log.WriteLineAsync("IP addressの書式が間違っています。Configureされません");
                }

                port.Open();

                bool wifi_configured = tbSsid.Text == "";
                bool count_configured = bones.Count == 0;
                int fix_configured_index = 0;
                int mov_configured_index = 0;

                if (wifi_configured) log.WriteLineAsync("SSIDが空白なため、Configureされません");
                if (count_configured) log.WriteLineAsync("ボーン構造が空白なため、Configureされません");

                byte[] buffer = new byte[128];

                int err_line_count = 0;
                port.WriteLine("dummy");
                while (true)
                {

                    string line = port.ReadLine();
                    if (!line.EndsWith(">\r"))
                    {
                        log.WriteLineAsync("> " + line);
                        err_line_count++;
                        if (err_line_count > 64)
                        {
                            log.WriteLineAsync("デバイスとの対話モードに失敗しました。");
                            break;
                        }
                        continue;
                    }

                    byte[] bs;
                    int b = 0;
                    UInt32 length;
                    if (!wifi_configured)
                    {
                        length = 4 + 4 + 32 + 32;
                        bs = BitConverter.GetBytes(length);
                        for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                        bs = BitConverter.GetBytes(cmd_wifi);
                        for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                        for (int i = 0; i < 64; i++) buffer[b + i] = 0x00;
                        for (int i = 0; i < 31 && i < tbSsid.Text.Length; i++) buffer[b++] = (byte)tbSsid.Text[i];
                        b = 4 + 4 + 32;
                        for (int i = 0; i < 31 && i < tbPassphrase.Text.Length; i++) buffer[b++] = (byte)tbPassphrase.Text[i];

                        port.BaseStream.Write(buffer, 0, (int)length);
                        wifi_configured = true;

                        log.WriteLineAsync("WiFi情報を設定しました");
                        Task.Delay(1000);
                        continue;
                    }

                    if (!host_configured)
                    {
                        length = 4 + 4 + 4 + 4;
                        bs = BitConverter.GetBytes(length);
                        for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                        bs = BitConverter.GetBytes(cmd_host);
                        for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                        for (int i = 0; i < 4; i++) buffer[b++] = ip[i];

                        byte[] _port = BitConverter.GetBytes((ushort)nmPort.Value);
                        for (int i = 0; i < 2; i++) buffer[b++] = _port[i];
                        buffer[b++] = 0;
                        buffer[b++] = 0;

                        port.BaseStream.Write(buffer, 0, (int)length);
                        host_configured = true;

                        log.WriteLineAsync("ホスト情報を設定しました");
                        Task.Delay(1000);
                        continue;
                    }

                    if (!count_configured)
                    {
                        length = 4 + 4 + 4;
                        bs = BitConverter.GetBytes(length);
                        for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                        bs = BitConverter.GetBytes(cmd_bonecount);
                        for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                        buffer[b++] = (byte)fix_count;
                        buffer[b++] = (byte)mov_count;
                        buffer[b++] = 0;
                        buffer[b++] = 0;

                        port.BaseStream.Write(buffer, 0, (int)length);
                        count_configured = true;

                        log.WriteLineAsync("ボーン構造の設定を開始しました");
                        Task.Delay(1000);
                        continue;
                    }

                    if ((fix_configured_index + mov_configured_index) < (fix_count + mov_count))
                    {
                        length = 4 + 4 + 20 + 4 + 12 + 16;
                        bs = BitConverter.GetBytes(length);
                        for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                        bool mode_fix = fix_configured_index < fix_count;
                        bs = BitConverter.GetBytes(mode_fix ? cmd_fixbone : cmd_movable);
                        for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                        int t = mode_fix ? fix_configured_index : mov_configured_index;
                        JointConfigure target = mode_fix ? fix[t] : mov[t];
                        for (int i = 0; i < 20; i++) buffer[b + i] = 0;
                        bs = Encoding.ASCII.GetBytes(target.JointRootSerial);
                        for (int i = 0; i < 19 && i < bs.Length; i++) buffer[b + i] = bs[i];
                        b += 20;

                        buffer[b++] = 0;
                        buffer[b++] = (byte)t;
                        buffer[b++] = target.WorkerAddress;
                        buffer[b++] = target.TrackerIndex;
                        float[] fs = { target.PositionX, target.PositionY, target.PositionZ, target.QuaternionX, target.QuaternionY, target.QuaternionZ, target.QuaternionW };
                        for (int i = 0; i < fs.Length; i++)
                        {
                            bs = BitConverter.GetBytes(fs[i]);
                            for (int j = 0; j < 4; j++) buffer[b++] = bs[j];
                        }

                        port.BaseStream.Write(buffer, 0, (int)length);
                        if (mode_fix) fix_configured_index++;
                        else mov_configured_index++;

                        log.WriteLineAsync((mode_fix ? "固定" : "可動") + "ボーン [" + t + "]を設定しました");
                        Task.Delay(1000);
                        continue;
                    }

                    log.WriteLineAsync("全ての情報を設定しました");

                    length = 4 + 4;
                    bs = BitConverter.GetBytes(length);
                    for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                    bs = BitConverter.GetBytes(cmd_reboot);
                    for (int i = 0; i < 4; i++) buffer[b++] = bs[i];

                    port.BaseStream.Write(buffer, 0, (int)length);

                    log.WriteLineAsync("デバイスを再起動させます");
                    break;
                }

                port.Close();


                ChangeEnableAsync(bParentConfigure, true);
                ChangeEnableAsync(bWorkerConfigure, true);
            });
        }

        delegate void EnabledChangeHandler(Control target, bool enabled);
        EnabledChangeHandler EnabledChanger;

        private void EnabledChange(Control target, bool enabled) { target.Enabled = enabled; }

        private void ChangeEnableAsync(Control target, bool enabled) { target.Invoke(EnabledChanger, target, enabled); }

        private bool completeModuleCheck = false;
        private bool completeFirmwareCheck = false;
        private void OnCompleteModuleCheck()
        {
            completeModuleCheck = true;
            if (completeFirmwareCheck)
            {
                ChangeEnableAsync(bParentFirmware, true);
                ChangeEnableAsync(bWorkerFirmware, true);
            }
        }

        private void OnCompleteFirmwareVersionCheck(bool completed, string dir)
        {
            log.WriteLineAsync((completed ? "オンライン上に" : "ローカルに") + "バージョン '" + Path.GetFileName(dir) + "'が見つかりました");

            cachedFirmwareDirectory = dir;
            
            completeFirmwareCheck = true;
            if (completeModuleCheck)
            {
                ChangeEnableAsync(bParentFirmware, true);
                ChangeEnableAsync(bWorkerFirmware, true);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            EnabledChanger = new EnabledChangeHandler(EnabledChange);

            github = new GitHubReleaseCheck("ixsiid", "JointTracker", "firmware");
            github.OnComplete += OnCompleteFirmwareVersionCheck;
            github.Start();

            python = new ProcessStartInfo("cmd", "/c python -m pip list esptool");
            python.RedirectStandardError = true;
            python.RedirectStandardOutput = true;
            python.UseShellExecute = false;
            python.CreateNoWindow = true;

            InstallESPTool();


            gvBone.Columns.AddRange(JointConfigure.GetColumns());

            bones = new List<JointConfigure>();

            string defaultCsvFile = "default_bones.csv";

            data = new BindingSource() { DataSource = bones };
            gvBone.DataSource = data;

            OpenBones(defaultCsvFile);

            RefreshListSerialPort();
        }

        private void RefreshListSerialPort()
        {
            UpdateComboBoxValueDelegate updateMethod = new UpdateComboBoxValueDelegate(UpdatePortList);

            Task.Run(async () =>
            {
                while (true)
                {
                    string[] ports = SerialPort.GetPortNames();

                    cbParentPort.Invoke(updateMethod, cbParentPort, ports);
                    cbWorkerPort.Invoke(updateMethod, cbWorkerPort, ports);

                    await Task.Delay(5000);
                }
            });
        }

        delegate void UpdateComboBoxValueDelegate(ComboBox sender, string[] items);

        private void UpdatePortList(ComboBox sender, string[] items)
        {
            string selectedItem = sender.SelectedItem?.ToString();
            sender.Items.Clear();
            sender.Items.AddRange(items);

            if (selectedItem == null)
            {
                if (items.Length > 0) sender.SelectedIndex = 0;
                return;
            }

            if (items.Contains(selectedItem))
            {
                int index = -1;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == selectedItem)
                    {
                        index = i;
                        break;
                    }
                }
                if (index >= 0) sender.SelectedIndex = index;
                else if (items.Length > 0) sender.SelectedIndex = 0;
            }

        }

        private void bRowAdd_Click(object sender, EventArgs e)
        {
            var bone = new JointConfigure(new string[] { "True", "VMT_", "1", "1", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "1.0"});
            data.Add(bone);
        }

        private void bLog_Click(object sender, EventArgs e)
        {
            log.Show();
            log.TopMost = true;
            log.TopMost = false;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Title = "トラッカーのジョイント構造を保存";
            d.DefaultExt = ".csv";
            d.FileName = "bones.csv";
            d.InitialDirectory = ".\\";

            if (d.ShowDialog() == DialogResult.OK)
            {
                using (TextWriter tw = new StreamWriter(d.FileName, false, Encoding.UTF8))
                {
                    foreach(var b in bones)
                    {
                        tw.WriteLine(b.ToString());
                    }
                }
            }
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "CSV(*.csv)|*.csv";
            d.Title = "トラッカーのジョイント構造を開く";

            if (d.ShowDialog() == DialogResult.OK) { OpenBones(d.FileName); }
        }

        private void bParentFirmware_Click(object sender, EventArgs e)
        {
            bParentFirmware.Enabled = false;

            string dir = string.Join(@"\", Directory.GetCurrentDirectory(), cachedFirmwareDirectory, "parent", "");
            python.Arguments = "/c python -m esptool " + string.Join(" ",
                "--chip", "esp32", "--port", cbParentPort.Text, "--baud", "460800",
                "--before", "default_reset", "--after", "hard_reset", "write_flash",
                "-z", "--flash_mode", "dio", "--flash_freq", "40m", "--flash_size", "detect",
                "0x1000", dir + "bootloader_dio_40m.bin",
                "0x8000", dir + "partitions.bin",
                "0xe000", dir + "boot_app0.bin",
                "0x10000", dir + "firmware.bin");

            Task.Run(() =>
            {
                var p = Process.Start(python);
                bool success = false;
                string text;
                while((text = p.StandardOutput.ReadLine()) != null)
                {
                    log.WriteLineAsync(text);
                    if (text == "Hash of data verified.") success = true;
                }

                log.WriteLineAsync("ParentデバイスのFirmware書き込み処理が終了しました");
                log.WriteLineAsync("結果: " + (success ? "成功" : "失敗"));
                ChangeEnableAsync(bParentFirmware, true);
            });
        }

        private void bWorkerFirmware_Click(object sender, EventArgs e)
        {
            bWorkerFirmware.Enabled = false;

            string dir = string.Join(@"\", Directory.GetCurrentDirectory(), cachedFirmwareDirectory, "worker", "");
            python.Arguments = "/c python -m esptool " + string.Join(" ",
                "--chip", "esp32", "--port", cbWorkerPort.Text, "--baud", "1500000",
                "--before", "default_reset", "--after", "hard_reset", "write_flash",
                "-z", "--flash_mode", "dio", "--flash_freq", "40m", "--flash_size", "detect",
                "0x1000", dir + "bootloader.bin",
                "0x8000", dir + "partitions.bin",
                "0x10000", dir + "firmware.bin");

            Task.Run(() =>
            {
                var p = Process.Start(python);
                bool success = false;
                string text;
                while ((text = p.StandardOutput.ReadLine()) != null)
                {
                    log.WriteLineAsync(text);
                    if (text == "Hash of data verified.") success = true;
                }

                log.WriteLineAsync("WorkerデバイスのFirmware書き込み処理が終了しました");
                log.WriteLineAsync("結果: " + (success ? "成功" : "失敗"));

                ChangeEnableAsync(bWorkerFirmware, true);
            });
        }

        private void bWorkerConfigure_Click(object sender, EventArgs e)
        {
            try
            {
                SerialPort port = new SerialPort(cbWorkerPort.SelectedItem.ToString(), 115200);
                port.Open();
                port.WriteLine(nmWorkerAddress.Value.ToString());
                port.Close();

                log.WriteLine("Workerデバイスにアドレスを設定しました。");
            }catch (Exception exc)
            {
                log.WriteLine("アドレス設定が失敗しました。\n > " + exc.Message);
            }
        }

        private void bRowDelete_Click(object sender, EventArgs e)
        {
            data.RemoveAt(gvBone.CurrentRow.Index);
        }
    }

}
