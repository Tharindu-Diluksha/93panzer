using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.Model
{
    class LifePack
    {
        private int[] cordinate;
        private int lifeTime;

        public void setCordinates(int x, int y)
        {
            cordinate = new int[2] { x, y };
        }

        public int[] getCordinate()
        {
            return cordinate;
        }

        public void setLife(int lifeTime)
        {
            this.lifeTime = lifeTime;
        }

        public int getLife()
        {
            return lifeTime;
        }
    }
}
