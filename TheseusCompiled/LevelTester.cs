using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheseusCompiled
{
    class LevelTester
    {
        AMap theMap;
        Game game;
        Tile[,] theTiles;
        Theseus theseus;
        List<Tile> checkedTiles, possibleTiles;


        public void SetMap(AMap newMap)
        {
            theMap = newMap;
            theTiles = theMap.Tiles;
            theseus = theMap.TheTheseus;
            checkedTiles = new List<Tile>();
            possibleTiles = new List<Tile>();
            

            //checkedTiles.Add(theTiles[theseus.Coordinate.X, theseus.Coordinate.Y]);
        }

        public List<Point> GetAdjacent()
        {
            List<Point> adjTiles = new List<Point>();
                        
            if (!theseus.IsBlocked(Direction.Up))
            {
                if (!checkedTiles.Contains(ReturnAdjPoint(Direction.Up)))
                {
                    adjTiles.Add(Direction.Up);
                    if (!possibleTiles.Contains(ReturnAdjPoint(Direction.Up)))
                    {
                        possibleTiles.Add(ReturnAdjPoint(Direction.Up));
                    }
                       
                }
                
            }
            if (!theseus.IsBlocked(Direction.Right))
            {
                if (!checkedTiles.Contains(ReturnAdjPoint(Direction.Right)))
                {
                    adjTiles.Add(Direction.Right);
                    if (!possibleTiles.Contains(ReturnAdjPoint(Direction.Right)))
                    {
                        possibleTiles.Add(ReturnAdjPoint(Direction.Right));
                    }
                }
            }
            if (!theseus.IsBlocked(Direction.Left))
            {
                if (!checkedTiles.Contains(ReturnAdjPoint(Direction.Left)))
                {
                    adjTiles.Add(Direction.Left);
                    if (!possibleTiles.Contains(ReturnAdjPoint(Direction.Left)))
                    {
                        possibleTiles.Add(ReturnAdjPoint(Direction.Left));
                    }
                }
            }
            if (!theseus.IsBlocked(Direction.Down))
            {
                if (!checkedTiles.Contains(ReturnAdjPoint(Direction.Down)))
                {
                    adjTiles.Add(Direction.Down);
                    if (!possibleTiles.Contains(ReturnAdjPoint(Direction.Down)))
                    {
                        possibleTiles.Add(ReturnAdjPoint(Direction.Down));
                    }
                }
            }

            return adjTiles;
        }

        protected Tile ReturnAdjPoint(Point direction)
        {
            Point thePoint = theseus.Coordinate;
            thePoint.X = thePoint.X + direction.X;
            thePoint.Y = thePoint.Y + direction.Y;
            return theTiles[thePoint.X, thePoint.Y];
        }


        public bool AdjTilesValid(List<Point> pointList)
        {
            foreach (Point p in pointList)
            {
                try
                {
                    theseus.Move(p);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }


        public void Iterate()
        {
            /*Tile currentTile, possibleTile;
            List<Point> adj;
            currentTile = theTiles[theseus.Coordinate.X, theseus.Coordinate.Y];
            adj = GetAdjacent();

           // while (possibleTiles.Count > 0)
           // {
            checkedTiles.Add(currentTile);
 
            theseus.Coordinate = new Point(t.Coordinate.X, t.Coordinate.Y);
            GetAdjacent();
            possibleTiles.Remove(t);



            Console.WriteLine(Test());


            /*possibleTile = ReturnAdjPoint(p);
            if (!checkedTiles.Contains(possibleTile))
            {
            theseus.Coordinate = new Point(possibleTile.Coordinate.X, possibleTile.Coordinate.Y);
                Console.WriteLine(Test());
                Console.WriteLine("Current tile: {0}", theseus.Coordinate);
            //possibleTiles.Remove(theTiles[p.X, p.Y]);
            Console.WriteLine(Test());
            }
        }*/



            //   }




        }














        public string Test()
        {
            string str = "";

            str += "Checked:\n";
            foreach (Tile t in checkedTiles)
            {
                str += t.Coordinate + "\n";
            }

            str += "Possible:\n";
            foreach (Tile t in possibleTiles)
            {
                str += t.Coordinate + "\n";
            }

            return str;
        }




    }
}
