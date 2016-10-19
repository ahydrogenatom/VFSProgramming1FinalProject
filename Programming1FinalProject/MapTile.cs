using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//MapTile class holds all information pertaining to the spaces that make up
//the game world that the player traverses.

namespace Programming1FinalProject
{
    class MapTile
    {

        //PRIVATE MEMBER VARIABLES

        //COORDS
        private int iXCoord;
        private int iYCoord;

        private TileType eMyTypemyType;

        //is the tile passable for the player
        private bool bIsPassable = false;

        //is the tile passable for the slimes
        private bool bIsEnemyPassable = false;

        //is the tile within the player's line of sight
        private bool bIsVisible;


        //CONSTRUCTOR
        public MapTile(int xCoordinate, int yCoordinate, TileType newTileType)
        {
            //set tile attributes
            setXCoord(xCoordinate);
            setYCoord(yCoordinate);
            setTileType(newTileType);

            //set if the tile is passable for the player
            if (eMyTypemyType == TileType.GRASS || eMyTypemyType == TileType.ROAD || eMyTypemyType == TileType.ITEMBOX || eMyTypemyType == TileType.SAFEZONE)
            {
                bIsPassable = true;
            }
            else bIsPassable = false;

            //set if the tile is passable for the enemies
            if (eMyTypemyType == TileType.GRASS || eMyTypemyType == TileType.ROAD)
            {
                bIsEnemyPassable = true;
            }
            else bIsEnemyPassable = false;

            bIsVisible = false;

        }

        //sets passability for player
        public void checkPassable()
        {
            if(eMyTypemyType == TileType.GRASS || eMyTypemyType == TileType.ROAD || eMyTypemyType == TileType.ITEMBOX || eMyTypemyType == TileType.SAFEZONE)
            {
                bIsPassable = true;
            }
            else bIsPassable = false;
        }

        //updates to check if the enemy can pass the tile
        //used when tiles change on the map to make sure the slimes are responding correctly to the new tile
        public void checkPassableEnemy()
        {
            if (eMyTypemyType == TileType.GRASS || eMyTypemyType == TileType.ROAD)
            {
                bIsEnemyPassable = true;
            }
            else bIsEnemyPassable = false;
        }

        //GETTERS & SETTERS

        //sets X coordinate of the MapTile
        public void setXCoord(int x)
        {
            iXCoord = x;
        }

        //sets Y coordinate of the MapTile
        public void setYCoord(int y)
        {
            iYCoord = y;
        }

        //sets the TileType of the MapTile
        public void setTileType(TileType newType)
        {
            eMyTypemyType = newType;
        }

        //sets whether or not the tile is visible
        //invisible tiles will show up as a "fog of war" tile on the map
        public void setIsVisible(bool visibility)
        {
            bIsVisible = visibility;
        }

        //returns the X coordinate
        public int getXCoord()
        {
            return iXCoord;
        }

        //returns the Y coordinate
        public int getYCoord()
        {
            return iYCoord;
        }

        //returns the TileType
        public TileType getTileType()
        {
            return eMyTypemyType;
        }

        //returns whether or not the player can pass through the tile
        public bool getPassable()
        {
            return bIsPassable;
        }

        //returns whether or not the slimes can pass through the tile
        public bool getIsEnemyPassable()
        {
            return bIsEnemyPassable;
        }

        //returns whether or not the tile is visible
        public bool getIsVisible()
        {
            return bIsVisible;
        }

        
    }
}
