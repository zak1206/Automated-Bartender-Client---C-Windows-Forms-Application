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
    [XmlRoot("Recipe")]
    public class Recipe
    {
        [XmlElement]
        public String RecipeName { get; set; }
        [XmlElement]
        public String RecipeDescription { get; set; }
        [XmlElement]
        public List<Drink> Ingredients { get; set; } = new List<Drink>();
        [XmlElement]
        public String CreatedBy { get; set; }
        [XmlElement]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [XmlElement]
        public Boolean HasErrors { get; set; } = false;
        [XmlElement]
        public String ErrorMsg { get; set; }
        [XmlElement]
        public Boolean RecipeRunning { get; set; } = false;
        [XmlElement]
        public int Downloads { get; set; } = 0;
        [XmlElement]
        public int Like { get; set; } = 0;
        [XmlElement]
        public int Dislikes { get; set; } = 0;
        [XmlElement]
        public int IsBlocked { get; set; } = 0;
        [XmlElement]
        public int FollowersGained { get; set; } = 0;
        [XmlElement]
        public Boolean RecipeCompleted { get; set; } = false;
        [XmlElement]
        public String PathToIcon { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "Drink Icons", "Drink_Unavailable.png");

        public void AddNewIngredient(int pumpID, double amtInFlOZ, String liquidName)
        {
            Drink newDrink = new Drink();
            newDrink.PumpID = pumpID;
            newDrink.AmountInFlOz = amtInFlOZ;
            newDrink.LiquidName = liquidName;

            Ingredients.Add(newDrink);
        }

        public bool RecipeHasErrors()
        {
            bool bRetVal = false;

            //Check For Recipe Errors
            if (RecipeName == "")
            {
                ErrorMsg = "Recipe Has No Name.";
                bRetVal = true;
            }
            else if (RecipeDescription == "")
            {
                ErrorMsg = "Recipe Has No Description.";
                bRetVal = true;
            }
            else if (Ingredients.Count == 0)
            {
                ErrorMsg = "Recipe Has No Igredients.";
                bRetVal = true;
            }

            return bRetVal;
        }

        #region Saving/Loading
        public Boolean Save(FormMainScreen frmMain, string fileName)
        {
            Boolean bRetVal = false;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Recipe));
                FileStream fs = new FileStream(fileName, FileMode.Create);
                serializer.Serialize(fs, this);
                fs.Close();
                bRetVal = true;
                frmMain.Logs.NewEntry(DateTime.Now, fileName + ".xml Saved.", "INFO");
            }
            catch (Exception ex)
            {
                frmMain.Logs.NewEntry(DateTime.Now, "Could NOT save " + frmMain.frmHome._selectedRecipe.RecipeName + ".xml - " + ex.Message, "ERROR");
            }
            return bRetVal;
        }

        internal Recipe Load(FormMainScreen frmMain, String fileName)
        {
            Recipe oRetVal = null;
            // Serialize the order to a file.
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Recipe));
                FileStream fs = new FileStream(fileName, FileMode.Open);
                oRetVal = (Recipe)serializer.Deserialize(fs);
                fs.Close();
                frmMain.Logs.NewEntry(DateTime.Now, "Recipe File Loaded Successfully.", "Startup");
            }
            catch (Exception ex)
            {
                frmMain.Logs.NewEntry(DateTime.Now, "ERROR: Could not read Recipe File - }" + ex.Message, "ERROR");
                oRetVal = null;
                throw ex;
            }
            return oRetVal;
        }
        #endregion
    }

    public class Drink
    {
        [XmlElement]
        public int PumpID { get; set; }
        [XmlElement]
        public double AmountInFlOz { get; set; }
        [XmlElement]
        public String LiquidName { get; set; }
        [XmlElement]
        public int DispenseTime { get; set; }
        [XmlElement]
        public Boolean Dispensing { get; set; } = false;
        [XmlElement]
        public Boolean isLiquor { get; set; } = false;
        [XmlElement]
        public Boolean isSoda { get; set; } = false;
        [XmlElement]
        public Boolean isJuice { get; set; } = false;
    }
}
