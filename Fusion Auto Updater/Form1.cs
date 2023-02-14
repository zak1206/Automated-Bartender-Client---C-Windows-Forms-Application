using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fusion_Auto_Updater
{
    public partial class Form1 : Form
    {
        private string SoftwareVersionLink { get; set; } = "http://fusionbartender.com/Updates/UpdateInfo.txt";
        private string OnlineSoftwareVersion { get; set; } = "";
        private string CurrentSoftwareVersionPath { get; set; } = "";
        private string CurrentSoftwareVersion { get; set; } = "";
        private string SoftwareUpdatePackageLink { get; set; } = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CurrentSoftwareVersionPath = Path.Combine(Directory.GetCurrentDirectory(), "SoftwareVersion.txt");
            string[] lines = File.ReadAllLines(CurrentSoftwareVersionPath);
            CurrentSoftwareVersion = lines[0];
            labelXTitle.Text = "Backing Up Curent Version...";
            pgbXMain.Value = 10;

            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());

            BackupFilesToArchive(files);
            pgbXMain.Value = 25;

            foreach (string directory in dirs)
                if (!directory.Contains("Archive"))
                    BackupDirectoryToArchive(directory);

            pgbXMain.Value = 35;

            labelXTitle.Text = "Checking Online Software Version...";
            DownloadFile(SoftwareVersionLink, "UpdateInfo.txt");
            pgbXMain.Value = 50;

            string[] updateInfo = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "UpdateInfo.txt"));
            OnlineSoftwareVersion = updateInfo[0];

            if (OnlineSoftwareVersion != CurrentSoftwareVersion)
            {
                labelXTitle.Text = "Update Required. Downloading Update...";
                pgbXMain.Value = 75;
                DownloadFile($"http://www.fusionbartender.com/Updates/Update_{OnlineSoftwareVersion}.zip", $"Update_{OnlineSoftwareVersion}.zip");
                ZipFile.ExtractToDirectory(Path.Combine(Directory.GetCurrentDirectory(), $"Update_{OnlineSoftwareVersion}.zip"), Directory.GetCurrentDirectory());
            }

            if (OnlineSoftwareVersion != CurrentSoftwareVersion)
                labelXTitle.Text = "Update Complete!";
            pgbXMain.Value = 100;

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), $"Fusion Bartender {OnlineSoftwareVersion}.exe")))
                Process.Start(Path.Combine(Directory.GetCurrentDirectory(), $"Fusion Bartender {OnlineSoftwareVersion}.exe"));

            this.Close();
        }

        private void DownloadFile(string url, string fileName)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, Path.Combine(Directory.GetCurrentDirectory(), fileName));
            }
        }
        private void BackupFilesToArchive(string[] files)
        {
            foreach (string file in files)
            {
                string[] splitted = file.Split('\\');
                string fileName = splitted[splitted.Length - 1];

                File.Copy(file, Path.Combine(Directory.GetCurrentDirectory(), "Archive", fileName));
                File.Delete(file);
            }
        }
        private void BackupDirectoryToArchive(string path)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                string[] splitted = file.Split('\\');
                string fileName = splitted[splitted.Length - 1];

                File.Copy(file, Path.Combine(Directory.GetCurrentDirectory(), "Archive", fileName));
                File.Delete(file);
            }
        }
    }
}
