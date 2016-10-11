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
        private bool m_bIsVisible; // the variable that checks whether then player is visible to the enemy

        // Constructs an instance of the enemy at the provided X and Y position
        public Enemy(int iPosX, int iPosY, int iPlayerX, int iPlayerY)
        {
            m_iHealth = 5; // initial health value of the enemy
            m_iDamage = 2; // initial damage value of the enemy
            m_sEnemyType = "Zombie"; // the type the enemy is of
            m_iEnemyPosX = iPosX; // the x position that the enemy spawns in
            m_iEnemyPosY = iPosY; // the y position that the enemy spawns in

            m_iRangeOfSight = 2; // the range that the enemy can see around them in

            

            m_bIsVisible = isPlayerInRange(iPlayerX, iPlayerY) ; // can the enemy see the player
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
        // Inputs: Player position
        // Processes: Changes enemy position
        // Outputs: Nothing - (can maybe output if it hit the player)
        public void MoveEnemy(int iPlayerX, int iPlayerY)
        {
            // check if player and enemy are visible to each other


            // if visible then

            // take into account own position and players position and select tile to move onto that would make enemy come closest possible

                    // if more than one answer, select a random direction to move in

                    // check if the selected direction can be moved in or not - if enemy can move there them move there

                    // if enemy cannot move there then check if the other option that it had could be moved in or not

                    // if enemy can move there then move it there

                    // if enemy cannot move anywhere that would make it come closer to the player then dont move enemy






            // if not visible then do nothing

        }

        public bool isPlayerInRange(int iPlayerX, int iPlayerY)
        {
            for (int iRangeStep = 0; iRangeStep <= m_iRangeOfSight; iRangeStep++)
            {
                if (iPlayerY == m_iEnemyPosY - iRangeStep || iPlayerY == m_iEnemyPosY + iRangeStep)
                {
                    if (iPlayerX >= m_iEnemyPosX - (m_iRangeOfSight + iRangeStep) && iPlayerX <= m_iEnemyPosX + (m_iRangeOfSight - iRangeStep))
                    {
                        return true;
                    }
                }
                
            }
            return false;
        }
    }
}
