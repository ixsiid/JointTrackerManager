
namespace JointTrackerManager
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bParentConfigure = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.nmPort = new System.Windows.Forms.NumericUpDown();
            this.bOpen = new System.Windows.Forms.Button();
            this.bUp = new System.Windows.Forms.Button();
            this.bDown = new System.Windows.Forms.Button();
            this.cbParentPort = new System.Windows.Forms.ComboBox();
            this.bRowAdd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gvBone = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.bParentFirmware = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.tbPassphrase = new System.Windows.Forms.TextBox();
            this.tbSsid = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nmWorkerAddress = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.bWorkerFirmware = new System.Windows.Forms.Button();
            this.bWorkerConfigure = new System.Windows.Forms.Button();
            this.cbWorkerPort = new System.Windows.Forms.ComboBox();
            this.bLog = new System.Windows.Forms.Button();
            this.bRowDelete = new System.Windows.Forms.Button();
            this.jointConfigureBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBone)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmWorkerAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jointConfigureBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bParentConfigure
            // 
            this.bParentConfigure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bParentConfigure.Location = new System.Drawing.Point(826, 424);
            this.bParentConfigure.Name = "bParentConfigure";
            this.bParentConfigure.Size = new System.Drawing.Size(128, 44);
            this.bParentConfigure.TabIndex = 0;
            this.bParentConfigure.TabStop = false;
            this.bParentConfigure.Text = "Configure";
            this.bParentConfigure.UseVisualStyleBackColor = true;
            this.bParentConfigure.Click += new System.EventHandler(this.bParentConfigure_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.bSave);
            this.panel1.Controls.Add(this.nmPort);
            this.panel1.Controls.Add(this.bOpen);
            this.panel1.Controls.Add(this.bUp);
            this.panel1.Controls.Add(this.bDown);
            this.panel1.Controls.Add(this.cbParentPort);
            this.panel1.Controls.Add(this.bRowDelete);
            this.panel1.Controls.Add(this.bRowAdd);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.gvBone);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.bParentFirmware);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbIp);
            this.panel1.Controls.Add(this.tbPassphrase);
            this.panel1.Controls.Add(this.tbSsid);
            this.panel1.Controls.Add(this.bParentConfigure);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 485);
            this.panel1.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "COM port";
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bSave.Location = new System.Drawing.Point(176, 423);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(82, 34);
            this.bSave.TabIndex = 10;
            this.bSave.TabStop = false;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // nmPort
            // 
            this.nmPort.Location = new System.Drawing.Point(596, 93);
            this.nmPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nmPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmPort.Name = "nmPort";
            this.nmPort.Size = new System.Drawing.Size(93, 19);
            this.nmPort.TabIndex = 105;
            this.nmPort.Value = new decimal(new int[] {
            39570,
            0,
            0,
            0});
            // 
            // bOpen
            // 
            this.bOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOpen.Location = new System.Drawing.Point(72, 424);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(82, 34);
            this.bOpen.TabIndex = 10;
            this.bOpen.TabStop = false;
            this.bOpen.Text = "Open";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // bUp
            // 
            this.bUp.Enabled = false;
            this.bUp.Location = new System.Drawing.Point(18, 158);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(50, 35);
            this.bUp.TabIndex = 9;
            this.bUp.TabStop = false;
            this.bUp.Text = "Up";
            this.bUp.UseVisualStyleBackColor = true;
            // 
            // bDown
            // 
            this.bDown.Enabled = false;
            this.bDown.Location = new System.Drawing.Point(16, 199);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(50, 35);
            this.bDown.TabIndex = 9;
            this.bDown.TabStop = false;
            this.bDown.Text = "Down";
            this.bDown.UseVisualStyleBackColor = true;
            // 
            // cbParentPort
            // 
            this.cbParentPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbParentPort.FormattingEnabled = true;
            this.cbParentPort.Location = new System.Drawing.Point(72, 39);
            this.cbParentPort.Name = "cbParentPort";
            this.cbParentPort.Size = new System.Drawing.Size(93, 20);
            this.cbParentPort.TabIndex = 101;
            // 
            // bRowAdd
            // 
            this.bRowAdd.Location = new System.Drawing.Point(16, 240);
            this.bRowAdd.Name = "bRowAdd";
            this.bRowAdd.Size = new System.Drawing.Size(50, 35);
            this.bRowAdd.TabIndex = 9;
            this.bRowAdd.TabStop = false;
            this.bRowAdd.Text = "行追加";
            this.bRowAdd.UseVisualStyleBackColor = true;
            this.bRowAdd.Click += new System.EventHandler(this.bRowAdd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(379, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 12);
            this.label11.TabIndex = 8;
            this.label11.Text = "VMT host";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "WiFi settings";
            // 
            // gvBone
            // 
            this.gvBone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvBone.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvBone.Location = new System.Drawing.Point(72, 132);
            this.gvBone.Name = "gvBone";
            this.gvBone.RowTemplate.Height = 21;
            this.gvBone.Size = new System.Drawing.Size(882, 286);
            this.gvBone.TabIndex = 7;
            this.gvBone.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(587, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(7, 12);
            this.label12.TabIndex = 5;
            this.label12.Text = ":";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(594, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "port";
            // 
            // bParentFirmware
            // 
            this.bParentFirmware.Enabled = false;
            this.bParentFirmware.Location = new System.Drawing.Point(253, 17);
            this.bParentFirmware.Name = "bParentFirmware";
            this.bParentFirmware.Size = new System.Drawing.Size(128, 42);
            this.bParentFirmware.TabIndex = 6;
            this.bParentFirmware.TabStop = false;
            this.bParentFirmware.Text = "Write Firmware";
            this.bParentFirmware.UseVisualStyleBackColor = true;
            this.bParentFirmware.Click += new System.EventHandler(this.bParentFirmware_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(444, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "IP address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Passphrase";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "SSID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Parent Device";
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(446, 93);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(135, 19);
            this.tbIp.TabIndex = 104;
            // 
            // tbPassphrase
            // 
            this.tbPassphrase.Location = new System.Drawing.Point(231, 93);
            this.tbPassphrase.Name = "tbPassphrase";
            this.tbPassphrase.Size = new System.Drawing.Size(122, 19);
            this.tbPassphrase.TabIndex = 103;
            // 
            // tbSsid
            // 
            this.tbSsid.Location = new System.Drawing.Point(111, 93);
            this.tbSsid.Name = "tbSsid";
            this.tbSsid.Size = new System.Drawing.Size(99, 19);
            this.tbSsid.TabIndex = 102;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.nmWorkerAddress);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.bWorkerFirmware);
            this.panel2.Controls.Add(this.bWorkerConfigure);
            this.panel2.Controls.Add(this.cbWorkerPort);
            this.panel2.Location = new System.Drawing.Point(12, 503);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(869, 70);
            this.panel2.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "COM port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(392, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "Worker Address";
            // 
            // nmWorkerAddress
            // 
            this.nmWorkerAddress.Location = new System.Drawing.Point(394, 35);
            this.nmWorkerAddress.Maximum = new decimal(new int[] {
            19,
            0,
            0,
            0});
            this.nmWorkerAddress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmWorkerAddress.Name = "nmWorkerAddress";
            this.nmWorkerAddress.Size = new System.Drawing.Size(93, 19);
            this.nmWorkerAddress.TabIndex = 202;
            this.nmWorkerAddress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Worker Device";
            // 
            // bWorkerFirmware
            // 
            this.bWorkerFirmware.Enabled = false;
            this.bWorkerFirmware.Location = new System.Drawing.Point(195, 12);
            this.bWorkerFirmware.Name = "bWorkerFirmware";
            this.bWorkerFirmware.Size = new System.Drawing.Size(128, 42);
            this.bWorkerFirmware.TabIndex = 6;
            this.bWorkerFirmware.TabStop = false;
            this.bWorkerFirmware.Text = "Write Firmware";
            this.bWorkerFirmware.UseVisualStyleBackColor = true;
            this.bWorkerFirmware.Click += new System.EventHandler(this.bWorkerFirmware_Click);
            // 
            // bWorkerConfigure
            // 
            this.bWorkerConfigure.Location = new System.Drawing.Point(509, 15);
            this.bWorkerConfigure.Name = "bWorkerConfigure";
            this.bWorkerConfigure.Size = new System.Drawing.Size(128, 44);
            this.bWorkerConfigure.TabIndex = 0;
            this.bWorkerConfigure.TabStop = false;
            this.bWorkerConfigure.Text = "Configure";
            this.bWorkerConfigure.UseVisualStyleBackColor = true;
            this.bWorkerConfigure.Click += new System.EventHandler(this.bWorkerConfigure_Click);
            // 
            // cbWorkerPort
            // 
            this.cbWorkerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWorkerPort.FormattingEnabled = true;
            this.cbWorkerPort.Location = new System.Drawing.Point(72, 39);
            this.cbWorkerPort.Name = "cbWorkerPort";
            this.cbWorkerPort.Size = new System.Drawing.Size(93, 20);
            this.cbWorkerPort.TabIndex = 201;
            // 
            // bLog
            // 
            this.bLog.Location = new System.Drawing.Point(900, 536);
            this.bLog.Name = "bLog";
            this.bLog.Size = new System.Drawing.Size(76, 37);
            this.bLog.TabIndex = 3;
            this.bLog.TabStop = false;
            this.bLog.Text = "Show logs";
            this.bLog.UseVisualStyleBackColor = true;
            this.bLog.Click += new System.EventHandler(this.bLog_Click);
            // 
            // bRowDelete
            // 
            this.bRowDelete.Location = new System.Drawing.Point(14, 281);
            this.bRowDelete.Name = "bRowDelete";
            this.bRowDelete.Size = new System.Drawing.Size(50, 35);
            this.bRowDelete.TabIndex = 9;
            this.bRowDelete.TabStop = false;
            this.bRowDelete.Text = "行削除";
            this.bRowDelete.UseVisualStyleBackColor = true;
            this.bRowDelete.Click += new System.EventHandler(this.bRowDelete_Click);
            // 
            // jointConfigureBindingSource
            // 
            this.jointConfigureBindingSource.DataSource = typeof(JointTrackerManager.JointConfigure);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 591);
            this.Controls.Add(this.bLog);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(700, 480);
            this.Name = "MainForm";
            this.Text = "Joint Tracker Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBone)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmWorkerAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jointConfigureBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bParentConfigure;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbParentPort;
        private System.Windows.Forms.TextBox tbPassphrase;
        private System.Windows.Forms.TextBox tbSsid;
        private System.Windows.Forms.Button bParentFirmware;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gvBone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bWorkerFirmware;
        private System.Windows.Forms.Button bWorkerConfigure;
        private System.Windows.Forms.NumericUpDown nmWorkerAddress;
        private System.Windows.Forms.BindingSource jointConfigureBindingSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bRowAdd;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bDown;
        private System.Windows.Forms.Button bLog;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbWorkerPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nmPort;
        private System.Windows.Forms.Button bRowDelete;
    }
}

