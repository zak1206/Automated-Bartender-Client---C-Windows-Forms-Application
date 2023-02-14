using DevComponents.DotNetBar;
using Fusion_Bartender_1._0.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fusion_Bartender_1._0._0
{
    public partial class FormSettings : Form, IUpdate
    {
        private FormMainScreen _frmMainScrn = null;
        private BackgroundWorker _bgHideSaved = new BackgroundWorker();
        public bool viewProfile = false;
        public bool viewPump = false;
        public bool viewMaint = false;
        public bool viewArduino = false;
        public bool viewAdmin = false;
        public bool loading = false;
        private Boolean IsDirty = false;
        private bool checkChanged = false;
        private bool isVis = false;

        public FormSettings(FormMainScreen frmMain)
        {
            _frmMainScrn = frmMain;
            _bgHideSaved.DoWork += _bgHideSaved_DoWork;
            _bgHideSaved.RunWorkerCompleted += _bgHideSaved_Completed;
            InitializeComponent();
        }

        #region Background Workers
        private void _bgHideSaved_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            labelXSaved.Visible = false;
        }
        private void _bgHideSaved_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(6000);
        }
        #endregion

        #region Menu Button Click Events
        private void buttonXMaint_Click(object sender, EventArgs e)
        {
            ResetAllPanelItems();
            buttonXMaint.Checked = true;
            gpMaintSettings.Visible = true;
            LoadMaintSettings();
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Maintenance</i><font><i></i><b></b></font></font></b>";
        }
        private void buttonXProfile_Click(object sender, EventArgs e)
        {
            ResetAllPanelItems();
            buttonXProfile.Checked = true;
            gpProfileSettings.Visible = true;
            LoadProfileSettings();
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Profile Settings</i><font><i></i><b></b></font></font></b>";
        }
        private void buttonXPump_Click(object sender, EventArgs e)
        {
            ResetAllPanelItems();
            buttonXPump.Checked = true;
            gpPumpSettings.Visible = true;
            LoadPumpSettings();
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Pump Settings</i><font><i></i><b></b></font></font></b>";
        }
        private void buttonXAdminPanel_Click(object sender, EventArgs e)
        {
            ResetAllPanelItems();
            buttonXAdminPanel.Checked = true;
            gpAdminPanel.Visible = true;
            LoadAdminSettings(_frmMainScrn.LoggedInUser.Rights);
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Moderator Panel</i><font><i></i><b></b></font></font></b>";
        }
        private void buttonXArduino_Click(object sender, EventArgs e)
        {
            ResetAllPanelItems();
            buttonXArduino.Checked = true;
            gpArduino.Visible = true;
            LoadArduinoSettings();
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Arduino Settings</i><font><i></i><b></b></font></font></b>";
        }
        #endregion

        #region Timers
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool starting = false;
            //Start BG Worker To Hide Label
            if (!_bgHideSaved.IsBusy && labelXSaved.Visible && !starting)
            {
                starting = true;
                _bgHideSaved.RunWorkerAsync();
            }
        }
        public void UpdateStatus()
        {
            isVis = Visible;

            if (isVis)
            {
                if (viewMaint)
                {
                    viewArduino = false;
                    ViewMaintenancePanel();
                }
                else if (viewProfile)
                {
                    viewArduino = false;
                    ViewProfilePanel();
                }
                else if (viewPump)
                {
                    viewArduino = false;
                    ViewPumpPanel();
                }
                else if (viewArduino)
                {
                    ViewArduinoPanel();
                    UpdateArduinoData();
                    StartArduinoDataUpdateWorker();
                }
                else if (viewAdmin)
                {
                    viewArduino = false;
                    ViewAdminPanel();
                } else
                {
                    viewArduino = false;
                }

                IsDirty = _frmMainScrn.LoggedInUser != null ? (_frmMainScrn.LoggedInUser.EMailAddress != textBoxEMail.Text && _frmMainScrn.LoggedInUser.Password != textBoxPassword.Text) || checkChanged : false;
                labelXRights.Text = _frmMainScrn.LoggedInUser.Rights;
                knobControlHum.Value = int.Parse(Math.Round(_frmMainScrn.Arduino.Humidity).ToString());
                knobControlTemp.Value = int.Parse(Math.Round(_frmMainScrn.Arduino.Temperature).ToString());
                labelXHumidity.Text = _frmMainScrn.Arduino.Humidity.ToString("#0") + " %";
                labelXTemperature.Text = _frmMainScrn.Arduino.Temperature.ToString("#0.00") + " °F";
                if (_frmMainScrn.Arduino.LastMsgReceived != lastMsg)
                {
                    lastMsg = _frmMainScrn.Arduino.LastMsgReceived;
                    richTextBoxArduino.Text += _frmMainScrn.Arduino.LastMsgReceived;
                }
                buttonXSaveUserSettings.Visible = IsDirty;
                labelXIsDirty.Visible = IsDirty;
                labelXJoinDate.Text = _frmMainScrn.LoggedInUser != null ? _frmMainScrn.LoggedInUser.JoinDate.ToLongDateString() : "Not Logged In...";
                textBoxEMail.Enabled = _frmMainScrn.LoggedInUser != null;
                textBoxPassword.Enabled = _frmMainScrn.LoggedInUser != null;
                checkBoxXAutoLogin.Enabled = _frmMainScrn.LoggedInUser != null;
                buttonXMaint.Visible = _frmMainScrn.LoggedInUser.UserName != "ZRowt";
                buttonXArduino.Visible = _frmMainScrn.LoggedInUser.UserName != "ZRowt";
                buttonArduinoConnect.Text = _frmMainScrn.Arduino.IsConnected ? "Disconnect" : "Connect";
                buttonArduinoConnect.BackColor = _frmMainScrn.Arduino.IsConnected ? Color.Lime : Color.LightGray;
                buttonXAdminPanel.Visible = _frmMainScrn.LoggedInUser.Rights == "Moderator" || _frmMainScrn.LoggedInUser.Rights == "Administrator" || _frmMainScrn.LoggedInUser.Rights == "Owner" || _frmMainScrn.LoggedInUser.Rights == "Creator/Developer";

                //Handle Rare Case Where Not Logged In Butt Viewing Profile Settings...
                if (_frmMainScrn.LoggedInUser == null)
                {
                    textBoxPassword.Text = "Not Logged In...";
                    textBoxEMail.Text = "Not Logged In...";
                }
            }
        }

        BackgroundWorker bgwArduinoData = null;
        private void StartArduinoDataUpdateWorker()
        {
            bgwArduinoData = new BackgroundWorker();
            bgwArduinoData.DoWork += BgwArduinoData_DoWork; 
            bgwArduinoData.RunWorkerCompleted += BgwArduinoData_RunWorkerCompleted;
            bgwArduinoData.RunWorkerAsync();
        }

        private void BgwArduinoData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void BgwArduinoData_DoWork(object sender, DoWorkEventArgs e)
        {
            while (viewArduino)
            {

                Thread.Sleep(50);
            }
        }

        private void UpdateArduinoData()
        {
                labelRecipeNAme.Text = $"Recipe Name: {_frmMainScrn.Arduino.RecipeRunningName}";
                labelP1L.Text = $"Pump-1 Liquid: {_frmMainScrn.Arduino.RecipePump1Liquid}";
                labelP2L.Text = $"Pump-2 Liquid: {_frmMainScrn.Arduino.RecipePump2Liquid}";
                labelP3L.Text = $"Pump-3 Liquid: {_frmMainScrn.Arduino.RecipePump3Liquid}";
                labelP4L.Text = $"Pump-4 Liquid: {_frmMainScrn.Arduino.RecipePump4Liquid}";
                labelP1TTR.Text = $"Pump-1 Time To Run: {_frmMainScrn.Arduino.RecipePump1TimeToRun}";
                labelP2TTR.Text = $"Pump-2 Time To Run: {_frmMainScrn.Arduino.RecipePump2TimeToRun}";
                labelP3TTR.Text = $"Pump-3 Time To Run: {_frmMainScrn.Arduino.RecipePump3TimeToRun}";
                labelP4TTR.Text = $"Pump-4 Time To Run: {_frmMainScrn.Arduino.RecipePump4TimeToRun}";
                labelP1Running.BackColor = _frmMainScrn.Arduino.Pump1Active ? Color.Lime : Color.Red;
                labelP2Running.BackColor = _frmMainScrn.Arduino.Pump2Active ? Color.Lime : Color.Red;
                labelP3Running.BackColor = _frmMainScrn.Arduino.Pump3Active ? Color.Lime : Color.Red;
                labelP4Running.BackColor = _frmMainScrn.Arduino.Pump4Active ? Color.Lime : Color.Red;

                label6.BackColor = _frmMainScrn.Arduino.RecipeStarted ? Color.Lime : Color.Red;
                label5.BackColor = _frmMainScrn.Arduino.RecipeRunning ? Color.Lime : Color.Red;
                label4.BackColor = _frmMainScrn.Arduino.RecipeComplete ? Color.Lime : Color.Red;
        }

        private string lastMsg = "";
        #endregion

        #region Panel Load Functions
        private void LoadArduinoSettings()
        {
            if (loading)
            {
                LoadAvailableCOMMPorts();
                comboBoxExCOM.Text = _frmMainScrn.Settings.ArduinoCOMPort;
            }
        }
        private void LoadAvailableCOMMPorts()
        {
            string[] ports = SerialPort.GetPortNames();

            _frmMainScrn.Logs.NewEntry(DateTime.Now, "The following serial ports were found: ", "INFO");

            foreach (string port in ports)
            {
                comboBoxExCOM.Items.Add(port);
                _frmMainScrn.Logs.NewEntry(DateTime.Now, "\t\t" + port, "INFO");
            }
        }
        private void LoadMaintSettings()
        {
            //Load UI Variables
            textBoxFileLoc.Text = _frmMainScrn.Settings.FileLocation;
            textBoxSecPerFLOZ.Text = _frmMainScrn.Settings.SecondsPerFluidOZ.ToString("#0.00");
            textBoxMaxHum.Text = _frmMainScrn.Settings.MaxHumidityAllowed.ToString("#0.00");
            textBoxMaxTemp.Text = _frmMainScrn.Settings.MaxInternalTemperatureAllowed.ToString("#0.00");

            //Add All Available COM Ports Into List
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                DevComponents.DotNetBar.ComboBoxItem item = new DevComponents.DotNetBar.ComboBoxItem();
                item.Text = port;
                comboBoxExCOM.Items.Add(item);
            }
        }
        private void LoadPumpSettings()
        {
            listBoxAdvPumpConfigs.Items.Clear();

            foreach (Pump pump in _frmMainScrn.Settings.Pumps)
            {
                ListBoxItem item = new ListBoxItem();
                item.Text = pump.LiquidName + "  -  " + "Pump ID: " + pump.PumpID;
                item.Tag = pump;
                item.Image = Properties.Resources.pump_50px;

                listBoxAdvPumpConfigs.Items.Add(item);
            }

            textBoxPumpCt.Text = _frmMainScrn.Settings.PumpCount.ToString("#0");

            if (_frmMainScrn.Settings.PumpCount < 8)
            {
                comboBoxExAvailPumps.Items.Remove(comboItem5);
                comboBoxExAvailPumps.Items.Remove(comboItem6);
                comboBoxExAvailPumps.Items.Remove(comboItem7);
                comboBoxExAvailPumps.Items.Remove(comboItem8);
            }
        }
        private void LoadProfileSettings()
        {
            string[] lines = File.ReadAllLines(_frmMainScrn.PathToAutoLoginFile);
            textBoxEMail.Text = _frmMainScrn.LoggedInUser.EMailAddress;
            textBoxPassword.Text = _frmMainScrn.LoggedInUser.Password;
            checkBoxXAutoLogin.Checked = _frmMainScrn.LoggedInUser.AutoLogin;
        }
        #endregion

        #region Helper Functions
        private void LoadAdminSettings(string rights)
        {
            if (rights == "Moderator")
            {
                labelXRights.Text = "Moderator";
            } else if (rights == "Administrator")
            {
                labelXRights.Text = "Administrator";
            } else if (rights == "Owner")
            {
                labelXRights.Text = "Badass MotherFuckin' Creator";
            }
        }
        #endregion

        #region Panel View Functions
        private void ViewAdminPanel()
        {
            ResetAllPanelItems();
            buttonXAdminPanel.Checked = true;
            gpAdminPanel.Visible = true;
            LoadAdminSettings(_frmMainScrn.LoggedInUser.Rights);
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Arduino Settings</i><font><i></i><b></b></font></font></b>";
        }
        private void ViewArduinoPanel()
        {
            ResetAllPanelItems();
            buttonXArduino.Checked = true;
            gpArduino.Visible = true;
            LoadArduinoSettings();
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Arduino Settings</i><font><i></i><b></b></font></font></b>";
        }
        private void ViewPumpPanel()
        {
            ResetAllPanelItems();
            buttonXPump.Checked = true;
            gpPumpSettings.Visible = true;
            LoadPumpSettings();
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Pump Settings</i><font><i></i><b></b></font></font></b>";
        }
        private void ViewProfilePanel()
        {
            ResetAllPanelItems();
            buttonXProfile.Checked = true;
            gpProfileSettings.Visible = true;
            LoadProfileSettings();
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Profile Settings</i><font><i></i><b></b></font></font></b>";
        }
        private void ViewMaintenancePanel()
        {
            ResetAllPanelItems();
            buttonXMaint.Checked = true;
            gpMaintSettings.Visible = true;
            LoadMaintSettings();
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Maintenance</i><font><i></i><b></b></font></font></b>";
        }
        private void ResetAllPanelItems()
        {
            buttonXMaint.Checked = false;
            buttonXProfile.Checked = false;
            buttonXPump.Checked = false;
            buttonXArduino.Checked = false;
            buttonXAdminPanel.Checked = false;

            gpArduino.Visible = false;
            gpProfileSettings.Visible = false;
            gpPumpSettings.Visible = false;
            gpMaintSettings.Visible = false;
            gpAdminPanel.Visible = false;
        }
        #endregion

        #region UI Event Handlers
        private void textBoxPumpCt_TextChanged(object sender, EventArgs e)
        {
            int pumpCount = 0;
            if (!int.TryParse(textBoxPumpCt.Text, out pumpCount))
                return;

            if (pumpCount != _frmMainScrn.Settings.PumpCount)
            {
                _frmMainScrn.Settings.PumpCount = pumpCount;
                _frmMainScrn.Settings.Save(_frmMainScrn);
                LoadPumpSettings();
                labelXSaved.Visible = true;
            }
        }
        private void FormSettings_Load(object sender, EventArgs e)
        {
            reflectionLabelMenu.Text = "<b><font size=\" + 28\"><i>Settings</i><font><i></i><b></b></font></font></b>";
            _frmMainScrn.Logs.NewEntry(DateTime.Now, "Settings Form Loaded Successfully", "ACTION");
            timer1.Enabled = true;
            buttonXProfile.PerformClick();
        }
        private void listBoxAdvPumpConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAdvPumpConfigs.SelectedIndex != -1)
            {
                Pump pump = (Pump)((ListBoxItem)listBoxAdvPumpConfigs.SelectedItem).Tag;

                textBoxLiquidName.Text = pump.LiquidName;
                comboBoxExAvailPumps.Text = pump.PumpID.ToString();
                buttonSavePumps.Visible = true;
            }
        }
        private void buttonSavePumps_Click(object sender, EventArgs e)
        {
            string pumpID = comboBoxExAvailPumps.Text;
            bool exists = false;
            int nTemp = 0;
            foreach (Pump pump in _frmMainScrn.Settings.Pumps)
            {
                if (pump.PumpID == int.Parse(pumpID))
                {
                    exists = true;
                    pump.LiquidName = textBoxLiquidName.Text;
                    _frmMainScrn.Settings.Save(_frmMainScrn);

                    textBoxLiquidName.Text = "";
                    comboBoxExAvailPumps.SelectedIndex = -1;
                    LoadPumpSettings();
                }
            }

            if (!exists)
            {
                Pump newPump = new Pump();
                newPump.LiquidName = textBoxLiquidName.Text; 
                int.TryParse(comboBoxExAvailPumps.Text, out nTemp);
                newPump.PumpID = nTemp;
                _frmMainScrn.Settings.Pumps.Add(newPump);
                _frmMainScrn.Settings.Save(_frmMainScrn);

                textBoxLiquidName.Text = "";
                comboBoxExAvailPumps.SelectedIndex = -1;
                LoadPumpSettings();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Settings");
            ofd.Filter = "xml files (*.xml)|*.xml";
            if (DialogResult.OK == ofd.ShowDialog())
            {
                _frmMainScrn.Settings.FileLocation = ofd.FileName;
                _frmMainScrn.Settings.Save(_frmMainScrn);
                labelXSaved.Visible = true;
                _frmMainScrn.Logs.NewEntry(DateTime.Now, "Settings.xml File Location Changed To: " + _frmMainScrn.Settings.FileLocation, "ACTION");
            }
        }
        private void comboBoxExCOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxExCOM.Text != "" && comboBoxExCOM.Text.Contains("COM"))
            {
                _frmMainScrn.Settings.ArduinoCOMPort = comboBoxExCOM.Text;
                _frmMainScrn.Settings.Save(_frmMainScrn);
                labelXSaved.Visible = true;
            }
        }
        private void textBoxSecPerFLOZ_TextChanged(object sender, EventArgs e)
        {
            double flOZ = 0;

            if (textBoxSecPerFLOZ.Text != _frmMainScrn.Settings.SecondsPerFluidOZ.ToString())
            {
                if (!double.TryParse(textBoxSecPerFLOZ.Text, out flOZ))
                    return;

                _frmMainScrn.Settings.SecondsPerFluidOZ = flOZ;
                _frmMainScrn.Settings.Save(_frmMainScrn);
                labelXSaved.Visible = true;
            }
        }
        private void textBoxMaxHum_TextChanged(object sender, EventArgs e)
        {
            double newMaxHum = 0;

            if (textBoxMaxHum.Text != _frmMainScrn.Settings.MaxHumidityAllowed.ToString())
            {
                if (!double.TryParse(textBoxMaxHum.Text, out newMaxHum))
                    return;

                _frmMainScrn.Settings.MaxHumidityAllowed = newMaxHum;
                _frmMainScrn.Settings.Save(_frmMainScrn);
                labelXSaved.Visible = true;
            }
        }
        private void textBoxMaxTemp_TextChanged(object sender, EventArgs e)
        {
            double newMaxHum = 0;

            if (textBoxMaxTemp.Text != _frmMainScrn.Settings.MaxInternalTemperatureAllowed.ToString())
            {
                if (!double.TryParse(textBoxMaxTemp.Text, out newMaxHum))
                    return;

                _frmMainScrn.Settings.MaxInternalTemperatureAllowed = newMaxHum;
                _frmMainScrn.Settings.Save(_frmMainScrn);
                labelXSaved.Visible = true;
            }
        }
        private void buttonXSaveUserSettings_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(_frmMainScrn.PathToAutoLoginFile);

            _frmMainScrn.LoggedInUser.EMailAddress = textBoxEMail.Text;
            _frmMainScrn.LoggedInUser.Password = textBoxPassword.Text;
            _frmMainScrn.LoggedInUser.AutoLogin = checkBoxXAutoLogin.Checked;

            //Modify Auto Login Details If Different
            if (checkBoxXAutoLogin.Checked)
            {
                File.WriteAllLines(_frmMainScrn.PathToAutoLoginFile, new string[] { textBoxEMail.Text, textBoxPassword.Text });
            } else
            {
                 File.WriteAllLines(_frmMainScrn.PathToAutoLoginFile, new string[] { "", "" });
            }

            IsDirty = !(_frmMainScrn.LoggedInUser.Save(_frmMainScrn));
            checkChanged = IsDirty;
        }
        private void checkBoxXAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            checkChanged = true;
        }
        private void buttonArduinoConnect_Click(object sender, EventArgs e)
        {
            if (!_frmMainScrn.Arduino.IsConnected)
            {
                _frmMainScrn.Settings.ArduinoCOMPort = comboBoxExCOM.Text;
                _frmMainScrn.Logs.NewEntry(DateTime.Now, $"Saving Arduino COM: {comboBoxExCOM.Text}.", "INFO");
                _frmMainScrn.Settings.Save(_frmMainScrn);
                _frmMainScrn.Logs.NewEntry(DateTime.Now, "Connecting To Arduino...", "INFO");
                if (_frmMainScrn.Arduino.Connect())
                {
                    _frmMainScrn.Logs.NewEntry(DateTime.Now, "Connection To Arduino Was Successful!", "INFO");
                } else
                {
                    _frmMainScrn.Logs.NewEntry(DateTime.Now, "Connection Unsuccessful!", "ERROR");
                }
            } else
            {
                _frmMainScrn.Logs.NewEntry(DateTime.Now, "Disconmnecting From Arduino... " + _frmMainScrn.Settings.ArduinoCOMPort, "INFO");
                _frmMainScrn.Arduino.Disconnect();
            }
        }
        private void textBoxLiquidName_TextChanged(object sender, EventArgs e)
        {
            buttonSavePumps.Visible = true;
        }
        private void comboBoxExAvailPumps_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonSavePumps.Visible = true;
        }
        private void listBoxAdvPumpConfigs_ItemClick(object sender, EventArgs e)
        {

        }
        #endregion

        private void labelP1TTR_Click(object sender, EventArgs e)
        {

        }

        private void labelP3TTR_Click(object sender, EventArgs e)
        {

        }

        private void labelP2TTR_Click(object sender, EventArgs e)
        {

        }

        private void labelP4TTR_Click(object sender, EventArgs e)
        {

        }

        private void gpArduino_VisibleChanged(object sender, EventArgs e)
        {
            viewArduino = gpArduino.Visible;
        }
    }
}
