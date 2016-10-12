using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming1FinalProject
{
    class MapTile
    {

        //PRIVATE MEMBER VARIABLES

        //COORDS
        private int xCoord;
        private int yCoord;

        private TileType myType;

        private bool isPassable = false;

        private bool isEnemyPassable = false;

        private bool isVisible;


        //CONSTRUCTOR
        public MapTile(int xCoordinate, int yCoordinate, TileType newTileType)
        {
            //set tile attributes
            setXCoord(xCoordinate);
            setYCoord(yCoordinate);
            setTileType(newTileType);

            //set if the tile is passable for the player
            if (myType == TileType.GRASS || myType == TileType.ROAD || myType == TileType.ITEMBOX || myType == TileType.SAFEZONE)
            {
                isPassable = true;
            }
            else isPassable = false;

            //set if the tile is passable for the enemies
            if (myType == TileType.GRASS || myType == TileType.ROAD)
            {
                isEnemyPassable = true;
            }
            else isEnemyPassable = false;

            isVisible = false;

        }

        //GETTERS & SETTERS

        public void setXCoord(int x)
        {
            xCoord = x;
        }

        public void setYCoord(int y)
        {
            yCoord = y;
        }

        public void setTileType(TileType newType)
        {
            myType = newType;
        }

        public void setIsVisible(bool visibility)
        {
            isVisible = visibility;
        }


        public int getXCoord()
        {
            return xCoord;
        }

        public int getYCoord()
        {
            return yCoord;
        }

        public TileType getTileType()
        {
            return myType;
        }

        public bool getPassable()
        {
            return isPassable;
        }

        public bool getIsEnemyPassable()
        {
            return isEnemyPassable;
        }

           
        public bool getIsVisible()
        {
            return isVisible;
        }

        
    }
}
