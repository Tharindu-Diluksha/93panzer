using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankgameG36ConsoleApp1.Model;

namespace TankgameG36ConsoleApp1.AI
{
    class AI
    {

        private static List<int> myPath;
        private static List<Array> lifecoinPath;
        

        public static List<int> runMe(){

           // int x = Game.getOurPlayer().getCordinate()[0];
           // int y = Game.getOurPlayer().getCordinate()[1];

            /*foreach (Array mov in Game.getMovableCells())
            {
                if(){

                }
            }*/
            GraphAdjList.setGraphAdjList(Game.getMovableCells().Count);
            for (int i = 0; i < Game.getMovableCells().Count; i++)
            {
                for (int j = i + 1; j < Game.getMovableCells().Count; j++)
                {
                    if ((Game.getMovableCells()[i].getLocation() - Game.getMovableCells()[j].getLocation() == 1) || (Game.getMovableCells()[i].getLocation() - Game.getMovableCells()[j].getLocation() == 10))
                    {
                        GraphAdjList.AddEdge(i,j);
                    }
                }
            }
            int x =Game.getOurPlayer().getCordinate()[0];
            int y = Game.getOurPlayer().getCordinate()[1];
            BreadthFirstSearch.doBreadthFirstSearch((10*x)+y);
            // above is getting the result of bfs gettin prdisissor(edgTo) and distance(disTo)
            for (int i = 0; i < GraphAdjList.VertexCount; i++) 
            {
                if (BreadthFirstSearch.distTo[i] != 0)
                {
                    int[] path;
                    for (int j = BreadthFirstSearch.distTo[i]; j > 0 ; j--)
                    {
                        /*if (((10 * x) + y)-)
                        {

                        }*/
                    }
                }
            }

                return myPath;
        }
    }
}
