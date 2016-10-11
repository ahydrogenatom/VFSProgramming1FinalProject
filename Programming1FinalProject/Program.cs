using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;





namespace Programming1FinalProject
{

    



    class Program
    {

        //get console window to create graphics object on
        [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
        private static extern IntPtr GetConsoleHandle();




        //PRIVATE MEMBER VARIABLES
        private static MapTile[,] theMap;
        private static int xDimensions = 30;
        private static int yDimensions = 30;

        private static int imageSizeX = 16;
        private static int imageSizeY = 16;

        static void Main(string[] args)
        {

            //TEST print a test map of all grass

            //var windowHandler = GetConsoleHandle();

            //using (var graphics = Graphics.FromHwnd(windowHandler))
            // using (var image = Properties.Resources.nick_mage_21) 
            //    graphics.DrawImage(image, 0, 0, 128, 128);

            Console.WriteLine("Please maximize screen");
            Console.ReadLine();
                createMap();

            drawMap();

            Console.ReadLine();
        }


        //create the array of MapTiles that represent the in game map
        static void createMap()
        {


            
            Random RNG = new Random();


            theMap = new MapTile[xDimensions, yDimensions];

            int randomNum;
            TileType newTile = TileType.GRASS;

            //TEMP Fill map for output
            for(int i = 0; i < xDimensions; i++)
            {
                for(int j = 0; j < yDimensions; j++)
                {
                    randomNum = RNG.Next(0, 5);

                    switch(randomNum)
                    {
                        case 0:
                            {
                                newTile = TileType.GRASS;
                                break;
                            }

                        case 1:
                            {
                                newTile = TileType.ROAD;
                                break;
                            }

                        case 2:
                            {
                                newTile = TileType.ROCK;
                                break;
                            }

                        case 3:
                            {
                                newTile = TileType.WATER;
                                break;
                            }

                        case 4:
                            {
                                newTile = TileType.ITEMBOX;
                                break;
                            }
                    }

                    theMap[i, j] = new MapTile(i, j, newTile);
                }
            }
            


            //TODO
            //create map array here
            //use the variable 'theMap'
        }

        //prints the map to the console
        static void drawMap()
        {

            var windowHandler = GetConsoleHandle();

            //current MapTile
            MapTile currentMapTile;
            TileType currentTileType;

            for(int i = 0; i < yDimensions; i++)
            {
                for(int j = 0; j < xDimensions; j++)
                {
                    currentMapTile = theMap[i, j];
                    currentTileType = currentMapTile.getTileType();

                    switch(currentTileType)
                    {
                        case TileType.GRASS:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.Grass)
                                    graphics.DrawImage(image, i*16, j*16, imageSizeX, imageSizeY);
                                break;
                            }

                        case TileType.ROCK:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.Rock_1)
                                    graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                                break;
                                
                            }

                        case TileType.ROAD:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.Road)
                                    graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                                break;
                            }

                        case TileType.ITEMBOX:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.Item_Box)
                                    graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                                break;
                            }

                        case TileType.PLAYER:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.Character)
                                    graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                                break;
                            }

                        case TileType.WATER:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.Water)
                                    graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                                break;
                            }

                        case TileType.SAFEZONE:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.End)
                                    graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                                break;
                            }

                        case TileType.START:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.Start)
                                    graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                                break;
                            }

                        case TileType.ENEMY:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.Enemy)
                                    graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                                break;
                            }

                        case TileType.FOG:
                            {
                                using (var graphics = Graphics.FromHwnd(windowHandler))
                                using (var image = Properties.Resources.Start)
                                    graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                                break;
                            }
                        default:
                            {
                                Console.Write("  ");
                                break;
                            }
                            
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
