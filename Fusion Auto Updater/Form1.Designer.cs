
namespace Fusion_Auto_Updater
{
    partial class Form1
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
            this.pgbXMain = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.labelXTitle = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // pgbXMain
            // 
            // 
            // 
            // 
            this.pgbXMain.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pgbXMain.Location = new System.Drawing.Point(0, 30);
            this.pgbXMain.Name = "pgbXMain";
            this.pgbXMain.Size = new System.Drawing.Size(766, 50);
            this.pgbXMain.TabIndex = 0;
            this.pgbXMain.Text = "progressBarX1";
            // 
            // labelXTitle
            // 
            this.labelXTitle.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelXTitle.Location = new System.Drawing.Point(0, -4);
            this.labelXTitle.Name = "labelXTitle";
            this.labelXTitle.Size = new System.Drawing.Size(766, 35);
            this.labelXTitle.TabIndex = 1;
            this.labelXTitle.Text = "Loading...";
            this.labelXTitle.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 80);
            this.Controls.Add(this.pgbXMain);
            this.Controls.Add(this.labelXTitle);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ProgressBarX pgbXMain;
        private DevComponents.DotNetBar.LabelX labelXTitle;
    }
}

