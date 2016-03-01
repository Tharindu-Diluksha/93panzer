using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panzer_93.Model
{
    class Player
    {
        String playerNo; // id of the player
        private int[] cordinate; //currant position of the player (X, Y)
        int direction; // Currant heading direction (North = 0, south = 2, west = 3, East = 1)
        int health_Level; // health level of the player
        int coins; // count of coins colleted
        int points; // count of points collected
        int shots;

        public void setCordinates(int x, int y)
        {
            cordinate = new int[2] { x, y };
        }

        public int[] getCordinate()
        {
            return cordinate;
        }

        public string PlayerNo
        {
            get { return playerNo; }
            set { playerNo = value; }

        }

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public int Health
        {
            get { return health_Level; }
            set { health_Level = value; }
        }

        public int Coins
        {
            get { return coins; }
            set { coins = value; }
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        public int Shots
        {
            get { return shots; }
            set { shots = value; }
        }

        

    }
}
