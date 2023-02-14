using DevComponents.DotNetBar;
using Fusion_Bartender_1._0;
using Fusion_Bartender_1._0._0;
using Fusion_Bartender_1._0._0.Properties;
using Fusion_Bartender_1._0.Utils;
using Fusion_Bartender_1.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fusion_Bartender_1._0._0;

namespace Fusion_Bartender_1
{
    public partial class FormMainScreen : Form
    {
        //Forms
        public List<Form> _allForms = new List<Form>();
        internal FormHome frmHome = null;
        internal FormSettings frmSettings = null;
        internal FormLogViewer frmLogViewer = null;
        internal FormCreateRecipe frmCreateRecipe = null;
        internal FormNewsFeed frmNewsFeed = null;
        internal FormGlobalRecipes frmGlobalRecipes = null;
        internal FormManualPouring frmManualPouring = null;
        internal MySQL DB = null;

        //Classes
        public List<Recipe> Recipes = new List<Recipe>();
        public User LoggedInUser = null;
        public List<User> Users = new List<User>();
        public Settings Settings = null;
        public Drinks Drinks = new Drinks();
        public Arduino Arduino = null;
        public Logs Logs = new Logs();
        public Alerts Alerts = new Alerts();
        public Timer _timerReconnect = null;

        //Variables
        public Boolean UserIsLoggedIn { get; set; } = false;
        public String PathToAutoLoginFile = Path.Combine(Directory.GetCurrentDirectory(), "AutoLogin.txt");
        public String PathToRecipes = Path.Combine(Directory.GetCurrentDirectory(), "Recipes");
        public static String DB_Server = "mysql.fusionbartender.com";
        public static String DB_Database = "fusionbartender_com";
        public static String DB_UID = "fusionbartenderc";
        public static String DB_Password = "W26XCbRy";
        public Boolean AutoMode { get; set; } = false;
        public Boolean ManualPourMode { get; set; } = false;

        public FormMainScreen()
        {
            InitializeComponent();
            GetAllUserFiles();
            SetupForms();
            SetupTimers();
            SetupFolders();
            Arduino = new Arduino(this, Settings.ArduinoCOMPort, Settings, Logs);//Connect Arduino
        }

        private void SetupFolders()
        {
            Logs.NewEntry(DateTime.Now, "Checking If All Directories Exist.", "STARTUP");

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Settings")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Settings"));
                Logs.NewEntry(DateTime.Now, "Settings Folder Added.", "STARTUP");
            }

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Recipes")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Recipes"));
                Logs.NewEntry(DateTime.Now, "Recipes Folder Added.", "STARTUP");
            }

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Logs")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Logs"));
                Logs.NewEntry(DateTime.Now, "Logs Folder Added.", "STARTUP");
            }

            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Logs", DateTime.Now.Day.ToString("#00") + DateTime.Now.Month.ToString("#00") + DateTime.Now.Year.ToString("#0000") + "_Log.xml")))
            {
                Logs = new Logs();
                Logs.CreatedDate = DateTime.Now;
                Logs.Save(this);
                Logs.NewEntry(DateTime.Now, "New Log File Added.", "STARTUP");
            } else
            {
                //Logs = Logs.Load(this, Path.Combine(Directory.GetCurrentDirectory(), "Logs", DateTime.Now.Day.ToString("#00") + DateTime.Now.Month.ToString("#00") + DateTime.Now.Year.ToString("#0000") + "_Log.xml"));

            }
        }

        private void SetupTimers()
        {
            //UI Updater
            Timer _timerUpdate = new Timer();
            _timerUpdate.Interval = 50;
            _timerUpdate.Enabled = true;
            _timerUpdate.Tick += _timerUpdate_Tick;
            Logs.NewEntry(DateTime.Now, "Update Timer Added.", "STARTUP");

            //AlarmChecker
            Timer _timerAlarms = new Timer();
            _timerAlarms.Interval = 5000;
            _timerAlarms.Enabled = true;
            _timerAlarms.Tick += _timerAlerts_Tick;
            Logs.NewEntry(DateTime.Now, "Alert Timer Added.", "STARTUP");

            //Reconnect
            _timerReconnect = new Timer();
            _timerReconnect.Interval = 5000;
            _timerReconnect.Enabled = false;
            _timerReconnect.Tick += _timerReconnect_Tick;
            Logs.NewEntry(DateTime.Now, "Reconnect Timer Added.", "STARTUP");

            Logs.NewEntry(DateTime.Now, "Timers Setup Successfully.", "STARTUP");
        }

        int retries = 0;
        private void _timerReconnect_Tick(object sender, EventArgs e)
        {
            if (Arduino.IsConnected && retries > 0) {
                retries = 0;
                _timerReconnect.Enabled = false;
                Logs.NewEntry(DateTime.Now, "Arduino Connection Retry Succsessfull!", "ARDUINO");
            }

            if (!Arduino.IsConnected)
            {
                retries++;
                Arduino.Connect();
                Logs.NewEntry(DateTime.Now, "Arduino Connection Retry: " + retries, "ARDUINO");
            }
        }

        int lastCount = 0;
        private void _timerAlerts_Tick(object sender, EventArgs e)
        {
            listBoxAdv1.Items.Clear();
            Alerts.AlertsList.Clear();
            listBoxAdv1.Refresh();

            ButtonItem newItem = new ButtonItem();
            List<Alert> newList = new List<Alert>();
            Alert newAlert = null;
            double time = DateTime.Now.Subtract(Arduino.timeSinceLastConnectionUpdate).TotalSeconds;
             
            //Network Check
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                newAlert = newAlert = new Alert(1, "Network Is Down", "Network Status Is Down, Please Check Your Internet Connection.", true, 15000);
                newItem.Text = newAlert.AlertTitle;
                newItem.Tooltip = newAlert.AlertMessage;
                newItem.Tag = newAlert;
                newItem.Visible = true;
                newItem.Checked = true;
                newAlert.MarkedAsRead = false;
                //Add icon

                newList.Add(newAlert);
            }

            //Door Open Check
            if (Arduino.IsConnected && Arduino.FridgeDoorOpen)
            {
                newAlert = newAlert = new Alert(1, "Door Open", "Fridge Door Open! Close To Continue...", true, 5000);
                newItem.Text = newAlert.AlertTitle;
                newItem.Tooltip = newAlert.AlertMessage;
                newItem.Tag = newAlert;
                newItem.Visible = true;
                newItem.Checked = true;
                newAlert.MarkedAsRead = false;
                //Add icon

                newList.Add(newAlert);
            }

            //Connection Check
            if (!Arduino.IsConnected || time > 15.0)
            {
                newAlert = newAlert = new Alert(3, "Arduino Is Down!", "Arduino Status Is Down, Please Check Your Arduino Connection.", true, 5000);
                newItem.Text = newAlert.AlertTitle;
                newItem.Tooltip = newAlert.AlertMessage;
                newItem.Tag = newAlert;
                newItem.Visible = true;
                newItem.Checked = true;
                newAlert.MarkedAsRead = false;
                //Arduino.IsConnected = false;
                //Arduino.Disconnect();
                //Add icon

                newList.Add(newAlert);
                _timerReconnect.Enabled = true;
            } else if (Arduino.IsConnected)
            {
                retries = 0;
                _timerReconnect.Enabled = false;
            }

            //DB Connection Check

            //Humidity Check
            if (Arduino.Humidity > Settings.MaxHumidityAllowed)
            {
                newAlert = newAlert = new Alert(3, "Humidity Level HIGH!!!", "Please Restart The System!", true, 1000);
                newItem.Text = newAlert.AlertTitle;
                newItem.Tooltip = newAlert.AlertMessage;
                newItem.Tag = newAlert;
                newItem.Visible = true;
                newItem.Checked = true;
                newAlert.MarkedAsRead = false;
                //Add icon

                newList.Add(newAlert);
            }

            //Internal Temperature Check
            if (Arduino.Temperature > Settings.MaxInternalTemperatureAllowed)
            {
                newAlert = newAlert = new Alert(3, "Internal Temperature TOO HIGH!!!", "Please Power Down The System!", true, 1000);
                newItem.Text = newAlert.AlertTitle;
                newItem.Tooltip = newAlert.AlertMessage;
                newItem.Tag = newAlert;
                newItem.Visible = true;
                newItem.Checked = true;
                newAlert.MarkedAsRead = false;
                //Add icon

                if (!newAlert.MarkedAsRead) newList.Add(newAlert);
            }

            //New Shared Recipe From Follower
            //Check Software Update Alerts

            if (Alerts.AlertsList.Count != newList.Count)
            {
                Alerts.AlertsList = newList;//Set New Alarm List
                foreach (Alert alert in Alerts.AlertsList)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Visible = true;
                    item.Image = Resources.delete_26px;
                    item.CheckState = !alert.MarkedAsRead ? CheckState.Checked : CheckState.Unchecked;
                    item.Enabled = true;
                    item.Text = alert.AlertTitle;
                    item.Tooltip = alert.AlertMessage;
                    item.Tag = alert;

                    if (!alert.MarkedAsRead)
                    {
                        unmarkedAsRead = true;
                        listBoxAdv1.Items.Add(item);
                    }
                }
            }

            if (Alerts.AlertsList.Count == 0 && Alerts.AlertsList.Count != lastCount)
            {
                listBoxAdv1.Items.Clear();
                expandablePanel1.ButtonImageCollapse = Resources.icons8_notification_48px_3;
                expandablePanel1.ButtonImageExpand = Resources.icons8_notification_48px_3;
            } else
            {
                if (unmarkedAsRead)
                {
                    expandablePanel1.ButtonImageCollapse = Resources.icons8_notification_48px_21;
                    expandablePanel1.ButtonImageExpand = Resources.icons8_notification_48px_21;
                } else
                {
                    expandablePanel1.ButtonImageCollapse = Resources.icons8_notification_48px_3;
                    expandablePanel1.ButtonImageExpand = Resources.icons8_notification_48px_3;
                }
            }

            if (listBoxAdv1.Items.Count > 0)
            {
                foreach (ListBoxItem item in listBoxAdv1.Items)
                {
                    Alert alert = (Alert)item.Tag;
                    if (((item.CheckState == CheckState.Unchecked) && !alert.MarkedAsRead) || ((item.CheckState == CheckState.Checked) && alert.MarkedAsRead))
                    {
                        item.CheckState = alert.MarkedAsRead ? CheckState.Unchecked : CheckState.Checked;
                        listBoxAdv1.RefreshItems();
                    }
                }
            }

            bool allAlertsMarkAsRead = true;
            if (listBoxAdv1.Items.Count > 0)
            {
                foreach (ListBoxItem item in listBoxAdv1.Items)
                {
                    Alert alert = (Alert)item.Tag;
                    if (!alert.MarkedAsRead) allAlertsMarkAsRead = false;
                }

                if (allAlertsMarkAsRead)
                {
                    lastCount = 0;
                    listBoxAdv1.Items.Clear();
                    listBoxAdv1.Refresh();
                }
            }

            if (Alerts.AlertsList.Count != lastCount) listBoxAdv1.Refresh();
            //Check Banned
            lastCount = Alerts.AlertsList.Count;
        }

        private void _timerUpdate_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
            CheckAlarmStatus();
            Logs.Save(this);
        }

        private void CheckAlarmStatus()
        {

        }

        int ctr = 0;
        internal void UpdateStatus()
        {
            CheckAlertUpdates();
            LoginUIUpdates();
            VerifyMode();

            //Call Update Status For All Forms
            foreach (IUpdate frm in _allForms)
            {
                frm.UpdateStatus();
            }
        }

        private void VerifyMode()
        {
            //Mode Changing
            buttonXAutoMode.Checked = AutoMode;
            buttonXManualPourMode.Checked = ManualPourMode;
        }

        private void LoginUIUpdates()
        {
            expandablePanel1.Visible = LoggedInUser != null && !gpLogin.Visible && !gpCreateNewUser.Visible;
            expandablePanelSettings.Visible = LoggedInUser != null && !gpLogin.Visible && !gpCreateNewUser.Visible;
            buttonXSignout.Text = !gpLogin.Visible && !gpCreateNewUser.Visible && LoggedInUser != null ? "Logout" : "Login";
        }

        private void CheckAlertUpdates()
        {
            if (LoggedInUser != null && !Alerts.HasUnreadAlert())
            {
                ctr = ctr + 1;
                if (ctr >= 75 && expandablePanel1.ButtonImageExpand.RawFormat == Resources.icons8_notification_48px_5.RawFormat)
                {
                    expandablePanel1.ButtonImageCollapse = Resources.icons8_notification_48px_3;
                    expandablePanel1.ButtonImageExpand = Resources.icons8_notification_48px_3;
                    expandablePanel1.Refresh();
                    ctr = 0;
                }
                else if (ctr >= 75 && expandablePanel1.ButtonImageExpand.RawFormat == Resources.icons8_notification_48px_3.RawFormat)
                {
                    expandablePanel1.ButtonImageCollapse = Resources.icons8_notification_48px_5;
                    expandablePanel1.ButtonImageExpand = Resources.icons8_notification_48px_5;
                    expandablePanel1.Refresh();
                    ctr = 0;
                }
            }

            buttonXMarkAllRead.Visible = _0._0.Properties.Settings.Default.InTesting || Alerts.HasUnreadAlert();
        }

        private void SetupForms()
        {
            //Manual Pouring Form
            frmManualPouring = new FormManualPouring(this);
            _allForms.Add(frmManualPouring);
            Logs.NewEntry(DateTime.Now, "Manual Pouring Form Setup Successfully.", "STARTUP");

            //Global Recipes Form
            frmGlobalRecipes = new FormGlobalRecipes(this);
            _allForms.Add(frmGlobalRecipes);
            Logs.NewEntry(DateTime.Now, "Global Recipes Form Setup Successfully.", "STARTUP");

            //News Feed Form
            frmNewsFeed = new FormNewsFeed(this);
            _allForms.Add(frmNewsFeed);
            Logs.NewEntry(DateTime.Now, "News Feed Form Setup Successfully.", "STARTUP");

            //Create Recipe Form
            frmCreateRecipe = new FormCreateRecipe(this);
            _allForms.Add(frmCreateRecipe);
            Logs.NewEntry(DateTime.Now, "CreateRecipe Form Setup Successfully.", "STARTUP");

            //Log Form
            frmLogViewer = new FormLogViewer(this, Logs);
            _allForms.Add(frmLogViewer);
            Logs.NewEntry(DateTime.Now, "LogViewer Form Setup Successfully.", "STARTUP");

            //Home Form
            frmHome = new FormHome(this);
            _allForms.Add(frmHome);
            Logs.NewEntry(DateTime.Now, "Home Form Setup Successfully.", "STARTUP");

            //Settings Form
            frmSettings = new FormSettings(this);
            _allForms.Add(frmSettings);
            Logs.NewEntry(DateTime.Now, "Settings Form Setup Successfully.", "STARTUP");

            //Setting
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Settings", "Settings.xml")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Settings"));
                Logs.NewEntry(DateTime.Now, "Settings Folder Created Successfully.", "STARTUP");
                Settings = new Settings();
                Settings.Save(this, "Settings.xml");
                Logs.NewEntry(DateTime.Now, "New Settings Created Successfully.", "STARTUP");
            }
            else
            {
                Settings = new Settings();
                Settings = Settings.Load(this);
                Logs.NewEntry(DateTime.Now, "Settings File Loaded Successfully.", "STARTUP");
            }

            //Load Recipes
            foreach (string file in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Recipes")))
            {
                Recipe newRecipe = new Recipe();
                newRecipe = newRecipe.Load(this, file);
                Recipes.Add(newRecipe);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void timerUpdateScreen_Tick(object sender, EventArgs e)
        {
            //Set Expandable Panel Image For Notifications depending on if have any notifications that are new
            //Update Profile Follower Count, Likes, Dislikes, Downloads

            if (expandablePanel1.Expanded)
            {
                expandablePanel1.Size = new Size(624, 389);
            } else
            {
                expandablePanel1.Size = new Size(624, 35);
            }

            if (expandablePanelSettings.Expanded)
            {
                expandablePanelSettings.Size = new Size(624, 520);
            } else
            {
                expandablePanelSettings.Size = new Size(624, 35);
            }            
        }

        public void ResetAllMenuItems()
        {
            buttonXHome.Checked = false;
            buttonXEdit.Checked = false;
            buttonXSocial.Checked = false;
            buttonXSettings.Checked = false;
            panelSocialSubMenu.Visible = false;
            buttonXDownloadRecipes.Checked = false;
            buttonXNewsFeed.Checked = false;
        }

        private DataTable getCustomerInfoList()
        {
            DataTable dtActivityLog = new DataTable();

            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM doorsensors", con))
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    dtActivityLog.Load(reader);
                }
            }

            return dtActivityLog;
        }

        public void LoadForm(Form newfrm)
        {
            if (frmCreateRecipe.EditingRecipe && frmCreateRecipe.IsDirty && frmCreateRecipe.IsLoaded)
            {
                if (!ContinueWithoutSaving)
                {
                    CheckButtonCreateRecipe();
                    return;
                } else
                {
                    ClearEditingFlags();
                }
            }
            newfrm.TopLevel = false;
            newfrm.Dock = DockStyle.Fill;
            panelForms.Controls.Clear();
            panelForms.Controls.Add(newfrm);
            newfrm.Show();
        }

        public bool SaveNewAutoLogin(String Email, String Password)
        {
            Boolean bRetVal = true;
            String[] linesToWrite = { Email, Password};

            try
            {
                File.WriteAllLines(PathToAutoLoginFile, linesToWrite);
                Logs.NewEntry(DateTime.Now, $"Saved New Auto Login Details. User: {Email}   Pass: {Password}", "INFO");
            } catch (Exception ex)
            {
                Logs.NewEntry(DateTime.Now, $"Failed To Save New Auto Login Details!", "ERROR");
                bRetVal = false;
            }

            return bRetVal;
        }

        public void WriteAllLinesAsync()
        {
            string[] lines = { "First line", "Second line", "Third line" };

            File.WriteAllLines("WriteLines.txt", lines);
        }

        private void FormMainScreen_Load(object sender, EventArgs e)
        {
            AutoMode = true;
            DB = new MySQL(this, DB_Server, DB_Database, DB_UID, DB_Password);
            LoadForm(frmHome);
            Logs.NewEntry(DateTime.Now, "FormMain Loaded Successfully.", "STARTUP");

            string[] lines = System.IO.File.ReadAllLines(PathToAutoLoginFile);

            if (lines.Length != 0)
            {
                string email = lines[0];
                string password = lines[1];

                if (email != "" && password != "")
                    LogInUserAcct(email, password);
                else
                    LoggedInUser = null;
            }

            if (LoggedInUser == null)
            {
                panelForms.Controls.Clear(); 

                //Lock All Controls
                foreach (ButtonX button in panelExSideMenu.Controls)
                {
                    if (button.Name != "buttonXLoginLogout")
                        button.Enabled = false;
                }

                expandablePanel1.Enabled = false;
                expandablePanel1.ExpandOnTitleClick = false;
                buttonXMarkAllRead.Visible = false;

                if (_0._0.Properties.Settings.Default.InTesting)
                {
                    LoadForm(frmHome);
                }
                else
                {
                    gpLogin.Visible = true;
                    textBoxLoginUsernameEmail.Focus();
                }
            } else
            {
                LoadForm(frmHome);
            }

            expandablePanelSettings.Expanded = false;
            expandablePanel1.Expanded = false;
            expandablePanel1.ExpandOnTitleClick = true;
            expandablePanelSettings.ExpandOnTitleClick = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbMinimize_MouseHover(object sender, EventArgs e)
        {
            pbMinimize.Image = Resources.minimize_window_50px_Hovered;
        }

        private void pbClose_MouseHover(object sender, EventArgs e)
        {
            pbClose.Image = Resources.delete_50px_Hovered;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            pbClose.Image = Resources.delete_50px1;
        }

        private void pbMinimize_MouseLeave(object sender, EventArgs e)
        {
            pbMinimize.Image = Resources.minimize_window_50px;
        }

        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            pbClose.Image = Resources.delete_50px_Hovered;
        }

        private void pbMinimize_MouseEnter(object sender, EventArgs e)
        {
            pbMinimize.Image = Resources.minimize_window_50px_Hovered;
        }

        private void buttonXSocial_Click(object sender, EventArgs e)
        {
            if (!panelSocialSubMenu.Visible)
            {
                ResetAllMenuItems();
                panelSocialSubMenu.Visible = true;
                buttonXSocial.Checked = true;
            } else
            {
                ResetAllMenuItems();
            }
        }

        private void buttonXHome_Click(object sender, EventArgs e)
        {
            if (!buttonXHome.Checked)
            {
                ResetAllMenuItems();
                buttonXHome.Checked = true;
                if (AutoMode)
                    LoadForm(frmHome);
                else if (ManualPourMode)
                    LoadForm(frmManualPouring);
            } else
            {
                ResetAllMenuItems();
                buttonXHome.Checked = true;
                if (AutoMode)
                    LoadForm(frmHome);
                else if (ManualPourMode)
                    LoadForm(frmManualPouring);
            }
        }

        private void buttonXEdit_Click(object sender, EventArgs e)
        {
            if (!buttonXEdit.Checked)
            {
                if (frmCreateRecipe.EditingRecipe && frmCreateRecipe.IsDirty && frmCreateRecipe.IsLoaded)
                {
                    if (ContinueWithoutSaving)
                    {
                        ResetCreateRecipeForm();
                        ResetAllMenuItems();
                        CheckButtonCreateRecipe();
                        LoadForm(frmCreateRecipe);
                        ClearEditingFlags();
                    } else
                    {
                        CheckButtonCreateRecipe();
                    }
                } else
                {
                    ResetCreateRecipeForm();
                    ResetAllMenuItems();
                    CheckButtonCreateRecipe();
                    LoadForm(frmCreateRecipe);
                }
            }
            else
            {
                if (frmCreateRecipe.EditingRecipe && frmCreateRecipe.IsDirty && frmCreateRecipe.IsLoaded)
                {
                    if (ContinueWithoutSaving)
                    {
                        ClearEditingFlags();
                        ResetAllMenuItems();
                    } else
                    {
                        CheckButtonCreateRecipe();
                    }
                } else
                {
                    ResetAllMenuItems();
                }
            }
        }

        private void ResetCreateRecipeForm()
        {
            frmCreateRecipe = new FormCreateRecipe(this);
            if (!_allForms.Contains(frmCreateRecipe)) _allForms.Add(frmCreateRecipe);
        }

        private void CheckButtonCreateRecipe()
        {
            buttonXEdit.Checked = true;
            buttonXSettings.Checked = false;
            buttonXHome.Checked = false;
            buttonXSocial.Checked = false;
            buttonXNewsFeed.Checked = false;
            buttonXDownloadRecipes.Checked = false;
        }

        private void ClearEditingFlags()
        {
            frmCreateRecipe.EditingRecipe = false;
            frmCreateRecipe.IsDirty = false;
            frmCreateRecipe.IsLoaded = false;
        }

        private void expandablePanelSettings_Click(object sender, EventArgs e)
        {
            if (panelSocialSubMenu.Visible)
            {
                panelSocialSubMenu.Visible = false;
            }
        }

        private void expandablePanelSettings_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            if (panelSocialSubMenu.Visible)
            {
                panelSocialSubMenu.Visible = false;
            }

            if (expandablePanel1.Expanded)
            {
                expandablePanelSettings.Expanded = false;
                expandablePanel1.ExpandButton.RaiseClick();
                expandablePanel1.Expanded = false;
                expandablePanel1.Refresh();
            }
        }

        private void expandablePanel1_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            if (panelSocialSubMenu.Visible)
            {
                panelSocialSubMenu.Visible = false;
            }

            if (expandablePanelSettings.Expanded)
            {
                expandablePanelSettings.Expanded = false;
                expandablePanel1.Expanded = false;
                expandablePanelSettings.ExpandButton.RaiseClick();
                expandablePanelSettings.Refresh();
            }
        }


        public SqlConnection conn = new SqlConnection();
        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (!buttonXDownloadRecipes.Checked)
            {
                ResetAllMenuItems();
                buttonXDownloadRecipes.Checked = true;
                buttonXSocial.Checked = true;
                LoadForm(frmGlobalRecipes);
            }
            else
            {
                ResetAllMenuItems();
            }
            //panelSocialSubMenu.Visible = false;
            //// Create the command
            //SqlCommand command = new SqlCommand("SELECT * FROM DoorSensors WHERE FirstColumn = @firstColumnValue", conn);

            //using (conn)
            //{
            //    conn.ConnectionString = "Server=sql200.epizy.com;Database=epiz_27587812_Security;Trusted_Connection=true";
            //    conn.Open();

            //    // Add the parameters.
            //    command.Parameters.Add(new SqlParameter("firstColumnValue", 1));

            //    command.ExecuteNonQuery();
            //}

            //using (SqlDataReader reader = command.ExecuteReader())
            //{
            //    // while there is another record present
            //    while (reader.Read())
            //    {
            //        // write the data on to the screen
            //        Console.WriteLine(String.Format("{0} \t | {1}",
            //        // call the objects from their index
            //        reader[0], reader[1]));
            //    }
            //}

        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            panelSocialSubMenu.Visible = false;
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (!buttonXSettings.Checked)
            {
                ResetAllMenuItems();
                buttonXSettings.Checked = true;
                LoadForm(frmSettings);
            }
            else
            {
                ResetAllMenuItems();
            }
        }

        private void expandablePanel1_Click(object sender, EventArgs e)
        {
            if (panelSocialSubMenu.Visible)
            {
                panelSocialSubMenu.Visible = false;
            }
        }

        private void buttonXLogs_Click(object sender, EventArgs e)
        {
            expandablePanelSettings.Expanded = false;
            LoadForm(frmLogViewer);
        }

        public void GetAllUserFiles()
        {
            Users.Clear();
            if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Users")))
            {
                foreach (string file in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Users")))
                {
                    User newuser = new User();
                    newuser = newuser.Load(this, file);
                    Users.Add(newuser);
                }
            }
        }

        private void buttonXLogin_Click(object sender, EventArgs e)
        {
            GetAllUserFiles();
            LogInUserAcct(textBoxLoginUsernameEmail.Text, textBoxLoginPass.Text);
        }

        private void LogInUserAcct(string eMailUser, string password)
        {
            if (!UserIsLoggedIn)
            {
                foreach (User user in Users)
                {
                    if (user.EMailAddress == eMailUser || user.UserName == eMailUser)
                    {
                        if (user.AutoLogin)
                        {
                            if (user.Password == password)
                            {
                                LoggedInUser = user;
                                UserIsLoggedIn = true;

                                labelXLoggedInName.Text = LoggedInUser.UserName;
                                labelXLoggedInStats.Text = String.Format($"Likes: {LoggedInUser.Likes}            |     Dislikes: {LoggedInUser.Dislikes}\nDownloads: {LoggedInUser.DownloadCount}    |     Followers: {LoggedInUser.Followers}");
                                gpLogin.Visible = false;

                                foreach (ButtonX button in panelExSideMenu.Controls)
                                {
                                    if (!button.Enabled)
                                        button.Enabled = true;
                                }

                                expandablePanel1.Enabled = true;
                                textBoxLoginUsernameEmail.Text = "";
                                textBoxLoginPass.Text = "";
                                LoadForm(frmHome);
                                buttonXHome.Checked = true;
                            } else
                            {
                                LoggedInUser = null;
                            }
                        } else
                        {
                            if (user.Password == password)
                            {
                                LoggedInUser = user;
                                UserIsLoggedIn = true;

                                labelXLoggedInName.Text = LoggedInUser.UserName;
                                labelXLoggedInStats.Text = String.Format($"Likes: {LoggedInUser.Likes}            |     Dislikes: {LoggedInUser.Dislikes}\nDownloads: {LoggedInUser.DownloadCount}    |     Followers: {LoggedInUser.Followers}");
                                gpLogin.Visible = false;

                                foreach (ButtonX button in panelExSideMenu.Controls)
                                {
                                    if (!button.Enabled)
                                        button.Enabled = true;
                                }

                                expandablePanel1.Enabled = true;
                                textBoxLoginUsernameEmail.Text = "";
                                textBoxLoginPass.Text = "";
                                LoadForm(frmHome);
                                buttonXHome.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        private void buttonXLoginLogout_Click(object sender, EventArgs e)
        {
            if (buttonXSignout.Text == "Hide Login Menu")
            {
                gpCreateNewUser.Visible = false;
                gpLogin.Visible = false;
                return;
            }

            if (!UserIsLoggedIn)
            {
                panelForms.Controls.Clear();
                gpLogin.Visible = true;
                textBoxLoginUsernameEmail.Focus();
            } else
            {
                panelForms.Controls.Clear();
                LogoutCurrentUser();
            }
        }

        public Boolean ContinueWithoutSaving { get { return DialogResult.Yes == MessageBox.Show(this, "Are You Sure You Want To Continue?\nChanges To This Recipe Will Not Be Saved!", "Continue Without Saving?", MessageBoxButtons.YesNo, MessageBoxIcon.Question); } }
        private void LogoutCurrentUser()
        {
            if (DialogResult.Yes == MessageBox.Show(this, "Are You Sure You Want To Logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                UserIsLoggedIn = false;
                LoggedInUser = null;

                labelXLoggedInName.Text = "Not Logged In...";
                labelXLoggedInStats.Text = "Likes: N/A            |     Dislikes: N/A\nDownloads: N/A    |     Followers: N/A";


                //Lock All Controls
                foreach (ButtonX button in panelExSideMenu.Controls)
                {
                    button.Enabled = false;
                }

                expandablePanel1.Enabled = false;
                expandablePanel1.ExpandOnTitleClick = false;
                buttonXMarkAllRead.Visible = false;

                gpLogin.Visible = true;
                panelForms.Controls.Clear();
            } else
            {
                CheckButtonCreateRecipe();
            }
        }

        private void buttonXCancelLogin_Click(object sender, EventArgs e)
        {
            gpLogin.Visible = false;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            gpCreateNewUser.Visible = true;
            gpLogin.Visible = false;
        }

        private void buttonXBackToLogin_Click(object sender, EventArgs e)
        {
            gpCreateNewUser.Visible = false;
            gpLogin.Visible = true;
        }

        private void buttonXCancelCreate_Click(object sender, EventArgs e)
        {
            gpCreateNewUser.Visible = false;
            gpLogin.Visible = true;
        }

        private void buttonXCreateUser_Click(object sender, EventArgs e)
        {
            //No Blank Fields
            //Email Field Must Have '.' & '@' Characters
            if (textBoxEmail.Text != "" && textBoxUser.Text != "" && textBoxPass.Text != "" && textBoxConfPass.Text != "" && (textBoxEmail.Text.Contains("@") && textBoxEmail.Text.Contains(".")))
            {
                //Passwords Must Match
                if (textBoxPass.Text == textBoxConfPass.Text)
                {
                    User newUser = new User();
                    newUser.EMailAddress = textBoxEmail.Text;
                    newUser.UserName = textBoxUser.Text;
                    newUser.Password = textBoxPass.Text;
                    newUser.JoinDate = DateTime.Now;

                    newUser.Save(this);

                    textBoxEmail.Text = "";
                    textBoxUser.Text = "";
                    textBoxPass.Text = "";
                    textBoxConfPass.Text = "";
                    gpCreateNewUser.Visible = false;

                    GetAllUserFiles();

                    //Populate EMail Text Field Ahead Of Time
                    gpLogin.Visible = true;
                    textBoxLoginUsernameEmail.Text = newUser.EMailAddress;
                    textBoxPass.Text = "";
                }
            }
        }

        private void x(object sender, EventArgs e)
        {

        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Please Use https://www.FusionBartender.com/Support To Request Password Reset.", "Forgot User", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        bool unmarkedAsRead = false;
        private void buttonXMarkAllRead_Click(object sender, EventArgs e)
        {
            List<Alert> alerts = new List<Alert>();
            foreach (ListBoxItem item in listBoxAdv1.Items)
            {
                Alert alert = (Alert)item.Tag;
                item.CheckState = CheckState.Unchecked;
                alert.MarkedAsRead = true;
                alerts.Add(alert);
            }
            unmarkedAsRead = false;
            Alerts.AlertsList = alerts;
            listBoxAdv1.Refresh();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            expandablePanelSettings.Expanded = false;
            frmSettings.viewProfile = true;
            frmSettings.viewPump = false;
            frmSettings.viewMaint = false;
            frmSettings.loading = true;
            LoadForm(frmSettings);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            expandablePanelSettings.Expanded = false;
            frmSettings.viewPump = true;
            frmSettings.viewProfile = false;
            frmSettings.viewMaint = false;
            frmSettings.loading = true;
            LoadForm(frmSettings);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            expandablePanelSettings.Expanded = false;
            frmSettings.viewMaint = true;
            frmSettings.viewPump = false;
            frmSettings.viewProfile = false;
            frmSettings.loading = true;
            LoadForm(frmSettings);
        }

        private void buttonX8_Click_1(object sender, EventArgs e)
        {
            if (buttonXSignout.Text == "Login")
            {
                expandablePanelSettings.Expanded = false;
                panelForms.Controls.Clear();
                gpLogin.Visible = true;
                textBoxLoginUsernameEmail.Focus();
            }
            else
            {
                expandablePanelSettings.Expanded = false;
                LogoutCurrentUser();
            }
        }

        private void buttonXNewsFeed_Click(object sender, EventArgs e)
        {
            ResetAllMenuItems();
            buttonXSocial.Checked = true;
            LoadForm(frmNewsFeed);
        }

        private void buttonXArduinoSettings_Click(object sender, EventArgs e)
        {
            expandablePanelSettings.Expanded = false;
            frmSettings.viewArduino = true;
            frmSettings.viewMaint = false;
            frmSettings.viewPump = false;
            frmSettings.viewProfile = false;
        }

        private void FormMainScreen_Leave(object sender, EventArgs e)
        {
            Logs.Save(this);
        }

        private void buttonXAutoMode_Click(object sender, EventArgs e)
        {
            expandablePanelSettings.Expanded = false;
            buttonXAutoMode.Checked = true;
            AutoMode = true;
            buttonXManualPourMode.Checked = false;
            ManualPourMode = false;
            buttonXHome.PerformClick();
        }

        private void buttonXManualPourMode_Click(object sender, EventArgs e)
        {
            expandablePanelSettings.Expanded = false;
            buttonXAutoMode.Checked = false;
            AutoMode = false;
            buttonXManualPourMode.Checked = true;
            ManualPourMode = true;
            buttonXHome.PerformClick();
        }

        private void panelExTopMenu_Click(object sender, EventArgs e)
        {

        }
    }
}
