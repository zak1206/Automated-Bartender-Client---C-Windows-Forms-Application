
namespace Fusion_Bartender_1._0._0
{
    partial class FormHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxAdv1 = new DevComponents.DotNetBar.ListBoxAdv();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonXEdit = new DevComponents.DotNetBar.ButtonX();
            this.buttonXDelete = new DevComponents.DotNetBar.ButtonX();
            this.buttonXMakeDrink = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelXDesc = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelXIngredients = new DevComponents.DotNetBar.LabelX();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.ribbonClientPanelMain = new DevComponents.DotNetBar.Ribbon.RibbonClientPanel();
            this.labelXRecipeMessage = new DevComponents.DotNetBar.LabelX();
            this.gpRunning = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelXPump8Running = new DevComponents.DotNetBar.LabelX();
            this.labelXPump7Running = new DevComponents.DotNetBar.LabelX();
            this.labelXPump6Running = new DevComponents.DotNetBar.LabelX();
            this.labelXPump5Running = new DevComponents.DotNetBar.LabelX();
            this.labelXPump4Running = new DevComponents.DotNetBar.LabelX();
            this.labelXPump3Running = new DevComponents.DotNetBar.LabelX();
            this.labelXPump2Running = new DevComponents.DotNetBar.LabelX();
            this.labelXPump1Running = new DevComponents.DotNetBar.LabelX();
            this.pictureBoxPump8 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPump7 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPump6 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPump5 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPump4 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPump3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPump2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPump1 = new System.Windows.Forms.PictureBox();
            this.ribbonClientPanelMain.SuspendLayout();
            this.gpRunning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxAdv1
            // 
            this.listBoxAdv1.AutoScroll = true;
            // 
            // 
            // 
            this.listBoxAdv1.BackgroundStyle.Class = "ListBoxAdv";
            this.listBoxAdv1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listBoxAdv1.ContainerControlProcessDialogKey = true;
            this.listBoxAdv1.DragDropSupport = true;
            this.listBoxAdv1.Font = new System.Drawing.Font("MV Boli", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxAdv1.ItemHeight = 35;
            this.listBoxAdv1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listBoxAdv1.Location = new System.Drawing.Point(12, 47);
            this.listBoxAdv1.Name = "listBoxAdv1";
            this.listBoxAdv1.Size = new System.Drawing.Size(516, 449);
            this.listBoxAdv1.TabIndex = 0;
            this.listBoxAdv1.Text = "listBoxAdv1";
            this.listBoxAdv1.SelectedIndexChanged += new System.EventHandler(this.listBoxAdv1_SelectedIndexChanged);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX1.Location = new System.Drawing.Point(14, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(147, 41);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "Recipes:";
            // 
            // buttonXEdit
            // 
            this.buttonXEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXEdit.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonXEdit.Location = new System.Drawing.Point(12, 555);
            this.buttonXEdit.Name = "buttonXEdit";
            this.buttonXEdit.Size = new System.Drawing.Size(180, 50);
            this.buttonXEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.buttonXEdit.TabIndex = 2;
            this.buttonXEdit.Text = "Edit Recipe";
            this.buttonXEdit.Visible = false;
            this.buttonXEdit.Click += new System.EventHandler(this.buttonXEdit_Click);
            // 
            // buttonXDelete
            // 
            this.buttonXDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXDelete.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonXDelete.Location = new System.Drawing.Point(348, 555);
            this.buttonXDelete.Name = "buttonXDelete";
            this.buttonXDelete.Size = new System.Drawing.Size(180, 50);
            this.buttonXDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.buttonXDelete.TabIndex = 3;
            this.buttonXDelete.Text = "Delete Recipe";
            this.buttonXDelete.Visible = false;
            this.buttonXDelete.Click += new System.EventHandler(this.buttonXDelete_Click);
            // 
            // buttonXMakeDrink
            // 
            this.buttonXMakeDrink.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXMakeDrink.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXMakeDrink.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonXMakeDrink.Location = new System.Drawing.Point(581, 555);
            this.buttonXMakeDrink.Name = "buttonXMakeDrink";
            this.buttonXMakeDrink.Size = new System.Drawing.Size(419, 50);
            this.buttonXMakeDrink.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.buttonXMakeDrink.TabIndex = 4;
            this.buttonXMakeDrink.Text = "Make Drink";
            this.buttonXMakeDrink.Visible = false;
            this.buttonXMakeDrink.Click += new System.EventHandler(this.buttonXMakeDrink_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("MV Boli", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX2.Location = new System.Drawing.Point(538, -2);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(188, 52);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "Description:";
            // 
            // labelXDesc
            // 
            this.labelXDesc.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXDesc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXDesc.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXDesc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXDesc.Location = new System.Drawing.Point(581, 46);
            this.labelXDesc.Name = "labelXDesc";
            this.labelXDesc.Size = new System.Drawing.Size(419, 123);
            this.labelXDesc.TabIndex = 6;
            this.labelXDesc.Text = "Margarita: 4 fl oz. Tequilla \r\nand 4 fl oz. Margarita Mix \r\nwith 4 Ice Cubes!";
            this.labelXDesc.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("MV Boli", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX4.Location = new System.Drawing.Point(538, 227);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(188, 52);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "Ingredients:";
            // 
            // labelXIngredients
            // 
            this.labelXIngredients.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXIngredients.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXIngredients.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXIngredients.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXIngredients.Location = new System.Drawing.Point(581, 280);
            this.labelXIngredients.Name = "labelXIngredients";
            this.labelXIngredients.Size = new System.Drawing.Size(419, 225);
            this.labelXIngredients.TabIndex = 8;
            this.labelXIngredients.Text = "- 2 fl oz. Tequilla\r\n- 4 fl oz. Margarita Mix";
            this.labelXIngredients.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelXIngredients.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 50;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // ribbonClientPanelMain
            // 
            this.ribbonClientPanelMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.ribbonClientPanelMain.Controls.Add(this.buttonXMakeDrink);
            this.ribbonClientPanelMain.Controls.Add(this.labelXIngredients);
            this.ribbonClientPanelMain.Controls.Add(this.buttonXDelete);
            this.ribbonClientPanelMain.Controls.Add(this.labelX1);
            this.ribbonClientPanelMain.Controls.Add(this.labelX4);
            this.ribbonClientPanelMain.Controls.Add(this.labelX2);
            this.ribbonClientPanelMain.Controls.Add(this.labelXDesc);
            this.ribbonClientPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonClientPanelMain.Location = new System.Drawing.Point(0, 0);
            this.ribbonClientPanelMain.Name = "ribbonClientPanelMain";
            this.ribbonClientPanelMain.Size = new System.Drawing.Size(1051, 617);
            // 
            // 
            // 
            this.ribbonClientPanelMain.Style.Class = "RibbonClientPanel";
            this.ribbonClientPanelMain.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonClientPanelMain.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonClientPanelMain.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonClientPanelMain.TabIndex = 9;
            // 
            // labelXRecipeMessage
            // 
            this.labelXRecipeMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            // 
            // 
            // 
            this.labelXRecipeMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXRecipeMessage.Font = new System.Drawing.Font("MV Boli", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXRecipeMessage.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXRecipeMessage.Location = new System.Drawing.Point(-6, 142);
            this.labelXRecipeMessage.Name = "labelXRecipeMessage";
            this.labelXRecipeMessage.Size = new System.Drawing.Size(598, 37);
            this.labelXRecipeMessage.TabIndex = 9;
            this.labelXRecipeMessage.Text = "Recipe Not Running!";
            this.labelXRecipeMessage.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelXRecipeMessage.Visible = false;
            // 
            // gpRunning
            // 
            this.gpRunning.BackColor = System.Drawing.Color.Transparent;
            this.gpRunning.CanvasColor = System.Drawing.Color.Transparent;
            this.gpRunning.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpRunning.Controls.Add(this.labelXPump8Running);
            this.gpRunning.Controls.Add(this.labelXPump7Running);
            this.gpRunning.Controls.Add(this.labelXPump6Running);
            this.gpRunning.Controls.Add(this.labelXPump5Running);
            this.gpRunning.Controls.Add(this.labelXPump4Running);
            this.gpRunning.Controls.Add(this.labelXPump3Running);
            this.gpRunning.Controls.Add(this.labelXPump2Running);
            this.gpRunning.Controls.Add(this.labelXPump1Running);
            this.gpRunning.Controls.Add(this.pictureBoxPump8);
            this.gpRunning.Controls.Add(this.pictureBoxPump7);
            this.gpRunning.Controls.Add(this.pictureBoxPump6);
            this.gpRunning.Controls.Add(this.pictureBoxPump5);
            this.gpRunning.Controls.Add(this.pictureBoxPump4);
            this.gpRunning.Controls.Add(this.pictureBoxPump3);
            this.gpRunning.Controls.Add(this.pictureBoxPump2);
            this.gpRunning.Controls.Add(this.pictureBoxPump1);
            this.gpRunning.Controls.Add(this.labelXRecipeMessage);
            this.gpRunning.DisabledBackColor = System.Drawing.Color.Empty;
            this.gpRunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpRunning.IsShadowEnabled = true;
            this.gpRunning.Location = new System.Drawing.Point(227, 141);
            this.gpRunning.Name = "gpRunning";
            this.gpRunning.Size = new System.Drawing.Size(596, 339);
            // 
            // 
            // 
            this.gpRunning.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpRunning.Style.BackColorGradientAngle = 90;
            this.gpRunning.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpRunning.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpRunning.Style.BorderBottomWidth = 3;
            this.gpRunning.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemDesignTimeBorder;
            this.gpRunning.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpRunning.Style.BorderLeftWidth = 3;
            this.gpRunning.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpRunning.Style.BorderRightWidth = 3;
            this.gpRunning.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpRunning.Style.BorderTopWidth = 3;
            this.gpRunning.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpRunning.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpRunning.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemDesignTimeBorder;
            this.gpRunning.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpRunning.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpRunning.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpRunning.TabIndex = 20;
            this.gpRunning.Text = "Recipe Running";
            this.gpRunning.Visible = false;
            // 
            // labelXPump8Running
            // 
            this.labelXPump8Running.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXPump8Running.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPump8Running.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXPump8Running.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXPump8Running.Location = new System.Drawing.Point(152, 23);
            this.labelXPump8Running.Name = "labelXPump8Running";
            this.labelXPump8Running.Size = new System.Drawing.Size(282, 28);
            this.labelXPump8Running.TabIndex = 28;
            this.labelXPump8Running.Text = "8";
            this.labelXPump8Running.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelXPump7Running
            // 
            this.labelXPump7Running.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXPump7Running.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPump7Running.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXPump7Running.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXPump7Running.Location = new System.Drawing.Point(152, 23);
            this.labelXPump7Running.Name = "labelXPump7Running";
            this.labelXPump7Running.Size = new System.Drawing.Size(282, 28);
            this.labelXPump7Running.TabIndex = 27;
            this.labelXPump7Running.Text = "7";
            this.labelXPump7Running.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelXPump6Running
            // 
            this.labelXPump6Running.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXPump6Running.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPump6Running.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXPump6Running.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXPump6Running.Location = new System.Drawing.Point(152, 23);
            this.labelXPump6Running.Name = "labelXPump6Running";
            this.labelXPump6Running.Size = new System.Drawing.Size(282, 28);
            this.labelXPump6Running.TabIndex = 26;
            this.labelXPump6Running.Text = "6";
            this.labelXPump6Running.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelXPump5Running
            // 
            this.labelXPump5Running.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXPump5Running.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPump5Running.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXPump5Running.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXPump5Running.Location = new System.Drawing.Point(152, 23);
            this.labelXPump5Running.Name = "labelXPump5Running";
            this.labelXPump5Running.Size = new System.Drawing.Size(282, 28);
            this.labelXPump5Running.TabIndex = 25;
            this.labelXPump5Running.Text = "5";
            this.labelXPump5Running.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelXPump4Running
            // 
            this.labelXPump4Running.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXPump4Running.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPump4Running.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXPump4Running.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXPump4Running.Location = new System.Drawing.Point(152, 23);
            this.labelXPump4Running.Name = "labelXPump4Running";
            this.labelXPump4Running.Size = new System.Drawing.Size(282, 28);
            this.labelXPump4Running.TabIndex = 24;
            this.labelXPump4Running.Text = "4";
            this.labelXPump4Running.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelXPump3Running
            // 
            this.labelXPump3Running.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXPump3Running.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPump3Running.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXPump3Running.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXPump3Running.Location = new System.Drawing.Point(152, 23);
            this.labelXPump3Running.Name = "labelXPump3Running";
            this.labelXPump3Running.Size = new System.Drawing.Size(282, 28);
            this.labelXPump3Running.TabIndex = 23;
            this.labelXPump3Running.Text = "3";
            this.labelXPump3Running.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelXPump2Running
            // 
            this.labelXPump2Running.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXPump2Running.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPump2Running.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXPump2Running.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXPump2Running.Location = new System.Drawing.Point(152, 23);
            this.labelXPump2Running.Name = "labelXPump2Running";
            this.labelXPump2Running.Size = new System.Drawing.Size(282, 28);
            this.labelXPump2Running.TabIndex = 22;
            this.labelXPump2Running.Text = "2";
            this.labelXPump2Running.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelXPump1Running
            // 
            this.labelXPump1Running.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXPump1Running.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPump1Running.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXPump1Running.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXPump1Running.Location = new System.Drawing.Point(152, 23);
            this.labelXPump1Running.Name = "labelXPump1Running";
            this.labelXPump1Running.Size = new System.Drawing.Size(282, 28);
            this.labelXPump1Running.TabIndex = 9;
            this.labelXPump1Running.Text = "1";
            this.labelXPump1Running.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // pictureBoxPump8
            // 
            this.pictureBoxPump8.Image = global::Fusion_Bartender_1._0._0.Properties.Resources.pump_inactive;
            this.pictureBoxPump8.Location = new System.Drawing.Point(421, 190);
            this.pictureBoxPump8.Name = "pictureBoxPump8";
            this.pictureBoxPump8.Size = new System.Drawing.Size(95, 89);
            this.pictureBoxPump8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPump8.TabIndex = 21;
            this.pictureBoxPump8.TabStop = false;
            // 
            // pictureBoxPump7
            // 
            this.pictureBoxPump7.Image = global::Fusion_Bartender_1._0._0.Properties.Resources.pump_inactive;
            this.pictureBoxPump7.Location = new System.Drawing.Point(304, 190);
            this.pictureBoxPump7.Name = "pictureBoxPump7";
            this.pictureBoxPump7.Size = new System.Drawing.Size(95, 89);
            this.pictureBoxPump7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPump7.TabIndex = 20;
            this.pictureBoxPump7.TabStop = false;
            // 
            // pictureBoxPump6
            // 
            this.pictureBoxPump6.Image = global::Fusion_Bartender_1._0._0.Properties.Resources.pump_active;
            this.pictureBoxPump6.Location = new System.Drawing.Point(187, 190);
            this.pictureBoxPump6.Name = "pictureBoxPump6";
            this.pictureBoxPump6.Size = new System.Drawing.Size(95, 89);
            this.pictureBoxPump6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPump6.TabIndex = 19;
            this.pictureBoxPump6.TabStop = false;
            // 
            // pictureBoxPump5
            // 
            this.pictureBoxPump5.Image = global::Fusion_Bartender_1._0._0.Properties.Resources.pump_inactive;
            this.pictureBoxPump5.Location = new System.Drawing.Point(70, 190);
            this.pictureBoxPump5.Name = "pictureBoxPump5";
            this.pictureBoxPump5.Size = new System.Drawing.Size(95, 89);
            this.pictureBoxPump5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPump5.TabIndex = 18;
            this.pictureBoxPump5.TabStop = false;
            // 
            // pictureBoxPump4
            // 
            this.pictureBoxPump4.Image = global::Fusion_Bartender_1._0._0.Properties.Resources.pump_inactive;
            this.pictureBoxPump4.Location = new System.Drawing.Point(421, 45);
            this.pictureBoxPump4.Name = "pictureBoxPump4";
            this.pictureBoxPump4.Size = new System.Drawing.Size(95, 89);
            this.pictureBoxPump4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPump4.TabIndex = 13;
            this.pictureBoxPump4.TabStop = false;
            // 
            // pictureBoxPump3
            // 
            this.pictureBoxPump3.Image = global::Fusion_Bartender_1._0._0.Properties.Resources.pump_inactive;
            this.pictureBoxPump3.Location = new System.Drawing.Point(304, 45);
            this.pictureBoxPump3.Name = "pictureBoxPump3";
            this.pictureBoxPump3.Size = new System.Drawing.Size(95, 89);
            this.pictureBoxPump3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPump3.TabIndex = 12;
            this.pictureBoxPump3.TabStop = false;
            // 
            // pictureBoxPump2
            // 
            this.pictureBoxPump2.Image = global::Fusion_Bartender_1._0._0.Properties.Resources.pump_active;
            this.pictureBoxPump2.Location = new System.Drawing.Point(187, 45);
            this.pictureBoxPump2.Name = "pictureBoxPump2";
            this.pictureBoxPump2.Size = new System.Drawing.Size(95, 89);
            this.pictureBoxPump2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPump2.TabIndex = 11;
            this.pictureBoxPump2.TabStop = false;
            // 
            // pictureBoxPump1
            // 
            this.pictureBoxPump1.Image = global::Fusion_Bartender_1._0._0.Properties.Resources.pump_inactive;
            this.pictureBoxPump1.Location = new System.Drawing.Point(70, 45);
            this.pictureBoxPump1.Name = "pictureBoxPump1";
            this.pictureBoxPump1.Size = new System.Drawing.Size(95, 89);
            this.pictureBoxPump1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPump1.TabIndex = 10;
            this.pictureBoxPump1.TabStop = false;
            // 
            // FormHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 617);
            this.Controls.Add(this.gpRunning);
            this.Controls.Add(this.buttonXEdit);
            this.Controls.Add(this.listBoxAdv1);
            this.Controls.Add(this.ribbonClientPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHome";
            this.Text = "FormHome";
            this.Load += new System.EventHandler(this.FormHome_Load);
            this.ribbonClientPanelMain.ResumeLayout(false);
            this.gpRunning.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPump1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ListBoxAdv listBoxAdv1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonXEdit;
        private DevComponents.DotNetBar.ButtonX buttonXDelete;
        private DevComponents.DotNetBar.ButtonX buttonXMakeDrink;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelXDesc;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelXIngredients;
        public System.Windows.Forms.Timer timerUpdate;
        private DevComponents.DotNetBar.Ribbon.RibbonClientPanel ribbonClientPanelMain;
        private DevComponents.DotNetBar.LabelX labelXRecipeMessage;
        private DevComponents.DotNetBar.Controls.GroupPanel gpRunning;
        private System.Windows.Forms.PictureBox pictureBoxPump1;
        private System.Windows.Forms.PictureBox pictureBoxPump4;
        private System.Windows.Forms.PictureBox pictureBoxPump3;
        private System.Windows.Forms.PictureBox pictureBoxPump2;
        private DevComponents.DotNetBar.LabelX labelXPump8Running;
        private DevComponents.DotNetBar.LabelX labelXPump7Running;
        private DevComponents.DotNetBar.LabelX labelXPump6Running;
        private DevComponents.DotNetBar.LabelX labelXPump5Running;
        private DevComponents.DotNetBar.LabelX labelXPump4Running;
        private DevComponents.DotNetBar.LabelX labelXPump3Running;
        private DevComponents.DotNetBar.LabelX labelXPump2Running;
        private DevComponents.DotNetBar.LabelX labelXPump1Running;
        private System.Windows.Forms.PictureBox pictureBoxPump8;
        private System.Windows.Forms.PictureBox pictureBoxPump7;
        private System.Windows.Forms.PictureBox pictureBoxPump6;
        private System.Windows.Forms.PictureBox pictureBoxPump5;
    }
}