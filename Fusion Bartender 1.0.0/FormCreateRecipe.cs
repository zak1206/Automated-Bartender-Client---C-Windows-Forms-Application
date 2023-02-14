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
    public partial class FormCreateRecipe : Form, IUpdate
    {
        private FormMainScreen _frmMain = null;
        public Recipe _selectedRecipe = null;
        public Drink _selectedDrink = null;
        public bool EditingRecipe = false;
        public bool CreatingRecipe = false;

        public FormCreateRecipe(FormMainScreen frmMain, Recipe selectedRecipe = null)
        {
            _selectedRecipe = selectedRecipe;
            _frmMain = frmMain;
            InitializeComponent();
        }

        public void ResetFields()
        {
            listBoxAdvIcons.Items.Clear();
            treeView1.Nodes[0].Nodes.Clear();
            treeView1.Nodes[1].Nodes.Clear();
            textBoxRecipeName.Text = "";
            textBoxDrinkAmtToUse.Text = "";
            richTextBoxExDEscription.Text = "";
            treeView1.Refresh();
        }

        public bool IsLoaded { get; set; } = false;
        private void FormHome_Load(object sender, EventArgs e)
        {
            //Load Recipe
            if (_selectedRecipe != null)
            {
                EditingRecipe = true;
            } else
            {
                _selectedRecipe = new Recipe();
                _selectedRecipe.Ingredients = new List<Drink>();
                LoadRecipe(_selectedRecipe);
                CreatingRecipe = true;
            }
            treeView1.ShowNodeToolTips = true;
            IsLoaded = true;
        }

        private void LoadIcons()
        {
            //Load Icons
            listBoxAdvIcons.Items.Clear();
            foreach (string file in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Drink Icons")))
            {
                ListBoxItem item = new ListBoxItem();
                string[] splitted = file.Split('\\');
                string fileName = splitted[splitted.Length - 1];

                item.Text = "";
                item.Image = Image.FromFile(file);
                item.Tag = file;

                listBoxAdvIcons.Items.Add(item);
            }

            listBoxAdvIcons.Refresh();
            listBoxAdvIcons.RefreshItems();
        }

        public void LoadRecipe(Recipe recipe)
        {
            textBoxRecipeName.Text = recipe.RecipeName;
            richTextBoxExDEscription.Text = recipe.RecipeDescription;

            foreach (Drink drnk in recipe.Ingredients)
            {
                TreeNode node = new TreeNode();
                node.Text = drnk.LiquidName + " - Amount (Fl. Oz.): " + drnk.AmountInFlOz;
                node.Tag = drnk;
                node.ToolTipText = "Liquid: " + drnk.LiquidName + "\n" + "Amount To Use (Fl. OZ): " + drnk.AmountInFlOz + "\n" + "Pump ID: " + drnk.PumpID;

                if (!treeView1.Nodes[1].Nodes.Contains(node)) treeView1.Nodes[1].Nodes.Add(node);
            }

            foreach (Pump pump in _frmMain.Settings.Pumps)
            {
                TreeNode node = new TreeNode();
                node.Text = pump.LiquidName;
                node.Tag = pump;
                node.ToolTipText = "Liquid: " + pump.LiquidName + "\n" + "Pump ID: " + pump.PumpID;

                if (!treeView1.Nodes[0].Nodes.Contains(node)) treeView1.Nodes[0].Nodes.Add(node);
            }

            LoadIcons();

            foreach (ListBoxItem item in listBoxAdvIcons.Items)
            {
                if (item.Tag.ToString() == recipe.PathToIcon)
                {
                    item.SetIsSelected(true, eEventSource.Mouse);
                    item.CheckState = CheckState.Checked;
                    break;
                }
            }
        }

        private void SaveRecipe(Recipe recipe)
        {
            bool iconSelected = false;

            if (textBoxRecipeName.Text != "")
                recipe.RecipeName = textBoxRecipeName.Text;
            if (richTextBoxExDEscription.Text != "")
                recipe.RecipeDescription = richTextBoxExDEscription.Text;
            if (listBoxAdvIcons.SelectedItem != null)
                iconSelected = true;

            if (recipe.Ingredients == null || recipe.Ingredients.Count == 0)
            {
                recipe.Ingredients = new List<Drink>();
            }
            else
            {
                recipe.Ingredients.Clear();
            }

            foreach (TreeNode node in treeView1.Nodes[1].Nodes)
            {
                Drink drink = (Drink)node.Tag;
                drink.DispenseTime = Convert.ToInt16(((drink.AmountInFlOz * _frmMain.Settings.SecondsPerFluidOZ) * 1000.0));
                if (!recipe.Ingredients.Contains(drink)) recipe.Ingredients.Add(drink);
            }

            if (iconSelected)
                recipe.PathToIcon = ((ListBoxItem)listBoxAdvIcons.SelectedItem).Tag.ToString();

            _selectedRecipe = recipe;
        }

        private void buttonXCreate_Click(object sender, EventArgs e)
        {
            SaveRecipe(_selectedRecipe);
            if (!_selectedRecipe.RecipeHasErrors())
            {
                if (!EditingRecipe) 
                {
                    _frmMain.Recipes.Add(_selectedRecipe);
                    _frmMain.LoadForm(_frmMain.frmHome);
                    _frmMain.frmHome.LoadRecipes();
                    if (_selectedRecipe.Save(_frmMain, Path.Combine(Directory.GetCurrentDirectory(), "Recipes", _selectedRecipe.RecipeName + ".xml")))
                    {
                        MessageBox.Show(this, "Recipe Created Successfully!", "Recipe Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    foreach (Recipe rec in _frmMain.Recipes)
                    {
                        if (rec.RecipeName == _selectedRecipe.RecipeName)
                        {
                            _frmMain.Recipes.Remove(rec);
                            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Recipes", rec.RecipeName + ".xml"));
                            _frmMain.Recipes.Add(_selectedRecipe);
                            if (_selectedRecipe.Save(_frmMain, Path.Combine(Directory.GetCurrentDirectory(), "Recipes", _selectedRecipe.RecipeName + ".xml")))
                            {
                                MessageBox.Show(this, "Recipe Saved Successfully!", "Recipe Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            _frmMain.LoadForm(_frmMain.frmHome);
                            _frmMain.frmHome.LoadRecipes();
                            return;
                        }
                    }
                }

            } else
            {
                MessageBox.Show(this, "Cannot Create Recipe, Recipe Has Errors.\nPlease Correct Errors Before Saving", "Recipe Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetFields();
            IsDirty = false;
        }

        private bool CheckFields()
        {
            bool bRetVal = true;

            if (textBoxRecipeName.Text == "")
                bRetVal &= false;
            if (richTextBoxExDEscription.Text == "")
                bRetVal &= false;

            return bRetVal;
        }

        public void UpdateStatus()
        {
            if (Visible)
            {
                buttonXRemoveDrink.Visible = treeView1.SelectedNode != null && treeView1.SelectedNode.Parent == treeView1.Nodes[1];
                buttonXAddDrink.Visible = treeView1.SelectedNode != null && treeView1.SelectedNode.Parent == treeView1.Nodes[0];
                buttonXCreate.Text = EditingRecipe ? "Save Recipe" : "Create Recipe";
                buttonXCancel.Visible = EditingRecipe;

                if (treeView1.Nodes.Count != 0)
                {
                    treeView1.Nodes[0].Expand();
                    treeView1.Nodes[1].Expand();
                }
            }
        }

        private void buttonXCancel_Click(object sender, EventArgs e)
        {
            _selectedRecipe = null;
            EditingRecipe = false;
            CreatingRecipe = false;

            _frmMain.LoadForm(_frmMain.frmHome);
        }

        private bool loading = false;
        public bool IsDirty { get; set; } = false;
        private void buttonXAddDrink_Click(object sender, EventArgs e)
        {
            Pump pump = (Pump)treeView1.SelectedNode.Tag;
            TreeNode newNode = new TreeNode();
            newNode.Text = pump.LiquidName + " - " + 0 + " Fl. Oz.";

            Drink newDrink = new Drink();
            newDrink.LiquidName = pump.LiquidName;
            newDrink.AmountInFlOz = 0;
            newDrink.PumpID = pump.PumpID;

            newNode.ToolTipText = "Liquid: " + pump.LiquidName + "\n" + "Amount To Use (Fl. OZ): " + newDrink.AmountInFlOz + "\n" + "Pump ID: " + newDrink.PumpID;
            newNode.Tag = newDrink;

            treeView1.Nodes[1].Nodes.Add(newNode);
            treeView1.SelectedNode = null;
            IsDirty = true;
        }

        private void buttonXSaveAmt_Click(object sender, EventArgs e)
        {
            double amtVal = 0;
            if (double.TryParse(textBoxDrinkAmtToUse.Text, out amtVal))
            {
                _selectedRecipe.Ingredients = new List<Drink>();
                _selectedDrink.AmountInFlOz = amtVal;
                foreach (Drink drink in _selectedRecipe.Ingredients)
                {
                    if (drink.LiquidName == _selectedDrink.LiquidName)
                    {
                        _selectedRecipe.Ingredients.Remove(drink);
                        _selectedRecipe.Ingredients.Add(_selectedDrink);
                    }
                }
                gpInputAmount.Visible = false;
            }
            treeView1.SelectedNode.Text = _selectedDrink.LiquidName + " - " + _selectedDrink.AmountInFlOz.ToString() + " Fl Oz.";
            treeView1.SelectedNode.Tag = _selectedDrink;
            treeView1.SelectedNode.ToolTipText = "Liquid: " + _selectedDrink.LiquidName + "\n" + "Amount (Fl. Oz): " + _selectedDrink.AmountInFlOz.ToString() + "\n" + "Pump ID: " + _selectedDrink.PumpID;
            treeView1.Refresh();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode.Parent == treeView1.Nodes[1])
            {
                Drink drink = (Drink)treeView1.SelectedNode.Tag;
                _selectedDrink = drink;
                gpInputAmount.Visible = true;
                textBoxDrinkAmtToUse.Text = drink.AmountInFlOz.ToString();
            }
        }

        private void buttonXCancelAmt_Click(object sender, EventArgs e)
        {
            gpInputAmount.Visible = false;
        }

        private void ribbonClientPanelMain_Click(object sender, EventArgs e)
        {

        }

        private void buttonXRemoveDrink_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent == treeView1.Nodes[1])
            {
                Drink drink = (Drink)treeView1.SelectedNode.Tag;
                treeView1.Nodes.Remove(treeView1.SelectedNode);
                IsDirty = true;
            }

            treeView1.Refresh();
        }

        private void richTextBoxExDEscription_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRecipe.RecipeDescription != richTextBoxExDEscription.Text)
            {
                IsDirty = true;
            }
        }

        private void textBoxRecipeName_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRecipe.RecipeName != textBoxRecipeName.Text)
            {
                IsDirty = true;
            }
        }

        private void listBoxAdvIcons_ItemClick(object sender, EventArgs e)
        {
            if (listBoxAdvIcons.SelectedIndex != -1)
            {
                string pathToIcon = ((ListBoxItem)listBoxAdvIcons.SelectedItem).Tag.ToString();
                if (_selectedRecipe.PathToIcon != pathToIcon)
                    IsDirty = true;
            }
        }
    }
}
