using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.Model
{
    class Player
    {
        String playerNo; // id of the player
        private int[] cordinate; //currant position of the player (X, Y)
        private int direction; // Currant heading direction (North = 0, south = 2, west = 3, East = 1)
        private int health_Level; // health level of the player
        private int coins; // count of coins colleted
        private int points; // count of points collected
        private int shoot;
        private List<int> playerPath;

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
        public int Shoot
        {
            get { return shoot; }
            set { shoot = value; }
        }

        

    }
}
