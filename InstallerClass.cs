using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;

namespace OffLine.Installer
{
	[RunInstaller(true)]
	public class InstallerClass : System.Configuration.Install.Installer
	{
		public InstallerClass()
			: base()
		{
			this.Committed += new System.Configuration.Install.InstallEventHandler(InstallerClass_Committed);
		}

		private void InstallerClass_Committed(object sender, System.Configuration.Install.InstallEventArgs e)
		{
			try
			{
				Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
				Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Camera Download.exe");
			}
			catch
			{
				// Do nothing
			}
		}
	}
}