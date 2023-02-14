using Fusion_Bartender_1._0._0;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fusion_Bartender_1
{
    [XmlRoot("Settings")]
    public class Settings
    {
        #region Properties
        [XmlElement]
        public String FileLocation { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "Settings", "Settings.xml");

        [XmlElement]
        public String ArduinoCOMPort { get; set; }

        [XmlElement]
        public Double SecondsPerFluidOZ { get; set; }

        [XmlElement]
        public Double MaxHumidityAllowed { get; set; }

        [XmlElement]
        public Double MaxInternalTemperatureAllowed { get; set; }

        [XmlElement]
        public int PumpCount { get; set; }

        [XmlElement]
        public List<Pump> Pumps { get; set; }
        #endregion

        #region Functions
        public void ChangePumpAssignment(int PumpID, int newID, String newLiquidName)
        {
            foreach (Pump pump in Pumps)
            {
                if (PumpID == pump.PumpID)
                {
                    pump.PumpID = newID;
                    pump.LiquidName = newLiquidName;
                }
            }
        }

        private void CreatePumpsList(FormMainScreen frmMain, int pumpCount)
        {
            Pumps = new List<Pump>();

            for (int i=0;i <= pumpCount - 1; i++)
            {
                Pump newPump = new Pump();
                newPump.PumpID = i + 1;
                newPump.LiquidName = "";

                Pumps.Add(newPump);
                frmMain.Logs.NewEntry(DateTime.Now, "Pump " + i + ": ID = "+ (i + 1).ToString() + ", Has Been Configured.", "INFO");
            }

            frmMain.Logs.NewEntry(DateTime.Now, "Pumps List Setup For First Time.", "INFO");
        }
        #endregion

        #region Saving/Loading
        public Boolean Save(FormMainScreen frmMain, string sFileName = "Settings.xml")
        {
            Boolean bRetVal = false;
            try
            {
                if (Pumps.Count == 0)
                {
                    CreatePumpsList(frmMain, PumpCount);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                FileStream fs = new FileStream(FileLocation, FileMode.Create);
                serializer.Serialize(fs, this);
                fs.Close();
                bRetVal = true;
                frmMain.Logs.NewEntry(DateTime.Now, "Settings.xml Saved.", "INFO");
            }
            catch (Exception ex)
            {
                frmMain.Logs.NewEntry(DateTime.Now, "Could NOT save Settings.xml - " + ex.Message, "ERROR");
            }
            return bRetVal;
        }

        internal Settings Load(FormMainScreen frmMain)
        {
            Settings oRetVal = null;
            // Serialize the order to a file.
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                FileStream fs = new FileStream(FileLocation, FileMode.Open);
                oRetVal = (Settings)serializer.Deserialize(fs);
                fs.Close();
                frmMain.Logs.NewEntry(DateTime.Now, "Settings File Loaded Successfully.", "Startup");
            }
            catch (Exception ex)
            {
                frmMain.Logs.NewEntry(DateTime.Now, "ERROR: Could not read Settings File - }" + ex.Message, "ERROR");
                oRetVal = null;
                throw ex;
            }
            return oRetVal;
        }
        #endregion
    }

    public class Pump
    {
        public int PumpID { get; set; } = 1;
        public String LiquidName { get; set; } = "";
    }
}
