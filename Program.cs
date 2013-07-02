using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.Deployment.Application;

namespace Camurphy.CameraDownload
{
    static class Program
    {
        public static MainForm MainForm { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainForm = new MainForm();
			Application.Run(MainForm);
		}

        public static bool IsValidPath(string path)
        {
            return System.IO.Directory.Exists(path);
        }

        public static bool IsValidFilename(string text)
        {
            if (text.IndexOfAny("\\/:*?\"<>|".ToCharArray()) == -1)
                return true;

            return false;
        }
    }
}