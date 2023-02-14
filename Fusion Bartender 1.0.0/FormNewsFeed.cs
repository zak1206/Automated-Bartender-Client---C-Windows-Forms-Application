using DevComponents.DotNetBar;
using Fusion_Bartender_1._0.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fusion_Bartender_1._0._0
{
    public partial class FormNewsFeed : Form, IUpdate
    {
        private FormMainScreen _frmMain = null;
        private bool Loading = false;
        bool SysOnly = false;
        bool FollowersOnly = false;
        bool ProfileOnly = false;
        public FormNewsFeed(FormMainScreen frmMain)
        {
            _frmMain = frmMain;
            InitializeComponent();
        }

        public void UpdateStatus()
        {
            if (Visible)
            {
                SysOnly = checkBoxSysUpdatesOnly.Checked;
                FollowersOnly = checkBoxFollowerOnlys.Checked;
                ProfileOnly = checkBoxMyProfileOnly.Checked;
                buttonXInstallUpdate.Visible = UpdateAvailable;
            }
        }

        public Dictionary<String, String> SystemUpdates { get; set; } = new Dictionary<string, string>();
        private void RefreshNewsFeedPanel()
        {
            TreeNode SysNode = new TreeNode();
            TreeNode FollowersNode = new TreeNode();
            TreeNode ProfileNode = new TreeNode();
            SysNode.Text = "System Updates News Feed:";
            FollowersNode.Text = "Followers Feed:";
            ProfileNode.Text = "My Profile Feed:";

            treeViewNewsFeed.Nodes.Clear();
            treeViewNewsFeed.Nodes.Add(SysNode);
            treeViewNewsFeed.Nodes.Add(ProfileNode);
            treeViewNewsFeed.Nodes.Add(FollowersNode);
            treeViewNewsFeed.Refresh();

            CheckSystemUpdates();

            //Update View
            if (SysOnly)
            {

            } else if (FollowersOnly)
            {

            } else if (ProfileOnly)
            {

            } else
            {

            }

            treeViewNewsFeed.Refresh();
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            Loading = true;
            checkBoxSysUpdatesOnly.Checked = false;
            checkBoxMyProfileOnly.Checked = false;
            checkBoxFollowerOnlys.Checked = false;
            Loading = false;
        }

        private void checkBoxFollowerOnlys_CheckedChanged(object sender, EventArgs e)
        {
            if (!Loading)
            {
                if (checkBoxFollowerOnlys.Checked)
                {
                    checkBoxMyProfileOnly.Checked = false;
                    checkBoxSysUpdatesOnly.Checked = false;
                }
                RefreshNewsFeedPanel();
            }
        }

        private void labelX2_Click(object sender, EventArgs e)
        {
        }

        private void checkBoxSysUpdatesOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!Loading)
            {
                if (checkBoxSysUpdatesOnly.Checked)
                {
                    checkBoxMyProfileOnly.Checked = false;
                    checkBoxFollowerOnlys.Checked = false;
                }
                RefreshNewsFeedPanel();
            }
        }

        private void CheckSystemUpdates()
        {
            DownloadFile(SoftwareVersionLink.Replace("UpdateInfo", "UpdateNews"), "UpdateNews.txt");
            String[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "UpdateNews.txt"));
            int count = 0;

            foreach (string line in lines)
            {
                if (line.Contains($"#.#.#.#:")) {
                    count++;
                    SystemUpdates.Add(line, "Version");
                } else
                {
                    SystemUpdates.Add(line, "Update");
                }
            }
        }

        private void checkBoxMyProfileOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!Loading)
            {
                if (checkBoxSysUpdatesOnly.Checked)
                {
                    checkBoxMyProfileOnly.Checked = false;
                    checkBoxFollowerOnlys.Checked = false;
                }
                RefreshNewsFeedPanel();
            }
        }

        public bool UpdateAvailable { get; set; } = false;
        private string SoftwareVersionLink { get; set; } = "http://fusionbartender.com/Updates/UpdateInfo.txt";
        private void buttonXCheckForUpdates_Click(object sender, EventArgs e)
        {
            _frmMain.Logs.NewEntry(DateTime.Now, "Checking For Updates...", "INFO");
            labelXCheckingForUpdates.Text = "Checking For Available Updates...";
            labelXCheckingForUpdates.Visible = true;
            DownloadFile(SoftwareVersionLink, "UpdateInfo.txt");
            UpdateAvailable = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "UpdateInfo.txt"))[0] != Properties.Settings.Default.SoftwareVersion;
            labelXCheckingForUpdates.Visible = false;

            if (UpdateAvailable)
            {
                _frmMain.Logs.NewEntry(DateTime.Now, "Updates Available!", "ALERT");
                labelXCheckingForUpdates.Text = "Update Available!";
                labelXCheckingForUpdates.Visible = true;
            }
        }

        private void DownloadFile(string url, string fileName)
        {
            _frmMain.Logs.NewEntry(DateTime.Now, $"Downloading New File: {fileName}...", "INFO");
            using (var client = new WebClient())
            {
                client.DownloadFile(url, Path.Combine(Directory.GetCurrentDirectory(), fileName));
            }
        }

        private void buttonXInstallUpdate_Click(object sender, EventArgs e)
        {
            _frmMain.Logs.NewEntry(DateTime.Now, "Installing New Update... Closing Software...", "INFO");
            Process.Start(Path.Combine(Directory.GetCurrentDirectory(), "Fusion Bartender Updater.exe"));
            _frmMain.Close();
        }
    }
}
