using DevComponents.DotNetBar.Controls;
using Fusion_Bartender_1._0.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fusion_Bartender_1._0._0
{
    public partial class FormLogViewer : Form, IUpdate
    {
        FormMainScreen _frmMain = null;
        String filter = "";
        String filtered = "";
        Logs Logs = null;
        DataGridViewX err = null;
        DataGridViewX warn = null;
        DataGridViewX info = null;
        DataGridViewX startup = null;
        DataGridViewX action = null;
        DataGridViewX arduino = null;
        public FormLogViewer(FormMainScreen frmMain, Logs logs)
        {
            _frmMain = frmMain;
            Logs = logs;
            InitializeComponent();
        }

        private void FormLogViewer_Load(object sender, EventArgs e)
        {
            _frmMain.Arduino.SilenceLogs = true;

            dataGridViewXLogView.Rows.Clear();

            reflectionLabelMenu.Text = String.Format("<b><font size=\" + 24\"><i>{0}</i><font><i></i><b></b></font></font></b>", Logs.CreatedDate.ToString("MMMM dd, yyyy"));
            LoadLog();
            _frmMain.Arduino.SilenceLogs = false;
        }

        private void LoadLog()
        {
            dataGridViewXLogView.Rows.Clear();

            bool errOtherChecked = !checkBoxErrors.Checked && (checkBoxInfo.Checked || checkBoxStartup.Checked || checkBoxWarnings.Checked || checkBoxAction.Checked || checkBoxArduino.Checked);
            bool infoOtherChecked = !checkBoxInfo.Checked && (checkBoxErrors.Checked || checkBoxStartup.Checked || checkBoxWarnings.Checked || checkBoxAction.Checked || checkBoxArduino.Checked);
            bool startupOtherChecked = !checkBoxStartup.Checked && (checkBoxInfo.Checked || checkBoxErrors.Checked || checkBoxWarnings.Checked || checkBoxAction.Checked || checkBoxArduino.Checked); ;
            bool actionOtherChecked = !checkBoxAction.Checked && (checkBoxInfo.Checked || checkBoxStartup.Checked || checkBoxWarnings.Checked || checkBoxErrors.Checked || checkBoxArduino.Checked);
            bool warningOtherChecked = !checkBoxWarnings.Checked && (checkBoxInfo.Checked || checkBoxStartup.Checked || checkBoxErrors.Checked || checkBoxAction.Checked || checkBoxArduino.Checked);
            bool arduinoOtherChecked = !checkBoxArduino.Checked && (checkBoxInfo.Checked || checkBoxStartup.Checked || checkBoxWarnings.Checked || checkBoxAction.Checked || checkBoxErrors.Checked);
            bool noneChecked = !checkBoxArduino.Checked && !checkBoxInfo.Checked && !checkBoxStartup.Checked && !checkBoxWarnings.Checked && !checkBoxAction.Checked && !checkBoxErrors.Checked;

            List<Entry> newlogs = Logs.LogEnteries;
            foreach (Entry log in newlogs)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewXLogView);
                row.Cells[0].Value = log.Tag;
                row.Cells[1].Value = log.LogTime.ToString("hh:mm:ss:ffff");
                row.Cells[2].Value = log.Message;

                if (((row.Cells[0].Value.ToString().Contains("STARTUP") || row.Cells[0].Value.ToString().Contains("Startup")) && !startupOtherChecked) || (!row.Cells[0].Value.ToString().Contains("STARTUP") && noneChecked))
                {
                    row.Cells[0].Style.BackColor = Color.LightBlue;
                    dataGridViewXLogView.Rows.Add(row);
                }
                else if (((row.Cells[0].Value.ToString().Contains("ERROR") || row.Cells[0].Value.ToString().Contains("Error")) && !errOtherChecked) || (!row.Cells[0].Value.ToString().Contains("ERROR") && noneChecked))
                {
                    row.Cells[0].Style.BackColor = Color.Salmon;
                    dataGridViewXLogView.Rows.Add(row);
                }
                else if (((row.Cells[0].Value.ToString().Contains("WARNING") || row.Cells[0].Value.ToString().Contains("Warning")) && !warningOtherChecked) || (!row.Cells[0].Value.ToString().Contains("WARNING") && noneChecked))
                {
                    row.Cells[0].Style.BackColor = Color.LightYellow;
                    dataGridViewXLogView.Rows.Add(row);
                }
                else if (((row.Cells[0].Value.ToString().Contains("ACTION") || row.Cells[0].Value.ToString().Contains("Action")) && !actionOtherChecked) || (!row.Cells[0].Value.ToString().Contains("ACTION") && noneChecked))
                { 
                    row.Cells[0].Style.BackColor = Color.Gainsboro;
                dataGridViewXLogView.Rows.Add(row);
                }                
                else if (((row.Cells[0].Value.ToString().Contains("INFO") || row.Cells[0].Value.ToString().Contains("Info")) && !infoOtherChecked) || (!row.Cells[0].Value.ToString().Contains("INFO") && noneChecked))
                {
                    row.Cells[0].Style.BackColor = Color.DeepSkyBlue;
                    dataGridViewXLogView.Rows.Add(row);
                }
                else if (((row.Cells[0].Value.ToString().Contains("ARDUINO") || row.Cells[0].Value.ToString().Contains("Arduino")) && !arduinoOtherChecked) || (!row.Cells[0].Value.ToString().Contains("ARDUINO") && noneChecked))
                {
                    row.Cells[0].Style.BackColor = Color.LightPink;
                    dataGridViewXLogView.Rows.Add(row);
                }
            }

            dataGridViewXLogView.ClearSelection();
            dataGridViewXLogView.Refresh();
        }

        private void dataGridViewXLogView_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewXLogView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewXLogView.SelectedRows.Count > 0)
            {
                string tag = dataGridViewXLogView.SelectedRows[0].Cells[0].Value.ToString();
                string time = dataGridViewXLogView.SelectedRows[0].Cells[1].Value.ToString();
                string msg = dataGridViewXLogView.SelectedRows[0].Cells[2].Value.ToString();
                MessageBox.Show(this, "Tag: " + tag + "\nTimeStamp: " + time + "\nMessage: " + msg, "Log Message Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void UpdateStatus()
        {
            if (Visible)
            {
                if (dataGridViewXLogView.Rows.Count > 0)
                {

                }
            }
        }

        private void UncheckAll(CheckBox leaveChecked)
        {
            if (leaveChecked != checkBoxStartup) checkBoxStartup.Checked = false;
            if (leaveChecked != checkBoxWarnings) checkBoxWarnings.Checked = false;
            if (leaveChecked != checkBoxInfo) checkBoxInfo.Checked = false;
            if (leaveChecked != checkBoxErrors) checkBoxErrors.Checked = false;
            if (leaveChecked != checkBoxArduino) checkBoxArduino.Checked = false;
            if (leaveChecked != checkBoxAction) checkBoxAction.Checked = false;
        }

        private void checkBoxErrors_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxErrors.Checked)
            {
                UncheckAll(checkBoxErrors);
                LoadLog();
            }
        }

        private void checkBoxWarnings_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWarnings.Checked)
            {
                UncheckAll(checkBoxWarnings);
                LoadLog();
            }
        }

        private void checkBoxStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStartup.Checked)
            {
                UncheckAll(checkBoxStartup);
                LoadLog();
            }
        }

        private void checkBoxAction_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAction.Checked)
            {
                UncheckAll(checkBoxAction);
                LoadLog();
            }
        }

        private void checkBoxInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxArduino.Checked)
            {
                UncheckAll(checkBoxInfo);
                LoadLog();
            }
        }

        private void checkBoxArduino_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxArduino.Checked)
            {
                UncheckAll(checkBoxArduino);
                LoadLog();
            }
        }
    }
}
