using DevComponents.DotNetBar;
using Fusion_Bartender_1._0.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fusion_Bartender_1._0._0
{
    public partial class FormHome : Form, IUpdate
    {
        private FormMainScreen _frmMain = null;
        public Recipe _selectedRecipe = null;

        public int pump1time = -1;
        public int pump2time = -1;
        public int pump3time = -1;
        public int pump4time = -1;
        public int pump5time = -1;
        public int pump6time = -1;
        public int pump7time = -1;
        public int pump8time = -1;
        public FormHome(FormMainScreen frmMain)
        {
            _frmMain = frmMain;
            InitializeComponent();
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            LoadRecipes();
        }

        public void LoadRecipes() 
        {
            listBoxAdv1.Items.Clear();

            double totalTime = 0;
            ListBoxItem item = new ListBoxItem();

            foreach (Recipe rec in _frmMain.Recipes)
            {
                item = new ListBoxItem();

                foreach (Drink drink in rec.Ingredients)
                {
                    totalTime += drink.DispenseTime;
                }

                item.Text = rec.RecipeName;
                //item.Image = Image.FromFile(rec.PathToIcon);
                item.Tag = rec;

                if (rec.RecipeName != "")
                {
                    listBoxAdv1.Items.Add(item);
                }
            }
        }

        public void DeleteRecipe(string recipeName)
        {
            //Delete The Recipe File From Recipes Directory
            File.Delete(Path.Combine(_frmMain.PathToRecipes, recipeName + ".xml"));

            //Remove Recipe From Recipes List
            foreach (Recipe recipe in _frmMain.Recipes)
            {
                if (recipe.RecipeName == recipeName)
                {
                    _frmMain.Recipes.Remove(recipe);
                    break;
                }
            }

            LoadRecipes();
        }

        public void UpdateStatus()
        {
            if (Visible)
            {
                if (pump1time > 0 && _frmMain.Arduino.Pump1Active)
                {
                    pump1time -= 49;
                } else if (pump1time == 0 && _frmMain.Arduino.RecipeComplete)
                {
                    pump1time = -1;
                }
                if (pump2time > 0 && _frmMain.Arduino.Pump2Active)
                {
                    pump2time -= 49;
                }
                else if (pump2time == 0 && _frmMain.Arduino.RecipeComplete)
                {
                    pump2time = -1;
                }
                if (pump3time > 0 && _frmMain.Arduino.Pump3Active)
                {
                    pump3time -= 49;
                }
                else if (pump3time == 0 && _frmMain.Arduino.RecipeComplete)
                {
                    pump3time = -1;
                }
                if (pump4time > 0 && _frmMain.Arduino.Pump4Active)
                {
                    pump4time -= 49;
                }
                else if (pump4time == 0 && _frmMain.Arduino.RecipeComplete)
                {
                    pump4time = -1;
                }
                if (pump5time > 0 && _frmMain.Arduino.Pump5Active)
                {
                    pump5time -= 49;
                }
                else if (pump5time == 0 && _frmMain.Arduino.RecipeComplete)
                {
                    pump5time = -1;
                }
                if (pump6time > 0 && _frmMain.Arduino.Pump6Active)
                {
                    pump6time -= 49;
                }
                else if (pump6time == 0 && _frmMain.Arduino.RecipeComplete)
                {
                    pump6time = -1;
                }
                if (pump7time > 0 && _frmMain.Arduino.Pump7Active)
                {
                    pump7time -= 49;
                }
                else if (pump7time == 0 && _frmMain.Arduino.RecipeComplete)
                {
                    pump7time = -1;
                }
                if (pump8time > 0 && _frmMain.Arduino.Pump8Active)
                {
                    pump8time -= 49;
                }
                else if (pump8time == 0 && _frmMain.Arduino.RecipeComplete)
                {
                    pump8time = -1;
                }
                if (listBoxAdv1.SelectedIndex != -1)
                    _selectedRecipe = (Recipe)((ListBoxItem)listBoxAdv1.SelectedItem).Tag;
                buttonXDelete.Visible = listBoxAdv1.SelectedIndex != -1;
                buttonXDelete.Enabled = !gpRunning.Visible;
                buttonXEdit.Visible = listBoxAdv1.SelectedIndex != -1;
                buttonXEdit.Enabled = !gpRunning.Visible;
                buttonXMakeDrink.Visible = listBoxAdv1.SelectedIndex != -1;
                buttonXMakeDrink.Enabled = !gpRunning.Visible;
                labelXDesc.Visible = listBoxAdv1.SelectedIndex != -1;
                labelXIngredients.Visible = listBoxAdv1.SelectedIndex != -1;
                labelX2.Visible = listBoxAdv1.SelectedIndex != -1;
                labelX4.Visible = listBoxAdv1.SelectedIndex != -1;
                labelXRecipeMessage.Visible = (_frmMain.Arduino.RecipeRunning && _frmMain.Arduino.RecipeMessage != "") || _frmMain.Arduino.RecipeComplete;
                pictureBoxPump1.Image = _frmMain.Arduino.Pump1Active ? Properties.Resources.pump_active : Properties.Resources.pump_inactive;
                pictureBoxPump2.Image = _frmMain.Arduino.Pump2Active ? Properties.Resources.pump_active : Properties.Resources.pump_inactive;
                pictureBoxPump3.Image = _frmMain.Arduino.Pump3Active ? Properties.Resources.pump_active : Properties.Resources.pump_inactive;
                pictureBoxPump4.Image = _frmMain.Arduino.Pump4Active ? Properties.Resources.pump_active : Properties.Resources.pump_inactive;
                pictureBoxPump5.Image = _frmMain.Arduino.Pump5Active ? Properties.Resources.pump_active : Properties.Resources.pump_inactive;
                pictureBoxPump6.Image = _frmMain.Arduino.Pump6Active ? Properties.Resources.pump_active : Properties.Resources.pump_inactive;
                pictureBoxPump7.Image = _frmMain.Arduino.Pump7Active ? Properties.Resources.pump_active : Properties.Resources.pump_inactive;
                pictureBoxPump8.Image = _frmMain.Arduino.Pump8Active ? Properties.Resources.pump_active : Properties.Resources.pump_inactive;
                gpRunning.Visible = (_frmMain.Arduino.RecipeRunning);
                labelXRecipeMessage.Text = _frmMain.Arduino.RecipeMessage;
                labelXPump1Running.Visible = _frmMain.Arduino.Pump1Active;
                labelXPump1Running.Text = _frmMain.Arduino.RecipePump1Liquid;
                labelXPump2Running.Visible = _frmMain.Arduino.Pump2Active;
                labelXPump2Running.Text = _frmMain.Arduino.RecipePump2Liquid;
                labelXPump3Running.Visible = _frmMain.Arduino.Pump3Active;
                labelXPump3Running.Text = _frmMain.Arduino.RecipePump3Liquid;
                labelXPump4Running.Visible = _frmMain.Arduino.Pump4Active;
                labelXPump4Running.Text = _frmMain.Arduino.RecipePump4Liquid;
                labelXPump5Running.Visible = _frmMain.Arduino.Pump5Active;
                labelXPump5Running.Text = _frmMain.Arduino.RecipePump5Liquid;
                labelXPump6Running.Visible = _frmMain.Arduino.Pump6Active;
                labelXPump6Running.Text = _frmMain.Arduino.RecipePump6Liquid;
                labelXPump7Running.Visible = _frmMain.Arduino.Pump7Active;
                labelXPump7Running.Text = _frmMain.Arduino.RecipePump7Liquid;
                labelXPump8Running.Visible = _frmMain.Arduino.Pump8Active;
                labelXPump8Running.Text = _frmMain.Arduino.RecipePump8Liquid;
                if (labelXPump1Running.Visible) labelXPump1Running.BringToFront();
                if (labelXPump2Running.Visible) labelXPump2Running.BringToFront();
                if (labelXPump3Running.Visible) labelXPump3Running.BringToFront();
                if (labelXPump4Running.Visible) labelXPump4Running.BringToFront();
                if (labelXPump5Running.Visible) labelXPump5Running.BringToFront();
                if (labelXPump6Running.Visible) labelXPump6Running.BringToFront();
                if (labelXPump7Running.Visible) labelXPump7Running.BringToFront();
                if (labelXPump8Running.Visible) labelXPump8Running.BringToFront();

                if (_selectedRecipe != null)
                {
                    labelXDesc.Text = _selectedRecipe.RecipeDescription;
                    List<String> ingreds = new List<string>();
                    foreach (Drink drink in _selectedRecipe.Ingredients)
                    {
                        string newStr = string.Format($"- {drink.AmountInFlOz} fl oz. Of {drink.LiquidName}");
                        ingreds.Add(newStr);
                    }

                    string newww = "";
                    foreach (string str in ingreds)
                    {
                        newww = newww + str + "\n";
                    }

                    labelXIngredients.Text = newww;
                }
            }
        }

        private void buttonXEdit_Click(object sender, EventArgs e)
        {
            _selectedRecipe = (Recipe)((ListBoxItem)listBoxAdv1.SelectedItem).Tag;

            _frmMain.frmCreateRecipe = new FormCreateRecipe(_frmMain);
            if (!_frmMain._allForms.Contains(_frmMain.frmCreateRecipe)) _frmMain._allForms.Add(_frmMain.frmCreateRecipe);
            _frmMain.frmCreateRecipe._selectedRecipe = _selectedRecipe;
            _frmMain.frmCreateRecipe.EditingRecipe = true;
            _frmMain.frmCreateRecipe.LoadRecipe(_selectedRecipe);
            _frmMain.LoadForm(_frmMain.frmCreateRecipe);
        }

        private void buttonXMakeDrink_Click(object sender, EventArgs e)
        {
            _selectedRecipe = (Recipe)((ListBoxItem)listBoxAdv1.SelectedItem).Tag;
            _frmMain.Arduino.SetRecipeParams(_selectedRecipe);
            listBoxAdv1.SelectedIndex = -1;
            listBoxAdv1.Refresh();
        }

        private void listBoxAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAdv1.SelectedItem != null)
                _selectedRecipe = (Recipe)((ListBoxItem)listBoxAdv1.SelectedItem).Tag;
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {

        }

        private void buttonXDelete_Click(object sender, EventArgs e)
        {
            if (_selectedRecipe != null)
            {
                DeleteRecipe(_selectedRecipe.RecipeName);
            }
        }
    }
}
