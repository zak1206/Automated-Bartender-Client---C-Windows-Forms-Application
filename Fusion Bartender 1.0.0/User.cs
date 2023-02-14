using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fusion_Bartender_1._0._0
{
    public class User
    {
        public String EMailAddress { get; set; } = "";
        public String UserName { get; set; } = "";
        public String Password { get; set; } = "";
        public String Rights { get; set; } = "Bartender";
        public List<Recipe> SharedRecipes { get; set; } = new List<Recipe>();
        [XmlElement("Shared_Recipes")] public List<string> BanList { get; set; } = new List<string>(); //[1:User Banned + Ban Properties + Duration + Reason  |  2: Current DateTime]
        public Boolean ShareBanned { get; set; } = false;
        public String Locked { get; set; } = "None";
        public DateTime LockedUntil { get; set; } = DateTime.Now;
        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;
        public int LikesToday { get; set; } = 0;
        public int DislikesToday { get; set; } = 0;
        public int Followers { get; set; } = 0;
        public int DownloadCount { get; set; } = 0;
        public Boolean LikeBanned { get; set; } = false;
        public DateTime LikeBannedUntil { get; set; }
        public Boolean IsLoggedIn { get; set; } = false;
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public Boolean AutoLogin { get; set; } = false;

        #region Saving/Loading
        public Boolean Save(FormMainScreen frmMain, string sFileName = "")
        {
            if (sFileName == "")
                sFileName = UserName + ".xml";

            Boolean bRetVal = false;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));
                FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Users", sFileName), FileMode.Create);
                serializer.Serialize(fs, this);
                fs.Close();
                bRetVal = true;
                frmMain.Logs.NewEntry(DateTime.Now, UserName + ".xml Saved.", "USERS");
            }
            catch (Exception ex)
            {
                frmMain.Logs.NewEntry(DateTime.Now, "Could NOT save " + UserName + ".xml - " + ex.Message, "ERROR");
            }
            return bRetVal;
        }

        internal User Load(FormMainScreen frmMain, string sPathName = "")
        {
            if (sPathName == "")
                sPathName = Path.Combine(Directory.GetCurrentDirectory(), "Users", "ZRowt.xml");

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Users")))
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Users"));

            User oRetVal = null;
            // Serialize the order to a file.
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));
                FileStream fs = new FileStream(sPathName, FileMode.Open);
                oRetVal = (User)serializer.Deserialize(fs);
                fs.Close();
                string[] splitted = sPathName.Split('\\');
                frmMain.Logs.NewEntry(DateTime.Now, "User File Loaded Successfully. File: " + splitted[splitted.Length - 1], "USERS");
            }
            catch (Exception ex)
            {
                frmMain.Logs.NewEntry(DateTime.Now, "ERROR: Could not read User File - }" + ex.Message, "ERROR");
                oRetVal = null;
                throw ex;
            }
            return oRetVal;
        }
        #endregion
    }
}
