using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankgameG36ConsoleApp1.Model;

namespace TankgameG36ConsoleApp1.AI
{
    class NearestThing
    {
        public static int calNearestCoin()
        {
            int location = 0;
            int time = 0;
            List<int> coinInMovList = new List<int>();
            foreach (Coin c in Game.getCoins())
            {
                //foreach (ReachableCell RCfc in Game.getMovableCells())
                for (int i = 0; i < Game.getMovableCells().Count();i++ )
                {
                    if (Game.getMovableCells()[i].getLocation() == c.getLocation())
                    {
                        coinInMovList.Add(i);
                        c.setDistance(BreadthFirstSearch.edgeTo[i]);
                    }
                }
                Console.WriteLine("iterate through coins");
            }
            Console.WriteLine("Try to get the nearest coin");

            int low = Game.getCoins()[0].getDistance();
            location = Game.getCoins()[0].getLocation();
            time = Game.getCoins()[0].getLife();
            for (int i = 0; i < Game.getCoins().Count(); i++)
            {
                if (Game.getCoins()[i].getDistance() < low)
                {
                    if (Game.getCoins()[i].getDistance() != 0 && Game.getCoins()[i].getDistance() != -1)
                    {
                        low = Game.getCoins()[i].getDistance();
                        location = Game.getCoins()[i].getLocation();
                    }
                }
                else if (Game.getCoins()[i].getDistance() == low)
                {
                    if (Game.getCoins()[i].getDistance() != 0 && Game.getCoins()[i].getDistance() != -1)
                    {
                        low = Game.getCoins()[i].getDistance();
                        location = Game.getCoins()[i].getLocation();
                    }
                }
            }
            return location;
        }

        public static void setDestination()
        {

        }
    }
}
