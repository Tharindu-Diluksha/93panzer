using TankgameG36ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.Model
{
    class Game
    {
        private static String myName;
        private static Player ourPlayer;
        private static List<Array> movableCellList;
        private static List<Array> fixedCellList;
        private static List<Player> playerList;
        private static List<Coin> coinPillList;
        private static List<LifePack> lifePackList;
        private static List<Array> walllist;
        private static List<Array> stonelist;
        private static List<Array> waterlist;
        

        public static void  gameinit(){
            walllist = new List<Array>(); //bricks which can be destroyed by shooting
            stonelist = new List<Array>();
            waterlist = new List<Array>();
            movableCellList = new List<Array>();
            fixedCellList = new List<Array>();
            playerList = new List<Player>();
            coinPillList = new List<Coin>();
            lifePackList = new List<LifePack>();
            
        }
        public static void setMyname(String name)
        {
            myName = name;
        }
        public static void setOurPlayer()
        {
            foreach (Player p in Game.getPlayers())
            {
                if (p.PlayerNo == myName)
                {
                    ourPlayer = p;
                }
            }
        }
        public static Player getOurPlayer()
        {
            return ourPlayer;
        }

        public static void setMovableCells(int[] XY)
        {
            movableCellList.Add(XY);
        }
        public static List<Array> getMovableCells()
        {
            return movableCellList;
        }

        public static void setFixedCells(int[] XY)
        {
            fixedCellList.Add(XY);
        }
        public static List<Array> getFixedCells()
        {
            return fixedCellList;
        }

        public static void setWall(string[] pointSplit)
        {
            walllist.Add(pointSplit);
        }
        public static List<Array> getWall()
        {
            return walllist;
        }

        public static void setStone(string[] pointSplit)
        {
            stonelist.Add(pointSplit);
        }
        public static List<Array> getStone()
        {
            return stonelist;
        }

        public static void setWater(string[] pointSplit)
        {
            waterlist.Add(pointSplit);
        }
        public static List<Array> getWater()
        {
            return waterlist;
        }

        public static void setplayers(Player player)
        {
            playerList.Add(player);
        }
        public static List<Player> getPlayers()
        {
            return playerList;
        }

        public static void setCoins(Coin coin)
        {
            coinPillList.Add(coin);
        }
        public static List<Coin> getCoins()
        {
            return coinPillList;
        }
        public static void removeCoin(Coin coin)
        {
            coinPillList.Remove(coin);
        }

        public static void setLifePacks(LifePack lifepack)
        {
            lifePackList.Add(lifepack);
        }
        public static List<LifePack> getLifePacks()
        {
            return lifePackList;
        }
        public static void removeLifePack(LifePack lifepack)
        {
            lifePackList.Remove(lifepack);
        }
    }
}
