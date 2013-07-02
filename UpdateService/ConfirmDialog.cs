using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Camurphy.UpdateService
{
	public partial class ConfirmDialog : Form
	{
		private string m_ApplicationName;

		public ConfirmDialog(string applicationName)
		{
			InitializeComponent();

			m_ApplicationName = applicationName;
		}

		private void ConfirmDialog_Load(object sender, EventArgs e)
		{
			lblMessage.Text = Properties.Resources.ConfirmMessage.Replace("<application>", m_ApplicationName);
		}
	}
}
