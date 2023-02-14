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
    public partial class FormManualPouring : Form, IUpdate
    {
        private FormMainScreen _frmMain = null;
        public Boolean PouringDrink { get; set; } = false;
        public Boolean ButtonIsPressed { get; set; } = false;
        public Boolean ArduinoCommandSent { get; set; } = false;
        public Int16 PumpSelected { get; set; } = -1;
        public FormManualPouring(FormMainScreen frmMain)
        {
            _frmMain = frmMain;
            InitializeComponent();
        }

        public void UpdateStatus()
        {
            if (Visible)
            {
                labelXPouringDrink1.Visible = _frmMain.Arduino.Pump1Active;
                labelXPouringDrink1.BackgroundStyle.TextShadowColor = Color.FromKnownColor(KnownColor.Transparent);
                labelXPouringDrink2.Visible = _frmMain.Arduino.Pump2Active;
                labelXPouringDrink2.BackgroundStyle.TextShadowColor = Color.FromKnownColor(KnownColor.Transparent);
                labelXPouringDrink3.Visible = _frmMain.Arduino.Pump3Active;
                labelXPouringDrink3.BackgroundStyle.TextShadowColor = Color.FromKnownColor(KnownColor.Transparent);
                labelXPouringDrink4.Visible = _frmMain.Arduino.Pump4Active;
                labelXPouringDrink4.BackgroundStyle.TextShadowColor = Color.FromKnownColor(KnownColor.Transparent);
                labelXPouringDrink5.Visible = _frmMain.Arduino.Pump5Active;
                labelXPouringDrink5.BackgroundStyle.TextShadowColor = Color.FromKnownColor(KnownColor.Transparent);
                labelXPouringDrink6.Visible = _frmMain.Arduino.Pump6Active;
                labelXPouringDrink6.BackgroundStyle.TextShadowColor = Color.FromKnownColor(KnownColor.Transparent);
                labelXPouringDrink7.Visible = _frmMain.Arduino.Pump7Active;
                labelXPouringDrink7.BackgroundStyle.TextShadowColor = Color.FromKnownColor(KnownColor.Transparent);
                labelXPouringDrink8.Visible = _frmMain.Arduino.Pump8Active;
                labelXPouringDrink8.BackgroundStyle.TextShadowColor = Color.FromKnownColor(KnownColor.Transparent);
                buttonXDrink1.Visible = _frmMain.Settings.PumpCount > 0;
                buttonXDrink2.Visible = _frmMain.Settings.PumpCount > 1;
                buttonXDrink3.Visible = _frmMain.Settings.PumpCount > 2;
                buttonXDrink4.Visible = _frmMain.Settings.PumpCount > 3;
                buttonXDrink5.Visible = _frmMain.Settings.PumpCount > 4;
                buttonXDrink6.Visible = _frmMain.Settings.PumpCount > 5;
                buttonXDrink7.Visible = _frmMain.Settings.PumpCount > 6;
                buttonXDrink8.Visible = _frmMain.Settings.PumpCount > 7;
                labelXTitle.Text = PouringDrink ? "Release The Button To Stop Pouring Your Drink..." : "Hold Any Button Below To Begin Pouring Your Drink...";

            }
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            SetupScreen();
        }

        private void SetupScreen()
        {
            switch (_frmMain.Settings.PumpCount)
            {
                case 4:
                    buttonXDrink1.Text = _frmMain.Settings.Pumps[0].LiquidName;
                    buttonXDrink2.Text = _frmMain.Settings.Pumps[1].LiquidName;
                    buttonXDrink3.Text = _frmMain.Settings.Pumps[2].LiquidName;
                    buttonXDrink4.Text = _frmMain.Settings.Pumps[3].LiquidName;
                    break;
                case 8:
                    buttonXDrink1.Text = _frmMain.Settings.Pumps[0].LiquidName;
                    buttonXDrink2.Text = _frmMain.Settings.Pumps[1].LiquidName;
                    buttonXDrink3.Text = _frmMain.Settings.Pumps[2].LiquidName;
                    buttonXDrink4.Text = _frmMain.Settings.Pumps[3].LiquidName;
                    buttonXDrink5.Text = _frmMain.Settings.Pumps[4].LiquidName;
                    buttonXDrink6.Text = _frmMain.Settings.Pumps[5].LiquidName;
                    buttonXDrink7.Text = _frmMain.Settings.Pumps[6].LiquidName;
                    buttonXDrink8.Text = _frmMain.Settings.Pumps[7].LiquidName;
                    break;
            }
        }

        private void ResetProperties()
        {
            PouringDrink = false;
            ArduinoCommandSent = false;
            ButtonIsPressed = false;
        }

        private void StartPouring(int pumpID)
        {
            _frmMain.Arduino.StartPouring(pumpID);
            ArduinoCommandSent = true;
        }
        private void StopPouring(int pumpID)
        {
            _frmMain.Arduino.StopPouring(pumpID);
            ArduinoCommandSent = false;
        }

        #region Button Events
        private void buttonXDrink_MouseDown(object sender, MouseEventArgs e)
        {
            PumpSelected = Int16.Parse(((ButtonX)sender).Tag.ToString());
            StartPouring(PumpSelected);
            PouringDrink = true;
            ButtonIsPressed = true;
        }
        private void buttonXDrink_MouseUp(object sender, MouseEventArgs e)
        {
            PumpSelected = Int16.Parse(((ButtonX)sender).Tag.ToString());
            StopPouring(PumpSelected);
            ResetProperties();
        }
        #endregion

        private void buttonXDrink1_Click(object sender, EventArgs e)
        {

        }

        private void buttonXDrink2_Click(object sender, EventArgs e)
        {

        }

        private void buttonXDrink3_Click(object sender, EventArgs e)
        {

        }

        private void buttonXDrink4_Click(object sender, EventArgs e)
        {

        }

        private void buttonXDrink8_Click(object sender, EventArgs e)
        {

        }

        private void buttonXDrink7_Click(object sender, EventArgs e)
        {

        }

        private void buttonXDrink6_Click(object sender, EventArgs e)
        {

        }

        private void buttonXDrink5_Click(object sender, EventArgs e)
        {

        }

        private void buttonXDrink1_MouseLeave(object sender, EventArgs e)
        {
        }
    }
}
