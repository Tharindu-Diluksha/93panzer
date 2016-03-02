using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankgameG36ConsoleApp1.Model
{
    class Coin
    {
        private int[] cordinate;
        private int value;
        private int lifeTime;

        public void setCordinates(int x,int y){
            cordinate = new int[2] {x,y};
        }
        public int[] getCordinate()
        {
            return cordinate;
        }

        public void setValue(int value)
        {
            this.value = value;
        }
        public int getValue(){
            return value;
        }

        public void setLife(int lifeTime)
        {
            this.lifeTime = lifeTime;
        }
        public float getLife()
        {
            return lifeTime;
        }

    }
}
