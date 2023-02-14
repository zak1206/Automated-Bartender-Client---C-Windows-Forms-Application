using DevComponents.DotNetBar;
using Fusion_Bartender_1._0.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fusion_Bartender_1._0._0
{
    public partial class FormGlobalRecipes : Form, IUpdate
    {
        private FormMainScreen _frmMain = null;
        private bool Loading = false;
        bool SysOnly = false;
        bool FollowersOnly = false;
        bool ProfileOnly = false;
        MySqlConnection conn = null;
        public FormGlobalRecipes(FormMainScreen frmMain)
        {
            _frmMain = frmMain;
            InitializeComponent();
        }

        public bool ConnectToDB()
        {
            Boolean bRetVal = _frmMain.DB.OpenConnection();

            if (bRetVal)
            {
                labelXDBConnection.Text = "Connected!";
            } else
            {
                labelXDBConnection.Text = "Connection Failed!";
            }

            return bRetVal;
        }

        BackgroundWorker _bgGetRows = new BackgroundWorker();
        public void GetRowsFromMySQLDatabase()
        {
            _bgGetRows.DoWork += _bgGetRows_DoWork;
            _bgGetRows.RunWorkerCompleted += _bgGetRows_RunWorkerCompleted;
            _bgGetRows.RunWorkerAsync();
        }

        private void _bgGetRows_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listBoxAdvGlobalRecipes.Refresh();
        }

        private void _bgGetRows_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = _frmMain.DB.GetAllRecipes();
            if (listBoxAdvGlobalRecipes.Items.Count > 0) listBoxAdvGlobalRecipes.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                Recipe recTag = new Recipe();
                ListBoxItem newItem = new ListBoxItem();

                recTag.RecipeName = row.ItemArray[1].ToString();
                recTag.RecipeDescription = row.ItemArray[2].ToString();
                string[] ingredients = row.ItemArray[3].ToString().Split('/');
                foreach (string ingredient in ingredients)
                {
                    string[] contents = ingredient.Split('*');
                    recTag.AddNewIngredient(int.Parse(contents[1]), double.Parse(contents[2]), contents[0]);
                }
                recTag.CreatedBy = row.ItemArray[4].ToString();
                recTag.CreationDate = DateTime.Parse(row.ItemArray[5].ToString());
                recTag.Downloads = int.Parse(row.ItemArray[6].ToString());
                recTag.Like = int.Parse(row.ItemArray[7].ToString());
                recTag.Dislikes = int.Parse(row.ItemArray[8].ToString());
                recTag.FollowersGained = int.Parse(row.ItemArray[9].ToString());
                recTag.IsBlocked = int.Parse(row.ItemArray[10].ToString());

                newItem.Tag = recTag;
                newItem.Text = recTag.RecipeName + " - " + row.ItemArray[4].ToString();
                listBoxAdvGlobalRecipes.Items.Add(newItem);
            }
        }

        public void UpdateStatus()
        {
            if (Visible)
            {
                labelXDBConnection.Text = _frmMain.DB.Connected ? "Connected!" : "Not Connected!";
            }
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            if (ConnectToDB()) labelXDBConnection.Text = "Connected!";
            GetRowsFromMySQLDatabase();
        }

        private void ribbonClientPanelMain_Click(object sender, EventArgs e)
        {

        }

        private void listBoxAdvGlobalRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAdvGlobalRecipes.SelectedIndex != -1)
            {
                int ingredientCount = 0;
                ListBoxItem selected = listBoxAdvGlobalRecipes.SelectedItems[0];
                Recipe rec = (Recipe)selected.Tag;

                labelXRecipeName.Text = rec.RecipeName;
                labelXDescription.Text = rec.RecipeDescription;
                labelXIngredients.Text = "";
                foreach (Drink drk in rec.Ingredients)
                {
                    ingredientCount++;
                    labelXIngredients.Text += $"◘\tIngredient {ingredientCount} - {drk.LiquidName} ({drk.AmountInFlOz} Fl Oz.)\n";
                }
                ingredientCount = 0;
                labelXDislikes.Text = "Dislikes: " + rec.Dislikes.ToString();
                labelXLikes.Text = "Likes: " + rec.Like.ToString();
                labelXDLs.Text = "Downloads: " + rec.Downloads.ToString();
            }
        }

        private void buttonXDLRecipe_Click(object sender, EventArgs e)
        {
            if (listBoxAdvGlobalRecipes.SelectedIndex != -1)
            {
                Recipe rec = (Recipe)listBoxAdvGlobalRecipes.SelectedItems[0].Tag;
                string recNAme = Path.Combine(Directory.GetCurrentDirectory(), "Recipes", $"{rec.RecipeName.Replace(' ', '_')}_Downloaded.xml");
                if (!File.Exists(recNAme)) rec.Save(_frmMain, $"{recNAme}");
            }
        }

        private void buttonXLike_Click(object sender, EventArgs e)
        {

        }
    }
}
