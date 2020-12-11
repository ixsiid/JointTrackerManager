using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JointTrackerManager
{
    public partial class Log : Form
    {
        private List<string> que;
        private bool loaded;

        public Log()
        {
            InitializeComponent();

            que = new List<string>();
            loaded = false;
        }

        private void Log_Load(object sender, EventArgs e)
        {
            WriteLine("Launch");
        }

        public void WriteLine(string text)
        {
            tbLog.Text += text + "\r\n";

            tbLog.SelectionStart = tbLog.Text.Length;
            tbLog.ScrollToCaret();
        }

        delegate void WriteLineDelegate (string text);

        public void WriteLineAsync(string text)
        {
            if (loaded)
                tbLog.Invoke(new WriteLineDelegate(WriteLine), text);
            else
                que.Add(text);
        }

        private void Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Log_Shown(object sender, EventArgs e)
        {
            loaded = true;
            foreach (string q in que) WriteLine(q);
            que.Clear();
        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbLog.Text);
        }
    }
}
