namespace TheseusFormed
{
    partial class FrmMap
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
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblEndLevel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlBackground
            // 
            this.pnlBackground.Location = new System.Drawing.Point(0, 51);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(500, 500);
            this.pnlBackground.TabIndex = 1;
            this.pnlBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBackground_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(-3, -2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(348, 50);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "label1";
            // 
            // lblEndLevel
            // 
            this.lblEndLevel.AutoSize = true;
            this.lblEndLevel.Font = new System.Drawing.Font("Hoefler Text Regular", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndLevel.Location = new System.Drawing.Point(120, 9);
            this.lblEndLevel.Name = "lblEndLevel";
            this.lblEndLevel.Size = new System.Drawing.Size(273, 23);
            this.lblEndLevel.TabIndex = 0;
            this.lblEndLevel.Text = "Press Spacebar to go to the next level";
            // 
            // FrmMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 562);
            this.Controls.Add(this.lblEndLevel);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlBackground);
            this.KeyPreview = true;
            this.Name = "FrmMap";
            this.Text = "FrmMap";
            this.Load += new System.EventHandler(this.FrmMap_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMap_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblEndLevel;
    }
}