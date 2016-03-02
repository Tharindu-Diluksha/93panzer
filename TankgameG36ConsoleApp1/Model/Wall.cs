using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.Model
{
    class Wall
    {
        private int[] cordinate;
        private int damage;

        public void setCordinates(int x, int y)
        {
            cordinate = new int[2] { x, y };
        }

        public int[] getCordinate()
        {
            return cordinate;
        }

        public void setdamage(int damage)
        {
            this.damage = damage;
        }
        public int getdamage()
        {
            return damage;
        }


    }
}
