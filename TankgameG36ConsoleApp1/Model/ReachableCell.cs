using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.Model
{
    class ReachableCell
    {
        private int[] cordinate;
        private List<int> shortestPath;
        private int location;

        private void setLocation(int locnumber)
        {
            location = locnumber;
        }
        public int getLocation()
        {
            return location;
        }

        public void setCordinates(int x, int y)
        {
            cordinate = new int[2] { x, y };
            setLocation((10*x)+y);
        }

        public int[] getCordinate()
        {
            return cordinate;
        }

        public void setShortestPath(int direction)
        {
            shortestPath.Add(direction);
        }
        public List<int> getShortestPath()
        {
            return shortestPath;
        }
    }
}
