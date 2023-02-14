using Fusion_Bartender_1._0._0;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fusion_Bartender_1._0
{
    [XmlRoot("Log Listing")]
    public class Logs
    {
        [XmlElement]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [XmlElement]
        public String FileName { get { return CreatedDate.Day.ToString() + CreatedDate.Month.ToString() + CreatedDate.Year.ToString() + "_Log.xml"; } set { } }

        [XmlElement]
        public List<Entry> LogEnteries { get; set; } = new List<Entry>();

        public void NewEntry(DateTime time, String msg, String tag)
        {
            Entry entry = new Entry();
            entry.LogTime = time;
            entry.Tag = tag;
            entry.Message = msg;

            LogEnteries.Add(entry);
        }

        #region Saving/Loading
        public Boolean Save(FormMainScreen frmMain)
        {
            Boolean bRetVal = false;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Logs));
                FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Logs", FileName), FileMode.Create);
                serializer.Serialize(fs, this);
                fs.Close();
                bRetVal = true;
            }
            catch (Exception ex)
            {
                frmMain.Logs.NewEntry(DateTime.Now, "Could NOT save Settings.xml - " + ex.Message, "ERROR");
            }
            return bRetVal;
        }

        public Logs Load(FormMainScreen frmMain, string FileLoc)
        {
            Logs oRetVal = null;
            // Serialize the order to a file.
            try
            {
                String FileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString("#0000") + "_Log.xml";
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                FileStream fs = new FileStream(FileLoc, FileMode.Open);
                oRetVal = (Logs)serializer.Deserialize(fs);
                fs.Close();
                frmMain.Logs.NewEntry(DateTime.Now, "Log File Loaded Successfully. File Name: " + FileName, "Startup");
            }
            catch (Exception ex)
            {
                frmMain.Logs.NewEntry(DateTime.Now, "ERROR: Could not read Log File - }" + ex.Message, "ERROR");
                oRetVal = null;
                throw ex;
            }
            return oRetVal;
        }
        #endregion
    }

    [XmlRoot("Log Entry")]
    public class Entry
    {
        [XmlElement]
        public DateTime LogTime { get; set; } = DateTime.Now;

        [XmlElement]
        public String Tag { get; set; } = "";

        [XmlElement]
        public String Message { get; set; } = "";
    }
}
