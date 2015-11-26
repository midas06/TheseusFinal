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
using System.IO;


namespace TheseusFormed
{
    public partial class FrmMap : Form
    {

        AMap theMap;
        int currentList;
        string currentMap;
        FileHandler filer;
        Game game;
        string mediaPath;
        PictureBox pbTheseus, pbMinotaur, pbExit;
        Theseus theseus; 
        Minotaur minotaur;
        FrmContainer parentForm;

        //ImageList tileList;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                FrmMap_KeyDown(this, new KeyEventArgs(keyData));
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /*private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode.ToString());
        }*/

        public FrmMap()
        {
            InitializeComponent();
            this.KeyPreview = true;

            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += control_PreviewKeyDown;
            }
        }

        public void SetParentForm(FrmContainer theParent)
        {
            parentForm = theParent;
        }

        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        public void SetMap(AMap newMap)
        {
            theMap = newMap;
            
        }

        public void Init(int theList, string newMap)
        {
            currentMap = newMap;
            currentList = theList;
            filer = new FileHandler();
            game = new Game();
            mediaPath = Painter.SetMediaPath();
            
            filer.Init();
            filer.SetMap(theList, newMap);
            game.SetFiler(filer);
            LoadMap();
        }

        public void LoadMap()
        {
            game.SetMap();
            SetMap(game.GetCurrentMap());

            theseus = theMap.TheTheseus;
            minotaur = theMap.TheMinotaur;
            SetTitle();
            Painter.SetPanelSize(pnlBackground, this, theMap.Tiles, 2);
            
            RenderCharacters();
        }




        private void FrmMap_Load(object sender, EventArgs e)
        {
  
        }

        protected void SetTitle()
        {
            lblTitle.Text = theMap.Name;
            lblTitle.Font = new Font("Georgia", 21);
            //lblTitle.TextAlign = HorizontalAlignment.Center;
        }
        
        private void pnlBackground_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;
            Pen pen = new Pen(Color.Red, 3);
            Brush brush = new SolidBrush(Color.Black);
            


            foreach (Tile tile in theMap.Tiles)
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

                // exit
                if (tile.MyWalls.HasFlag(TheWalls.End))
                {
                    if (pbExit == null)
                    {
                        pbExit = new PictureBox();
                        pbExit.Name = "pbExit";
                        Point exitPoint = Painter.SetLocation(tile.Coordinate, 50);
                        exitPoint.X = exitPoint.X + 5;
                        exitPoint.Y = exitPoint.Y + 5;


                        pbExit.Location = exitPoint;
                        pbExit.Image = Image.FromFile(mediaPath + @"\images\exit.gif");
                        pbExit.Size = new Size(30, 30);
                        pbExit.SizeMode = PictureBoxSizeMode.Zoom;

                        pnlBackground.Controls.Add(pbExit);
                        pbExit.BringToFront();
                    }
                    else
                   {
                        Point exitPoint = Painter.SetLocation(tile.Coordinate, 50);
                        exitPoint.X = exitPoint.X + 5;
                        exitPoint.Y = exitPoint.Y + 5;
                        pbExit.Location = exitPoint;
                    }
                    
                }
            }

            g.Dispose();
        }


        protected void RenderCharacters()
        {
            if (pbTheseus == null)
            {
                pbTheseus = new PictureBox();
                pbTheseus.Name = "pbTheseus";
                pbTheseus.Location = Painter.SetLocation(theseus.Coordinate, 50);
                pbTheseus.Image = Image.FromFile(mediaPath + @"\images\theseus.png");
                pbTheseus.Size = pbTheseus.Image.Size;
            }
            else
            {
                pbTheseus.Location = Painter.SetLocation(theseus.Coordinate, 50);
            }

            if (pbMinotaur == null)
            {
                pbMinotaur = new PictureBox();
                pbMinotaur.Name = "pbMinotaur";
                pbMinotaur.Location = Painter.SetLocation(minotaur.Coordinate, 50);
                pbMinotaur.Image = Image.FromFile(mediaPath + @"\images\minotaur.png");
                pbMinotaur.Size = pbMinotaur.Image.Size;
            }
            else
            {
                pbMinotaur.Location = Painter.SetLocation(minotaur.Coordinate, 50);
            }

            SetLabel("Use arrow keys to move Theseus, Enter to delay");


            pnlBackground.Controls.Add(pbTheseus);
            pnlBackground.Controls.Add(pbMinotaur);
            pbTheseus.BringToFront();
            pbMinotaur.BringToFront();
        }
        protected void ClearCharacters()
        {
            pnlBackground.Controls.Remove(pbTheseus);
            pnlBackground.Controls.Remove(pbMinotaur);
        }
        protected void UpdateLocation()
        {
            if (!game.IsGameOver())
            {
                pbTheseus.Location = Painter.SetLocation(theseus.Coordinate, 50);
                pbMinotaur.Location = Painter.SetLocation(minotaur.Coordinate, 50);
                pbTheseus.BringToFront();
                pbMinotaur.BringToFront();
            }
            else
            {
                if (theseus.IsFinished())
                {
                    pbExit.Location = new Point(-30, 30);
                    this.Invalidate();
                    this.Refresh();
                    pbTheseus.Location = Painter.SetLocation(theseus.Coordinate, 50);
                    if (currentList == 0)
                    {
                        if (filer.IsNextMap())
                        {
                            SetLabel("Too easy.\nPress Spacebar to go to the next level");
                        }
                        else
                        {
                            SetLabel("Congrats! You've finished!\nPress Spacebar to try a user created map, \nor Enter to return to the Main Menu");
                        }
                    }
                    else if (currentList == 1)
                    {
                        SetLabel("Congrats!\nPress Spacebar to try another user created map, \nor Enter to return to the Main Menu");
                    }
                }
                else if (minotaur.HasEaten())
                {
                    pbMinotaur.Location = Painter.SetLocation(minotaur.Coordinate, 50);
                    pbTheseus.Location = new Point(-300, 30);
                    this.Invalidate();
                    this.Refresh();
                    SetLabel("Unfortunate, friend.\nPress 'R' to try again,\nSpacebar to load another map,\nor Enter to return to the Main Menu");
                }
                                    
            }
        }

        protected void SetLabel(string text)
        {
            int x, y;
            x = 5;
            y = pnlBackground.Height + 20;

            lblEndLevel.Text = text;

            lblEndLevel.Location = new Point(x, y);
            lblEndLevel.BringToFront();
            this.Invalidate();
            this.Refresh();
            this.Update();
            Application.DoEvents();
        }

        


        private void FrmMap_KeyDown(object sender, KeyEventArgs e)
        {
            Keys theKey = e.KeyCode;

            if (!game.IsGameOver())
            {
                if (theKey == Keys.Up || theKey == Keys.Down || theKey == Keys.Left || theKey == Keys.Right || theKey == Keys.Enter)
                {
                    game.Run(theKey);
                    UpdateLocation();
                }
                
            }
            else if (theseus.IsFinished())
            {
                if (currentList == 0)
                {
                    if (filer.IsNextMap())
                    {
                        if (theKey == Keys.Space)
                        {
                            filer.SetNextMap();
                            LoadMap();
                            this.Refresh();
                            parentForm.SetClientSize(this);
                            currentMap = theMap.Name;
                        }

                    }
                    else
                    {
                        SetLabel("Congrats! You've finished!\nPress Spacebar to try a user created map, or Enter to return to the Main Menu");
                        if (theKey == Keys.Space)
                        {
                            parentForm.Back();
                        }
                        if (theKey == Keys.Enter)
                        {
                            parentForm.Home();
                        }
                    }
                }
                
            }
            else if (minotaur.HasEaten())
            {
                if (theKey == Keys.R)
                {
                    filer.SetMap(currentList, currentMap);
                    LoadMap();
                    this.Refresh();
                    parentForm.SetClientSize(this);
                    currentMap = theMap.Name;
                }
                if (theKey == Keys.Space)
                {
                    parentForm.Back();
                }
                if (theKey == Keys.Enter)
                {
                    parentForm.Home();
                }
            }
            
        }
        

    }
}
