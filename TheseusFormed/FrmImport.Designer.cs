namespace TheseusFormed
{
    partial class FrmImport
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
            this.btnTestMap = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.tbxPreview = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTestMap
            // 
            this.btnTestMap.Location = new System.Drawing.Point(52, 331);
            this.btnTestMap.Name = "btnTestMap";
            this.btnTestMap.Size = new System.Drawing.Size(106, 54);
            this.btnTestMap.TabIndex = 1;
            this.btnTestMap.Text = "Test Map";
            this.btnTestMap.UseVisualStyleBackColor = true;
            this.btnTestMap.Click += new System.EventHandler(this.btnTestMap_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(52, 144);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(35, 13);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "label1";
            // 
            // tbxPreview
            // 
            this.tbxPreview.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPreview.Location = new System.Drawing.Point(52, 32);
            this.tbxPreview.MaximumSize = new System.Drawing.Size(500, 1000);
            this.tbxPreview.Multiline = true;
            this.tbxPreview.Name = "tbxPreview";
            this.tbxPreview.Size = new System.Drawing.Size(100, 20);
            this.tbxPreview.TabIndex = 4;
            this.tbxPreview.WordWrap = false;
            // 
            // FrmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(537, 457);
            this.Controls.Add(this.tbxPreview);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnTestMap);
            this.Name = "FrmImport";
            this.Text = "FrmImport";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmImport_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTestMap;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox tbxPreview;
    }
}