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
        private int location;
        private int value;
        private int lifeTime;

        public void setCordinates(int x,int y){
            cordinate = new int[2] {x,y};
            setLocation((10*x)+y);
        }
        public int[] getCordinate()
        {
            return cordinate;
        }

        private void setLocation(int loc)
        {
            location = loc;
        }

        public int getLocation()
        {
            return location;
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
        public int getLife()
        {
            return lifeTime;
        }

    }
}
