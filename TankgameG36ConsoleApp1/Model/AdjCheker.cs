using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.Model
{
    class AdjCheker
    {
        public static bool checkAdj(int a, int b){
            Boolean adj = true;
            if ((a == 9 || a==19 || a == 29 || a == 39 || a == 49 || a == 59 || a == 69 || a == 79 || a == 89) && b%10==0)
            {
                adj = false;
            }
            else if ((b == 9 || b==19 || b == 29 || b == 39 || b == 49 || b == 59 || b == 69 || b == 79 || b == 89) && a%10 == 0)
            {
                adj = false;
            }
            return adj;
        }
    }
}
