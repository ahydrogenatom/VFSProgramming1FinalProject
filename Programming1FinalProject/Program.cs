using System;
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

        //PRIVATE MEMBER VARIABLES
        private static MapTile[,] theMap;
        private static int xDimensions = 30;
        private static int yDimensions = 30;

        static void Main(string[] args)
        {

            //TEST print a test map of all grass



            createMap();

            printMap();

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

        }

        //prints the map to the console
        static void printMap()
        {

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
                                Console.Write("G ");
                                break;
                            }

                        case TileType.ROCK:
                            {
                                Console.Write("R ");
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
