using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fusion_Bartender_1.DB
{
    public class MySQL
    {
        private FormMainScreen frmMain = null;
        private static string server = "tcp:fusionbartender.database.windows.net,1433";
        private string database = "dbGlobalRecipes";
        private string uid = "zak";
        private string password = "Rags120694!";
        private string connectionString = _0._0.Properties.Settings.Default.dbFusionBartenderConnectionString;
        public MySqlConnection connection = null;

        public Boolean Connected { get; set; } = false;

        public MySQL(FormMainScreen _frmMain, string Server = "", string Database = "", string UID = "", string Password = "")
        {
            frmMain = _frmMain;

            server = Server != "" ? Server : server;
            database = Database != "" ? Database : database;
            uid = UID != "" ? UID : uid;
            password = Password != "" ? Password : password;
        }

        private bool InitializeDBConnection()
        {
            Boolean bRetVal = false;

            try
            {
                connection = new MySqlConnection(connectionString);
                bRetVal = true;
            } 
            catch (Exception ex)
            {
                bRetVal = false;
            }

            return bRetVal;
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                bool initOK = InitializeDBConnection();
                Connected = initOK;
                if (initOK) connection.Open(); else return false;
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show(frmMain, "Cannot connect to server.  Contact administrator", "Couldn't Connect To Server!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 1045:
                        MessageBox.Show(frmMain, "Cannot connect to server.  Contact administrator", "Invalid Username/Password!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(frmMain, "Exception Message:\n" + ex.Message, "Disconnect Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //Select statement
        public DataTable GetAllRecipes()
        {
            SqlConnection conn = new SqlConnection(_0._0.Properties.Settings.Default.dbFusionBartenderConnectionString);
            SqlCommand cmd = new SqlCommand("select * from GlobalRecipes", conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
    }
}
