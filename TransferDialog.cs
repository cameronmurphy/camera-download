using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Camurphy.CameraDownload
{
    public partial class TransferDialog : Form
    {
        private const int cNamingOptionsOffset = 51;

        public TransferDialog()
        {
            InitializeComponent();

            
        }

        public int Progress
        {
            set
            {
                pgbProgress.Value = value;
				this.Text = String.Format("{0}%", value.ToString());
            }
        }

        public string StatusBarText
        {
            set
            {
                lblStatus.Text = value;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Worker.Instance.Cancel();
        }

        private void TransferDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}