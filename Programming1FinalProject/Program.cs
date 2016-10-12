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
        private static MapTile[,] theMapOriginal;
        private static MapTile tileUnderPlayer;
        private static int xDimensions = 30;
        private static int yDimensions = 30;

        private static int imageSizeX = 16;
        private static int imageSizeY = 16;

        private static bool isPlaying;

        private static int playerXCoord;
        private static int playerYCoord;

        static void Main(string[] args)
        {
            // start - checking enemy movement
            // create an array of enemies with their starting locations
            Enemy[] enemiesArray = {new Enemy(2, 3),
                                    new Enemy(9, 0),
                                    new Enemy(14, 4),
                                    new Enemy(6, 10),
                                    new Enemy(0, 14),
                                    new Enemy(8, 16),
                                    new Enemy(3, 21),
                                    new Enemy(13, 21),
                                    new Enemy(2, 27),
                                    new Enemy(17, 27),
                                    new Enemy(25, 1),
                                    new Enemy(24, 6),
                                    new Enemy(27, 14),
                                    new Enemy(24, 20),
                                    new Enemy(25, 26),
                                    new Enemy(26, 29),
                                    new Enemy(29, 26)};

            Enemy E1 = new Enemy(2, 4);
            // end - checking enemy movement

            //TEST print a test map of all grass

            //var windowHandler = GetConsoleHandle();

            //using (var graphics = Graphics.FromHwnd(windowHandler))
            // using (var image = Properties.Resources.nick_mage_21) 
            //    graphics.DrawImage(image, 0, 0, 128, 128);

            Console.SetWindowSize(80, 60);

            Console.WriteLine("Please maximize screen");
            Console.ReadLine();

            
                createMap();

            drawMap();



            Console.SetCursorPosition(60, 60);
           // Console.WriteLine("Press enter to start the game");
           // Console.ReadLine();

            isPlaying = true;

            

            //save tile under player


            //set starting character position
            tileUnderPlayer = theMap[0, 0];
            setCharacterPosition(0, 0);
            

            //test draw the board a bunch
            for(int i = 0; i < 10; i++)
            {
                drawMap();
            }

            // start - checking enemy movement
            //theMap[E1X, E1Y] = new MapTile(E1X, E1Y, TileType.ENEMY);
            // end - checking enemy movement

            while (isPlaying)
            {

                //structure into specific actions

                


                //character controls

                //check if a key was available and pressed
                if (Console.KeyAvailable)
                {

                    int E1X = E1.GetPosX();
                    int E1Y = E1.GetPosY();

                    int enemyNextTileUp = E1Y + 1;
                    int enemyNextTileDown = E1Y - 1;

                    int enemyNextTileLeft = E1X - 1;
                    int enemyNextTileRight = E1X + 1;

                    if (E1X == 0)
                    {
                        enemyNextTileLeft = E1X;
                    }
                    if (E1X == (xDimensions - 1))
                    {
                        enemyNextTileRight = E1X;
                    }
                    if (E1Y == 0)
                    {
                        enemyNextTileDown = E1Y;
                    }
                    if (E1Y == (yDimensions - 1))
                    {
                        enemyNextTileUp = E1Y;
                    }


                    ConsoleKeyInfo currentKey = Console.ReadKey(true);
                    //player goes UP
                    if ((currentKey.Key == ConsoleKey.W) && moveIsValid(playerXCoord, playerYCoord, ControlDirection.UP))
                    {
                        //set the space under the player back to what it was on the map
                        theMap[playerXCoord, playerYCoord].setTileType(theMapOriginal[playerXCoord, playerYCoord].getTileType());

                        setCharacterPosition(playerXCoord, playerYCoord - 1);

                        

                        // Move enemy every time the player moves
                        E1.MoveEnemy(playerXCoord, playerYCoord, theMap[E1X, enemyNextTileUp], theMap[E1X, enemyNextTileDown], theMap[enemyNextTileLeft, E1Y], theMap[enemyNextTileRight, E1Y]);

                        // Get new position of enemy
                        int E1NewX = E1.GetPosX();
                        int E1NewY = E1.GetPosY();

                        // Change tile where enemy moves to enemy type
                        theMap[E1NewX, E1NewY].setTileType(TileType.ENEMY);

                        // Change tile that enemy was on back to original
                        theMap[E1X, E1Y].setTileType(theMapOriginal[E1X, E1Y].getTileType());

                        drawMap();
                    }

                    //player goes RIGHT
                    else if ((currentKey.Key == ConsoleKey.D) && moveIsValid(playerXCoord, playerYCoord, ControlDirection.RIGHT))
                    {
                        //set the space under the player back to what it was on the map
                        theMap[playerXCoord, playerYCoord].setTileType(theMapOriginal[playerXCoord, playerYCoord].getTileType());

                        setCharacterPosition(playerXCoord + 1, playerYCoord);

                        // Move enemy every time the player moves
                        E1.MoveEnemy(playerXCoord, playerYCoord, theMap[E1X, enemyNextTileUp], theMap[E1X, enemyNextTileDown], theMap[enemyNextTileLeft, E1Y], theMap[enemyNextTileRight, E1Y]);

                        // Get new position of enemy
                        int E1NewX = E1.GetPosX();
                        int E1NewY = E1.GetPosY();

                        // Change tile where enemy moves to enemy type
                        theMap[E1NewX, E1NewY].setTileType(TileType.ENEMY);

                        // Change tile that enemy was on back to original
                        theMap[E1X, E1Y].setTileType(theMapOriginal[E1X, E1Y].getTileType());


                        drawMap();
                    }

                    //player goes LEFT
                    else if ((currentKey.Key == ConsoleKey.A) && moveIsValid(playerXCoord, playerYCoord, ControlDirection.LEFT))
                    {
                        //set the space under the player back to what it was on the map
                        theMap[playerXCoord, playerYCoord].setTileType(theMapOriginal[playerXCoord, playerYCoord].getTileType());

                        setCharacterPosition(playerXCoord - 1, playerYCoord);

                        // Move enemy every time the player moves
                        E1.MoveEnemy(playerXCoord, playerYCoord, theMap[E1X, enemyNextTileUp], theMap[E1X, enemyNextTileDown], theMap[enemyNextTileLeft, E1Y], theMap[enemyNextTileRight, E1Y]);

                        // Get new position of enemy
                        int E1NewX = E1.GetPosX();
                        int E1NewY = E1.GetPosY();

                        // Change tile where enemy moves to enemy type
                        theMap[E1NewX, E1NewY].setTileType(TileType.ENEMY);

                        // Change tile that enemy was on back to original
                        theMap[E1X, E1Y].setTileType(theMapOriginal[E1X, E1Y].getTileType());

                        drawMap();
                    }

                    //player goes DOWN
                    else if ((currentKey.Key == ConsoleKey.S) && moveIsValid(playerXCoord, playerYCoord, ControlDirection.DOWN))
                    {
                        //set the space under the player back to what it was on the map
                        theMap[playerXCoord, playerYCoord].setTileType(theMapOriginal[playerXCoord,playerYCoord].getTileType());

                        setCharacterPosition(playerXCoord, playerYCoord + 1);

                        // Move enemy every time the player moves
                        E1.MoveEnemy(playerXCoord, playerYCoord, theMap[E1X, enemyNextTileUp], theMap[E1X, enemyNextTileDown], theMap[enemyNextTileLeft, E1Y], theMap[enemyNextTileRight, E1Y]);

                        // Get new position of enemy
                        int E1NewX = E1.GetPosX();
                        int E1NewY = E1.GetPosY();

                        // Change tile where enemy moves to enemy type
                        theMap[E1NewX, E1NewY].setTileType(TileType.ENEMY);

                        // Change tile that enemy was on back to original
                        theMap[E1X, E1Y].setTileType(theMapOriginal[E1X, E1Y].getTileType());


                        drawMap();
                    }
                }
            }
            
        }


        //create the array of MapTiles that represent the in game map
        static void createMap()
        {
            //TESTING: getting map from file
            //string mapFromTxt = Properties.Resources.mapText;




            theMapOriginal = new MapTile[xDimensions, yDimensions];

            theMap = new MapTile[xDimensions, yDimensions];
            // GRASS, ROAD, ROCK, ITEMBOX, WATER, START, SAFEZONE, PLAYER, ENEMY, FOG

            //create copy of the original map
            theMapOriginal[0, 0] = new MapTile(0, 0, TileType.START);
            theMapOriginal[0, 1] = new MapTile(0, 1, TileType.GRASS);
            theMapOriginal[0, 2] = new MapTile(0, 2, TileType.GRASS);
            theMapOriginal[0, 3] = new MapTile(0, 3, TileType.GRASS);
            theMapOriginal[0, 4] = new MapTile(0, 4, TileType.GRASS);
            theMapOriginal[0, 5] = new MapTile(0, 5, TileType.GRASS);
            theMapOriginal[0, 6] = new MapTile(0, 6, TileType.GRASS);
            theMapOriginal[0, 7] = new MapTile(0, 7, TileType.GRASS);
            theMapOriginal[0, 8] = new MapTile(0, 8, TileType.ROCK);
            theMapOriginal[0, 9] = new MapTile(0, 9, TileType.ROCK);
            theMapOriginal[0, 10] = new MapTile(0, 10, TileType.ROCK);
            theMapOriginal[0, 11] = new MapTile(0, 11, TileType.ROCK);
            theMapOriginal[0, 12] = new MapTile(0, 12, TileType.GRASS);
            theMapOriginal[0, 13] = new MapTile(0, 13, TileType.GRASS);
            theMapOriginal[0, 14] = new MapTile(0, 14, TileType.GRASS);
            theMapOriginal[0, 15] = new MapTile(0, 15, TileType.GRASS);
            theMapOriginal[0, 16] = new MapTile(0, 16, TileType.GRASS);
            theMapOriginal[0, 17] = new MapTile(0, 17, TileType.GRASS);
            theMapOriginal[0, 18] = new MapTile(0, 18, TileType.GRASS);
            theMapOriginal[0, 19] = new MapTile(0, 19, TileType.GRASS);
            theMapOriginal[0, 20] = new MapTile(0, 20, TileType.GRASS);
            theMapOriginal[0, 21] = new MapTile(0, 21, TileType.GRASS);
            theMapOriginal[0, 22] = new MapTile(0, 22, TileType.GRASS);
            theMapOriginal[0, 23] = new MapTile(0, 23, TileType.GRASS);
            theMapOriginal[0, 24] = new MapTile(0, 24, TileType.GRASS);
            theMapOriginal[0, 25] = new MapTile(0, 25, TileType.GRASS);
            theMapOriginal[0, 26] = new MapTile(0, 26, TileType.GRASS);
            theMapOriginal[0, 27] = new MapTile(0, 27, TileType.GRASS);
            theMapOriginal[0, 28] = new MapTile(0, 28, TileType.GRASS);
            theMapOriginal[0, 29] = new MapTile(0, 29, TileType.GRASS);

            // X = 1
            theMapOriginal[1, 0] = new MapTile(1, 0, TileType.GRASS);
            theMapOriginal[1, 1] = new MapTile(1, 1, TileType.GRASS);
            theMapOriginal[1, 2] = new MapTile(1, 2, TileType.GRASS);
            theMapOriginal[1, 3] = new MapTile(1, 3, TileType.GRASS);
            theMapOriginal[1, 4] = new MapTile(1, 4, TileType.GRASS);
            theMapOriginal[1, 5] = new MapTile(1, 5, TileType.GRASS);
            theMapOriginal[1, 6] = new MapTile(1, 6, TileType.GRASS);
            theMapOriginal[1, 7] = new MapTile(1, 7, TileType.ITEMBOX);
            theMapOriginal[1, 8] = new MapTile(1, 8, TileType.GRASS);
            theMapOriginal[1, 9] = new MapTile(1, 9, TileType.ROCK);
            theMapOriginal[1, 10] = new MapTile(1, 10, TileType.ROCK);
            theMapOriginal[1, 11] = new MapTile(1, 11, TileType.GRASS);
            theMapOriginal[1, 12] = new MapTile(1, 12, TileType.GRASS);
            theMapOriginal[1, 13] = new MapTile(1, 13, TileType.GRASS);
            theMapOriginal[1, 14] = new MapTile(1, 14, TileType.GRASS);
            theMapOriginal[1, 15] = new MapTile(1, 15, TileType.GRASS);
            theMapOriginal[1, 16] = new MapTile(1, 16, TileType.GRASS);
            theMapOriginal[1, 17] = new MapTile(1, 17, TileType.GRASS);
            theMapOriginal[1, 18] = new MapTile(1, 18, TileType.GRASS);
            theMapOriginal[1, 19] = new MapTile(1, 19, TileType.GRASS);
            theMapOriginal[1, 20] = new MapTile(1, 20, TileType.ITEMBOX);
            theMapOriginal[1, 21] = new MapTile(1, 21, TileType.GRASS);
            theMapOriginal[1, 22] = new MapTile(1, 22, TileType.GRASS);
            theMapOriginal[1, 23] = new MapTile(1, 23, TileType.GRASS);
            theMapOriginal[1, 24] = new MapTile(1, 24, TileType.GRASS);
            theMapOriginal[1, 25] = new MapTile(1, 25, TileType.GRASS);
            theMapOriginal[1, 26] = new MapTile(1, 26, TileType.GRASS);
            theMapOriginal[1, 27] = new MapTile(1, 27, TileType.GRASS);
            theMapOriginal[1, 28] = new MapTile(1, 28, TileType.GRASS);
            theMapOriginal[1, 29] = new MapTile(1, 29, TileType.GRASS);

            // X = 2
            theMapOriginal[2, 0] = new MapTile(2, 0, TileType.GRASS);
            theMapOriginal[2, 1] = new MapTile(2, 1, TileType.GRASS);
            theMapOriginal[2, 2] = new MapTile(2, 2, TileType.GRASS);
            theMapOriginal[2, 3] = new MapTile(2, 3, TileType.GRASS);
            theMapOriginal[2, 4] = new MapTile(2, 4, TileType.GRASS);
            theMapOriginal[2, 5] = new MapTile(2, 5, TileType.GRASS);
            theMapOriginal[2, 6] = new MapTile(2, 6, TileType.GRASS);
            theMapOriginal[2, 7] = new MapTile(2, 7, TileType.GRASS);
            theMapOriginal[2, 8] = new MapTile(2, 8, TileType.GRASS);
            theMapOriginal[2, 9] = new MapTile(2, 9, TileType.GRASS);
            theMapOriginal[2, 10] = new MapTile(2, 10, TileType.GRASS);
            theMapOriginal[2, 11] = new MapTile(2, 11, TileType.GRASS);
            theMapOriginal[2, 12] = new MapTile(2, 12, TileType.GRASS);
            theMapOriginal[2, 13] = new MapTile(2, 13, TileType.ROCK);
            theMapOriginal[2, 14] = new MapTile(2, 14, TileType.ROCK);
            theMapOriginal[2, 15] = new MapTile(2, 15, TileType.ROCK);
            theMapOriginal[2, 16] = new MapTile(2, 16, TileType.ROCK);
            theMapOriginal[2, 17] = new MapTile(2, 17, TileType.GRASS);
            theMapOriginal[2, 18] = new MapTile(2, 18, TileType.GRASS);
            theMapOriginal[2, 19] = new MapTile(2, 19, TileType.GRASS);
            theMapOriginal[2, 20] = new MapTile(2, 20, TileType.GRASS);
            theMapOriginal[2, 21] = new MapTile(2, 21, TileType.GRASS);
            theMapOriginal[2, 22] = new MapTile(2, 22, TileType.GRASS);
            theMapOriginal[2, 23] = new MapTile(2, 23, TileType.GRASS);
            theMapOriginal[2, 24] = new MapTile(2, 24, TileType.GRASS);
            theMapOriginal[2, 25] = new MapTile(2, 25, TileType.GRASS);
            theMapOriginal[2, 26] = new MapTile(2, 26, TileType.GRASS);
            theMapOriginal[2, 27] = new MapTile(2, 27, TileType.GRASS);
            theMapOriginal[2, 28] = new MapTile(2, 28, TileType.GRASS);
            theMapOriginal[2, 29] = new MapTile(2, 29, TileType.GRASS);

            // X = 3
            theMapOriginal[3, 0] = new MapTile(3, 0, TileType.GRASS);
            theMapOriginal[3, 1] = new MapTile(3, 1, TileType.GRASS);
            theMapOriginal[3, 2] = new MapTile(3, 2, TileType.GRASS);
            theMapOriginal[3, 3] = new MapTile(3, 3, TileType.GRASS);
            theMapOriginal[3, 4] = new MapTile(3, 4, TileType.GRASS);
            theMapOriginal[3, 5] = new MapTile(3, 5, TileType.ROCK);
            theMapOriginal[3, 6] = new MapTile(3, 6, TileType.ROCK);
            theMapOriginal[3, 7] = new MapTile(3, 7, TileType.GRASS);
            theMapOriginal[3, 8] = new MapTile(3, 8, TileType.GRASS);
            theMapOriginal[3, 9] = new MapTile(3, 9, TileType.GRASS);
            theMapOriginal[3, 10] = new MapTile(3, 10, TileType.GRASS);
            theMapOriginal[3, 11] = new MapTile(3, 11, TileType.GRASS);
            theMapOriginal[3, 12] = new MapTile(3, 12, TileType.ROCK);
            theMapOriginal[3, 13] = new MapTile(3, 13, TileType.ROCK);
            theMapOriginal[3, 14] = new MapTile(3, 14, TileType.ROCK);
            theMapOriginal[3, 15] = new MapTile(3, 15, TileType.ROCK);
            theMapOriginal[3, 16] = new MapTile(3, 16, TileType.ROCK);
            theMapOriginal[3, 17] = new MapTile(3, 17, TileType.ROCK);
            theMapOriginal[3, 18] = new MapTile(3, 18, TileType.GRASS);
            theMapOriginal[3, 19] = new MapTile(3, 19, TileType.GRASS);
            theMapOriginal[3, 20] = new MapTile(3, 20, TileType.GRASS);
            theMapOriginal[3, 21] = new MapTile(3, 21, TileType.GRASS);
            theMapOriginal[3, 22] = new MapTile(3, 22, TileType.GRASS);
            theMapOriginal[3, 23] = new MapTile(3, 23, TileType.GRASS);
            theMapOriginal[3, 24] = new MapTile(3, 24, TileType.GRASS);
            theMapOriginal[3, 25] = new MapTile(3, 25, TileType.ROCK);
            theMapOriginal[3, 26] = new MapTile(3, 26, TileType.ROCK);
            theMapOriginal[3, 27] = new MapTile(3, 27, TileType.GRASS);
            theMapOriginal[3, 28] = new MapTile(3, 28, TileType.GRASS);
            theMapOriginal[3, 29] = new MapTile(3, 29, TileType.GRASS);

            // X = 4
            theMapOriginal[4, 0] = new MapTile(4, 0, TileType.ROCK);
            theMapOriginal[4, 1] = new MapTile(4, 1, TileType.GRASS);
            theMapOriginal[4, 2] = new MapTile(4, 2, TileType.GRASS);
            theMapOriginal[4, 3] = new MapTile(4, 3, TileType.GRASS);
            theMapOriginal[4, 4] = new MapTile(4, 4, TileType.GRASS);
            theMapOriginal[4, 5] = new MapTile(4, 5, TileType.ROCK);
            theMapOriginal[4, 6] = new MapTile(4, 6, TileType.ROCK);
            theMapOriginal[4, 7] = new MapTile(4, 7, TileType.GRASS);
            theMapOriginal[4, 8] = new MapTile(4, 8, TileType.GRASS);
            theMapOriginal[4, 9] = new MapTile(4, 9, TileType.GRASS);
            theMapOriginal[4, 10] = new MapTile(4, 10, TileType.GRASS);
            theMapOriginal[4, 11] = new MapTile(4, 11, TileType.GRASS);
            theMapOriginal[4, 12] = new MapTile(4, 12, TileType.ROCK);
            theMapOriginal[4, 13] = new MapTile(4, 13, TileType.ROCK);
            theMapOriginal[4, 14] = new MapTile(4, 14, TileType.GRASS);
            theMapOriginal[4, 15] = new MapTile(4, 15, TileType.GRASS);
            theMapOriginal[4, 16] = new MapTile(4, 16, TileType.GRASS);
            theMapOriginal[4, 17] = new MapTile(4, 17, TileType.GRASS);
            theMapOriginal[4, 18] = new MapTile(4, 18, TileType.GRASS);
            theMapOriginal[4, 19] = new MapTile(4, 19, TileType.GRASS);
            theMapOriginal[4, 20] = new MapTile(4, 20, TileType.GRASS);
            theMapOriginal[4, 21] = new MapTile(4, 21, TileType.GRASS);
            theMapOriginal[4, 22] = new MapTile(4, 22, TileType.GRASS);
            theMapOriginal[4, 23] = new MapTile(4, 23, TileType.GRASS);
            theMapOriginal[4, 24] = new MapTile(4, 24, TileType.ROCK);
            theMapOriginal[4, 25] = new MapTile(4, 25, TileType.ROCK);
            theMapOriginal[4, 26] = new MapTile(4, 26, TileType.ITEMBOX);
            theMapOriginal[4, 27] = new MapTile(4, 27, TileType.GRASS);
            theMapOriginal[4, 28] = new MapTile(4, 28, TileType.GRASS);
            theMapOriginal[4, 29] = new MapTile(4, 29, TileType.GRASS);

            // X = 5
            theMapOriginal[5, 0] = new MapTile(5, 0, TileType.ROCK);
            theMapOriginal[5, 1] = new MapTile(5, 1, TileType.ROCK);
            theMapOriginal[5, 2] = new MapTile(5, 2, TileType.GRASS);
            theMapOriginal[5, 3] = new MapTile(5, 3, TileType.GRASS);
            theMapOriginal[5, 4] = new MapTile(5, 4, TileType.GRASS);
            theMapOriginal[5, 5] = new MapTile(5, 5, TileType.ROCK);
            theMapOriginal[5, 6] = new MapTile(5, 6, TileType.ROCK);
            theMapOriginal[5, 7] = new MapTile(5, 7, TileType.GRASS);
            theMapOriginal[5, 8] = new MapTile(5, 8, TileType.GRASS);
            theMapOriginal[5, 9] = new MapTile(5, 9, TileType.GRASS);
            theMapOriginal[5, 10] = new MapTile(5, 10, TileType.GRASS);
            theMapOriginal[5, 11] = new MapTile(5, 11, TileType.GRASS);
            theMapOriginal[5, 12] = new MapTile(5, 12, TileType.ITEMBOX);
            theMapOriginal[5, 13] = new MapTile(5, 13, TileType.ROCK);
            theMapOriginal[5, 14] = new MapTile(5, 14, TileType.ROCK);
            theMapOriginal[5, 15] = new MapTile(5, 15, TileType.GRASS);
            theMapOriginal[5, 16] = new MapTile(5, 16, TileType.GRASS);
            theMapOriginal[5, 17] = new MapTile(5, 17, TileType.GRASS);
            theMapOriginal[5, 18] = new MapTile(5, 18, TileType.GRASS);
            theMapOriginal[5, 19] = new MapTile(5, 19, TileType.GRASS);
            theMapOriginal[5, 20] = new MapTile(5, 20, TileType.GRASS);
            theMapOriginal[5, 21] = new MapTile(5, 21, TileType.GRASS);
            theMapOriginal[5, 22] = new MapTile(5, 22, TileType.ROCK);
            theMapOriginal[5, 23] = new MapTile(5, 23, TileType.ROCK);
            theMapOriginal[5, 24] = new MapTile(5, 24, TileType.ROCK);
            theMapOriginal[5, 25] = new MapTile(5, 25, TileType.ROCK);
            theMapOriginal[5, 26] = new MapTile(5, 26, TileType.ROCK);
            theMapOriginal[5, 27] = new MapTile(5, 27, TileType.GRASS);
            theMapOriginal[5, 28] = new MapTile(5, 28, TileType.GRASS);
            theMapOriginal[5, 29] = new MapTile(5, 29, TileType.GRASS);

            // X = 6
            theMapOriginal[6, 0] = new MapTile(6, 0, TileType.ROCK);
            theMapOriginal[6, 1] = new MapTile(6, 1, TileType.ROCK);
            theMapOriginal[6, 2] = new MapTile(6, 2, TileType.GRASS);
            theMapOriginal[6, 3] = new MapTile(6, 3, TileType.GRASS);
            theMapOriginal[6, 4] = new MapTile(6, 4, TileType.ROCK);
            theMapOriginal[6, 5] = new MapTile(6, 5, TileType.ROCK);
            theMapOriginal[6, 6] = new MapTile(6, 6, TileType.ROCK);
            theMapOriginal[6, 7] = new MapTile(6, 7, TileType.GRASS);
            theMapOriginal[6, 8] = new MapTile(6, 8, TileType.GRASS);
            theMapOriginal[6, 9] = new MapTile(6, 9, TileType.GRASS);
            theMapOriginal[6, 10] = new MapTile(6, 10, TileType.GRASS);
            theMapOriginal[6, 11] = new MapTile(6, 11, TileType.GRASS);
            theMapOriginal[6, 12] = new MapTile(6, 12, TileType.GRASS);
            theMapOriginal[6, 13] = new MapTile(6, 13, TileType.ROCK);
            theMapOriginal[6, 14] = new MapTile(6, 14, TileType.ROCK);
            theMapOriginal[6, 15] = new MapTile(6, 15, TileType.ROCK);
            theMapOriginal[6, 16] = new MapTile(6, 16, TileType.GRASS);
            theMapOriginal[6, 17] = new MapTile(6, 17, TileType.GRASS);
            theMapOriginal[6, 18] = new MapTile(6, 18, TileType.GRASS);
            theMapOriginal[6, 19] = new MapTile(6, 19, TileType.GRASS);
            theMapOriginal[6, 20] = new MapTile(6, 20, TileType.GRASS);
            theMapOriginal[6, 21] = new MapTile(6, 21, TileType.GRASS);
            theMapOriginal[6, 22] = new MapTile(6, 22, TileType.ROCK);
            theMapOriginal[6, 23] = new MapTile(6, 23, TileType.ROCK);
            theMapOriginal[6, 24] = new MapTile(6, 24, TileType.ROCK);
            theMapOriginal[6, 25] = new MapTile(6, 25, TileType.ROCK);
            theMapOriginal[6, 26] = new MapTile(6, 26, TileType.ROCK);
            theMapOriginal[6, 27] = new MapTile(6, 27, TileType.GRASS);
            theMapOriginal[6, 28] = new MapTile(6, 28, TileType.GRASS);
            theMapOriginal[6, 29] = new MapTile(6, 29, TileType.GRASS);

            // X = 7
            theMapOriginal[7, 0] = new MapTile(7, 0, TileType.GRASS);
            theMapOriginal[7, 1] = new MapTile(7, 1, TileType.ITEMBOX);
            theMapOriginal[7, 2] = new MapTile(7, 2, TileType.GRASS);
            theMapOriginal[7, 3] = new MapTile(7, 3, TileType.ROCK);
            theMapOriginal[7, 4] = new MapTile(7, 4, TileType.ROCK);
            theMapOriginal[7, 5] = new MapTile(7, 5, TileType.ROCK);
            theMapOriginal[7, 6] = new MapTile(7, 6, TileType.GRASS);
            theMapOriginal[7, 7] = new MapTile(7, 7, TileType.GRASS);
            theMapOriginal[7, 8] = new MapTile(7, 8, TileType.GRASS);
            theMapOriginal[7, 9] = new MapTile(7, 9, TileType.GRASS);
            theMapOriginal[7, 10] = new MapTile(7, 10, TileType.GRASS);
            theMapOriginal[7, 11] = new MapTile(7, 11, TileType.GRASS);
            theMapOriginal[7, 12] = new MapTile(7, 12, TileType.GRASS);
            theMapOriginal[7, 13] = new MapTile(7, 13, TileType.GRASS);
            theMapOriginal[7, 14] = new MapTile(7, 14, TileType.GRASS);
            theMapOriginal[7, 15] = new MapTile(7, 15, TileType.GRASS);
            theMapOriginal[7, 16] = new MapTile(7, 16, TileType.GRASS);
            theMapOriginal[7, 17] = new MapTile(7, 17, TileType.GRASS);
            theMapOriginal[7, 18] = new MapTile(7, 18, TileType.GRASS);
            theMapOriginal[7, 19] = new MapTile(7, 19, TileType.GRASS);
            theMapOriginal[7, 20] = new MapTile(7, 20, TileType.GRASS);
            theMapOriginal[7, 21] = new MapTile(7, 21, TileType.ROCK);
            theMapOriginal[7, 22] = new MapTile(7, 22, TileType.WATER);
            theMapOriginal[7, 23] = new MapTile(7, 23, TileType.WATER);
            theMapOriginal[7, 24] = new MapTile(7, 24, TileType.WATER);
            theMapOriginal[7, 25] = new MapTile(7, 25, TileType.ROCK);
            theMapOriginal[7, 26] = new MapTile(7, 26, TileType.ROCK);
            theMapOriginal[7, 27] = new MapTile(7, 27, TileType.ROCK);
            theMapOriginal[7, 28] = new MapTile(7, 28, TileType.GRASS);
            theMapOriginal[7, 29] = new MapTile(7, 29, TileType.GRASS);

            // X = 8
            theMapOriginal[8, 0] = new MapTile(8, 0, TileType.GRASS);
            theMapOriginal[8, 1] = new MapTile(8, 1, TileType.GRASS);
            theMapOriginal[8, 2] = new MapTile(8, 2, TileType.GRASS);
            theMapOriginal[8, 3] = new MapTile(8, 3, TileType.ROCK);
            theMapOriginal[8, 4] = new MapTile(8, 4, TileType.ROCK);
            theMapOriginal[8, 5] = new MapTile(8, 5, TileType.ROCK);
            theMapOriginal[8, 6] = new MapTile(8, 6, TileType.GRASS);
            theMapOriginal[8, 7] = new MapTile(8, 7, TileType.GRASS);
            theMapOriginal[8, 8] = new MapTile(8, 8, TileType.GRASS);
            theMapOriginal[8, 9] = new MapTile(8, 9, TileType.GRASS);
            theMapOriginal[8, 10] = new MapTile(8, 10, TileType.GRASS);
            theMapOriginal[8, 11] = new MapTile(8, 11, TileType.GRASS);
            theMapOriginal[8, 12] = new MapTile(8, 12, TileType.GRASS);
            theMapOriginal[8, 13] = new MapTile(8, 13, TileType.GRASS);
            theMapOriginal[8, 14] = new MapTile(8, 14, TileType.GRASS);
            theMapOriginal[8, 15] = new MapTile(8, 15, TileType.GRASS);
            theMapOriginal[8, 16] = new MapTile(8, 16, TileType.GRASS);
            theMapOriginal[8, 17] = new MapTile(8, 17, TileType.GRASS);
            theMapOriginal[8, 18] = new MapTile(8, 18, TileType.GRASS);
            theMapOriginal[8, 19] = new MapTile(8, 19, TileType.GRASS);
            theMapOriginal[8, 20] = new MapTile(8, 20, TileType.GRASS);
            theMapOriginal[8, 21] = new MapTile(8, 21, TileType.GRASS);
            theMapOriginal[8, 22] = new MapTile(8, 22, TileType.GRASS);
            theMapOriginal[8, 23] = new MapTile(8, 23, TileType.WATER);
            theMapOriginal[8, 24] = new MapTile(8, 24, TileType.WATER);
            theMapOriginal[8, 25] = new MapTile(8, 25, TileType.WATER);
            theMapOriginal[8, 26] = new MapTile(8, 26, TileType.ROCK);
            theMapOriginal[8, 27] = new MapTile(8, 27, TileType.ROCK);
            theMapOriginal[8, 28] = new MapTile(8, 28, TileType.ROCK);
            theMapOriginal[8, 29] = new MapTile(8, 29, TileType.GRASS);

            // X = 9
            theMapOriginal[9, 0] = new MapTile(9, 0, TileType.GRASS);
            theMapOriginal[9, 1] = new MapTile(9, 1, TileType.GRASS);
            theMapOriginal[9, 2] = new MapTile(9, 2, TileType.GRASS);
            theMapOriginal[9, 3] = new MapTile(9, 3, TileType.ROCK);
            theMapOriginal[9, 4] = new MapTile(9, 4, TileType.ROCK);
            theMapOriginal[9, 5] = new MapTile(9, 5, TileType.GRASS);
            theMapOriginal[9, 6] = new MapTile(9, 6, TileType.GRASS);
            theMapOriginal[9, 7] = new MapTile(9, 7, TileType.GRASS);
            theMapOriginal[9, 8] = new MapTile(9, 8, TileType.GRASS);
            theMapOriginal[9, 9] = new MapTile(9, 9, TileType.GRASS);
            theMapOriginal[9, 10] = new MapTile(9, 10, TileType.WATER);
            theMapOriginal[9, 11] = new MapTile(9, 11, TileType.WATER);
            theMapOriginal[9, 12] = new MapTile(9, 12, TileType.WATER);
            theMapOriginal[9, 13] = new MapTile(9, 13, TileType.WATER);
            theMapOriginal[9, 14] = new MapTile(9, 14, TileType.GRASS);
            theMapOriginal[9, 15] = new MapTile(9, 15, TileType.GRASS);
            theMapOriginal[9, 16] = new MapTile(9, 16, TileType.GRASS);
            theMapOriginal[9, 17] = new MapTile(9, 17, TileType.GRASS);
            theMapOriginal[9, 18] = new MapTile(9, 18, TileType.ROCK);
            theMapOriginal[9, 19] = new MapTile(9, 19, TileType.ROCK);
            theMapOriginal[9, 20] = new MapTile(9, 20, TileType.GRASS);
            theMapOriginal[9, 21] = new MapTile(9, 21, TileType.GRASS);
            theMapOriginal[9, 22] = new MapTile(9, 22, TileType.GRASS);
            theMapOriginal[9, 23] = new MapTile(9, 23, TileType.GRASS);
            theMapOriginal[9, 24] = new MapTile(9, 24, TileType.WATER);
            theMapOriginal[9, 25] = new MapTile(9, 25, TileType.WATER);
            theMapOriginal[9, 26] = new MapTile(9, 26, TileType.WATER);
            theMapOriginal[9, 27] = new MapTile(9, 27, TileType.ROCK);
            theMapOriginal[9, 28] = new MapTile(9, 28, TileType.ROCK);
            theMapOriginal[9, 29] = new MapTile(9, 29, TileType.GRASS);

            // X = 10
            theMapOriginal[10, 0] = new MapTile(10, 0, TileType.GRASS);
            theMapOriginal[10, 1] = new MapTile(10, 1, TileType.GRASS);
            theMapOriginal[10, 2] = new MapTile(10, 2, TileType.ROCK);
            theMapOriginal[10, 3] = new MapTile(10, 3, TileType.ROCK);
            theMapOriginal[10, 4] = new MapTile(10, 4, TileType.GRASS);
            theMapOriginal[10, 5] = new MapTile(10, 5, TileType.GRASS);
            theMapOriginal[10, 6] = new MapTile(10, 6, TileType.GRASS);
            theMapOriginal[10, 7] = new MapTile(10, 7, TileType.GRASS);
            theMapOriginal[10, 8] = new MapTile(10, 8, TileType.GRASS);
            theMapOriginal[10, 9] = new MapTile(10, 9, TileType.GRASS);
            theMapOriginal[10, 10] = new MapTile(10, 10, TileType.WATER);
            theMapOriginal[10, 11] = new MapTile(10, 11, TileType.WATER);
            theMapOriginal[10, 12] = new MapTile(10, 12, TileType.WATER);
            theMapOriginal[10, 13] = new MapTile(10, 13, TileType.WATER);
            theMapOriginal[10, 14] = new MapTile(10, 14, TileType.WATER);
            theMapOriginal[10, 15] = new MapTile(10, 15, TileType.ROCK);
            theMapOriginal[10, 16] = new MapTile(10, 16, TileType.ROCK);
            theMapOriginal[10, 17] = new MapTile(10, 17, TileType.ROCK);
            theMapOriginal[10, 18] = new MapTile(10, 18, TileType.ROCK);
            theMapOriginal[10, 19] = new MapTile(10, 19, TileType.ROCK);
            theMapOriginal[10, 20] = new MapTile(10, 20, TileType.GRASS);
            theMapOriginal[10, 21] = new MapTile(10, 21, TileType.GRASS);
            theMapOriginal[10, 22] = new MapTile(10, 22, TileType.GRASS);
            theMapOriginal[10, 23] = new MapTile(10, 23, TileType.GRASS);
            theMapOriginal[10, 24] = new MapTile(10, 24, TileType.WATER);
            theMapOriginal[10, 25] = new MapTile(10, 25, TileType.WATER);
            theMapOriginal[10, 26] = new MapTile(10, 26, TileType.WATER);
            theMapOriginal[10, 27] = new MapTile(10, 27, TileType.ROCK);
            theMapOriginal[10, 28] = new MapTile(10, 28, TileType.ROCK);
            theMapOriginal[10, 29] = new MapTile(10, 29, TileType.ROCK);

            // X = 11
            theMapOriginal[11, 0] = new MapTile(11, 0, TileType.GRASS);
            theMapOriginal[11, 1] = new MapTile(11, 1, TileType.GRASS);
            theMapOriginal[11, 2] = new MapTile(11, 2, TileType.GRASS);
            theMapOriginal[11, 3] = new MapTile(11, 3, TileType.GRASS);
            theMapOriginal[11, 4] = new MapTile(11, 4, TileType.GRASS);
            theMapOriginal[11, 5] = new MapTile(11, 5, TileType.GRASS);
            theMapOriginal[11, 6] = new MapTile(11, 6, TileType.GRASS);
            theMapOriginal[11, 7] = new MapTile(11, 7, TileType.GRASS);
            theMapOriginal[11, 8] = new MapTile(11, 8, TileType.GRASS);
            theMapOriginal[11, 9] = new MapTile(11, 9, TileType.WATER);
            theMapOriginal[11, 10] = new MapTile(11, 10, TileType.WATER);
            theMapOriginal[11, 11] = new MapTile(11, 11, TileType.WATER);
            theMapOriginal[11, 12] = new MapTile(11, 12, TileType.WATER);
            theMapOriginal[11, 13] = new MapTile(11, 13, TileType.WATER);
            theMapOriginal[11, 14] = new MapTile(11, 14, TileType.WATER);
            theMapOriginal[11, 15] = new MapTile(11, 15, TileType.WATER);
            theMapOriginal[11, 16] = new MapTile(11, 16, TileType.ROCK);
            theMapOriginal[11, 17] = new MapTile(11, 17, TileType.ROCK);
            theMapOriginal[11, 18] = new MapTile(11, 18, TileType.ROCK);
            theMapOriginal[11, 19] = new MapTile(11, 19, TileType.ROCK);
            theMapOriginal[11, 20] = new MapTile(11, 20, TileType.GRASS);
            theMapOriginal[11, 21] = new MapTile(11, 21, TileType.GRASS);
            theMapOriginal[11, 22] = new MapTile(11, 22, TileType.GRASS);
            theMapOriginal[11, 23] = new MapTile(11, 23, TileType.WATER);
            theMapOriginal[11, 24] = new MapTile(11, 24, TileType.WATER);
            theMapOriginal[11, 25] = new MapTile(11, 25, TileType.WATER);
            theMapOriginal[11, 26] = new MapTile(11, 26, TileType.WATER);
            theMapOriginal[11, 27] = new MapTile(11, 27, TileType.WATER);
            theMapOriginal[11, 28] = new MapTile(11, 28, TileType.ROCK);
            theMapOriginal[11, 29] = new MapTile(11, 29, TileType.ROCK);

            // X = 12
            theMapOriginal[12, 0] = new MapTile(12, 0, TileType.GRASS);
            theMapOriginal[12, 1] = new MapTile(12, 1, TileType.GRASS);
            theMapOriginal[12, 2] = new MapTile(12, 2, TileType.GRASS);
            theMapOriginal[12, 3] = new MapTile(12, 3, TileType.GRASS);
            theMapOriginal[12, 4] = new MapTile(12, 4, TileType.GRASS);
            theMapOriginal[12, 5] = new MapTile(12, 5, TileType.GRASS);
            theMapOriginal[12, 6] = new MapTile(12, 6, TileType.GRASS);
            theMapOriginal[12, 7] = new MapTile(12, 7, TileType.GRASS);
            theMapOriginal[12, 8] = new MapTile(12, 8, TileType.WATER);
            theMapOriginal[12, 9] = new MapTile(12, 9, TileType.WATER);
            theMapOriginal[12, 10] = new MapTile(12, 10, TileType.WATER);
            theMapOriginal[12, 11] = new MapTile(12, 11, TileType.WATER);
            theMapOriginal[12, 12] = new MapTile(12, 12, TileType.WATER);
            theMapOriginal[12, 13] = new MapTile(12, 13, TileType.WATER);
            theMapOriginal[12, 14] = new MapTile(12, 14, TileType.WATER);
            theMapOriginal[12, 15] = new MapTile(12, 15, TileType.WATER);
            theMapOriginal[12, 16] = new MapTile(12, 16, TileType.WATER);
            theMapOriginal[12, 17] = new MapTile(12, 17, TileType.ROCK);
            theMapOriginal[12, 18] = new MapTile(12, 18, TileType.ROCK);
            theMapOriginal[12, 19] = new MapTile(12, 19, TileType.GRASS);
            theMapOriginal[12, 20] = new MapTile(12, 20, TileType.GRASS);
            theMapOriginal[12, 21] = new MapTile(12, 21, TileType.GRASS);
            theMapOriginal[12, 22] = new MapTile(12, 22, TileType.WATER);
            theMapOriginal[12, 23] = new MapTile(12, 23, TileType.WATER);
            theMapOriginal[12, 24] = new MapTile(12, 24, TileType.WATER);
            theMapOriginal[12, 25] = new MapTile(12, 25, TileType.WATER);
            theMapOriginal[12, 26] = new MapTile(12, 26, TileType.WATER);
            theMapOriginal[12, 27] = new MapTile(12, 27, TileType.WATER);
            theMapOriginal[12, 28] = new MapTile(12, 28, TileType.ROCK);
            theMapOriginal[12, 29] = new MapTile(12, 29, TileType.ROCK);

            // X = 13
            theMapOriginal[13, 0] = new MapTile(13, 0, TileType.GRASS);
            theMapOriginal[13, 1] = new MapTile(13, 1, TileType.GRASS);
            theMapOriginal[13, 2] = new MapTile(13, 2, TileType.GRASS);
            theMapOriginal[13, 3] = new MapTile(13, 3, TileType.GRASS);
            theMapOriginal[13, 4] = new MapTile(13, 4, TileType.GRASS);
            theMapOriginal[13, 5] = new MapTile(13, 5, TileType.GRASS);
            theMapOriginal[13, 6] = new MapTile(13, 6, TileType.GRASS);
            theMapOriginal[13, 7] = new MapTile(13, 7, TileType.WATER);
            theMapOriginal[13, 8] = new MapTile(13, 8, TileType.WATER);
            theMapOriginal[13, 9] = new MapTile(13, 9, TileType.WATER);
            theMapOriginal[13, 10] = new MapTile(13, 10, TileType.WATER);
            theMapOriginal[13, 11] = new MapTile(13, 11, TileType.WATER);
            theMapOriginal[13, 12] = new MapTile(13, 12, TileType.WATER);
            theMapOriginal[13, 13] = new MapTile(13, 13, TileType.WATER);
            theMapOriginal[13, 14] = new MapTile(13, 14, TileType.WATER);
            theMapOriginal[13, 15] = new MapTile(13, 15, TileType.WATER);
            theMapOriginal[13, 16] = new MapTile(13, 16, TileType.WATER);
            theMapOriginal[13, 17] = new MapTile(13, 17, TileType.ROCK);
            theMapOriginal[13, 18] = new MapTile(13, 18, TileType.GRASS);
            theMapOriginal[13, 19] = new MapTile(13, 19, TileType.GRASS);
            theMapOriginal[13, 20] = new MapTile(13, 20, TileType.GRASS);
            theMapOriginal[13, 21] = new MapTile(13, 21, TileType.GRASS);
            theMapOriginal[13, 22] = new MapTile(13, 22, TileType.GRASS);
            theMapOriginal[13, 23] = new MapTile(13, 23, TileType.ITEMBOX);
            theMapOriginal[13, 24] = new MapTile(13, 24, TileType.WATER);
            theMapOriginal[13, 25] = new MapTile(13, 25, TileType.WATER);
            theMapOriginal[13, 26] = new MapTile(13, 26, TileType.WATER);
            theMapOriginal[13, 27] = new MapTile(13, 27, TileType.WATER);
            theMapOriginal[13, 28] = new MapTile(13, 28, TileType.ROCK);
            theMapOriginal[13, 29] = new MapTile(13, 29, TileType.ROCK);

            // X = 14
            theMapOriginal[14, 0] = new MapTile(14, 0, TileType.GRASS);
            theMapOriginal[14, 1] = new MapTile(14, 1, TileType.GRASS);
            theMapOriginal[14, 2] = new MapTile(14, 2, TileType.GRASS);
            theMapOriginal[14, 3] = new MapTile(14, 3, TileType.GRASS);
            theMapOriginal[14, 4] = new MapTile(14, 4, TileType.ITEMBOX);
            theMapOriginal[14, 5] = new MapTile(14, 5, TileType.GRASS);
            theMapOriginal[14, 6] = new MapTile(14, 6, TileType.GRASS);
            theMapOriginal[14, 7] = new MapTile(14, 7, TileType.WATER);
            theMapOriginal[14, 8] = new MapTile(14, 8, TileType.WATER);
            theMapOriginal[14, 9] = new MapTile(14, 9, TileType.WATER);
            theMapOriginal[14, 10] = new MapTile(14, 10, TileType.WATER);
            theMapOriginal[14, 11] = new MapTile(14, 11, TileType.WATER);
            theMapOriginal[14, 12] = new MapTile(14, 12, TileType.WATER);
            theMapOriginal[14, 13] = new MapTile(14, 13, TileType.WATER);
            theMapOriginal[14, 14] = new MapTile(14, 14, TileType.WATER);
            theMapOriginal[14, 15] = new MapTile(14, 15, TileType.WATER);
            theMapOriginal[14, 16] = new MapTile(14, 16, TileType.WATER);
            theMapOriginal[14, 17] = new MapTile(14, 17, TileType.WATER);
            theMapOriginal[14, 18] = new MapTile(14, 18, TileType.GRASS);
            theMapOriginal[14, 19] = new MapTile(14, 19, TileType.GRASS);
            theMapOriginal[14, 20] = new MapTile(14, 20, TileType.GRASS);
            theMapOriginal[14, 21] = new MapTile(14, 21, TileType.GRASS);
            theMapOriginal[14, 22] = new MapTile(14, 22, TileType.GRASS);
            theMapOriginal[14, 23] = new MapTile(14, 23, TileType.GRASS);
            theMapOriginal[14, 24] = new MapTile(14, 24, TileType.GRASS);
            theMapOriginal[14, 25] = new MapTile(14, 25, TileType.GRASS);
            theMapOriginal[14, 26] = new MapTile(14, 26, TileType.GRASS);
            theMapOriginal[14, 27] = new MapTile(14, 27, TileType.ROCK);
            theMapOriginal[14, 28] = new MapTile(14, 28, TileType.ROCK);
            theMapOriginal[14, 29] = new MapTile(14, 29, TileType.ROCK);

            // X = 15
            theMapOriginal[15, 0] = new MapTile(15, 0, TileType.ITEMBOX);
            theMapOriginal[15, 1] = new MapTile(15, 1, TileType.ROCK);
            theMapOriginal[15, 2] = new MapTile(15, 2, TileType.GRASS);
            theMapOriginal[15, 3] = new MapTile(15, 3, TileType.GRASS);
            theMapOriginal[15, 4] = new MapTile(15, 4, TileType.GRASS);
            theMapOriginal[15, 5] = new MapTile(15, 5, TileType.GRASS);
            theMapOriginal[15, 6] = new MapTile(15, 6, TileType.GRASS);
            theMapOriginal[15, 7] = new MapTile(15, 7, TileType.GRASS);
            theMapOriginal[15, 8] = new MapTile(15, 8, TileType.WATER);
            theMapOriginal[15, 9] = new MapTile(15, 9, TileType.WATER);
            theMapOriginal[15, 10] = new MapTile(15, 10, TileType.WATER);
            theMapOriginal[15, 11] = new MapTile(15, 11, TileType.WATER);
            theMapOriginal[15, 12] = new MapTile(15, 12, TileType.WATER);
            theMapOriginal[15, 13] = new MapTile(15, 13, TileType.WATER);
            theMapOriginal[15, 14] = new MapTile(15, 14, TileType.WATER);
            theMapOriginal[15, 15] = new MapTile(15, 15, TileType.WATER);
            theMapOriginal[15, 16] = new MapTile(15, 16, TileType.WATER);
            theMapOriginal[15, 17] = new MapTile(15, 17, TileType.WATER);
            theMapOriginal[15, 18] = new MapTile(15, 18, TileType.GRASS);
            theMapOriginal[15, 19] = new MapTile(15, 19, TileType.GRASS);
            theMapOriginal[15, 20] = new MapTile(15, 20, TileType.GRASS);
            theMapOriginal[15, 21] = new MapTile(15, 21, TileType.GRASS);
            theMapOriginal[15, 22] = new MapTile(15, 22, TileType.GRASS);
            theMapOriginal[15, 23] = new MapTile(15, 23, TileType.GRASS);
            theMapOriginal[15, 24] = new MapTile(15, 24, TileType.GRASS);
            theMapOriginal[15, 25] = new MapTile(15, 25, TileType.GRASS);
            theMapOriginal[15, 26] = new MapTile(15, 26, TileType.GRASS);
            theMapOriginal[15, 27] = new MapTile(15, 27, TileType.GRASS);
            theMapOriginal[15, 28] = new MapTile(15, 28, TileType.ROCK);
            theMapOriginal[15, 29] = new MapTile(15, 29, TileType.ROCK);

            // X = 16
            theMapOriginal[16, 0] = new MapTile(16, 0, TileType.ROCK);
            theMapOriginal[16, 1] = new MapTile(16, 1, TileType.ROCK);
            theMapOriginal[16, 2] = new MapTile(16, 2, TileType.ROCK);
            theMapOriginal[16, 3] = new MapTile(16, 3, TileType.ROCK);
            theMapOriginal[16, 4] = new MapTile(16, 4, TileType.ROCK);
            theMapOriginal[16, 5] = new MapTile(16, 5, TileType.GRASS);
            theMapOriginal[16, 6] = new MapTile(16, 6, TileType.GRASS);
            theMapOriginal[16, 7] = new MapTile(16, 7, TileType.GRASS);
            theMapOriginal[16, 8] = new MapTile(16, 8, TileType.GRASS);
            theMapOriginal[16, 9] = new MapTile(16, 9, TileType.GRASS);
            theMapOriginal[16, 10] = new MapTile(16, 10, TileType.WATER);
            theMapOriginal[16, 11] = new MapTile(16, 11, TileType.WATER);
            theMapOriginal[16, 12] = new MapTile(16, 12, TileType.WATER);
            theMapOriginal[16, 13] = new MapTile(16, 13, TileType.WATER);
            theMapOriginal[16, 14] = new MapTile(16, 14, TileType.WATER);
            theMapOriginal[16, 15] = new MapTile(16, 15, TileType.WATER);
            theMapOriginal[16, 16] = new MapTile(16, 16, TileType.WATER);
            theMapOriginal[16, 17] = new MapTile(16, 17, TileType.WATER);
            theMapOriginal[16, 18] = new MapTile(16, 18, TileType.WATER);
            theMapOriginal[16, 19] = new MapTile(16, 19, TileType.GRASS);
            theMapOriginal[16, 20] = new MapTile(16, 20, TileType.GRASS);
            theMapOriginal[16, 21] = new MapTile(16, 21, TileType.GRASS);
            theMapOriginal[16, 22] = new MapTile(16, 22, TileType.GRASS);
            theMapOriginal[16, 23] = new MapTile(16, 23, TileType.GRASS);
            theMapOriginal[16, 24] = new MapTile(16, 24, TileType.GRASS);
            theMapOriginal[16, 25] = new MapTile(16, 25, TileType.GRASS);
            theMapOriginal[16, 26] = new MapTile(16, 26, TileType.GRASS);
            theMapOriginal[16, 27] = new MapTile(16, 27, TileType.GRASS);
            theMapOriginal[16, 28] = new MapTile(16, 28, TileType.ROCK);
            theMapOriginal[16, 29] = new MapTile(16, 29, TileType.ROCK);

            // X = 17
            theMapOriginal[17, 0] = new MapTile(17, 0, TileType.GRASS);
            theMapOriginal[17, 1] = new MapTile(17, 1, TileType.ROCK);
            theMapOriginal[17, 2] = new MapTile(17, 2, TileType.ROCK);
            theMapOriginal[17, 3] = new MapTile(17, 3, TileType.ROCK);
            theMapOriginal[17, 4] = new MapTile(17, 4, TileType.GRASS);
            theMapOriginal[17, 5] = new MapTile(17, 5, TileType.GRASS);
            theMapOriginal[17, 6] = new MapTile(17, 6, TileType.ROCK);
            theMapOriginal[17, 7] = new MapTile(17, 7, TileType.GRASS);
            theMapOriginal[17, 8] = new MapTile(17, 8, TileType.GRASS);
            theMapOriginal[17, 9] = new MapTile(17, 9, TileType.GRASS);
            theMapOriginal[17, 10] = new MapTile(17, 10, TileType.WATER);
            theMapOriginal[17, 11] = new MapTile(17, 11, TileType.WATER);
            theMapOriginal[17, 12] = new MapTile(17, 12, TileType.WATER);
            theMapOriginal[17, 13] = new MapTile(17, 13, TileType.WATER);
            theMapOriginal[17, 14] = new MapTile(17, 14, TileType.WATER);
            theMapOriginal[17, 15] = new MapTile(17, 15, TileType.WATER);
            theMapOriginal[17, 16] = new MapTile(17, 16, TileType.WATER);
            theMapOriginal[17, 17] = new MapTile(17, 17, TileType.WATER);
            theMapOriginal[17, 18] = new MapTile(17, 18, TileType.WATER);
            theMapOriginal[17, 19] = new MapTile(17, 19, TileType.GRASS);
            theMapOriginal[17, 20] = new MapTile(17, 20, TileType.GRASS);
            theMapOriginal[17, 21] = new MapTile(17, 21, TileType.GRASS);
            theMapOriginal[17, 22] = new MapTile(17, 22, TileType.GRASS);
            theMapOriginal[17, 23] = new MapTile(17, 23, TileType.ROCK);
            theMapOriginal[17, 24] = new MapTile(17, 24, TileType.ROCK);
            theMapOriginal[17, 25] = new MapTile(17, 25, TileType.ROCK);
            theMapOriginal[17, 26] = new MapTile(17, 26, TileType.GRASS);
            theMapOriginal[17, 27] = new MapTile(17, 27, TileType.GRASS);
            theMapOriginal[17, 28] = new MapTile(17, 28, TileType.GRASS);
            theMapOriginal[17, 29] = new MapTile(17, 29, TileType.ROCK);

            // X = 18
            theMapOriginal[18, 0] = new MapTile(18, 0, TileType.GRASS);
            theMapOriginal[18, 1] = new MapTile(18, 1, TileType.GRASS);
            theMapOriginal[18, 2] = new MapTile(18, 2, TileType.GRASS);
            theMapOriginal[18, 3] = new MapTile(18, 3, TileType.GRASS);
            theMapOriginal[18, 4] = new MapTile(18, 4, TileType.GRASS);
            theMapOriginal[18, 5] = new MapTile(18, 5, TileType.ROCK);
            theMapOriginal[18, 6] = new MapTile(18, 6, TileType.ROCK);
            theMapOriginal[18, 7] = new MapTile(18, 7, TileType.ITEMBOX);
            theMapOriginal[18, 8] = new MapTile(18, 8, TileType.GRASS);
            theMapOriginal[18, 9] = new MapTile(18, 9, TileType.GRASS);
            theMapOriginal[18, 10] = new MapTile(18, 10, TileType.WATER);
            theMapOriginal[18, 11] = new MapTile(18, 11, TileType.WATER);
            theMapOriginal[18, 12] = new MapTile(18, 12, TileType.WATER);
            theMapOriginal[18, 13] = new MapTile(18, 13, TileType.WATER);
            theMapOriginal[18, 14] = new MapTile(18, 14, TileType.WATER);
            theMapOriginal[18, 15] = new MapTile(18, 15, TileType.WATER);
            theMapOriginal[18, 16] = new MapTile(18, 16, TileType.WATER);
            theMapOriginal[18, 17] = new MapTile(18, 17, TileType.WATER);
            theMapOriginal[18, 18] = new MapTile(18, 18, TileType.WATER);
            theMapOriginal[18, 19] = new MapTile(18, 19, TileType.GRASS);
            theMapOriginal[18, 20] = new MapTile(18, 20, TileType.GRASS);
            theMapOriginal[18, 21] = new MapTile(18, 21, TileType.GRASS);
            theMapOriginal[18, 22] = new MapTile(18, 22, TileType.ROCK);
            theMapOriginal[18, 23] = new MapTile(18, 23, TileType.ROCK);
            theMapOriginal[18, 24] = new MapTile(18, 24, TileType.GRASS);
            theMapOriginal[18, 25] = new MapTile(18, 25, TileType.GRASS);
            theMapOriginal[18, 26] = new MapTile(18, 26, TileType.GRASS);
            theMapOriginal[18, 27] = new MapTile(18, 27, TileType.GRASS);
            theMapOriginal[18, 28] = new MapTile(18, 28, TileType.ITEMBOX);
            theMapOriginal[18, 29] = new MapTile(18, 29, TileType.GRASS);

            // X = 19
            theMapOriginal[19, 0] = new MapTile(19, 0, TileType.GRASS);
            theMapOriginal[19, 1] = new MapTile(19, 1, TileType.GRASS);
            theMapOriginal[19, 2] = new MapTile(19, 2, TileType.GRASS);
            theMapOriginal[19, 3] = new MapTile(91, 3, TileType.GRASS);
            theMapOriginal[19, 4] = new MapTile(19, 4, TileType.GRASS);
            theMapOriginal[19, 5] = new MapTile(19, 5, TileType.ROCK);
            theMapOriginal[19, 6] = new MapTile(19, 6, TileType.ROCK);
            theMapOriginal[19, 7] = new MapTile(19, 7, TileType.GRASS);
            theMapOriginal[19, 8] = new MapTile(19, 8, TileType.GRASS);
            theMapOriginal[19, 9] = new MapTile(19, 9, TileType.WATER);
            theMapOriginal[19, 10] = new MapTile(19, 10, TileType.WATER);
            theMapOriginal[19, 11] = new MapTile(19, 11, TileType.WATER);
            theMapOriginal[19, 12] = new MapTile(19, 12, TileType.WATER);
            theMapOriginal[19, 13] = new MapTile(19, 13, TileType.WATER);
            theMapOriginal[19, 14] = new MapTile(19, 14, TileType.WATER);
            theMapOriginal[19, 15] = new MapTile(19, 15, TileType.WATER);
            theMapOriginal[19, 16] = new MapTile(19, 16, TileType.WATER);
            theMapOriginal[19, 17] = new MapTile(19, 17, TileType.WATER);
            theMapOriginal[19, 18] = new MapTile(19, 18, TileType.WATER);
            theMapOriginal[19, 19] = new MapTile(19, 19, TileType.GRASS);
            theMapOriginal[19, 20] = new MapTile(19, 20, TileType.GRASS);
            theMapOriginal[19, 21] = new MapTile(19, 21, TileType.GRASS);
            theMapOriginal[19, 22] = new MapTile(19, 22, TileType.ROCK);
            theMapOriginal[19, 23] = new MapTile(19, 23, TileType.ROCK);
            theMapOriginal[19, 24] = new MapTile(19, 24, TileType.GRASS);
            theMapOriginal[19, 25] = new MapTile(19, 25, TileType.GRASS);
            theMapOriginal[19, 26] = new MapTile(19, 26, TileType.GRASS);
            theMapOriginal[19, 27] = new MapTile(19, 27, TileType.ROCK);
            theMapOriginal[19, 28] = new MapTile(19, 28, TileType.ROCK);
            theMapOriginal[19, 29] = new MapTile(19, 29, TileType.GRASS);

            // X = 20
            theMapOriginal[20, 0] = new MapTile(20, 0, TileType.GRASS);
            theMapOriginal[20, 1] = new MapTile(20, 1, TileType.GRASS);
            theMapOriginal[20, 2] = new MapTile(20, 2, TileType.GRASS);
            theMapOriginal[20, 3] = new MapTile(20, 3, TileType.GRASS);
            theMapOriginal[20, 4] = new MapTile(20, 4, TileType.GRASS);
            theMapOriginal[20, 5] = new MapTile(20, 5, TileType.GRASS);
            theMapOriginal[20, 6] = new MapTile(20, 6, TileType.GRASS);
            theMapOriginal[20, 7] = new MapTile(20, 7, TileType.GRASS);
            theMapOriginal[20, 8] = new MapTile(20, 8, TileType.WATER);
            theMapOriginal[20, 9] = new MapTile(20, 9, TileType.WATER);
            theMapOriginal[20, 10] = new MapTile(20, 10, TileType.WATER);
            theMapOriginal[20, 11] = new MapTile(20, 11, TileType.WATER);
            theMapOriginal[20, 12] = new MapTile(20, 12, TileType.WATER);
            theMapOriginal[20, 13] = new MapTile(20, 13, TileType.WATER);
            theMapOriginal[20, 14] = new MapTile(20, 14, TileType.WATER);
            theMapOriginal[20, 15] = new MapTile(20, 15, TileType.WATER);
            theMapOriginal[20, 16] = new MapTile(20, 16, TileType.WATER);
            theMapOriginal[20, 17] = new MapTile(20, 17, TileType.WATER);
            theMapOriginal[20, 18] = new MapTile(20, 18, TileType.WATER);
            theMapOriginal[20, 19] = new MapTile(20, 19, TileType.GRASS);
            theMapOriginal[20, 20] = new MapTile(20, 20, TileType.GRASS);
            theMapOriginal[20, 21] = new MapTile(20, 21, TileType.GRASS);
            theMapOriginal[20, 22] = new MapTile(20, 22, TileType.ROCK);
            theMapOriginal[20, 23] = new MapTile(20, 23, TileType.ITEMBOX);
            theMapOriginal[20, 24] = new MapTile(20, 24, TileType.GRASS);
            theMapOriginal[20, 25] = new MapTile(20, 25, TileType.GRASS);
            theMapOriginal[20, 26] = new MapTile(20, 26, TileType.GRASS);
            theMapOriginal[20, 27] = new MapTile(20, 27, TileType.ROCK);
            theMapOriginal[20, 28] = new MapTile(20, 28, TileType.ROCK);
            theMapOriginal[20, 29] = new MapTile(20, 29, TileType.GRASS);

            // X = 21
            theMapOriginal[21, 0] = new MapTile(21, 0, TileType.GRASS);
            theMapOriginal[21, 1] = new MapTile(21, 1, TileType.GRASS);
            theMapOriginal[21, 2] = new MapTile(21, 2, TileType.GRASS);
            theMapOriginal[21, 3] = new MapTile(21, 3, TileType.GRASS);
            theMapOriginal[21, 4] = new MapTile(21, 4, TileType.GRASS);
            theMapOriginal[21, 5] = new MapTile(21, 5, TileType.GRASS);
            theMapOriginal[21, 6] = new MapTile(21, 6, TileType.GRASS);
            theMapOriginal[21, 7] = new MapTile(21, 7, TileType.GRASS);
            theMapOriginal[21, 8] = new MapTile(21, 8, TileType.ROCK);
            theMapOriginal[21, 9] = new MapTile(21, 9, TileType.WATER);
            theMapOriginal[21, 10] = new MapTile(21, 10, TileType.WATER);
            theMapOriginal[21, 11] = new MapTile(21, 11, TileType.WATER);
            theMapOriginal[21, 12] = new MapTile(21, 12, TileType.WATER);
            theMapOriginal[21, 13] = new MapTile(21, 13, TileType.WATER);
            theMapOriginal[21, 14] = new MapTile(21, 14, TileType.WATER);
            theMapOriginal[21, 15] = new MapTile(21, 15, TileType.WATER);
            theMapOriginal[21, 16] = new MapTile(21, 16, TileType.WATER);
            theMapOriginal[21, 17] = new MapTile(21, 17, TileType.WATER);
            theMapOriginal[21, 18] = new MapTile(21, 18, TileType.GRASS);
            theMapOriginal[21, 19] = new MapTile(21, 19, TileType.GRASS);
            theMapOriginal[21, 20] = new MapTile(21, 20, TileType.GRASS);
            theMapOriginal[21, 21] = new MapTile(21, 21, TileType.GRASS);
            theMapOriginal[21, 22] = new MapTile(21, 22, TileType.ROCK);
            theMapOriginal[21, 23] = new MapTile(21, 23, TileType.GRASS);
            theMapOriginal[21, 24] = new MapTile(21, 24, TileType.GRASS);
            theMapOriginal[21, 25] = new MapTile(21, 25, TileType.GRASS);
            theMapOriginal[21, 26] = new MapTile(21, 26, TileType.GRASS);
            theMapOriginal[21, 27] = new MapTile(21, 27, TileType.ROCK);
            theMapOriginal[21, 28] = new MapTile(21, 28, TileType.ROCK);
            theMapOriginal[21, 29] = new MapTile(21, 29, TileType.GRASS);

            // X = 22
            theMapOriginal[22, 0] = new MapTile(22, 0, TileType.GRASS);
            theMapOriginal[22, 1] = new MapTile(22, 1, TileType.GRASS);
            theMapOriginal[22, 2] = new MapTile(22, 2, TileType.GRASS);
            theMapOriginal[22, 3] = new MapTile(22, 3, TileType.GRASS);
            theMapOriginal[22, 4] = new MapTile(22, 4, TileType.GRASS);
            theMapOriginal[22, 5] = new MapTile(22, 5, TileType.GRASS);
            theMapOriginal[22, 6] = new MapTile(22, 6, TileType.GRASS);
            theMapOriginal[22, 7] = new MapTile(22, 7, TileType.ROCK);
            theMapOriginal[22, 8] = new MapTile(22, 8, TileType.ROCK);
            theMapOriginal[22, 9] = new MapTile(22, 9, TileType.ROCK);
            theMapOriginal[22, 10] = new MapTile(22, 10, TileType.WATER);
            theMapOriginal[22, 11] = new MapTile(22, 11, TileType.WATER);
            theMapOriginal[22, 12] = new MapTile(22, 12, TileType.WATER);
            theMapOriginal[22, 13] = new MapTile(22, 13, TileType.ITEMBOX);
            theMapOriginal[22, 14] = new MapTile(22, 14, TileType.WATER);
            theMapOriginal[22, 15] = new MapTile(22, 15, TileType.WATER);
            theMapOriginal[22, 16] = new MapTile(22, 16, TileType.WATER);
            theMapOriginal[22, 17] = new MapTile(22, 17, TileType.WATER);
            theMapOriginal[22, 18] = new MapTile(22, 18, TileType.GRASS);
            theMapOriginal[22, 19] = new MapTile(22, 19, TileType.GRASS);
            theMapOriginal[22, 20] = new MapTile(22, 20, TileType.GRASS);
            theMapOriginal[22, 21] = new MapTile(22, 21, TileType.GRASS);
            theMapOriginal[22, 22] = new MapTile(22, 22, TileType.GRASS);
            theMapOriginal[22, 23] = new MapTile(22, 23, TileType.GRASS);
            theMapOriginal[22, 24] = new MapTile(22, 24, TileType.GRASS);
            theMapOriginal[22, 25] = new MapTile(22, 25, TileType.ROCK);
            theMapOriginal[22, 26] = new MapTile(22, 26, TileType.ROCK);
            theMapOriginal[22, 27] = new MapTile(22, 27, TileType.ROCK);
            theMapOriginal[22, 28] = new MapTile(22, 28, TileType.ROCK);
            theMapOriginal[22, 29] = new MapTile(22, 29, TileType.GRASS);

            // X = 23
            theMapOriginal[23, 0] = new MapTile(23, 0, TileType.ITEMBOX);
            theMapOriginal[23, 1] = new MapTile(23, 1, TileType.GRASS);
            theMapOriginal[23, 2] = new MapTile(23, 2, TileType.ROCK);
            theMapOriginal[23, 3] = new MapTile(23, 3, TileType.ROCK);
            theMapOriginal[23, 4] = new MapTile(23, 4, TileType.GRASS);
            theMapOriginal[23, 5] = new MapTile(23, 5, TileType.GRASS);
            theMapOriginal[23, 6] = new MapTile(23, 6, TileType.GRASS);
            theMapOriginal[23, 7] = new MapTile(23, 7, TileType.ROCK);
            theMapOriginal[23, 8] = new MapTile(23, 8, TileType.ROCK);
            theMapOriginal[23, 9] = new MapTile(23, 9, TileType.ROCK);
            theMapOriginal[23, 10] = new MapTile(23, 10, TileType.ROCK);
            theMapOriginal[23, 11] = new MapTile(23, 11, TileType.ROCK);
            theMapOriginal[23, 12] = new MapTile(23, 12, TileType.GRASS);
            theMapOriginal[23, 13] = new MapTile(23, 13, TileType.GRASS);
            theMapOriginal[23, 14] = new MapTile(23, 14, TileType.WATER);
            theMapOriginal[23, 15] = new MapTile(23, 15, TileType.WATER);
            theMapOriginal[23, 16] = new MapTile(23, 16, TileType.WATER);
            theMapOriginal[23, 17] = new MapTile(23, 17, TileType.GRASS);
            theMapOriginal[23, 18] = new MapTile(23, 18, TileType.GRASS);
            theMapOriginal[23, 19] = new MapTile(23, 19, TileType.GRASS);
            theMapOriginal[23, 20] = new MapTile(23, 20, TileType.GRASS);
            theMapOriginal[23, 21] = new MapTile(23, 21, TileType.GRASS);
            theMapOriginal[23, 22] = new MapTile(23, 22, TileType.GRASS);
            theMapOriginal[23, 23] = new MapTile(23, 23, TileType.GRASS);
            theMapOriginal[23, 24] = new MapTile(23, 24, TileType.GRASS);
            theMapOriginal[23, 25] = new MapTile(23, 25, TileType.GRASS);
            theMapOriginal[23, 26] = new MapTile(23, 26, TileType.ITEMBOX);
            theMapOriginal[23, 27] = new MapTile(23, 27, TileType.ROCK);
            theMapOriginal[23, 28] = new MapTile(23, 28, TileType.GRASS);
            theMapOriginal[23, 29] = new MapTile(23, 29, TileType.GRASS);

            // X = 24
            theMapOriginal[24, 0] = new MapTile(24, 0, TileType.GRASS);
            theMapOriginal[24, 1] = new MapTile(24, 1, TileType.GRASS);
            theMapOriginal[24, 2] = new MapTile(24, 2, TileType.ROCK);
            theMapOriginal[24, 3] = new MapTile(24, 3, TileType.ROCK);
            theMapOriginal[24, 4] = new MapTile(24, 4, TileType.ROCK);
            theMapOriginal[24, 5] = new MapTile(24, 5, TileType.GRASS);
            theMapOriginal[24, 6] = new MapTile(24, 6, TileType.GRASS);
            theMapOriginal[24, 7] = new MapTile(24, 7, TileType.GRASS);
            theMapOriginal[24, 8] = new MapTile(24, 8, TileType.ROCK);
            theMapOriginal[24, 9] = new MapTile(24, 9, TileType.ROCK);
            theMapOriginal[24, 10] = new MapTile(24, 10, TileType.ROCK);
            theMapOriginal[24, 11] = new MapTile(24, 11, TileType.ROCK);
            theMapOriginal[24, 12] = new MapTile(24, 12, TileType.ROCK);
            theMapOriginal[24, 13] = new MapTile(24, 13, TileType.GRASS);
            theMapOriginal[24, 14] = new MapTile(24, 14, TileType.GRASS);
            theMapOriginal[24, 15] = new MapTile(24, 15, TileType.GRASS);
            theMapOriginal[24, 16] = new MapTile(24, 16, TileType.GRASS);
            theMapOriginal[24, 17] = new MapTile(24, 17, TileType.GRASS);
            theMapOriginal[24, 18] = new MapTile(24, 18, TileType.GRASS);
            theMapOriginal[24, 19] = new MapTile(24, 19, TileType.GRASS);
            theMapOriginal[24, 20] = new MapTile(24, 20, TileType.GRASS);
            theMapOriginal[24, 21] = new MapTile(24, 21, TileType.GRASS);
            theMapOriginal[24, 22] = new MapTile(24, 22, TileType.GRASS);
            theMapOriginal[24, 23] = new MapTile(24, 23, TileType.GRASS);
            theMapOriginal[24, 24] = new MapTile(24, 24, TileType.ROCK);
            theMapOriginal[24, 25] = new MapTile(24, 25, TileType.GRASS);
            theMapOriginal[24, 26] = new MapTile(24, 26, TileType.GRASS);
            theMapOriginal[24, 27] = new MapTile(24, 27, TileType.GRASS);
            theMapOriginal[24, 28] = new MapTile(24, 28, TileType.GRASS);
            theMapOriginal[24, 29] = new MapTile(24, 29, TileType.GRASS);

            // X = 25
            theMapOriginal[25, 0] = new MapTile(25, 0, TileType.GRASS);
            theMapOriginal[25, 1] = new MapTile(25, 1, TileType.GRASS);
            theMapOriginal[25, 2] = new MapTile(25, 2, TileType.ROCK);
            theMapOriginal[25, 3] = new MapTile(25, 3, TileType.ROCK);
            theMapOriginal[25, 4] = new MapTile(25, 4, TileType.ROCK);
            theMapOriginal[25, 5] = new MapTile(25, 5, TileType.ROCK);
            theMapOriginal[25, 6] = new MapTile(25, 6, TileType.GRASS);
            theMapOriginal[25, 7] = new MapTile(25, 7, TileType.GRASS);
            theMapOriginal[25, 8] = new MapTile(25, 8, TileType.GRASS);
            theMapOriginal[25, 9] = new MapTile(25, 9, TileType.GRASS);
            theMapOriginal[25, 10] = new MapTile(25, 10, TileType.GRASS);
            theMapOriginal[25, 11] = new MapTile(25, 11, TileType.ROCK);
            theMapOriginal[25, 12] = new MapTile(25, 12, TileType.ROCK);
            theMapOriginal[25, 13] = new MapTile(25, 13, TileType.GRASS);
            theMapOriginal[25, 14] = new MapTile(25, 14, TileType.GRASS);
            theMapOriginal[25, 15] = new MapTile(25, 15, TileType.GRASS);
            theMapOriginal[25, 16] = new MapTile(25, 16, TileType.GRASS);
            theMapOriginal[25, 17] = new MapTile(25, 17, TileType.GRASS);
            theMapOriginal[25, 18] = new MapTile(25, 18, TileType.GRASS);
            theMapOriginal[25, 19] = new MapTile(25, 19, TileType.GRASS);
            theMapOriginal[25, 20] = new MapTile(25, 20, TileType.GRASS);
            theMapOriginal[25, 21] = new MapTile(25, 21, TileType.GRASS);
            theMapOriginal[25, 22] = new MapTile(25, 22, TileType.GRASS);
            theMapOriginal[25, 23] = new MapTile(25, 23, TileType.GRASS);
            theMapOriginal[25, 24] = new MapTile(25, 24, TileType.ROCK);
            theMapOriginal[25, 25] = new MapTile(25, 25, TileType.GRASS);
            theMapOriginal[25, 26] = new MapTile(25, 26, TileType.GRASS);
            theMapOriginal[25, 27] = new MapTile(25, 27, TileType.GRASS);
            theMapOriginal[25, 28] = new MapTile(25, 28, TileType.ROCK);
            theMapOriginal[25, 29] = new MapTile(25, 29, TileType.GRASS);

            // X = 26
            theMapOriginal[26, 0] = new MapTile(26, 0, TileType.GRASS);
            theMapOriginal[26, 1] = new MapTile(26, 1, TileType.GRASS);
            theMapOriginal[26, 2] = new MapTile(26, 2, TileType.GRASS);
            theMapOriginal[26, 3] = new MapTile(26, 3, TileType.GRASS);
            theMapOriginal[26, 4] = new MapTile(26, 4, TileType.GRASS);
            theMapOriginal[26, 5] = new MapTile(26, 5, TileType.GRASS);
            theMapOriginal[26, 6] = new MapTile(26, 6, TileType.GRASS);
            theMapOriginal[26, 7] = new MapTile(26, 7, TileType.GRASS);
            theMapOriginal[26, 8] = new MapTile(26, 8, TileType.GRASS);
            theMapOriginal[26, 9] = new MapTile(26, 9, TileType.GRASS);
            theMapOriginal[26, 10] = new MapTile(26, 10, TileType.GRASS);
            theMapOriginal[26, 11] = new MapTile(26, 11, TileType.ROCK);
            theMapOriginal[26, 12] = new MapTile(26, 12, TileType.ROCK);
            theMapOriginal[26, 13] = new MapTile(26, 13, TileType.GRASS);
            theMapOriginal[26, 14] = new MapTile(26, 14, TileType.GRASS);
            theMapOriginal[26, 15] = new MapTile(26, 15, TileType.ROCK);
            theMapOriginal[26, 16] = new MapTile(26, 16, TileType.ROCK);
            theMapOriginal[26, 17] = new MapTile(26, 17, TileType.ROCK);
            theMapOriginal[26, 18] = new MapTile(26, 18, TileType.ROCK);
            theMapOriginal[26, 19] = new MapTile(26, 19, TileType.ROCK);
            theMapOriginal[26, 20] = new MapTile(26, 20, TileType.GRASS);
            theMapOriginal[26, 21] = new MapTile(26, 21, TileType.GRASS);
            theMapOriginal[26, 22] = new MapTile(26, 22, TileType.GRASS);
            theMapOriginal[26, 23] = new MapTile(26, 23, TileType.GRASS);
            theMapOriginal[26, 24] = new MapTile(26, 24, TileType.ROCK);
            theMapOriginal[26, 25] = new MapTile(26, 25, TileType.ROCK);
            theMapOriginal[26, 26] = new MapTile(26, 26, TileType.GRASS);
            theMapOriginal[26, 27] = new MapTile(26, 27, TileType.GRASS);
            theMapOriginal[26, 28] = new MapTile(26, 28, TileType.ROCK);
            theMapOriginal[26, 29] = new MapTile(26, 29, TileType.GRASS);

            // X = 27
            theMapOriginal[27, 0] = new MapTile(27, 0, TileType.GRASS);
            theMapOriginal[27, 1] = new MapTile(27, 1, TileType.GRASS);
            theMapOriginal[27, 2] = new MapTile(27, 2, TileType.ROCK);
            theMapOriginal[27, 3] = new MapTile(27, 3, TileType.GRASS);
            theMapOriginal[27, 4] = new MapTile(27, 4, TileType.GRASS);
            theMapOriginal[27, 5] = new MapTile(27, 5, TileType.GRASS);
            theMapOriginal[27, 6] = new MapTile(27, 6, TileType.GRASS);
            theMapOriginal[27, 7] = new MapTile(27, 7, TileType.GRASS);
            theMapOriginal[27, 8] = new MapTile(27, 8, TileType.GRASS);
            theMapOriginal[27, 9] = new MapTile(27, 9, TileType.GRASS);
            theMapOriginal[27, 10] = new MapTile(27, 10, TileType.GRASS);
            theMapOriginal[27, 11] = new MapTile(27, 11, TileType.GRASS);
            theMapOriginal[27, 12] = new MapTile(27, 12, TileType.ROCK);
            theMapOriginal[27, 13] = new MapTile(27, 13, TileType.GRASS);
            theMapOriginal[27, 14] = new MapTile(27, 14, TileType.GRASS);
            theMapOriginal[27, 15] = new MapTile(27, 15, TileType.GRASS);
            theMapOriginal[27, 16] = new MapTile(27, 16, TileType.ROCK);
            theMapOriginal[27, 17] = new MapTile(27, 17, TileType.ROCK);
            theMapOriginal[27, 18] = new MapTile(27, 18, TileType.ROCK);
            theMapOriginal[27, 19] = new MapTile(27, 19, TileType.ROCK);
            theMapOriginal[27, 20] = new MapTile(27, 20, TileType.ROCK);
            theMapOriginal[27, 21] = new MapTile(27, 21, TileType.ROCK);
            theMapOriginal[27, 22] = new MapTile(27, 22, TileType.GRASS);
            theMapOriginal[27, 23] = new MapTile(27, 23, TileType.GRASS);
            theMapOriginal[27, 24] = new MapTile(27, 24, TileType.GRASS);
            theMapOriginal[27, 25] = new MapTile(27, 25, TileType.ROCK);
            theMapOriginal[27, 26] = new MapTile(27, 26, TileType.ROCK);
            theMapOriginal[27, 27] = new MapTile(27, 27, TileType.ROCK);
            theMapOriginal[27, 28] = new MapTile(27, 28, TileType.ROCK);
            theMapOriginal[27, 29] = new MapTile(27, 29, TileType.GRASS);

            // X = 28
            theMapOriginal[28, 0] = new MapTile(28, 0, TileType.GRASS);
            theMapOriginal[28, 1] = new MapTile(28, 1, TileType.GRASS);
            theMapOriginal[28, 2] = new MapTile(28, 2, TileType.ITEMBOX);
            theMapOriginal[28, 3] = new MapTile(28, 3, TileType.ROCK);
            theMapOriginal[28, 4] = new MapTile(28, 4, TileType.ROCK);
            theMapOriginal[28, 5] = new MapTile(28, 5, TileType.ROCK);
            theMapOriginal[28, 6] = new MapTile(28, 6, TileType.GRASS);
            theMapOriginal[28, 7] = new MapTile(28, 7, TileType.GRASS);
            theMapOriginal[28, 8] = new MapTile(28, 8, TileType.GRASS);
            theMapOriginal[28, 9] = new MapTile(28, 9, TileType.GRASS);
            theMapOriginal[28, 10] = new MapTile(28, 10, TileType.GRASS);
            theMapOriginal[28, 11] = new MapTile(28, 11, TileType.GRASS);
            theMapOriginal[28, 12] = new MapTile(28, 12, TileType.GRASS);
            theMapOriginal[28, 13] = new MapTile(28, 13, TileType.GRASS);
            theMapOriginal[28, 14] = new MapTile(28, 14, TileType.GRASS);
            theMapOriginal[28, 15] = new MapTile(28, 15, TileType.GRASS);
            theMapOriginal[28, 16] = new MapTile(28, 16, TileType.GRASS);
            theMapOriginal[28, 17] = new MapTile(28, 17, TileType.GRASS);
            theMapOriginal[28, 18] = new MapTile(28, 18, TileType.GRASS);
            theMapOriginal[28, 19] = new MapTile(28, 19, TileType.GRASS);
            theMapOriginal[28, 20] = new MapTile(28, 20, TileType.ROCK);
            theMapOriginal[28, 21] = new MapTile(28, 21, TileType.ROCK);
            theMapOriginal[28, 22] = new MapTile(28, 22, TileType.ROCK);
            theMapOriginal[28, 23] = new MapTile(28, 23, TileType.GRASS);
            theMapOriginal[28, 24] = new MapTile(28, 24, TileType.GRASS);
            theMapOriginal[28, 25] = new MapTile(28, 25, TileType.GRASS);
            theMapOriginal[28, 26] = new MapTile(28, 26, TileType.GRASS);
            theMapOriginal[28, 27] = new MapTile(28, 27, TileType.GRASS);
            theMapOriginal[28, 28] = new MapTile(28, 28, TileType.GRASS);
            theMapOriginal[28, 29] = new MapTile(28, 29, TileType.GRASS);

            // X = 29
            theMapOriginal[29, 0] = new MapTile(29, 0, TileType.GRASS);
            theMapOriginal[29, 1] = new MapTile(29, 1, TileType.GRASS);
            theMapOriginal[29, 2] = new MapTile(29, 2, TileType.ROCK);
            theMapOriginal[29, 3] = new MapTile(29, 3, TileType.ROCK);
            theMapOriginal[29, 4] = new MapTile(29, 4, TileType.ROCK);
            theMapOriginal[29, 5] = new MapTile(29, 5, TileType.ROCK);
            theMapOriginal[29, 6] = new MapTile(29, 6, TileType.ROCK);
            theMapOriginal[29, 7] = new MapTile(29, 7, TileType.ITEMBOX);
            theMapOriginal[29, 8] = new MapTile(29, 8, TileType.GRASS);
            theMapOriginal[29, 9] = new MapTile(29, 9, TileType.GRASS);
            theMapOriginal[29, 10] = new MapTile(29, 10, TileType.GRASS);
            theMapOriginal[29, 11] = new MapTile(29, 11, TileType.GRASS);
            theMapOriginal[29, 12] = new MapTile(29, 12, TileType.GRASS);
            theMapOriginal[29, 13] = new MapTile(29, 13, TileType.GRASS);
            theMapOriginal[29, 14] = new MapTile(29, 14, TileType.GRASS);
            theMapOriginal[29, 15] = new MapTile(29, 15, TileType.GRASS);
            theMapOriginal[29, 16] = new MapTile(29, 16, TileType.GRASS);
            theMapOriginal[29, 17] = new MapTile(29, 17, TileType.GRASS);
            theMapOriginal[29, 18] = new MapTile(29, 18, TileType.GRASS);
            theMapOriginal[29, 19] = new MapTile(29, 19, TileType.GRASS);
            theMapOriginal[29, 20] = new MapTile(29, 20, TileType.GRASS);
            theMapOriginal[29, 21] = new MapTile(29, 21, TileType.ROCK);
            theMapOriginal[29, 22] = new MapTile(29, 22, TileType.ROCK);
            theMapOriginal[29, 23] = new MapTile(29, 23, TileType.ITEMBOX);
            theMapOriginal[29, 24] = new MapTile(29, 24, TileType.GRASS);
            theMapOriginal[29, 25] = new MapTile(29, 25, TileType.GRASS);
            theMapOriginal[29, 26] = new MapTile(29, 26, TileType.GRASS);
            theMapOriginal[29, 27] = new MapTile(29, 27, TileType.GRASS);
            theMapOriginal[29, 28] = new MapTile(29, 28, TileType.GRASS);
            theMapOriginal[29, 29] = new MapTile(29, 29, TileType.SAFEZONE);


            //create working copy
            // X = 0
            theMap[0, 0] = new MapTile(0, 0, TileType.START);
            theMap[0, 0].setIsVisible(true);
            theMap[0, 1] = new MapTile(0, 1, TileType.GRASS);
            theMap[0, 1].setIsVisible(true);
            theMap[0, 2] = new MapTile(0, 2, TileType.GRASS);
            theMap[0, 2].setIsVisible(true);
            theMap[0, 3] = new MapTile(0, 3, TileType.GRASS);
            theMap[0, 4] = new MapTile(0, 4, TileType.GRASS);
            theMap[0, 5] = new MapTile(0, 5, TileType.GRASS);
            theMap[0, 6] = new MapTile(0, 6, TileType.GRASS);
            theMap[0, 7] = new MapTile(0, 7, TileType.GRASS);
            theMap[0, 8] = new MapTile(0, 8, TileType.ROCK);
            theMap[0, 9] = new MapTile(0, 9, TileType.ROCK);
            theMap[0, 10] = new MapTile(0, 10, TileType.ROCK);
            theMap[0, 11] = new MapTile(0, 11, TileType.ROCK);
            theMap[0, 12] = new MapTile(0, 12, TileType.GRASS);
            theMap[0, 13] = new MapTile(0, 13, TileType.GRASS);
            theMap[0, 14] = new MapTile(0, 14, TileType.GRASS);
            theMap[0, 15] = new MapTile(0, 15, TileType.GRASS);
            theMap[0, 16] = new MapTile(0, 16, TileType.GRASS);
            theMap[0, 17] = new MapTile(0, 17, TileType.GRASS);
            theMap[0, 18] = new MapTile(0, 18, TileType.GRASS);
            theMap[0, 19] = new MapTile(0, 19, TileType.GRASS);
            theMap[0, 20] = new MapTile(0, 20, TileType.GRASS);
            theMap[0, 21] = new MapTile(0, 21, TileType.GRASS);
            theMap[0, 22] = new MapTile(0, 22, TileType.GRASS);
            theMap[0, 23] = new MapTile(0, 23, TileType.GRASS);
            theMap[0, 24] = new MapTile(0, 24, TileType.GRASS);
            theMap[0, 25] = new MapTile(0, 25, TileType.GRASS);
            theMap[0, 26] = new MapTile(0, 26, TileType.GRASS);
            theMap[0, 27] = new MapTile(0, 27, TileType.GRASS);
            theMap[0, 28] = new MapTile(0, 28, TileType.GRASS);
            theMap[0, 29] = new MapTile(0, 29, TileType.GRASS);

            // X = 1
            theMap[1, 0] = new MapTile(1, 0, TileType.GRASS);
            theMap[1, 0].setIsVisible(true);
            theMap[1, 1] = new MapTile(1, 1, TileType.GRASS);
            theMap[1, 1].setIsVisible(true);
            theMap[1, 2] = new MapTile(1, 2, TileType.GRASS);
            theMap[1, 3] = new MapTile(1, 3, TileType.GRASS);
            theMap[1, 4] = new MapTile(1, 4, TileType.GRASS);
            theMap[1, 5] = new MapTile(1, 5, TileType.GRASS);
            theMap[1, 6] = new MapTile(1, 6, TileType.GRASS);
            theMap[1, 7] = new MapTile(1, 7, TileType.GRASS);
            theMap[1, 8] = new MapTile(1, 8, TileType.GRASS);
            theMap[1, 9] = new MapTile(1, 9, TileType.ROCK);
            theMap[1, 10] = new MapTile(1, 10, TileType.ROCK);
            theMap[1, 11] = new MapTile(1, 11, TileType.GRASS);
            theMap[1, 12] = new MapTile(1, 12, TileType.GRASS);
            theMap[1, 13] = new MapTile(1, 13, TileType.GRASS);
            theMap[1, 14] = new MapTile(1, 14, TileType.GRASS);
            theMap[1, 15] = new MapTile(1, 15, TileType.GRASS);
            theMap[1, 16] = new MapTile(1, 16, TileType.GRASS);
            theMap[1, 17] = new MapTile(1, 17, TileType.GRASS);
            theMap[1, 18] = new MapTile(1, 18, TileType.GRASS);
            theMap[1, 19] = new MapTile(1, 19, TileType.GRASS);
            theMap[1, 20] = new MapTile(1, 20, TileType.GRASS);
            theMap[1, 21] = new MapTile(1, 21, TileType.GRASS);
            theMap[1, 22] = new MapTile(1, 22, TileType.GRASS);
            theMap[1, 23] = new MapTile(1, 23, TileType.GRASS);
            theMap[1, 24] = new MapTile(1, 24, TileType.GRASS);
            theMap[1, 25] = new MapTile(1, 25, TileType.GRASS);
            theMap[1, 26] = new MapTile(1, 26, TileType.GRASS);
            theMap[1, 27] = new MapTile(1, 27, TileType.GRASS);
            theMap[1, 28] = new MapTile(1, 28, TileType.GRASS);
            theMap[1, 29] = new MapTile(1, 29, TileType.GRASS);

            // X = 2
            theMap[2, 0] = new MapTile(2, 0, TileType.GRASS);
            theMap[2, 0].setIsVisible(true);
            theMap[2, 1] = new MapTile(2, 1, TileType.GRASS);
            theMap[2, 2] = new MapTile(2, 2, TileType.GRASS);
            theMap[2, 3] = new MapTile(2, 3, TileType.GRASS);
            theMap[2, 4] = new MapTile(2, 4, TileType.GRASS);
            theMap[2, 5] = new MapTile(2, 5, TileType.GRASS);
            theMap[2, 6] = new MapTile(2, 6, TileType.GRASS);
            theMap[2, 7] = new MapTile(2, 7, TileType.GRASS);
            theMap[2, 8] = new MapTile(2, 8, TileType.GRASS);
            theMap[2, 9] = new MapTile(2, 9, TileType.GRASS);
            theMap[2, 10] = new MapTile(2, 10, TileType.GRASS);
            theMap[2, 11] = new MapTile(2, 11, TileType.GRASS);
            theMap[2, 12] = new MapTile(2, 12, TileType.GRASS);
            theMap[2, 13] = new MapTile(2, 13, TileType.ROCK);
            theMap[2, 14] = new MapTile(2, 14, TileType.ROCK);
            theMap[2, 15] = new MapTile(2, 15, TileType.ROCK);
            theMap[2, 16] = new MapTile(2, 16, TileType.ROCK);
            theMap[2, 17] = new MapTile(2, 17, TileType.GRASS);
            theMap[2, 18] = new MapTile(2, 18, TileType.GRASS);
            theMap[2, 19] = new MapTile(2, 19, TileType.GRASS);
            theMap[2, 20] = new MapTile(2, 20, TileType.GRASS);
            theMap[2, 21] = new MapTile(2, 21, TileType.GRASS);
            theMap[2, 22] = new MapTile(2, 22, TileType.GRASS);
            theMap[2, 23] = new MapTile(2, 23, TileType.GRASS);
            theMap[2, 24] = new MapTile(2, 24, TileType.GRASS);
            theMap[2, 25] = new MapTile(2, 25, TileType.GRASS);
            theMap[2, 26] = new MapTile(2, 26, TileType.GRASS);
            theMap[2, 27] = new MapTile(2, 27, TileType.GRASS);
            theMap[2, 28] = new MapTile(2, 28, TileType.GRASS);
            theMap[2, 29] = new MapTile(2, 29, TileType.GRASS);

            // X = 3
            theMap[3, 0] = new MapTile(3, 0, TileType.GRASS);
            theMap[3, 1] = new MapTile(3, 1, TileType.GRASS);
            theMap[3, 2] = new MapTile(3, 2, TileType.GRASS);
            theMap[3, 3] = new MapTile(3, 3, TileType.GRASS);
            theMap[3, 4] = new MapTile(3, 4, TileType.GRASS);
            theMap[3, 5] = new MapTile(3, 5, TileType.ROCK);
            theMap[3, 6] = new MapTile(3, 6, TileType.ROCK);
            theMap[3, 7] = new MapTile(3, 7, TileType.GRASS);
            theMap[3, 8] = new MapTile(3, 8, TileType.GRASS);
            theMap[3, 9] = new MapTile(3, 9, TileType.GRASS);
            theMap[3, 10] = new MapTile(3, 10, TileType.GRASS);
            theMap[3, 11] = new MapTile(3, 11, TileType.GRASS);
            theMap[3, 12] = new MapTile(3, 12, TileType.ROCK);
            theMap[3, 13] = new MapTile(3, 13, TileType.ROCK);
            theMap[3, 14] = new MapTile(3, 14, TileType.ROCK);
            theMap[3, 15] = new MapTile(3, 15, TileType.ROCK);
            theMap[3, 16] = new MapTile(3, 16, TileType.ROCK);
            theMap[3, 17] = new MapTile(3, 17, TileType.ROCK);
            theMap[3, 18] = new MapTile(3, 18, TileType.GRASS);
            theMap[3, 19] = new MapTile(3, 19, TileType.GRASS);
            theMap[3, 20] = new MapTile(3, 20, TileType.GRASS);
            theMap[3, 21] = new MapTile(3, 21, TileType.GRASS);
            theMap[3, 22] = new MapTile(3, 22, TileType.GRASS);
            theMap[3, 23] = new MapTile(3, 23, TileType.GRASS);
            theMap[3, 24] = new MapTile(3, 24, TileType.GRASS);
            theMap[3, 25] = new MapTile(3, 25, TileType.ROCK);
            theMap[3, 26] = new MapTile(3, 26, TileType.ROCK);
            theMap[3, 27] = new MapTile(3, 27, TileType.GRASS);
            theMap[3, 28] = new MapTile(3, 28, TileType.GRASS);
            theMap[3, 29] = new MapTile(3, 29, TileType.GRASS);

            // X = 4
            theMap[4, 0] = new MapTile(4, 0, TileType.ROCK);
            theMap[4, 1] = new MapTile(4, 1, TileType.GRASS);
            theMap[4, 2] = new MapTile(4, 2, TileType.GRASS);
            theMap[4, 3] = new MapTile(4, 3, TileType.GRASS);
            theMap[4, 4] = new MapTile(4, 4, TileType.GRASS);
            theMap[4, 5] = new MapTile(4, 5, TileType.ROCK);
            theMap[4, 6] = new MapTile(4, 6, TileType.ROCK);
            theMap[4, 7] = new MapTile(4, 7, TileType.GRASS);
            theMap[4, 8] = new MapTile(4, 8, TileType.GRASS);
            theMap[4, 9] = new MapTile(4, 9, TileType.GRASS);
            theMap[4, 10] = new MapTile(4, 10, TileType.GRASS);
            theMap[4, 11] = new MapTile(4, 11, TileType.GRASS);
            theMap[4, 12] = new MapTile(4, 12, TileType.ROCK);
            theMap[4, 13] = new MapTile(4, 13, TileType.ROCK);
            theMap[4, 14] = new MapTile(4, 14, TileType.GRASS);
            theMap[4, 15] = new MapTile(4, 15, TileType.GRASS);
            theMap[4, 16] = new MapTile(4, 16, TileType.GRASS);
            theMap[4, 17] = new MapTile(4, 17, TileType.GRASS);
            theMap[4, 18] = new MapTile(4, 18, TileType.GRASS);
            theMap[4, 19] = new MapTile(4, 19, TileType.GRASS);
            theMap[4, 20] = new MapTile(4, 20, TileType.GRASS);
            theMap[4, 21] = new MapTile(4, 21, TileType.GRASS);
            theMap[4, 22] = new MapTile(4, 22, TileType.GRASS);
            theMap[4, 23] = new MapTile(4, 23, TileType.GRASS);
            theMap[4, 24] = new MapTile(4, 24, TileType.ROCK);
            theMap[4, 25] = new MapTile(4, 25, TileType.ROCK);
            theMap[4, 26] = new MapTile(4, 26, TileType.ITEMBOX);
            theMap[4, 27] = new MapTile(4, 27, TileType.GRASS);
            theMap[4, 28] = new MapTile(4, 28, TileType.GRASS);
            theMap[4, 29] = new MapTile(4, 29, TileType.GRASS);

            // X = 5
            theMap[5, 0] = new MapTile(5, 0, TileType.ROCK);
            theMap[5, 1] = new MapTile(5, 1, TileType.ROCK);
            theMap[5, 2] = new MapTile(5, 2, TileType.GRASS);
            theMap[5, 3] = new MapTile(5, 3, TileType.GRASS);
            theMap[5, 4] = new MapTile(5, 4, TileType.GRASS);
            theMap[5, 5] = new MapTile(5, 5, TileType.ROCK);
            theMap[5, 6] = new MapTile(5, 6, TileType.ROCK);
            theMap[5, 7] = new MapTile(5, 7, TileType.GRASS);
            theMap[5, 8] = new MapTile(5, 8, TileType.GRASS);
            theMap[5, 9] = new MapTile(5, 9, TileType.GRASS);
            theMap[5, 10] = new MapTile(5, 10, TileType.GRASS);
            theMap[5, 11] = new MapTile(5, 11, TileType.GRASS);
            theMap[5, 12] = new MapTile(5, 12, TileType.ITEMBOX);
            theMap[5, 13] = new MapTile(5, 13, TileType.ROCK);
            theMap[5, 14] = new MapTile(5, 14, TileType.ROCK);
            theMap[5, 15] = new MapTile(5, 15, TileType.GRASS);
            theMap[5, 16] = new MapTile(5, 16, TileType.GRASS);
            theMap[5, 17] = new MapTile(5, 17, TileType.GRASS);
            theMap[5, 18] = new MapTile(5, 18, TileType.GRASS);
            theMap[5, 19] = new MapTile(5, 19, TileType.GRASS);
            theMap[5, 20] = new MapTile(5, 20, TileType.GRASS);
            theMap[5, 21] = new MapTile(5, 21, TileType.GRASS);
            theMap[5, 22] = new MapTile(5, 22, TileType.ROCK);
            theMap[5, 23] = new MapTile(5, 23, TileType.ROCK);
            theMap[5, 24] = new MapTile(5, 24, TileType.ROCK);
            theMap[5, 25] = new MapTile(5, 25, TileType.ROCK);
            theMap[5, 26] = new MapTile(5, 26, TileType.ROCK);
            theMap[5, 27] = new MapTile(5, 27, TileType.GRASS);
            theMap[5, 28] = new MapTile(5, 28, TileType.GRASS);
            theMap[5, 29] = new MapTile(5, 29, TileType.GRASS);

            // X = 6
            theMap[6, 0] = new MapTile(6, 0, TileType.ROCK);
            theMap[6, 1] = new MapTile(6, 1, TileType.ROCK);
            theMap[6, 2] = new MapTile(6, 2, TileType.GRASS);
            theMap[6, 3] = new MapTile(6, 3, TileType.GRASS);
            theMap[6, 4] = new MapTile(6, 4, TileType.ROCK);
            theMap[6, 5] = new MapTile(6, 5, TileType.ROCK);
            theMap[6, 6] = new MapTile(6, 6, TileType.ROCK);
            theMap[6, 7] = new MapTile(6, 7, TileType.GRASS);
            theMap[6, 8] = new MapTile(6, 8, TileType.GRASS);
            theMap[6, 9] = new MapTile(6, 9, TileType.GRASS);
            theMap[6, 10] = new MapTile(6, 10, TileType.GRASS);
            theMap[6, 11] = new MapTile(6, 11, TileType.GRASS);
            theMap[6, 12] = new MapTile(6, 12, TileType.GRASS);
            theMap[6, 13] = new MapTile(6, 13, TileType.ROCK);
            theMap[6, 14] = new MapTile(6, 14, TileType.ROCK);
            theMap[6, 15] = new MapTile(6, 15, TileType.ROCK);
            theMap[6, 16] = new MapTile(6, 16, TileType.GRASS);
            theMap[6, 17] = new MapTile(6, 17, TileType.GRASS);
            theMap[6, 18] = new MapTile(6, 18, TileType.GRASS);
            theMap[6, 19] = new MapTile(6, 19, TileType.GRASS);
            theMap[6, 20] = new MapTile(6, 20, TileType.GRASS);
            theMap[6, 21] = new MapTile(6, 21, TileType.GRASS);
            theMap[6, 22] = new MapTile(6, 22, TileType.ROCK);
            theMap[6, 23] = new MapTile(6, 23, TileType.ROCK);
            theMap[6, 24] = new MapTile(6, 24, TileType.ROCK);
            theMap[6, 25] = new MapTile(6, 25, TileType.ROCK);
            theMap[6, 26] = new MapTile(6, 26, TileType.ROCK);
            theMap[6, 27] = new MapTile(6, 27, TileType.GRASS);
            theMap[6, 28] = new MapTile(6, 28, TileType.GRASS);
            theMap[6, 29] = new MapTile(6, 29, TileType.GRASS);

            // X = 7
            theMap[7, 0] = new MapTile(7, 0, TileType.GRASS);
            theMap[7, 1] = new MapTile(7, 1, TileType.ITEMBOX);
            theMap[7, 2] = new MapTile(7, 2, TileType.GRASS);
            theMap[7, 3] = new MapTile(7, 3, TileType.ROCK);
            theMap[7, 4] = new MapTile(7, 4, TileType.ROCK);
            theMap[7, 5] = new MapTile(7, 5, TileType.ROCK);
            theMap[7, 6] = new MapTile(7, 6, TileType.GRASS);
            theMap[7, 7] = new MapTile(7, 7, TileType.GRASS);
            theMap[7, 8] = new MapTile(7, 8, TileType.GRASS);
            theMap[7, 9] = new MapTile(7, 9, TileType.GRASS);
            theMap[7, 10] = new MapTile(7, 10, TileType.GRASS);
            theMap[7, 11] = new MapTile(7, 11, TileType.GRASS);
            theMap[7, 12] = new MapTile(7, 12, TileType.GRASS);
            theMap[7, 13] = new MapTile(7, 13, TileType.GRASS);
            theMap[7, 14] = new MapTile(7, 14, TileType.GRASS);
            theMap[7, 15] = new MapTile(7, 15, TileType.GRASS);
            theMap[7, 16] = new MapTile(7, 16, TileType.GRASS);
            theMap[7, 17] = new MapTile(7, 17, TileType.GRASS);
            theMap[7, 18] = new MapTile(7, 18, TileType.GRASS);
            theMap[7, 19] = new MapTile(7, 19, TileType.GRASS);
            theMap[7, 20] = new MapTile(7, 20, TileType.GRASS);
            theMap[7, 21] = new MapTile(7, 21, TileType.ROCK);
            theMap[7, 22] = new MapTile(7, 22, TileType.WATER);
            theMap[7, 23] = new MapTile(7, 23, TileType.WATER);
            theMap[7, 24] = new MapTile(7, 24, TileType.WATER);
            theMap[7, 25] = new MapTile(7, 25, TileType.ROCK);
            theMap[7, 26] = new MapTile(7, 26, TileType.ROCK);
            theMap[7, 27] = new MapTile(7, 27, TileType.ROCK);
            theMap[7, 28] = new MapTile(7, 28, TileType.GRASS);
            theMap[7, 29] = new MapTile(7, 29, TileType.GRASS);

            // X = 8
            theMap[8, 0] = new MapTile(8, 0, TileType.GRASS);
            theMap[8, 1] = new MapTile(8, 1, TileType.GRASS);
            theMap[8, 2] = new MapTile(8, 2, TileType.GRASS);
            theMap[8, 3] = new MapTile(8, 3, TileType.ROCK);
            theMap[8, 4] = new MapTile(8, 4, TileType.ROCK);
            theMap[8, 5] = new MapTile(8, 5, TileType.ROCK);
            theMap[8, 6] = new MapTile(8, 6, TileType.GRASS);
            theMap[8, 7] = new MapTile(8, 7, TileType.GRASS);
            theMap[8, 8] = new MapTile(8, 8, TileType.GRASS);
            theMap[8, 9] = new MapTile(8, 9, TileType.GRASS);
            theMap[8, 10] = new MapTile(8, 10, TileType.GRASS);
            theMap[8, 11] = new MapTile(8, 11, TileType.GRASS);
            theMap[8, 12] = new MapTile(8, 12, TileType.GRASS);
            theMap[8, 13] = new MapTile(8, 13, TileType.GRASS);
            theMap[8, 14] = new MapTile(8, 14, TileType.GRASS);
            theMap[8, 15] = new MapTile(8, 15, TileType.GRASS);
            theMap[8, 16] = new MapTile(8, 16, TileType.GRASS);
            theMap[8, 17] = new MapTile(8, 17, TileType.GRASS);
            theMap[8, 18] = new MapTile(8, 18, TileType.GRASS);
            theMap[8, 19] = new MapTile(8, 19, TileType.GRASS);
            theMap[8, 20] = new MapTile(8, 20, TileType.GRASS);
            theMap[8, 21] = new MapTile(8, 21, TileType.GRASS);
            theMap[8, 22] = new MapTile(8, 22, TileType.GRASS);
            theMap[8, 23] = new MapTile(8, 23, TileType.WATER);
            theMap[8, 24] = new MapTile(8, 24, TileType.WATER);
            theMap[8, 25] = new MapTile(8, 25, TileType.WATER);
            theMap[8, 26] = new MapTile(8, 26, TileType.ROCK);
            theMap[8, 27] = new MapTile(8, 27, TileType.ROCK);
            theMap[8, 28] = new MapTile(8, 28, TileType.ROCK);
            theMap[8, 29] = new MapTile(8, 29, TileType.GRASS);

            // X = 9
            theMap[9, 0] = new MapTile(9, 0, TileType.GRASS);
            theMap[9, 1] = new MapTile(9, 1, TileType.GRASS);
            theMap[9, 2] = new MapTile(9, 2, TileType.GRASS);
            theMap[9, 3] = new MapTile(9, 3, TileType.ROCK);
            theMap[9, 4] = new MapTile(9, 4, TileType.ROCK);
            theMap[9, 5] = new MapTile(9, 5, TileType.GRASS);
            theMap[9, 6] = new MapTile(9, 6, TileType.GRASS);
            theMap[9, 7] = new MapTile(9, 7, TileType.GRASS);
            theMap[9, 8] = new MapTile(9, 8, TileType.GRASS);
            theMap[9, 9] = new MapTile(9, 9, TileType.GRASS);
            theMap[9, 10] = new MapTile(9, 10, TileType.WATER);
            theMap[9, 11] = new MapTile(9, 11, TileType.WATER);
            theMap[9, 12] = new MapTile(9, 12, TileType.WATER);
            theMap[9, 13] = new MapTile(9, 13, TileType.WATER);
            theMap[9, 14] = new MapTile(9, 14, TileType.GRASS);
            theMap[9, 15] = new MapTile(9, 15, TileType.GRASS);
            theMap[9, 16] = new MapTile(9, 16, TileType.GRASS);
            theMap[9, 17] = new MapTile(9, 17, TileType.GRASS);
            theMap[9, 18] = new MapTile(9, 18, TileType.ROCK);
            theMap[9, 19] = new MapTile(9, 19, TileType.ROCK);
            theMap[9, 20] = new MapTile(9, 20, TileType.GRASS);
            theMap[9, 21] = new MapTile(9, 21, TileType.GRASS);
            theMap[9, 22] = new MapTile(9, 22, TileType.GRASS);
            theMap[9, 23] = new MapTile(9, 23, TileType.GRASS);
            theMap[9, 24] = new MapTile(9, 24, TileType.WATER);
            theMap[9, 25] = new MapTile(9, 25, TileType.WATER);
            theMap[9, 26] = new MapTile(9, 26, TileType.WATER);
            theMap[9, 27] = new MapTile(9, 27, TileType.ROCK);
            theMap[9, 28] = new MapTile(9, 28, TileType.ROCK);
            theMap[9, 29] = new MapTile(9, 29, TileType.GRASS);

            // X = 10
            theMap[10, 0] = new MapTile(10, 0, TileType.GRASS);
            theMap[10, 1] = new MapTile(10, 1, TileType.GRASS);
            theMap[10, 2] = new MapTile(10, 2, TileType.ROCK);
            theMap[10, 3] = new MapTile(10, 3, TileType.ROCK);
            theMap[10, 4] = new MapTile(10, 4, TileType.GRASS);
            theMap[10, 5] = new MapTile(10, 5, TileType.GRASS);
            theMap[10, 6] = new MapTile(10, 6, TileType.GRASS);
            theMap[10, 7] = new MapTile(10, 7, TileType.GRASS);
            theMap[10, 8] = new MapTile(10, 8, TileType.GRASS);
            theMap[10, 9] = new MapTile(10, 9, TileType.GRASS);
            theMap[10, 10] = new MapTile(10, 10, TileType.WATER);
            theMap[10, 11] = new MapTile(10, 11, TileType.WATER);
            theMap[10, 12] = new MapTile(10, 12, TileType.WATER);
            theMap[10, 13] = new MapTile(10, 13, TileType.WATER);
            theMap[10, 14] = new MapTile(10, 14, TileType.WATER);
            theMap[10, 15] = new MapTile(10, 15, TileType.ROCK);
            theMap[10, 16] = new MapTile(10, 16, TileType.ROCK);
            theMap[10, 17] = new MapTile(10, 17, TileType.ROCK);
            theMap[10, 18] = new MapTile(10, 18, TileType.ROCK);
            theMap[10, 19] = new MapTile(10, 19, TileType.ROCK);
            theMap[10, 20] = new MapTile(10, 20, TileType.GRASS);
            theMap[10, 21] = new MapTile(10, 21, TileType.GRASS);
            theMap[10, 22] = new MapTile(10, 22, TileType.GRASS);
            theMap[10, 23] = new MapTile(10, 23, TileType.GRASS);
            theMap[10, 24] = new MapTile(10, 24, TileType.WATER);
            theMap[10, 25] = new MapTile(10, 25, TileType.WATER);
            theMap[10, 26] = new MapTile(10, 26, TileType.WATER);
            theMap[10, 27] = new MapTile(10, 27, TileType.ROCK);
            theMap[10, 28] = new MapTile(10, 28, TileType.ROCK);
            theMap[10, 29] = new MapTile(10, 29, TileType.ROCK);

            // X = 11
            theMap[11, 0] = new MapTile(11, 0, TileType.GRASS);
            theMap[11, 1] = new MapTile(11, 1, TileType.GRASS);
            theMap[11, 2] = new MapTile(11, 2, TileType.GRASS);
            theMap[11, 3] = new MapTile(11, 3, TileType.GRASS);
            theMap[11, 4] = new MapTile(11, 4, TileType.GRASS);
            theMap[11, 5] = new MapTile(11, 5, TileType.GRASS);
            theMap[11, 6] = new MapTile(11, 6, TileType.GRASS);
            theMap[11, 7] = new MapTile(11, 7, TileType.GRASS);
            theMap[11, 8] = new MapTile(11, 8, TileType.GRASS);
            theMap[11, 9] = new MapTile(11, 9, TileType.WATER);
            theMap[11, 10] = new MapTile(11, 10, TileType.WATER);
            theMap[11, 11] = new MapTile(11, 11, TileType.WATER);
            theMap[11, 12] = new MapTile(11, 12, TileType.WATER);
            theMap[11, 13] = new MapTile(11, 13, TileType.WATER);
            theMap[11, 14] = new MapTile(11, 14, TileType.WATER);
            theMap[11, 15] = new MapTile(11, 15, TileType.WATER);
            theMap[11, 16] = new MapTile(11, 16, TileType.ROCK);
            theMap[11, 17] = new MapTile(11, 17, TileType.ROCK);
            theMap[11, 18] = new MapTile(11, 18, TileType.ROCK);
            theMap[11, 19] = new MapTile(11, 19, TileType.ROCK);
            theMap[11, 20] = new MapTile(11, 20, TileType.GRASS);
            theMap[11, 21] = new MapTile(11, 21, TileType.GRASS);
            theMap[11, 22] = new MapTile(11, 22, TileType.GRASS);
            theMap[11, 23] = new MapTile(11, 23, TileType.WATER);
            theMap[11, 24] = new MapTile(11, 24, TileType.WATER);
            theMap[11, 25] = new MapTile(11, 25, TileType.WATER);
            theMap[11, 26] = new MapTile(11, 26, TileType.WATER);
            theMap[11, 27] = new MapTile(11, 27, TileType.WATER);
            theMap[11, 28] = new MapTile(11, 28, TileType.ROCK);
            theMap[11, 29] = new MapTile(11, 29, TileType.ROCK);

            // X = 12
            theMap[12, 0] = new MapTile(12, 0, TileType.GRASS);
            theMap[12, 1] = new MapTile(12, 1, TileType.GRASS);
            theMap[12, 2] = new MapTile(12, 2, TileType.GRASS);
            theMap[12, 3] = new MapTile(12, 3, TileType.GRASS);
            theMap[12, 4] = new MapTile(12, 4, TileType.GRASS);
            theMap[12, 5] = new MapTile(12, 5, TileType.GRASS);
            theMap[12, 6] = new MapTile(12, 6, TileType.GRASS);
            theMap[12, 7] = new MapTile(12, 7, TileType.GRASS);
            theMap[12, 8] = new MapTile(12, 8, TileType.WATER);
            theMap[12, 9] = new MapTile(12, 9, TileType.WATER);
            theMap[12, 10] = new MapTile(12, 10, TileType.WATER);
            theMap[12, 11] = new MapTile(12, 11, TileType.WATER);
            theMap[12, 12] = new MapTile(12, 12, TileType.WATER);
            theMap[12, 13] = new MapTile(12, 13, TileType.WATER);
            theMap[12, 14] = new MapTile(12, 14, TileType.WATER);
            theMap[12, 15] = new MapTile(12, 15, TileType.WATER);
            theMap[12, 16] = new MapTile(12, 16, TileType.WATER);
            theMap[12, 17] = new MapTile(12, 17, TileType.ROCK);
            theMap[12, 18] = new MapTile(12, 18, TileType.ROCK);
            theMap[12, 19] = new MapTile(12, 19, TileType.GRASS);
            theMap[12, 20] = new MapTile(12, 20, TileType.GRASS);
            theMap[12, 21] = new MapTile(12, 21, TileType.GRASS);
            theMap[12, 22] = new MapTile(12, 22, TileType.WATER);
            theMap[12, 23] = new MapTile(12, 23, TileType.WATER);
            theMap[12, 24] = new MapTile(12, 24, TileType.WATER);
            theMap[12, 25] = new MapTile(12, 25, TileType.WATER);
            theMap[12, 26] = new MapTile(12, 26, TileType.WATER);
            theMap[12, 27] = new MapTile(12, 27, TileType.WATER);
            theMap[12, 28] = new MapTile(12, 28, TileType.ROCK);
            theMap[12, 29] = new MapTile(12, 29, TileType.ROCK);

            // X = 13
            theMap[13, 0] = new MapTile(13, 0, TileType.GRASS);
            theMap[13, 1] = new MapTile(13, 1, TileType.GRASS);
            theMap[13, 2] = new MapTile(13, 2, TileType.GRASS);
            theMap[13, 3] = new MapTile(13, 3, TileType.GRASS);
            theMap[13, 4] = new MapTile(13, 4, TileType.GRASS);
            theMap[13, 5] = new MapTile(13, 5, TileType.GRASS);
            theMap[13, 6] = new MapTile(13, 6, TileType.GRASS);
            theMap[13, 7] = new MapTile(13, 7, TileType.WATER);
            theMap[13, 8] = new MapTile(13, 8, TileType.WATER);
            theMap[13, 9] = new MapTile(13, 9, TileType.WATER);
            theMap[13, 10] = new MapTile(13, 10, TileType.WATER);
            theMap[13, 11] = new MapTile(13, 11, TileType.WATER);
            theMap[13, 12] = new MapTile(13, 12, TileType.WATER);
            theMap[13, 13] = new MapTile(13, 13, TileType.WATER);
            theMap[13, 14] = new MapTile(13, 14, TileType.WATER);
            theMap[13, 15] = new MapTile(13, 15, TileType.WATER);
            theMap[13, 16] = new MapTile(13, 16, TileType.WATER);
            theMap[13, 17] = new MapTile(13, 17, TileType.ROCK);
            theMap[13, 18] = new MapTile(13, 18, TileType.GRASS);
            theMap[13, 19] = new MapTile(13, 19, TileType.GRASS);
            theMap[13, 20] = new MapTile(13, 20, TileType.GRASS);
            theMap[13, 21] = new MapTile(13, 21, TileType.GRASS);
            theMap[13, 22] = new MapTile(13, 22, TileType.GRASS);
            theMap[13, 23] = new MapTile(13, 23, TileType.ITEMBOX);
            theMap[13, 24] = new MapTile(13, 24, TileType.WATER);
            theMap[13, 25] = new MapTile(13, 25, TileType.WATER);
            theMap[13, 26] = new MapTile(13, 26, TileType.WATER);
            theMap[13, 27] = new MapTile(13, 27, TileType.WATER);
            theMap[13, 28] = new MapTile(13, 28, TileType.ROCK);
            theMap[13, 29] = new MapTile(13, 29, TileType.ROCK);

            // X = 14
            theMap[14, 0] = new MapTile(14, 0, TileType.GRASS);
            theMap[14, 1] = new MapTile(14, 1, TileType.GRASS);
            theMap[14, 2] = new MapTile(14, 2, TileType.GRASS);
            theMap[14, 3] = new MapTile(14, 3, TileType.GRASS);
            theMap[14, 4] = new MapTile(14, 4, TileType.ITEMBOX);
            theMap[14, 5] = new MapTile(14, 5, TileType.GRASS);
            theMap[14, 6] = new MapTile(14, 6, TileType.GRASS);
            theMap[14, 7] = new MapTile(14, 7, TileType.WATER);
            theMap[14, 8] = new MapTile(14, 8, TileType.WATER);
            theMap[14, 9] = new MapTile(14, 9, TileType.WATER);
            theMap[14, 10] = new MapTile(14, 10, TileType.WATER);
            theMap[14, 11] = new MapTile(14, 11, TileType.WATER);
            theMap[14, 12] = new MapTile(14, 12, TileType.WATER);
            theMap[14, 13] = new MapTile(14, 13, TileType.WATER);
            theMap[14, 14] = new MapTile(14, 14, TileType.WATER);
            theMap[14, 15] = new MapTile(14, 15, TileType.WATER);
            theMap[14, 16] = new MapTile(14, 16, TileType.WATER);
            theMap[14, 17] = new MapTile(14, 17, TileType.WATER);
            theMap[14, 18] = new MapTile(14, 18, TileType.GRASS);
            theMap[14, 19] = new MapTile(14, 19, TileType.GRASS);
            theMap[14, 20] = new MapTile(14, 20, TileType.GRASS);
            theMap[14, 21] = new MapTile(14, 21, TileType.GRASS);
            theMap[14, 22] = new MapTile(14, 22, TileType.GRASS);
            theMap[14, 23] = new MapTile(14, 23, TileType.GRASS);
            theMap[14, 24] = new MapTile(14, 24, TileType.GRASS);
            theMap[14, 25] = new MapTile(14, 25, TileType.GRASS);
            theMap[14, 26] = new MapTile(14, 26, TileType.GRASS);
            theMap[14, 27] = new MapTile(14, 27, TileType.ROCK);
            theMap[14, 28] = new MapTile(14, 28, TileType.ROCK);
            theMap[14, 29] = new MapTile(14, 29, TileType.ROCK);

            // X = 15
            theMap[15, 0] = new MapTile(15, 0, TileType.ITEMBOX);
            theMap[15, 1] = new MapTile(15, 1, TileType.ROCK);
            theMap[15, 2] = new MapTile(15, 2, TileType.GRASS);
            theMap[15, 3] = new MapTile(15, 3, TileType.GRASS);
            theMap[15, 4] = new MapTile(15, 4, TileType.GRASS);
            theMap[15, 5] = new MapTile(15, 5, TileType.GRASS);
            theMap[15, 6] = new MapTile(15, 6, TileType.GRASS);
            theMap[15, 7] = new MapTile(15, 7, TileType.GRASS);
            theMap[15, 8] = new MapTile(15, 8, TileType.WATER);
            theMap[15, 9] = new MapTile(15, 9, TileType.WATER);
            theMap[15, 10] = new MapTile(15, 10, TileType.WATER);
            theMap[15, 11] = new MapTile(15, 11, TileType.WATER);
            theMap[15, 12] = new MapTile(15, 12, TileType.WATER);
            theMap[15, 13] = new MapTile(15, 13, TileType.WATER);
            theMap[15, 14] = new MapTile(15, 14, TileType.WATER);
            theMap[15, 15] = new MapTile(15, 15, TileType.WATER);
            theMap[15, 16] = new MapTile(15, 16, TileType.WATER);
            theMap[15, 17] = new MapTile(15, 17, TileType.WATER);
            theMap[15, 18] = new MapTile(15, 18, TileType.GRASS);
            theMap[15, 19] = new MapTile(15, 19, TileType.GRASS);
            theMap[15, 20] = new MapTile(15, 20, TileType.GRASS);
            theMap[15, 21] = new MapTile(15, 21, TileType.GRASS);
            theMap[15, 22] = new MapTile(15, 22, TileType.GRASS);
            theMap[15, 23] = new MapTile(15, 23, TileType.GRASS);
            theMap[15, 24] = new MapTile(15, 24, TileType.GRASS);
            theMap[15, 25] = new MapTile(15, 25, TileType.GRASS);
            theMap[15, 26] = new MapTile(15, 26, TileType.GRASS);
            theMap[15, 27] = new MapTile(15, 27, TileType.GRASS);
            theMap[15, 28] = new MapTile(15, 28, TileType.ROCK);
            theMap[15, 29] = new MapTile(15, 29, TileType.ROCK);

            // X = 16
            theMap[16, 0] = new MapTile(16, 0, TileType.ROCK);
            theMap[16, 1] = new MapTile(16, 1, TileType.ROCK);
            theMap[16, 2] = new MapTile(16, 2, TileType.ROCK);
            theMap[16, 3] = new MapTile(16, 3, TileType.ROCK);
            theMap[16, 4] = new MapTile(16, 4, TileType.ROCK);
            theMap[16, 5] = new MapTile(16, 5, TileType.GRASS);
            theMap[16, 6] = new MapTile(16, 6, TileType.GRASS);
            theMap[16, 7] = new MapTile(16, 7, TileType.GRASS);
            theMap[16, 8] = new MapTile(16, 8, TileType.GRASS);
            theMap[16, 9] = new MapTile(16, 9, TileType.GRASS);
            theMap[16, 10] = new MapTile(16, 10, TileType.WATER);
            theMap[16, 11] = new MapTile(16, 11, TileType.WATER);
            theMap[16, 12] = new MapTile(16, 12, TileType.WATER);
            theMap[16, 13] = new MapTile(16, 13, TileType.WATER);
            theMap[16, 14] = new MapTile(16, 14, TileType.WATER);
            theMap[16, 15] = new MapTile(16, 15, TileType.WATER);
            theMap[16, 16] = new MapTile(16, 16, TileType.WATER);
            theMap[16, 17] = new MapTile(16, 17, TileType.WATER);
            theMap[16, 18] = new MapTile(16, 18, TileType.WATER);
            theMap[16, 19] = new MapTile(16, 19, TileType.GRASS);
            theMap[16, 20] = new MapTile(16, 20, TileType.GRASS);
            theMap[16, 21] = new MapTile(16, 21, TileType.GRASS);
            theMap[16, 22] = new MapTile(16, 22, TileType.GRASS);
            theMap[16, 23] = new MapTile(16, 23, TileType.GRASS);
            theMap[16, 24] = new MapTile(16, 24, TileType.GRASS);
            theMap[16, 25] = new MapTile(16, 25, TileType.GRASS);
            theMap[16, 26] = new MapTile(16, 26, TileType.GRASS);
            theMap[16, 27] = new MapTile(16, 27, TileType.GRASS);
            theMap[16, 28] = new MapTile(16, 28, TileType.ROCK);
            theMap[16, 29] = new MapTile(16, 29, TileType.ROCK);

            // X = 17
            theMap[17, 0] = new MapTile(17, 0, TileType.GRASS);
            theMap[17, 1] = new MapTile(17, 1, TileType.ROCK);
            theMap[17, 2] = new MapTile(17, 2, TileType.ROCK);
            theMap[17, 3] = new MapTile(17, 3, TileType.ROCK);
            theMap[17, 4] = new MapTile(17, 4, TileType.GRASS);
            theMap[17, 5] = new MapTile(17, 5, TileType.GRASS);
            theMap[17, 6] = new MapTile(17, 6, TileType.ROCK);
            theMap[17, 7] = new MapTile(17, 7, TileType.GRASS);
            theMap[17, 8] = new MapTile(17, 8, TileType.GRASS);
            theMap[17, 9] = new MapTile(17, 9, TileType.GRASS);
            theMap[17, 10] = new MapTile(17, 10, TileType.WATER);
            theMap[17, 11] = new MapTile(17, 11, TileType.WATER);
            theMap[17, 12] = new MapTile(17, 12, TileType.WATER);
            theMap[17, 13] = new MapTile(17, 13, TileType.WATER);
            theMap[17, 14] = new MapTile(17, 14, TileType.WATER);
            theMap[17, 15] = new MapTile(17, 15, TileType.WATER);
            theMap[17, 16] = new MapTile(17, 16, TileType.WATER);
            theMap[17, 17] = new MapTile(17, 17, TileType.WATER);
            theMap[17, 18] = new MapTile(17, 18, TileType.WATER);
            theMap[17, 19] = new MapTile(17, 19, TileType.GRASS);
            theMap[17, 20] = new MapTile(17, 20, TileType.GRASS);
            theMap[17, 21] = new MapTile(17, 21, TileType.GRASS);
            theMap[17, 22] = new MapTile(17, 22, TileType.GRASS);
            theMap[17, 23] = new MapTile(17, 23, TileType.ROCK);
            theMap[17, 24] = new MapTile(17, 24, TileType.ROCK);
            theMap[17, 25] = new MapTile(17, 25, TileType.ROCK);
            theMap[17, 26] = new MapTile(17, 26, TileType.GRASS);
            theMap[17, 27] = new MapTile(17, 27, TileType.GRASS);
            theMap[17, 28] = new MapTile(17, 28, TileType.GRASS);
            theMap[17, 29] = new MapTile(17, 29, TileType.ROCK);

            // X = 18
            theMap[18, 0] = new MapTile(18, 0, TileType.GRASS);
            theMap[18, 1] = new MapTile(18, 1, TileType.GRASS);
            theMap[18, 2] = new MapTile(18, 2, TileType.GRASS);
            theMap[18, 3] = new MapTile(18, 3, TileType.GRASS);
            theMap[18, 4] = new MapTile(18, 4, TileType.GRASS);
            theMap[18, 5] = new MapTile(18, 5, TileType.ROCK);
            theMap[18, 6] = new MapTile(18, 6, TileType.ROCK);
            theMap[18, 7] = new MapTile(18, 7, TileType.ITEMBOX);
            theMap[18, 8] = new MapTile(18, 8, TileType.GRASS);
            theMap[18, 9] = new MapTile(18, 9, TileType.GRASS);
            theMap[18, 10] = new MapTile(18, 10, TileType.WATER);
            theMap[18, 11] = new MapTile(18, 11, TileType.WATER);
            theMap[18, 12] = new MapTile(18, 12, TileType.WATER);
            theMap[18, 13] = new MapTile(18, 13, TileType.WATER);
            theMap[18, 14] = new MapTile(18, 14, TileType.WATER);
            theMap[18, 15] = new MapTile(18, 15, TileType.WATER);
            theMap[18, 16] = new MapTile(18, 16, TileType.WATER);
            theMap[18, 17] = new MapTile(18, 17, TileType.WATER);
            theMap[18, 18] = new MapTile(18, 18, TileType.WATER);
            theMap[18, 19] = new MapTile(18, 19, TileType.GRASS);
            theMap[18, 20] = new MapTile(18, 20, TileType.GRASS);
            theMap[18, 21] = new MapTile(18, 21, TileType.GRASS);
            theMap[18, 22] = new MapTile(18, 22, TileType.ROCK);
            theMap[18, 23] = new MapTile(18, 23, TileType.ROCK);
            theMap[18, 24] = new MapTile(18, 24, TileType.GRASS);
            theMap[18, 25] = new MapTile(18, 25, TileType.GRASS);
            theMap[18, 26] = new MapTile(18, 26, TileType.GRASS);
            theMap[18, 27] = new MapTile(18, 27, TileType.GRASS);
            theMap[18, 28] = new MapTile(18, 28, TileType.ITEMBOX);
            theMap[18, 29] = new MapTile(18, 29, TileType.GRASS);

            // X = 19
            theMap[19, 0] = new MapTile(19, 0, TileType.GRASS);
            theMap[19, 1] = new MapTile(19, 1, TileType.GRASS);
            theMap[19, 2] = new MapTile(19, 2, TileType.GRASS);
            theMap[19, 3] = new MapTile(91, 3, TileType.GRASS);
            theMap[19, 4] = new MapTile(19, 4, TileType.GRASS);
            theMap[19, 5] = new MapTile(19, 5, TileType.ROCK);
            theMap[19, 6] = new MapTile(19, 6, TileType.ROCK);
            theMap[19, 7] = new MapTile(19, 7, TileType.GRASS);
            theMap[19, 8] = new MapTile(19, 8, TileType.GRASS);
            theMap[19, 9] = new MapTile(19, 9, TileType.WATER);
            theMap[19, 10] = new MapTile(19, 10, TileType.WATER);
            theMap[19, 11] = new MapTile(19, 11, TileType.WATER);
            theMap[19, 12] = new MapTile(19, 12, TileType.WATER);
            theMap[19, 13] = new MapTile(19, 13, TileType.WATER);
            theMap[19, 14] = new MapTile(19, 14, TileType.WATER);
            theMap[19, 15] = new MapTile(19, 15, TileType.WATER);
            theMap[19, 16] = new MapTile(19, 16, TileType.WATER);
            theMap[19, 17] = new MapTile(19, 17, TileType.WATER);
            theMap[19, 18] = new MapTile(19, 18, TileType.WATER);
            theMap[19, 19] = new MapTile(19, 19, TileType.GRASS);
            theMap[19, 20] = new MapTile(19, 20, TileType.GRASS);
            theMap[19, 21] = new MapTile(19, 21, TileType.GRASS);
            theMap[19, 22] = new MapTile(19, 22, TileType.ROCK);
            theMap[19, 23] = new MapTile(19, 23, TileType.ROCK);
            theMap[19, 24] = new MapTile(19, 24, TileType.GRASS);
            theMap[19, 25] = new MapTile(19, 25, TileType.GRASS);
            theMap[19, 26] = new MapTile(19, 26, TileType.GRASS);
            theMap[19, 27] = new MapTile(19, 27, TileType.ROCK);
            theMap[19, 28] = new MapTile(19, 28, TileType.ROCK);
            theMap[19, 29] = new MapTile(19, 29, TileType.GRASS);

            // X = 20
            theMap[20, 0] = new MapTile(20, 0, TileType.GRASS);
            theMap[20, 1] = new MapTile(20, 1, TileType.GRASS);
            theMap[20, 2] = new MapTile(20, 2, TileType.GRASS);
            theMap[20, 3] = new MapTile(20, 3, TileType.GRASS);
            theMap[20, 4] = new MapTile(20, 4, TileType.GRASS);
            theMap[20, 5] = new MapTile(20, 5, TileType.GRASS);
            theMap[20, 6] = new MapTile(20, 6, TileType.GRASS);
            theMap[20, 7] = new MapTile(20, 7, TileType.GRASS);
            theMap[20, 8] = new MapTile(20, 8, TileType.WATER);
            theMap[20, 9] = new MapTile(20, 9, TileType.WATER);
            theMap[20, 10] = new MapTile(20, 10, TileType.WATER);
            theMap[20, 11] = new MapTile(20, 11, TileType.WATER);
            theMap[20, 12] = new MapTile(20, 12, TileType.WATER);
            theMap[20, 13] = new MapTile(20, 13, TileType.WATER);
            theMap[20, 14] = new MapTile(20, 14, TileType.WATER);
            theMap[20, 15] = new MapTile(20, 15, TileType.WATER);
            theMap[20, 16] = new MapTile(20, 16, TileType.WATER);
            theMap[20, 17] = new MapTile(20, 17, TileType.WATER);
            theMap[20, 18] = new MapTile(20, 18, TileType.WATER);
            theMap[20, 19] = new MapTile(20, 19, TileType.GRASS);
            theMap[20, 20] = new MapTile(20, 20, TileType.GRASS);
            theMap[20, 21] = new MapTile(20, 21, TileType.GRASS);
            theMap[20, 22] = new MapTile(20, 22, TileType.ROCK);
            theMap[20, 23] = new MapTile(20, 23, TileType.ITEMBOX);
            theMap[20, 24] = new MapTile(20, 24, TileType.GRASS);
            theMap[20, 25] = new MapTile(20, 25, TileType.GRASS);
            theMap[20, 26] = new MapTile(20, 26, TileType.GRASS);
            theMap[20, 27] = new MapTile(20, 27, TileType.ROCK);
            theMap[20, 28] = new MapTile(20, 28, TileType.ROCK);
            theMap[20, 29] = new MapTile(20, 29, TileType.GRASS);

            // X = 21
            theMap[21, 0] = new MapTile(21, 0, TileType.GRASS);
            theMap[21, 1] = new MapTile(21, 1, TileType.GRASS);
            theMap[21, 2] = new MapTile(21, 2, TileType.GRASS);
            theMap[21, 3] = new MapTile(21, 3, TileType.GRASS);
            theMap[21, 4] = new MapTile(21, 4, TileType.GRASS);
            theMap[21, 5] = new MapTile(21, 5, TileType.GRASS);
            theMap[21, 6] = new MapTile(21, 6, TileType.GRASS);
            theMap[21, 7] = new MapTile(21, 7, TileType.GRASS);
            theMap[21, 8] = new MapTile(21, 8, TileType.ROCK);
            theMap[21, 9] = new MapTile(21, 9, TileType.WATER);
            theMap[21, 10] = new MapTile(21, 10, TileType.WATER);
            theMap[21, 11] = new MapTile(21, 11, TileType.WATER);
            theMap[21, 12] = new MapTile(21, 12, TileType.WATER);
            theMap[21, 13] = new MapTile(21, 13, TileType.WATER);
            theMap[21, 14] = new MapTile(21, 14, TileType.WATER);
            theMap[21, 15] = new MapTile(21, 15, TileType.WATER);
            theMap[21, 16] = new MapTile(21, 16, TileType.WATER);
            theMap[21, 17] = new MapTile(21, 17, TileType.WATER);
            theMap[21, 18] = new MapTile(21, 18, TileType.GRASS);
            theMap[21, 19] = new MapTile(21, 19, TileType.GRASS);
            theMap[21, 20] = new MapTile(21, 20, TileType.GRASS);
            theMap[21, 21] = new MapTile(21, 21, TileType.GRASS);
            theMap[21, 22] = new MapTile(21, 22, TileType.ROCK);
            theMap[21, 23] = new MapTile(21, 23, TileType.GRASS);
            theMap[21, 24] = new MapTile(21, 24, TileType.GRASS);
            theMap[21, 25] = new MapTile(21, 25, TileType.GRASS);
            theMap[21, 26] = new MapTile(21, 26, TileType.GRASS);
            theMap[21, 27] = new MapTile(21, 27, TileType.ROCK);
            theMap[21, 28] = new MapTile(21, 28, TileType.ROCK);
            theMap[21, 29] = new MapTile(21, 29, TileType.GRASS);

            // X = 22
            theMap[22, 0] = new MapTile(22, 0, TileType.GRASS);
            theMap[22, 1] = new MapTile(22, 1, TileType.GRASS);
            theMap[22, 2] = new MapTile(22, 2, TileType.GRASS);
            theMap[22, 3] = new MapTile(22, 3, TileType.GRASS);
            theMap[22, 4] = new MapTile(22, 4, TileType.GRASS);
            theMap[22, 5] = new MapTile(22, 5, TileType.GRASS);
            theMap[22, 6] = new MapTile(22, 6, TileType.GRASS);
            theMap[22, 7] = new MapTile(22, 7, TileType.ROCK);
            theMap[22, 8] = new MapTile(22, 8, TileType.ROCK);
            theMap[22, 9] = new MapTile(22, 9, TileType.ROCK);
            theMap[22, 10] = new MapTile(22, 10, TileType.WATER);
            theMap[22, 11] = new MapTile(22, 11, TileType.WATER);
            theMap[22, 12] = new MapTile(22, 12, TileType.WATER);
            theMap[22, 13] = new MapTile(22, 13, TileType.ITEMBOX);
            theMap[22, 14] = new MapTile(22, 14, TileType.WATER);
            theMap[22, 15] = new MapTile(22, 15, TileType.WATER);
            theMap[22, 16] = new MapTile(22, 16, TileType.WATER);
            theMap[22, 17] = new MapTile(22, 17, TileType.WATER);
            theMap[22, 18] = new MapTile(22, 18, TileType.GRASS);
            theMap[22, 19] = new MapTile(22, 19, TileType.GRASS);
            theMap[22, 20] = new MapTile(22, 20, TileType.GRASS);
            theMap[22, 21] = new MapTile(22, 21, TileType.GRASS);
            theMap[22, 22] = new MapTile(22, 22, TileType.GRASS);
            theMap[22, 23] = new MapTile(22, 23, TileType.GRASS);
            theMap[22, 24] = new MapTile(22, 24, TileType.GRASS);
            theMap[22, 25] = new MapTile(22, 25, TileType.ROCK);
            theMap[22, 26] = new MapTile(22, 26, TileType.ROCK);
            theMap[22, 27] = new MapTile(22, 27, TileType.ROCK);
            theMap[22, 28] = new MapTile(22, 28, TileType.ROCK);
            theMap[22, 29] = new MapTile(22, 29, TileType.GRASS);

            // X = 23
            theMap[23, 0] = new MapTile(23, 0, TileType.ITEMBOX);
            theMap[23, 1] = new MapTile(23, 1, TileType.GRASS);
            theMap[23, 2] = new MapTile(23, 2, TileType.ROCK);
            theMap[23, 3] = new MapTile(23, 3, TileType.ROCK);
            theMap[23, 4] = new MapTile(23, 4, TileType.GRASS);
            theMap[23, 5] = new MapTile(23, 5, TileType.GRASS);
            theMap[23, 6] = new MapTile(23, 6, TileType.GRASS);
            theMap[23, 7] = new MapTile(23, 7, TileType.ROCK);
            theMap[23, 8] = new MapTile(23, 8, TileType.ROCK);
            theMap[23, 9] = new MapTile(23, 9, TileType.ROCK);
            theMap[23, 10] = new MapTile(23, 10, TileType.ROCK);
            theMap[23, 11] = new MapTile(23, 11, TileType.ROCK);
            theMap[23, 12] = new MapTile(23, 12, TileType.GRASS);
            theMap[23, 13] = new MapTile(23, 13, TileType.GRASS);
            theMap[23, 14] = new MapTile(23, 14, TileType.WATER);
            theMap[23, 15] = new MapTile(23, 15, TileType.WATER);
            theMap[23, 16] = new MapTile(23, 16, TileType.WATER);
            theMap[23, 17] = new MapTile(23, 17, TileType.GRASS);
            theMap[23, 18] = new MapTile(23, 18, TileType.GRASS);
            theMap[23, 19] = new MapTile(23, 19, TileType.GRASS);
            theMap[23, 20] = new MapTile(23, 20, TileType.GRASS);
            theMap[23, 21] = new MapTile(23, 21, TileType.GRASS);
            theMap[23, 22] = new MapTile(23, 22, TileType.GRASS);
            theMap[23, 23] = new MapTile(23, 23, TileType.GRASS);
            theMap[23, 24] = new MapTile(23, 24, TileType.GRASS);
            theMap[23, 25] = new MapTile(23, 25, TileType.GRASS);
            theMap[23, 26] = new MapTile(23, 26, TileType.ITEMBOX);
            theMap[23, 27] = new MapTile(23, 27, TileType.ROCK);
            theMap[23, 28] = new MapTile(23, 28, TileType.GRASS);
            theMap[23, 29] = new MapTile(23, 29, TileType.GRASS);

            // X = 24
            theMap[24, 0] = new MapTile(24, 0, TileType.GRASS);
            theMap[24, 1] = new MapTile(24, 1, TileType.GRASS);
            theMap[24, 2] = new MapTile(24, 2, TileType.ROCK);
            theMap[24, 3] = new MapTile(24, 3, TileType.ROCK);
            theMap[24, 4] = new MapTile(24, 4, TileType.ROCK);
            theMap[24, 5] = new MapTile(24, 5, TileType.GRASS);
            theMap[24, 6] = new MapTile(24, 6, TileType.GRASS);
            theMap[24, 7] = new MapTile(24, 7, TileType.GRASS);
            theMap[24, 8] = new MapTile(24, 8, TileType.ROCK);
            theMap[24, 9] = new MapTile(24, 9, TileType.ROCK);
            theMap[24, 10] = new MapTile(24, 10, TileType.ROCK);
            theMap[24, 11] = new MapTile(24, 11, TileType.ROCK);
            theMap[24, 12] = new MapTile(24, 12, TileType.ROCK);
            theMap[24, 13] = new MapTile(24, 13, TileType.GRASS);
            theMap[24, 14] = new MapTile(24, 14, TileType.GRASS);
            theMap[24, 15] = new MapTile(24, 15, TileType.GRASS);
            theMap[24, 16] = new MapTile(24, 16, TileType.GRASS);
            theMap[24, 17] = new MapTile(24, 17, TileType.GRASS);
            theMap[24, 18] = new MapTile(24, 18, TileType.GRASS);
            theMap[24, 19] = new MapTile(24, 19, TileType.GRASS);
            theMap[24, 20] = new MapTile(24, 20, TileType.GRASS);
            theMap[24, 21] = new MapTile(24, 21, TileType.GRASS);
            theMap[24, 22] = new MapTile(24, 22, TileType.GRASS);
            theMap[24, 23] = new MapTile(24, 23, TileType.GRASS);
            theMap[24, 24] = new MapTile(24, 24, TileType.ROCK);
            theMap[24, 25] = new MapTile(24, 25, TileType.GRASS);
            theMap[24, 26] = new MapTile(24, 26, TileType.GRASS);
            theMap[24, 27] = new MapTile(24, 27, TileType.GRASS);
            theMap[24, 28] = new MapTile(24, 28, TileType.GRASS);
            theMap[24, 29] = new MapTile(24, 29, TileType.GRASS);

            // X = 25
            theMap[25, 0] = new MapTile(25, 0, TileType.GRASS);
            theMap[25, 1] = new MapTile(25, 1, TileType.GRASS);
            theMap[25, 2] = new MapTile(25, 2, TileType.ROCK);
            theMap[25, 3] = new MapTile(25, 3, TileType.ROCK);
            theMap[25, 4] = new MapTile(25, 4, TileType.ROCK);
            theMap[25, 5] = new MapTile(25, 5, TileType.ROCK);
            theMap[25, 6] = new MapTile(25, 6, TileType.GRASS);
            theMap[25, 7] = new MapTile(25, 7, TileType.GRASS);
            theMap[25, 8] = new MapTile(25, 8, TileType.GRASS);
            theMap[25, 9] = new MapTile(25, 9, TileType.GRASS);
            theMap[25, 10] = new MapTile(25, 10, TileType.GRASS);
            theMap[25, 11] = new MapTile(25, 11, TileType.ROCK);
            theMap[25, 12] = new MapTile(25, 12, TileType.ROCK);
            theMap[25, 13] = new MapTile(25, 13, TileType.GRASS);
            theMap[25, 14] = new MapTile(25, 14, TileType.GRASS);
            theMap[25, 15] = new MapTile(25, 15, TileType.GRASS);
            theMap[25, 16] = new MapTile(25, 16, TileType.GRASS);
            theMap[25, 17] = new MapTile(25, 17, TileType.GRASS);
            theMap[25, 18] = new MapTile(25, 18, TileType.GRASS);
            theMap[25, 19] = new MapTile(25, 19, TileType.GRASS);
            theMap[25, 20] = new MapTile(25, 20, TileType.GRASS);
            theMap[25, 21] = new MapTile(25, 21, TileType.GRASS);
            theMap[25, 22] = new MapTile(25, 22, TileType.GRASS);
            theMap[25, 23] = new MapTile(25, 23, TileType.GRASS);
            theMap[25, 24] = new MapTile(25, 24, TileType.ROCK);
            theMap[25, 25] = new MapTile(25, 25, TileType.GRASS);
            theMap[25, 26] = new MapTile(25, 26, TileType.GRASS);
            theMap[25, 27] = new MapTile(25, 27, TileType.GRASS);
            theMap[25, 28] = new MapTile(25, 28, TileType.ROCK);
            theMap[25, 29] = new MapTile(25, 29, TileType.GRASS);

            // X = 26
            theMap[26, 0] = new MapTile(26, 0, TileType.GRASS);
            theMap[26, 1] = new MapTile(26, 1, TileType.GRASS);
            theMap[26, 2] = new MapTile(26, 2, TileType.GRASS);
            theMap[26, 3] = new MapTile(26, 3, TileType.GRASS);
            theMap[26, 4] = new MapTile(26, 4, TileType.GRASS);
            theMap[26, 5] = new MapTile(26, 5, TileType.GRASS);
            theMap[26, 6] = new MapTile(26, 6, TileType.GRASS);
            theMap[26, 7] = new MapTile(26, 7, TileType.GRASS);
            theMap[26, 8] = new MapTile(26, 8, TileType.GRASS);
            theMap[26, 9] = new MapTile(26, 9, TileType.GRASS);
            theMap[26, 10] = new MapTile(26, 10, TileType.GRASS);
            theMap[26, 11] = new MapTile(26, 11, TileType.ROCK);
            theMap[26, 12] = new MapTile(26, 12, TileType.ROCK);
            theMap[26, 13] = new MapTile(26, 13, TileType.GRASS);
            theMap[26, 14] = new MapTile(26, 14, TileType.GRASS);
            theMap[26, 15] = new MapTile(26, 15, TileType.ROCK);
            theMap[26, 16] = new MapTile(26, 16, TileType.ROCK);
            theMap[26, 17] = new MapTile(26, 17, TileType.ROCK);
            theMap[26, 18] = new MapTile(26, 18, TileType.ROCK);
            theMap[26, 19] = new MapTile(26, 19, TileType.ROCK);
            theMap[26, 20] = new MapTile(26, 20, TileType.GRASS);
            theMap[26, 21] = new MapTile(26, 21, TileType.GRASS);
            theMap[26, 22] = new MapTile(26, 22, TileType.GRASS);
            theMap[26, 23] = new MapTile(26, 23, TileType.GRASS);
            theMap[26, 24] = new MapTile(26, 24, TileType.ROCK);
            theMap[26, 25] = new MapTile(26, 25, TileType.ROCK);
            theMap[26, 26] = new MapTile(26, 26, TileType.GRASS);
            theMap[26, 27] = new MapTile(26, 27, TileType.GRASS);
            theMap[26, 28] = new MapTile(26, 28, TileType.ROCK);
            theMap[26, 29] = new MapTile(26, 29, TileType.GRASS);

            // X = 27
            theMap[27, 0] = new MapTile(27, 0, TileType.GRASS);
            theMap[27, 1] = new MapTile(27, 1, TileType.GRASS);
            theMap[27, 2] = new MapTile(27, 2, TileType.ROCK);
            theMap[27, 3] = new MapTile(27, 3, TileType.GRASS);
            theMap[27, 4] = new MapTile(27, 4, TileType.GRASS);
            theMap[27, 5] = new MapTile(27, 5, TileType.GRASS);
            theMap[27, 6] = new MapTile(27, 6, TileType.GRASS);
            theMap[27, 7] = new MapTile(27, 7, TileType.GRASS);
            theMap[27, 8] = new MapTile(27, 8, TileType.GRASS);
            theMap[27, 9] = new MapTile(27, 9, TileType.GRASS);
            theMap[27, 10] = new MapTile(27, 10, TileType.GRASS);
            theMap[27, 11] = new MapTile(27, 11, TileType.GRASS);
            theMap[27, 12] = new MapTile(27, 12, TileType.ROCK);
            theMap[27, 13] = new MapTile(27, 13, TileType.GRASS);
            theMap[27, 14] = new MapTile(27, 14, TileType.GRASS);
            theMap[27, 15] = new MapTile(27, 15, TileType.GRASS);
            theMap[27, 16] = new MapTile(27, 16, TileType.ROCK);
            theMap[27, 17] = new MapTile(27, 17, TileType.ROCK);
            theMap[27, 18] = new MapTile(27, 18, TileType.ROCK);
            theMap[27, 19] = new MapTile(27, 19, TileType.ROCK);
            theMap[27, 20] = new MapTile(27, 20, TileType.ROCK);
            theMap[27, 21] = new MapTile(27, 21, TileType.ROCK);
            theMap[27, 22] = new MapTile(27, 22, TileType.GRASS);
            theMap[27, 23] = new MapTile(27, 23, TileType.GRASS);
            theMap[27, 24] = new MapTile(27, 24, TileType.GRASS);
            theMap[27, 25] = new MapTile(27, 25, TileType.ROCK);
            theMap[27, 26] = new MapTile(27, 26, TileType.ROCK);
            theMap[27, 27] = new MapTile(27, 27, TileType.ROCK);
            theMap[27, 28] = new MapTile(27, 28, TileType.ROCK);
            theMap[27, 29] = new MapTile(27, 29, TileType.GRASS);

            // X = 28
            theMap[28, 0] = new MapTile(28, 0, TileType.GRASS);
            theMap[28, 1] = new MapTile(28, 1, TileType.GRASS);
            theMap[28, 2] = new MapTile(28, 2, TileType.ITEMBOX);
            theMap[28, 3] = new MapTile(28, 3, TileType.ROCK);
            theMap[28, 4] = new MapTile(28, 4, TileType.ROCK);
            theMap[28, 5] = new MapTile(28, 5, TileType.ROCK);
            theMap[28, 6] = new MapTile(28, 6, TileType.GRASS);
            theMap[28, 7] = new MapTile(28, 7, TileType.GRASS);
            theMap[28, 8] = new MapTile(28, 8, TileType.GRASS);
            theMap[28, 9] = new MapTile(28, 9, TileType.GRASS);
            theMap[28, 10] = new MapTile(28, 10, TileType.GRASS);
            theMap[28, 11] = new MapTile(28, 11, TileType.GRASS);
            theMap[28, 12] = new MapTile(28, 12, TileType.GRASS);
            theMap[28, 13] = new MapTile(28, 13, TileType.GRASS);
            theMap[28, 14] = new MapTile(28, 14, TileType.GRASS);
            theMap[28, 15] = new MapTile(28, 15, TileType.GRASS);
            theMap[28, 16] = new MapTile(28, 16, TileType.GRASS);
            theMap[28, 17] = new MapTile(28, 17, TileType.GRASS);
            theMap[28, 18] = new MapTile(28, 18, TileType.GRASS);
            theMap[28, 19] = new MapTile(28, 19, TileType.GRASS);
            theMap[28, 20] = new MapTile(28, 20, TileType.ROCK);
            theMap[28, 21] = new MapTile(28, 21, TileType.ROCK);
            theMap[28, 22] = new MapTile(28, 22, TileType.ROCK);
            theMap[28, 23] = new MapTile(28, 23, TileType.GRASS);
            theMap[28, 24] = new MapTile(28, 24, TileType.GRASS);
            theMap[28, 25] = new MapTile(28, 25, TileType.GRASS);
            theMap[28, 26] = new MapTile(28, 26, TileType.GRASS);
            theMap[28, 27] = new MapTile(28, 27, TileType.GRASS);
            theMap[28, 28] = new MapTile(28, 28, TileType.GRASS);
            theMap[28, 29] = new MapTile(28, 29, TileType.GRASS);

            // X = 29
            theMap[29, 0] = new MapTile(29, 0, TileType.GRASS);
            theMap[29, 1] = new MapTile(29, 1, TileType.GRASS);
            theMap[29, 2] = new MapTile(29, 2, TileType.ROCK);
            theMap[29, 3] = new MapTile(29, 3, TileType.ROCK);
            theMap[29, 4] = new MapTile(29, 4, TileType.ROCK);
            theMap[29, 5] = new MapTile(29, 5, TileType.ROCK);
            theMap[29, 6] = new MapTile(29, 6, TileType.ROCK);
            theMap[29, 7] = new MapTile(29, 7, TileType.ITEMBOX);
            theMap[29, 8] = new MapTile(29, 8, TileType.GRASS);
            theMap[29, 9] = new MapTile(29, 9, TileType.GRASS);
            theMap[29, 10] = new MapTile(29, 10, TileType.GRASS);
            theMap[29, 11] = new MapTile(29, 11, TileType.GRASS);
            theMap[29, 12] = new MapTile(29, 12, TileType.GRASS);
            theMap[29, 13] = new MapTile(29, 13, TileType.GRASS);
            theMap[29, 14] = new MapTile(29, 14, TileType.GRASS);
            theMap[29, 15] = new MapTile(29, 15, TileType.GRASS);
            theMap[29, 16] = new MapTile(29, 16, TileType.GRASS);
            theMap[29, 17] = new MapTile(29, 17, TileType.GRASS);
            theMap[29, 18] = new MapTile(29, 18, TileType.GRASS);
            theMap[29, 19] = new MapTile(29, 19, TileType.GRASS);
            theMap[29, 20] = new MapTile(29, 20, TileType.GRASS);
            theMap[29, 21] = new MapTile(29, 21, TileType.ROCK);
            theMap[29, 22] = new MapTile(29, 22, TileType.ROCK);
            theMap[29, 23] = new MapTile(29, 23, TileType.ITEMBOX);
            theMap[29, 24] = new MapTile(29, 24, TileType.GRASS);
            theMap[29, 25] = new MapTile(29, 25, TileType.GRASS);
            theMap[29, 26] = new MapTile(29, 26, TileType.GRASS);
            theMap[29, 27] = new MapTile(29, 27, TileType.GRASS);
            theMap[29, 28] = new MapTile(29, 28, TileType.GRASS);
            theMap[29, 29] = new MapTile(29, 29, TileType.SAFEZONE);



        }


        //check to make sure a move on the map is valid
        static bool moveIsValid(int currentX, int currentY, ControlDirection nextAction)
        {
            bool isValidMove = false;

            switch(nextAction)
            {
                case ControlDirection.UP:
                    {
                        if (currentY != 0)
                        {
                            if ((theMap[currentX, (currentY - 1)] != null))
                            {
                                if (theMap[currentX, (currentY - 1)].getPassable() == true)
                                {
                                    isValidMove = true;
                                }

                            }
                        }
                        break;
                    }

                case ControlDirection.LEFT:
                    {
                        if (currentX != 0)
                        {
                            if ((theMap[(currentX - 1), (currentY)] != null))
                            {
                                if (theMap[(currentX - 1), (currentY)].getPassable() == true)
                                {
                                    isValidMove = true;
                                }
                            }
                        }
                        break;
                    }

                case ControlDirection.RIGHT:
                    {
                        if (currentX != 29)
                        {
                            if ((theMap[(currentX + 1), (currentY)] != null))
                            {
                                if (theMap[(currentX + 1), (currentY)].getPassable() == true)
                                {
                                    isValidMove = true;
                                }
                            }
                        }
                        break;
                    }

                case ControlDirection.DOWN:
                    {
                        if (currentY != 29)
                        {
                            if ((theMap[currentX, (currentY + 1)] != null))
                            {
                                if (theMap[currentX, (currentY + 1)].getPassable() == true)
                                {
                                    isValidMove = true;
                                }
                            }
                        }
                        break;
                    }

                default:
                    {
                        isValidMove = false;
                        break;
                    }
            }

            return isValidMove;
        }

        //sets a new character position, and saves the type of map tile they occupy for later redrawing
        static void setCharacterPosition(int newX, int newY)
        {
            tileUnderPlayer = theMap[newX, newY];
            theMap[newX, newY].setTileType(TileType.PLAYER);

            setMapVisibility(newX, newY, 4);

            playerXCoord = newX;
            playerYCoord = newY;
        }

        //reveals fog of war in specific range around character
        static void setMapVisibility(int currentX, int currentY, int visionRange)
        {
            int currentVisionExtension = 0;

            while(currentVisionExtension <= visionRange)
            {
                //vision up
                
                if ((currentY - currentVisionExtension) >= 0)
                {
                    theMap[(currentX), (currentY - currentVisionExtension)].setIsVisible(true);
                }
                

                //vision right
                if ((currentX + currentVisionExtension) <= (xDimensions-1))
                {
                    theMap[(currentX + currentVisionExtension), (currentY)].setIsVisible(true);
                }

                //vision left
                if ((currentX - currentVisionExtension) >= 0)
                {
                    theMap[(currentX - currentVisionExtension), (currentY)].setIsVisible(true);
                }

                //vision down
                if ((currentY + currentVisionExtension) <= (yDimensions-1))
                {
                    theMap[(currentX), (currentY + currentVisionExtension)].setIsVisible(true);
                }


                //do diagonals
                if(currentVisionExtension > 1)
                {

                    //up-left
                    if (((currentY - (currentVisionExtension-1)) >= 0) && ((currentX - (currentVisionExtension-1)) >= 0))
                    {
                        theMap[(currentX - 1), (currentY - 1)].setIsVisible(true);
                    }

                    //up-right
                    if (((currentY - (currentVisionExtension - 1)) >= 0) && ((currentX + (currentVisionExtension - 1)) <= (xDimensions-1)))
                    {
                        theMap[(currentX + 1), (currentY - 1)].setIsVisible(true);
                    }

                    //down-left
                    if (((currentY + (currentVisionExtension - 1)) <= (yDimensions-1)) && ((currentX - (currentVisionExtension - 1)) >= 0))
                    {
                        theMap[(currentX - 1), (currentY + 1)].setIsVisible(true);
                    }

                    //down-right
                    if (((currentY + (currentVisionExtension - 1)) <= (yDimensions-1)) && ((currentX + (currentVisionExtension - 1)) <= (xDimensions-1)))
                    {
                        theMap[(currentX + 1), (currentY + 1)].setIsVisible(true);
                    }
                }

                currentVisionExtension++;
            }
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

                    if (currentMapTile.getIsVisible() == false)
                    {
                        using (var graphics = Graphics.FromHwnd(windowHandler))
                        using (var image = Properties.Resources.fog)
                            graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
                    }

                    else
                    {
                        switch (currentTileType)
                        {
                            case TileType.GRASS:
                                {
                                    using (var graphics = Graphics.FromHwnd(windowHandler))
                                    using (var image = Properties.Resources.Grass)
                                        graphics.DrawImage(image, i * 16, j * 16, imageSizeX, imageSizeY);
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
                                    using (var image = Properties.Resources.fog)
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
                }
                Console.WriteLine("");
            }
        }
        
    }
}


/* RANDOM GENERATION OF MAP
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
*/
