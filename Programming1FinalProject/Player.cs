using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming1FinalProject
{
    class Player
    {
        private int m_iHealth; // the variable that holds the health value of the enemy
        private int m_iDamage; // the variable that holds the damage value of the enemy

        private int m_iPlayerPosX; // the variable that holds the x position value of the enemy
        private int m_iPlayerPosY; // the variable that holds the y position value of the enemy
        private int m_iRangeOfSight; // the variable that holds the range of sight value of the enemy
                                     //       private bool m_bIsVisible; // the variable that checks whether then player is visible to the enemy

        private bool isAlive;




        // Defining a health getter function so that the enemy can return its name
        public int GetHealth()
        { return m_iHealth; }

        // Defining a damage getter function so that the enemy can return its damage
        public int GetDamage()
        { return m_iDamage; }

        // Defining a position X getter function so that the enemy can return its X position
        public int GetPosX()
        { return m_iPlayerPosX; }

        // Defining a position Y getter function so that the enemy can return its Y position
        public int GetPosY()
        { return m_iPlayerPosY; }

        // Defining an enemy Range Of Sight getter function so that the enemy can return its enemy Range Of Sight
        public int GetRangeOfSight()
        { return m_iRangeOfSight; }



        // Defining a health setter function so that the enemy can set its name
        public void SetHealth(int value)
        { m_iHealth = value; }

        // Defining a damage setter function so that the enemy can set its damage
        public void SetDamage(int value)
        { m_iDamage = value; }

        // Defining a position X setter function so that the enemy can set its X position
        public void SetPosX(int value)
        { m_iPlayerPosX = value; }

        // Defining a position Y setter function so that the enemy can set its Y position
        public void SetPosY(int value)
        { m_iPlayerPosY = value; }

        // Defining an enemy Range Of Sight setter function so that the enemy can set its enemy Range Of Sight
        public void SetRangeOfSight(int value)
        { m_iRangeOfSight = value; }
        
        public bool IsPlayerDead()
        {
            if (m_iHealth <= 0)
            {
                return true;
            }
            return false;
        }




    }
}
