using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TankgameG36ConsoleApp1.Model;
using TankgameG36ConsoleApp1.Network;

namespace TankgameG36ConsoleApp1.AI
{
    class AiBrain
    {

        //private static List<int> myPath;
        private static List<List<int>> lifecoinPath;
        public static Communicator cm = Communicator.GetInstance();
        

        public static void runMeFindShortestPath(){

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
                    if ((Math.Abs(Game.getMovableCells()[i].getLocation() - Game.getMovableCells()[j].getLocation()) == 1) || (Math.Abs(Game.getMovableCells()[i].getLocation() - Game.getMovableCells()[j].getLocation()) == 10))
                    {
                        // check for 9 and 10 type cell they are not adjasent
                        if (AdjCheker.checkAdj(Game.getMovableCells()[i].getLocation(), Game.getMovableCells()[j].getLocation()))
                        {
                            GraphAdjList.AddEdge(i, j);
                        }
                        
                    }
                }
            }
            int x =Game.getOurPlayer().getCordinate()[0];
            int y = Game.getOurPlayer().getCordinate()[1];
            int ourLocation = 0;
            for (int i = 0; i < Game.getMovableCells().Count();i++ )
            {
                if (Game.getMovableCells()[i].getLocation() == (10 * x) + y)
                {
                    ourLocation = i;
                }
            }
            /*if (ourLocation != 0)
            {

            }*/
            BreadthFirstSearch.doBreadthFirstSearch(ourLocation);
            
            // above is getting the result of bfs gettin prdisissor(edgTo) and distance(disTo)
            for (int i = 0; i < GraphAdjList.VertexCount; i++) 
            {

                if (BreadthFirstSearch.distTo[i] == -1)
                {
                    Game.getMovableCells()[i].setShortestPath(-1);
                }
                //Game.getMovableCells()[i].clearShortestPathList();
                else if (BreadthFirstSearch.distTo[i] != 0)
                {
                    int e = i;
                    List<int> path = new List<int>();
                    for (int j = BreadthFirstSearch.distTo[i]; j > 0 ; j--)
                    {
                        int pred = BreadthFirstSearch.edgeTo[e];
                        if (pred!=-1)
                        {
                            //int dif = BreadthFirstSearch.edgeTo[pred] - BreadthFirstSearch.edgeTo[i];
                            int dif = Game.getMovableCells()[pred].getLocation() - Game.getMovableCells()[e].getLocation();
                            if (dif == 1)
                            {
                                path.Add(0);
                            }
                            else if (dif == -10)
                            {
                                path.Add(1);
                            }
                            else if (dif == -1)
                            {
                                path.Add(2);
                            }
                            else if (dif == 10)
                            {
                                path.Add(3);
                            }

                            e = pred;
                        }
                    }
                    //List<int> myPath = new List<int>();
                    for (int k = path.Count - 1; k >= 0; k--)
                    {
                        //myPath.Add(path[k]);
                        Game.getMovableCells()[i].setShortestPath(path[k]);
                    }

                }
                else if (BreadthFirstSearch.distTo[i] == 0)
                {
                    // at the starting not (cuurent palyer location)
                    //List<int> myPath = new List<int>();
                    //myPath.Add(0); 
                    Game.getMovableCells()[i].setShortestPath(0);//not need to give direction for the same location
                }
                
                
            }
            /*foreach (ReachableCell rc in Game.getMovableCells())
            {
                Console.Write(Convert.ToString(rc.getShortestPath()[rc.getShortestPath().Count()-1]) + " ");
            }
            Console.WriteLine(Game.getMovableCells()[Game.getMovableCells().Count() - 1].getShortestPath().Count());*/
            List<int> playerpath = new List<int>();
            playerpath = Game.getMovableCells()[Game.getMovableCells().Count()- 1].getShortestPath();
            Console.WriteLine(playerpath.Count());
            int p = playerpath[0];
            String msg ="UP#" ;
            if (p == 0){
                msg = "UP#";
                
            }
            else if (p == 1)
            {
                msg = "RIGHT#";
            }
            else if (p == 2)
            {
                msg = "DOWN#";
            }
            else if (p == 3)
            {
                msg = "LEFT#";
            }           
            //serverConnection.SendData(dataobject);
            
            if (Game.getOurPlayer().Direction != p)
            {
                //Game.getOurPlayer().Direction = p;
                DObject dataobject = new DObject(msg, "127.0.0.1", 6000);
                Console.WriteLine();
                Console.WriteLine(msg);
                Console.WriteLine();
                cm.SendData(dataobject);

            }
            else
            {
                if (playerpath.Count > 0)
                {
                    playerpath.RemoveAt(0);
                }
                //Game.getOurPlayer().Direction = p;
                DObject dataobject = new DObject(msg, "127.0.0.1", 6000);
                Console.WriteLine();
                Console.WriteLine(msg);
                Console.WriteLine();
                cm.SendData(dataobject);
            }
            
   
        }

        public static void setPlayerPath()
        {
            List<int> playerpath = new List<int>();
            playerpath = Game.getMovableCells()[-1].getShortestPath();
            foreach (int p in playerpath)
            {
                if (Game.getOurPlayer().Direction != p)
                {
                    Game.getOurPlayer().Direction = p;
                }
                //wait for 1 second
                Thread.Sleep(1000);
                Game.getOurPlayer().Direction = p;
            }
            //Game.getOurPlayer().Direction=1;
        }
    }
}
