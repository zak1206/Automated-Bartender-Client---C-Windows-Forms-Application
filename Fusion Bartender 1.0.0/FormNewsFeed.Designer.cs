
namespace Fusion_Bartender_1._0._0
{
    partial class FormNewsFeed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Followers Feed:");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("System Update News Feed:");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("My Profile Feed:");
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonXRefresh = new DevComponents.DotNetBar.ButtonX();
            this.treeViewNewsFeed = new System.Windows.Forms.TreeView();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.checkBoxFollowerOnlys = new System.Windows.Forms.CheckBox();
            this.checkBoxSysUpdatesOnly = new System.Windows.Forms.CheckBox();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.checkBoxMyProfileOnly = new System.Windows.Forms.CheckBox();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.ribbonClientPanelMain = new DevComponents.DotNetBar.Ribbon.RibbonClientPanel();
            this.buttonXCheckForUpdates = new DevComponents.DotNetBar.ButtonX();
            this.labelXCheckingForUpdates = new DevComponents.DotNetBar.LabelX();
            this.buttonXInstallUpdate = new DevComponents.DotNetBar.ButtonX();
            this.ribbonClientPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX1.Location = new System.Drawing.Point(10, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(177, 41);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "News Feed:";
            // 
            // buttonXRefresh
            // 
            this.buttonXRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXRefresh.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonXRefresh.Location = new System.Drawing.Point(10, 556);
            this.buttonXRefresh.Name = "buttonXRefresh";
            this.buttonXRefresh.Size = new System.Drawing.Size(180, 50);
            this.buttonXRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.buttonXRefresh.TabIndex = 2;
            this.buttonXRefresh.Text = "Refresh";
            // 
            // treeViewNewsFeed
            // 
            this.treeViewNewsFeed.Font = new System.Drawing.Font("Mongolian Baiti", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewNewsFeed.FullRowSelect = true;
            this.treeViewNewsFeed.HideSelection = false;
            this.treeViewNewsFeed.Indent = 35;
            this.treeViewNewsFeed.ItemHeight = 40;
            this.treeViewNewsFeed.Location = new System.Drawing.Point(9, 54);
            this.treeViewNewsFeed.Name = "treeViewNewsFeed";
            treeNode7.Name = "NodeFollowers";
            treeNode7.Text = "Followers Feed:";
            treeNode7.ToolTipText = "News Feed From Your Followers";
            treeNode8.Name = "NodeSystem";
            treeNode8.Text = "System Update News Feed:";
            treeNode8.ToolTipText = "News Feed With The Latest Software / Hardware Updates!";
            treeNode9.Name = "NodeProfile";
            treeNode9.Text = "My Profile Feed:";
            treeNode9.ToolTipText = "Checkout Your Latest Accomplishments!";
            this.treeViewNewsFeed.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            this.treeViewNewsFeed.Size = new System.Drawing.Size(1030, 491);
            this.treeViewNewsFeed.TabIndex = 3;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX2.Location = new System.Drawing.Point(318, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(175, 29);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "Followers Only";
            this.labelX2.Click += new System.EventHandler(this.labelX2_Click);
            // 
            // checkBoxFollowerOnlys
            // 
            this.checkBoxFollowerOnlys.AutoSize = true;
            this.checkBoxFollowerOnlys.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxFollowerOnlys.Location = new System.Drawing.Point(297, 20);
            this.checkBoxFollowerOnlys.Name = "checkBoxFollowerOnlys";
            this.checkBoxFollowerOnlys.Size = new System.Drawing.Size(15, 14);
            this.checkBoxFollowerOnlys.TabIndex = 5;
            this.checkBoxFollowerOnlys.UseVisualStyleBackColor = false;
            this.checkBoxFollowerOnlys.CheckedChanged += new System.EventHandler(this.checkBoxFollowerOnlys_CheckedChanged);
            // 
            // checkBoxSysUpdatesOnly
            // 
            this.checkBoxSysUpdatesOnly.AutoSize = true;
            this.checkBoxSysUpdatesOnly.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxSysUpdatesOnly.Location = new System.Drawing.Point(531, 20);
            this.checkBoxSysUpdatesOnly.Name = "checkBoxSysUpdatesOnly";
            this.checkBoxSysUpdatesOnly.Size = new System.Drawing.Size(15, 14);
            this.checkBoxSysUpdatesOnly.TabIndex = 7;
            this.checkBoxSysUpdatesOnly.UseVisualStyleBackColor = false;
            this.checkBoxSysUpdatesOnly.CheckedChanged += new System.EventHandler(this.checkBoxSysUpdatesOnly_CheckedChanged);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX3.Location = new System.Drawing.Point(552, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(245, 29);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "System Updates Only";
            // 
            // checkBoxMyProfileOnly
            // 
            this.checkBoxMyProfileOnly.AutoSize = true;
            this.checkBoxMyProfileOnly.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxMyProfileOnly.Location = new System.Drawing.Point(834, 20);
            this.checkBoxMyProfileOnly.Name = "checkBoxMyProfileOnly";
            this.checkBoxMyProfileOnly.Size = new System.Drawing.Size(15, 14);
            this.checkBoxMyProfileOnly.TabIndex = 9;
            this.checkBoxMyProfileOnly.UseVisualStyleBackColor = false;
            this.checkBoxMyProfileOnly.CheckedChanged += new System.EventHandler(this.checkBoxMyProfileOnly_CheckedChanged);
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelX4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX4.Location = new System.Drawing.Point(855, 12);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(175, 29);
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "My Profile Only";
            // 
            // ribbonClientPanelMain
            // 
            this.ribbonClientPanelMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.ribbonClientPanelMain.Controls.Add(this.buttonXInstallUpdate);
            this.ribbonClientPanelMain.Controls.Add(this.treeViewNewsFeed);
            this.ribbonClientPanelMain.Controls.Add(this.labelXCheckingForUpdates);
            this.ribbonClientPanelMain.Controls.Add(this.buttonXCheckForUpdates);
            this.ribbonClientPanelMain.Controls.Add(this.checkBoxFollowerOnlys);
            this.ribbonClientPanelMain.Controls.Add(this.buttonXRefresh);
            this.ribbonClientPanelMain.Controls.Add(this.checkBoxSysUpdatesOnly);
            this.ribbonClientPanelMain.Controls.Add(this.checkBoxMyProfileOnly);
            this.ribbonClientPanelMain.Controls.Add(this.labelX1);
            this.ribbonClientPanelMain.Controls.Add(this.labelX4);
            this.ribbonClientPanelMain.Controls.Add(this.labelX2);
            this.ribbonClientPanelMain.Controls.Add(this.labelX3);
            this.ribbonClientPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonClientPanelMain.Location = new System.Drawing.Point(0, 0);
            this.ribbonClientPanelMain.Name = "ribbonClientPanelMain";
            this.ribbonClientPanelMain.Size = new System.Drawing.Size(1051, 617);
            // 
            // 
            // 
            this.ribbonClientPanelMain.Style.Class = "RibbonClientPanel";
            this.ribbonClientPanelMain.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonClientPanelMain.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonClientPanelMain.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonClientPanelMain.TabIndex = 10;
            // 
            // buttonXCheckForUpdates
            // 
            this.buttonXCheckForUpdates.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCheckForUpdates.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCheckForUpdates.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonXCheckForUpdates.Location = new System.Drawing.Point(807, 556);
            this.buttonXCheckForUpdates.Name = "buttonXCheckForUpdates";
            this.buttonXCheckForUpdates.Size = new System.Drawing.Size(232, 50);
            this.buttonXCheckForUpdates.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.buttonXCheckForUpdates.TabIndex = 10;
            this.buttonXCheckForUpdates.Text = "Check For Updates";
            this.buttonXCheckForUpdates.Click += new System.EventHandler(this.buttonXCheckForUpdates_Click);
            // 
            // labelXCheckingForUpdates
            // 
            this.labelXCheckingForUpdates.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXCheckingForUpdates.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXCheckingForUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelXCheckingForUpdates.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXCheckingForUpdates.Location = new System.Drawing.Point(237, 563);
            this.labelXCheckingForUpdates.Name = "labelXCheckingForUpdates";
            this.labelXCheckingForUpdates.Size = new System.Drawing.Size(412, 34);
            this.labelXCheckingForUpdates.TabIndex = 11;
            this.labelXCheckingForUpdates.Text = "Checking Online Software  Version...";
            this.labelXCheckingForUpdates.TextAlignment = System.Drawing.StringAlignment.Far;
            this.labelXCheckingForUpdates.Visible = false;
            // 
            // buttonXInstallUpdate
            // 
            this.buttonXInstallUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXInstallUpdate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXInstallUpdate.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonXInstallUpdate.Location = new System.Drawing.Point(686, 556);
            this.buttonXInstallUpdate.Name = "buttonXInstallUpdate";
            this.buttonXInstallUpdate.Size = new System.Drawing.Size(116, 50);
            this.buttonXInstallUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.buttonXInstallUpdate.TabIndex = 12;
            this.buttonXInstallUpdate.Text = "Install";
            this.buttonXInstallUpdate.Visible = false;
            this.buttonXInstallUpdate.Click += new System.EventHandler(this.buttonXInstallUpdate_Click);
            // 
            // FormNewsFeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 617);
            this.Controls.Add(this.ribbonClientPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormNewsFeed";
            this.Text = "FormHome";
            this.Load += new System.EventHandler(this.FormHome_Load);
            this.ribbonClientPanelMain.ResumeLayout(false);
            this.ribbonClientPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonXRefresh;
        private System.Windows.Forms.TreeView treeViewNewsFeed;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.CheckBox checkBoxFollowerOnlys;
        private System.Windows.Forms.CheckBox checkBoxSysUpdatesOnly;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.CheckBox checkBoxMyProfileOnly;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Ribbon.RibbonClientPanel ribbonClientPanelMain;
        private DevComponents.DotNetBar.ButtonX buttonXInstallUpdate;
        private DevComponents.DotNetBar.LabelX labelXCheckingForUpdates;
        private DevComponents.DotNetBar.ButtonX buttonXCheckForUpdates;
    }
}