using Panzer_93.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.Model
{
    class Map
    {
        public static String[,] mapDetail;

        public static void initMap(List<Array> wall, List<Array> stone, List<Array> water)
        {   
            mapDetail = new String[10, 10];
            foreach (string[] bcordinate in wall){
                mapDetail[int.Parse(bcordinate[0]),int.Parse(bcordinate[1])] = "B0";
            }
            foreach (string[] scordinate in stone)
            {
                mapDetail[int.Parse(scordinate[0]), int.Parse(scordinate[1])] = "S";
            }
            foreach (string[] wcordinate in water)
            {
                mapDetail[int.Parse(wcordinate[0]), int.Parse(wcordinate[1])] = "W";
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mapDetail[i, j] == null || mapDetail[i, j] != "S" || mapDetail[i, j] != "W" || mapDetail[i, j] != "B0" || mapDetail[i, j] != "B1" || mapDetail[i, j] != "B2" || mapDetail[i, j] != "B3")
                    {
                        mapDetail[i, j] = "k";
                    }
                }
            }

        }

        public static void updateMap()
        {
            foreach (string[] bcordinate in Game.getWall())
            {
                mapDetail[int.Parse(bcordinate[0]), int.Parse(bcordinate[1])] = "B0";
            }
            foreach (string[] scordinate in Game.getStone())
            {
                mapDetail[int.Parse(scordinate[0]), int.Parse(scordinate[1])] = "S";
            }
            foreach (string[] wcordinate in Game.getWater())
            {
                mapDetail[int.Parse(wcordinate[0]), int.Parse(wcordinate[1])] = "W";
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mapDetail[i, j] == null || (mapDetail[i, j] != "S" && mapDetail[i, j] != "W" && mapDetail[i, j] != "B0" && mapDetail[i, j] != "B1" && mapDetail[i, j] != "B2" && mapDetail[i, j] != "B3"))
                    {
                        mapDetail[i, j] = "N";
                    }
                }
            }

            initPlayers(Game.getPlayers());
            displayCoins(Game.getCoins());
            displayLifePacks(Game.getLifePacks());

        }

        public static void initPlayers(List<Player> playerlist)
        {
            foreach (Player player in playerlist)
            {
                String direction="-1";
                if (player.Direction == 0)
                {
                    direction = "0";
                }
                else if (player.Direction == 1)
                {
                    direction = "1";
                }
                else if (player.Direction == 2)
                {
                    direction = "2";
                }
                else if (player.Direction == 3)
                {
                    direction = "3";
                }
                
                mapDetail[player.getCordinate()[0], player.getCordinate()[1]] = player.PlayerNo+direction;
            }
        }

        public static void displayCoins(List<Coin> coinlist)
        {
            foreach (Coin c in coinlist)
            {
                mapDetail[c.getCordinate()[0], c.getCordinate()[1]] = "C";
                
            }
        }

        public static void displayLifePacks(List<LifePack> lifepacklist)
        {
            foreach (LifePack l in lifepacklist)
            {
                mapDetail[l.getCordinate()[0], l.getCordinate()[1]] = "L";

            }
        }
    }
}
