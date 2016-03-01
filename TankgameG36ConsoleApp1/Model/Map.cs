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
                mapDetail[int.Parse(bcordinate[0]),int.Parse(bcordinate[1])] = "B4";
            }
            foreach (string[] scordinate in stone)
            {
                mapDetail[int.Parse(scordinate[0]), int.Parse(scordinate[1])] = "S";
            }
            foreach (string[] wcordinate in water)
            {
                mapDetail[int.Parse(wcordinate[0]), int.Parse(wcordinate[1])] = "W";
            }

        }

    }
}
