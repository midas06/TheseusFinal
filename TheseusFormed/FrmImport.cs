using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheseusCompiled;

namespace TheseusFormed
{
    public partial class FrmImport : Form
    {
        FrmContainer parentForm;
        FileHandler filer;
        string theFile;
        bool mapTested;
        bool mapValid;
        public FrmImport()
        {
            InitializeComponent();
            this.KeyPreview = true;

            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += control_PreviewKeyDown;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                FrmImport_KeyDown(this, new KeyEventArgs(keyData));
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        public void SetParentForm(FrmContainer theParent)
        {
            parentForm = theParent;
        }

        public void SetText(string newText)
        {
            theFile = newText;

            if (filer == null)
            {
                filer = new FileHandler();
                filer.Init();
            }

            mapTested = false;
            mapValid = false;
            tbxPreview.Location = new Point(15, 15);
            tbxPreview.Text = filer.LoadTextFile(theFile);
            //tbxPreview.Font = new Font("Courier", 15);
            tbxPreview.ScrollBars = ScrollBars.Both;
            SetTbxDimensions();           
            tbxPreview.WordWrap = false;
            tbxPreview.ReadOnly = true;

            lblResult.Text = "";
            lblResult.Location = new Point(tbxPreview.Location.X, tbxPreview.Location.Y + tbxPreview.Size.Height + 20);

            btnTestMap.Location = new Point(tbxPreview.Location.X, lblResult.Location.Y + lblResult.Size.Height + 60);

        }

        private void btnTestMap_Click(object sender, EventArgs e)
        {
            if (filer == null)
            {
                filer = new FileHandler();
                filer.Init();
            }

            if (!filer.TestImportedMap(theFile))
            {
                lblResult.Text = "Sorry, the text file was not in one of the correct formats.\nPress 'N' to select a different file to import\nPress Enter to return to the main menu";
                mapTested = true;
            }
            else
            {
                lblResult.Text = "The map was successfully validated.\nPress Spacebar to open the map in the Editor,\nPress 'N' to select a different file to import\nPress Enter to return to the main menu";
                mapTested = true;
                mapValid = true;
            }

        }


        protected void SetTbxDimensions()
        {
            Size size = TextRenderer.MeasureText(tbxPreview.Text, tbxPreview.Font);
            if (size.Height > 1000)
            {
                tbxPreview.Height = 1000;
            }
            else
            {
                tbxPreview.Height = size.Height + 30;
            }
            if (size.Width > 800)
            {
                tbxPreview.Width = 800;
            }
            else if (tbxPreview.Width > 450)
            {
                tbxPreview.Width = size.Width + 20;
            }
            else if (tbxPreview.Width <= 450)
            {
                tbxPreview.Width = 450;
            }

            this.Size = new Size(tbxPreview.Width + 30, tbxPreview.Height + 120);
            

        }

        private void FrmImport_KeyDown(object sender, KeyEventArgs e)
        {
            Keys theKey = e.KeyCode;
            if (mapTested == true)
            {
                if(theKey == Keys.N)
                {
                    parentForm.Back();
                }
                if(theKey == Keys.Enter)
                {
                    parentForm.Home();
                }
                if(mapValid == true)
                {
                    if (theKey == Keys.Space)
                    {
                        parentForm.OpenExistingBuilder(filer.ReturnImportedMap(theFile));
                        //parentForm.SetPreviousForm(parentForm.GetDesigner());
                        //parentForm.SetDesignerBackButton();
                        this.Hide();
                    }
                }
            }
        }
    }
}
