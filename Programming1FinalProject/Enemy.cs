using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming1FinalProject
{
    class Enemy
    {
        private int m_iHealth; // the variable that holds the health value of the enemy
        private int m_iDamage; // the variable that holds the damage value of the enemy
        private string m_sEnemyType; // the variable that holds the type of the enemy

        private int m_iEnemyPosX; // the variable that holds the x position value of the enemy
        private int m_iEnemyPosY; // the variable that holds the y position value of the enemy
        private int m_iRangeOfSight; // the variable that holds the range of sight value of the enemy
 //       private bool m_bIsVisible; // the variable that checks whether then player is visible to the enemy

        // Constructs an instance of the enemy at the provided X and Y position
        public Enemy(int iPosX, int iPosY)
        {
            m_iHealth = 5; // initial health value of the enemy
            m_iDamage = 2; // initial damage value of the enemy
            m_sEnemyType = "Slug"; // the type the enemy is of
            m_iEnemyPosX = iPosX; // the x position that the enemy spawns in
            m_iEnemyPosY = iPosY; // the y position that the enemy spawns in

            m_iRangeOfSight = 3; // the range that the enemy can see around them in
    }

        // Defining a health getter function so that the enemy can return its name
        public int GetHealth()
        { return m_iHealth; }

        // Defining a damage getter function so that the enemy can return its damage
        public int GetDamage()
        { return m_iDamage; }

        // Defining an enemy type getter function so that the enemy can return its enemy type
        public string GetEnemyType()
        { return m_sEnemyType; }

        // Defining a position X getter function so that the enemy can return its X position
        public int GetPosX()
        { return m_iEnemyPosX; }

        // Defining a position Y getter function so that the enemy can return its Y position
        public int GetPosY()
        { return m_iEnemyPosY; }

        // Defining an enemy Range Of Sight getter function so that the enemy can return its enemy Range Of Sight
        public int GetRangeOfSight()
        { return m_iRangeOfSight; }


        // Defining a health setter function to set the enemys health
        public void SetHealth(int value)
        { m_iHealth = value; }

        // Defining a damage setter function to set the enemys damage
        public void SetDamage(int value)
        { m_iDamage = value; }

        // Defining a enemy type setter function to set the enemys enemy type
        public void SetEnemyType(string value)
        { m_sEnemyType = value; }

        // Attempts to move the enemy instance closed to the player
        // Inputs: Player position, MapTiles Up Down Left Right
        // Processes: Changes enemy position
        // Outputs: Nothing - (can maybe output if it hit the player)
        public void MoveEnemy(int iPlayerX, int iPlayerY, MapTile up, MapTile down, MapTile left, MapTile right)
        {
            // check if regions are within map as well

            bool bCheckIfPlayerInRange = isPlayerInRange(iPlayerX, iPlayerY);
            // check if player and enemy are visible to each other
            if (bCheckIfPlayerInRange == true)
            {

                // if player visible then check which MapTiles around enemy are passable
                bool bIsUpPassable = up.getIsEnemyPassable();
                bool bIsDownPassable = down.getIsEnemyPassable();
                bool bIsLeftPassable = left.getIsEnemyPassable();
                bool bIsRightPassable = right.getIsEnemyPassable();

                // take into account own position and players position and select tile to move onto that would make enemy come closest possible
                

                int iEnemyYUp = m_iEnemyPosY + 1;
                int iEnemyYDown = m_iEnemyPosY - 1;
                int iEnemyXRight = m_iEnemyPosX + 1;
                int iEnemyXLeft = m_iEnemyPosX - 1;

                int iCurrentXDistToPlayer = iPlayerX - m_iEnemyPosX;
                int iIfRightXDistToPlayer = iPlayerX - iEnemyXRight;
                int iIfLeftXDistToPlayer = iPlayerX - iEnemyXLeft;

                int iCurrentYDistToPlayer = iPlayerY - m_iEnemyPosY;
                int iIfUpYDistToPlayer = iPlayerY - iEnemyYUp;
                int iIfDownYDistToPlayer = iPlayerY - iEnemyYDown;

                int iCurrentDistToPlayer = Math.Abs(iCurrentXDistToPlayer) + Math.Abs(iCurrentYDistToPlayer);
                int iMoveUpDistToPlayer = Math.Abs(iCurrentXDistToPlayer) + Math.Abs(iIfUpYDistToPlayer);
                int iMoveDownDistToPlayer = Math.Abs(iCurrentXDistToPlayer) + Math.Abs(iIfDownYDistToPlayer);
                int iMoveRightDistToPlayer = Math.Abs(iIfRightXDistToPlayer) + Math.Abs(iCurrentYDistToPlayer);
                int iMoveLeftDistToPlayer = Math.Abs(iIfLeftXDistToPlayer) + Math.Abs(iCurrentYDistToPlayer);

                int iLeftOrRightOrEqual = 2;
                int iDownOrUpOrEqual = 2;

                if (iCurrentXDistToPlayer < 0)
                { iLeftOrRightOrEqual = 0; } // move left
                else if (iCurrentXDistToPlayer > 0)
                { iLeftOrRightOrEqual = 1; } // move right
                else
                { iLeftOrRightOrEqual = 2; } // Player X and Enemy X are the same

                if (iCurrentYDistToPlayer < 0)
                { iDownOrUpOrEqual = 0; } // move down
                else if (iCurrentYDistToPlayer > 0)
                { iDownOrUpOrEqual = 1; } // move up
                else
                { iDownOrUpOrEqual = 2; } // Player Y and Enemy Y are the same

                Random randomDirection = new Random();
                

                if (iLeftOrRightOrEqual == 0)
                {
                    if (iDownOrUpOrEqual == 0)
                    {
                        if (bIsLeftPassable && bIsDownPassable)
                        {
                            int randomDirectionSelector = randomDirection.Next(1, 3);

                            if (randomDirectionSelector == 1)
                            {
                                m_iEnemyPosX = m_iEnemyPosX - 1; // move left
                            }
                            else
                            {
                                m_iEnemyPosY = m_iEnemyPosY - 1; // move down
                            }
                        }
                        else if (bIsLeftPassable && !bIsDownPassable)
                        {
                            m_iEnemyPosX = m_iEnemyPosX - 1; // move left
                        }
                        else if (!bIsLeftPassable && bIsDownPassable)
                        {
                            m_iEnemyPosY = m_iEnemyPosY - 1; // move down
                        }
                    }
                    else if (iDownOrUpOrEqual == 1)
                    {
                        if (bIsLeftPassable && bIsUpPassable)
                        {
                            int randomDirectionSelector = randomDirection.Next(1, 3);

                            if (randomDirectionSelector == 1)
                            {
                                m_iEnemyPosX = m_iEnemyPosX - 1; // move left
                            }
                            else
                            {
                                m_iEnemyPosY = m_iEnemyPosY + 1; // move up
                            }
                        }
                        else if (bIsLeftPassable && !bIsUpPassable)
                        {
                            m_iEnemyPosX = m_iEnemyPosX - 1; // move left
                        }
                        else if (!bIsLeftPassable && bIsUpPassable)
                        {
                            m_iEnemyPosY = m_iEnemyPosY + 1; // move up
                        }
                    }
                    else if (iDownOrUpOrEqual == 2)
                    {
                        if (bIsLeftPassable)
                        {
                            m_iEnemyPosX = m_iEnemyPosX - 1; // move left
                        }
                    }
                }
                else if (iLeftOrRightOrEqual == 1)
                {
                    if (iDownOrUpOrEqual == 0)
                    {
                        if (bIsRightPassable && bIsDownPassable)
                        {
                            int randomDirectionSelector = randomDirection.Next(1, 3);

                            if (randomDirectionSelector == 1)
                            {
                                m_iEnemyPosX = m_iEnemyPosX + 1; // move right
                            }
                            else
                            {
                                m_iEnemyPosY = m_iEnemyPosY - 1; // move down
                            }
                        }
                        else if (bIsRightPassable && !bIsDownPassable)
                        {
                            m_iEnemyPosX = m_iEnemyPosX + 1; // move right
                        }
                        else if (!bIsRightPassable && bIsDownPassable)
                        {
                            m_iEnemyPosY = m_iEnemyPosY - 1; // move down
                        }
                    }
                    else if (iDownOrUpOrEqual == 1)
                    {
                        if (bIsRightPassable && bIsUpPassable)
                        {
                            int randomDirectionSelector = randomDirection.Next(1, 3);

                            if (randomDirectionSelector == 1)
                            {
                                m_iEnemyPosX = m_iEnemyPosX + 1; // move right
                            }
                            else
                            {
                                m_iEnemyPosY = m_iEnemyPosY + 1; // move up
                            }
                        }
                        else if (bIsRightPassable && !bIsUpPassable)
                        {
                            m_iEnemyPosX = m_iEnemyPosX + 1; // move right
                        }
                        else if (!bIsRightPassable && bIsUpPassable)
                        {
                            m_iEnemyPosY = m_iEnemyPosY + 1; // move up
                        }
                    }
                    else if (iDownOrUpOrEqual == 2)
                    {
                        if (bIsRightPassable)
                        {
                            m_iEnemyPosX = m_iEnemyPosX + 1; // move right
                        }
                    }
                }
                else if (iLeftOrRightOrEqual == 2)
                {
                    if (iDownOrUpOrEqual == 0 && bIsDownPassable)
                    {
                        m_iEnemyPosY = m_iEnemyPosY - 1; // move down
                    }
                    else if (iDownOrUpOrEqual == 1 && bIsUpPassable)
                    {
                        m_iEnemyPosY = m_iEnemyPosY + 1; // move Up
                    }
                }
                
                // check if the selected direction can be moved in or not - if enemy can move there them move there

                // if enemy cannot move there then check if the other option that it had could be moved in or not

                // if enemy can move there then move it there

                // if enemy cannot move anywhere that would make it come closer to the player then dont move enemy

            }

            // if not visible then do nothing

        }

        public bool isPlayerInRange(int iPlayerX, int iPlayerY)
        {
            for (int iRangeStep = 0; iRangeStep <= m_iRangeOfSight; iRangeStep++)
            {
                if (iPlayerY == m_iEnemyPosY - iRangeStep || iPlayerY == m_iEnemyPosY + iRangeStep)
                {
                    if (iPlayerX >= (m_iEnemyPosX - m_iRangeOfSight) + iRangeStep && iPlayerX <= (m_iEnemyPosX + m_iRangeOfSight) - iRangeStep)
                    {
                        return true;
                    }
                }
                
            }
            return false;
        }
    }
}
