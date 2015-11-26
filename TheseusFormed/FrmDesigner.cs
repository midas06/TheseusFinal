﻿using System;
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
    public partial class FrmDesigner : Form
    {
        LevelBuilder builder;
        FileHandler filer;
        PictureBox pbTheseus, pbMinotaur, pbExit, pbSelectedTile;
        string mediaPath;
        Point point;
        FrmContainer parentForm;
        bool mapSaved;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                FrmDesigner_KeyDown(this, new KeyEventArgs(keyData));
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public FrmDesigner()
        {
            InitializeComponent();
            this.KeyPreview = true;

            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += control_PreviewKeyDown;
            }
        }
        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        public void SetParentForm(FrmContainer newPF)
        {
            parentForm = newPF;
            parentForm.SetDesignerBackButton();
        }

        public Size GetSize()
        {
            return this.Size;
        }

        public void Init(Point dimensions)
        {
            builder = new LevelBuilder();
            builder.Init(dimensions);
            //Painter.SetMediaPath(@"H:\2015\semester 2\PR 283 C#\Theseus\TheseusFormed - Test\media");
            mediaPath = Painter.SetMediaPath();
            //SetPanelSize();
            Draw();
        }

        public void LoadUserMap(AMap theMap)
        {
            builder = new LevelBuilder();
            mediaPath = Painter.SetMediaPath();
            builder.LoadUserMap(theMap);
            
            tbxMapName.Text = theMap.Name;
            //Painter.SetPanelSize(
            //SetPanelSize();
            Draw();
            //RenderCharacters();
        }

       

        public void Draw()
        {
            Painter.SetPanelSize(pnlBackground, this, builder.GetTiles(), 1);
            SetButtonLocations();
            RenderCharacters();
            mapSaved = false;
        }

        

        public void SetButtonLocations()
        {
            Point newPoint = new Point();

            // nw
            newPoint.X = pnlBackground.Width + 75;
            newPoint.Y = 35;
            btnNW.Location = newPoint;

            // ww
            newPoint.X = pnlBackground.Width + 25;
            newPoint.Y = 65;
            btnWW.Location = newPoint;

            // ew
            newPoint.X = pnlBackground.Width + 125;
            btnEW.Location = newPoint;

            // sw
            newPoint.X = pnlBackground.Width + 75;
            newPoint.Y = 95;
            btnSW.Location = newPoint;

            //min
            newPoint.Y = 145;
            newPoint.X = pnlBackground.Width + 25;
            btnMinotaur.Location = newPoint;

            // the
            newPoint.X = pnlBackground.Width + 125;
            btnTheseus.Location = newPoint;

            // exit
            newPoint.X = pnlBackground.Width + 75;
            newPoint.Y = 200;
            btnExit.Location = newPoint;

            // lbl
            newPoint.X = pnlBackground.Width + 25;
            newPoint.Y = 235;
            lblMapName.Location = newPoint;

            // tbx
            newPoint.Y = newPoint.Y + 20;
            tbxMapName.Location = newPoint;

            // btn
            newPoint.X = pnlBackground.Width + 25;
            newPoint.Y = 300;
            btnOK.Location = newPoint;

            // clear
            newPoint.X = pnlBackground.Width + 130;
            newPoint.Y = 300;
            btnClear.Location = newPoint;

            //lbl
            newPoint.X = pnlBackground.Width + 25;
            newPoint.Y = 375;
            lblWarnings.Text = "";
            lblWarnings.Location = newPoint;
        }


        protected void ShowSelectedTile(Point coordinate)
        {
            Point newPoint = new Point();
            newPoint.X = (coordinate.X * 50) + 5;
            newPoint.Y = (coordinate.Y * 50) + 5;
            if (pbSelectedTile == null)
            {
                pbSelectedTile = new PictureBox();
                pbSelectedTile.Name = "pbSelectedTile";
                pbSelectedTile.Location = newPoint;
                pbSelectedTile.Size = new Size(45, 45);
                pbSelectedTile.BackColor = Color.Honeydew;
                pnlBackground.Controls.Add(pbSelectedTile);
                pbSelectedTile.BringToFront();
            }
            else
            {
                pbSelectedTile.Location = newPoint;
            }
        }


        private void pnlBackground_Click(object sender, EventArgs e)
        {
            if (builder.GetTiles() != null)
            {
                point = pnlBackground.PointToClient(Cursor.Position);
                point.X = point.X / 50;
                point.Y = point.Y / 50;

                if ((point.X < builder.GetTiles().GetLength(0)) && (point.Y < builder.GetTiles().GetLength(1)))
                {
                    builder.SelectTile(point);
                    ShowSelectedTile(point);

                }
                //MessageBox.Show(builder.GetTheTile().Coordinate.ToString());
                pnlBackground.Invalidate();
                Refresh();// DrawMap();
            }

        }

        private void pnlBackground_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            Pen pen = new Pen(Color.Red, 3);
            Brush brush = new SolidBrush(Color.Black);



            foreach (Tile tile in builder.GetTiles())
            {
                Point topLeft, topRight, bottomLeft, bottomRight;
                topLeft = new Point(tile.Coordinate.X * 50, tile.Coordinate.Y * 50);
                topRight = new Point((tile.Coordinate.X + 1) * 50, tile.Coordinate.Y * 50);
                bottomLeft = new Point(tile.Coordinate.X * 50, (tile.Coordinate.Y + 1) * 50);
                bottomRight = new Point((tile.Coordinate.X + 1) * 50, (tile.Coordinate.Y + 1) * 50);

                // add 4 points

                g.FillEllipse(brush, topLeft.X, topLeft.Y, 3, 3);
                g.FillEllipse(brush, topRight.X, topRight.Y, 3, 3);
                g.FillEllipse(brush, bottomLeft.X, bottomLeft.Y, 3, 3);
                g.FillEllipse(brush, bottomRight.X, bottomRight.Y, 3, 3);

                // walls
                if (tile.MyWalls.HasFlag(TheWalls.North))
                {
                    g.DrawLine(pen, topLeft, topRight);
                }
                if (tile.MyWalls.HasFlag(TheWalls.South))
                {
                    g.DrawLine(pen, bottomLeft, bottomRight);
                }
                if (tile.MyWalls.HasFlag(TheWalls.East))
                {
                    g.DrawLine(pen, topRight, bottomRight);
                }
                if (tile.MyWalls.HasFlag(TheWalls.West))
                {
                    g.DrawLine(pen, topLeft, bottomLeft);
                }

                //exit
                
            }

            g.Dispose();
        }

        protected void RenderCharacters()
        {
            pbTheseus = new PictureBox();
            pbTheseus.Name = "pbTheseus";
            pbTheseus.Image = Image.FromFile(mediaPath + @"\images\theseus.png");
            pbTheseus.Size = pbTheseus.Image.Size;
            pnlBackground.Controls.Add(pbTheseus);
            

            if (builder.GetTheseus() != null)
            {
                pbTheseus.Location = Painter.SetLocation(builder.GetTheseus().Coordinate, 50);
            }
            else
            {
                pbTheseus.Location = new Point(-50, 0);
            }

            pbMinotaur = new PictureBox();
            pbMinotaur.Name = "pbMinotaur";
            
            pbMinotaur.Image = Image.FromFile(mediaPath + @"\images\minotaur.png");
            pbMinotaur.Size = pbMinotaur.Image.Size;
            pnlBackground.Controls.Add(pbMinotaur);
            

            if (builder.GetMinotaur() != null)
            {
                pbMinotaur.Location = Painter.SetLocation(builder.GetMinotaur().Coordinate, 50);
            }
            else
            {
                pbMinotaur.Location = new Point(-50, 25);
            }

            pbExit = new PictureBox();
            pbExit.Name = "pbExit";
            pbExit.Image = Image.FromFile(mediaPath + @"\images\exit.gif");
            pbExit.Size = new Size(30, 30);
            pbExit.SizeMode = PictureBoxSizeMode.Zoom;
            pnlBackground.Controls.Add(pbExit);
            

            if (builder.GetExit() != null)
            {
                Point exitPoint = Painter.SetLocation(builder.GetExit().Coordinate, 50);
                exitPoint.X = exitPoint.X + 3;
                exitPoint.Y = exitPoint.Y + 3;
                pbExit.Location = exitPoint;
            }
            else
            {
                pbExit.Location = new Point(-50, 50);
            }

            pbSelectedTile = new PictureBox();
            pbSelectedTile.Name = "pbSelectedTile";
            pbSelectedTile.Location = new Point(-50, 75);
            pbSelectedTile.Size = new Size(45, 45);
            pbSelectedTile.BackColor = Color.Honeydew;
            pnlBackground.Controls.Add(pbSelectedTile);
            pbSelectedTile.BringToFront();

            pbTheseus.BringToFront();
            pbExit.BringToFront();
            pbMinotaur.BringToFront();

        }

        public void ClearTheseus()
        {
            if (builder.GetTheseus() == null && pbTheseus != null)
            {
                pnlBackground.Controls.Remove(pbTheseus);
                pbTheseus = null;
            }
            this.Invalidate();
            Refresh();
        }
        public void ClearMinotaur()
        {
            if (builder.GetMinotaur() == null && pbMinotaur != null)
            {
                pnlBackground.Controls.Remove(pbMinotaur);
                pbMinotaur = null;
            }
            this.Invalidate();
            Refresh();
        }
        public void ClearExit()
        {
            if (builder.GetExit() == null && pbExit != null)
            {
                pnlBackground.Controls.Remove(pbExit);
                pbExit = null;
            }
            this.Invalidate();
            Refresh();
        }
        public void ClearSelected()
        {
            pnlBackground.Controls.Remove(pbSelectedTile);
            pbSelectedTile = null;
            this.Invalidate();
            Refresh();
        }
         


        private void btnNW_Click(object sender, EventArgs e)
        {
            if (point != null)
            {
                builder.NorthWall();
                this.Invalidate();
                Refresh();
            }
           
        }

        private void btnEW_Click(object sender, EventArgs e)
        {
            if (point != null)
            {
                builder.EastWall();
                this.Invalidate();
                Refresh();

            }
            
        }

        private void btnSW_Click(object sender, EventArgs e)
        {
            if (point != null)
            {
                builder.SouthWall();
                this.Invalidate();
                Refresh();
            }
            
        }

        private void btnWW_Click(object sender, EventArgs e)
        {
            if (point != null)
            {
                builder.WestWall();
                this.Invalidate();
                Refresh();
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (point != null)
            {
                if (builder.Exit())
                {
                    Point exitPoint = Painter.SetLocation(builder.GetExit().Coordinate, 50);
                    exitPoint.X = exitPoint.X + 3;
                    exitPoint.Y = exitPoint.Y + 3;
                    pbExit.Location = exitPoint;
                    this.Invalidate();
                    Refresh();
                }
                
                
                
            }

        }

      
        private void btnTheseus_Click(object sender, EventArgs e)
        {
            if (point != null)
            {
                if (builder.SetTheseus())
                {
                    pbTheseus.Location = Painter.SetLocation(builder.GetTheseus().Coordinate, 50);
                    this.Invalidate();
                    Refresh();
                }
            }
        }


       
        private void btnMinotaur_Click(object sender, EventArgs e)
        {
            if (point != null)
            {
                if (builder.SetMinotaur())
                {
                    pbMinotaur.Location = Painter.SetLocation(builder.GetMinotaur().Coordinate, 50);
                    this.Invalidate();
                    Refresh();
                }
                
            }


        }
        protected void SetLabel(string text)
        {
            
            lblWarnings.Text = text;

            //lblWarnings.Location = new Point(x, y);
            lblWarnings.BringToFront();
            this.Invalidate();
            this.Refresh();
            this.Update();
            Application.DoEvents();
        }

        

        protected bool IsValid()
        {
            if (!builder.IsValid())
            {
                //MessageBox.Show("Your map is not valid, you need a Theseus, Minotaur and an Exit");
                SetLabel("Your map is not valid,\nyou need a Theseus, Minotaur and an Exit");
               
                return false;
            }
            if (filer == null)
            {
                filer = new FileHandler();
            }
            filer.Init();

            if (tbxMapName.Text == "")
            {
                //MessageBox.Show("Please enter a name for your map");
                SetLabel("Please enter a name for your map");
                return false;
            }
            if (filer.MapNameUsed(tbxMapName.Text))
            {
                DialogResult dialogResult = MessageBox.Show("This map name is already used. Do you want to overwrite the existing map?", "Overwrite", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    filer.ConfirmOverwrite(true);
                    return true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    filer.ConfirmOverwrite(false);
                    return false;
                }
                
            }
            return true;
        

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure?", "Confirm selection", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    filer.NewUserMap(tbxMapName.Text, builder.Export());
                    SetLabel("Your map has been saved!\nPress: 'N' to create a new map,\n'L' to edit an existing map,\nEnter to return to the main menu");
                    mapSaved = true;
                   
                }
                else if (dialogResult == DialogResult.No)
                {
                    // nothing
                }
            }
        }

        private void FrmDesigner_KeyDown(object sender, KeyEventArgs e)
        {
            Keys theKey = e.KeyCode;
            if (mapSaved == true)
            {
                if (theKey == Keys.N)
                {
                    parentForm.OpenDimensions();
                    this.Hide();
                }
                if (theKey == Keys.L)
                {
                    parentForm.OpenMapListBuilder();
                    this.Hide();
                }
                if (theKey == Keys.Enter)
                {
                    parentForm.Home();
                    this.Hide();
                }

            }
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            builder.Clear();
            ClearTheseus();
            ClearMinotaur();
            ClearExit();
            ClearSelected();
            RenderCharacters();
            this.Invalidate();
            Refresh();
        }
    }
}
