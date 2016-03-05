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

                Game.getMovableCells()[i].clearShortestPathList();
                if (BreadthFirstSearch.distTo[i] == -1)
                {
                    Game.getMovableCells()[i].setShortestPath(-1);
                }
                
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
            //int location = Game.getOurPlayer().getLocation();
            int location = Game.getMovableCells().Count() - 1;
            //int location;
            if (Game.getCoins() != null)
            {
                Console.WriteLine("Coin list is not null");
                try
                {
                    location = NearestThing.calNearestCoin();
                    Console.WriteLine("Nearest coin applied");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    location = Game.getMovableCells().Count() - 1;
                    Console.WriteLine("Nearest coin ** NOT ** applied");
                }
                
            }
            else
            {
                Console.WriteLine("Coin list is null");
                location = Game.getMovableCells().Count() - 1;
            }
            //location = NearestThing.calNearestCoin();
            //location = Game.getMovableCells().Count() - 1;
            Console.WriteLine("Index of the target in movableCells: " + Convert.ToString(location));
            Console.WriteLine("MovableCell size: " + Convert.ToString(Game.getMovableCells().Count()));
            Console.WriteLine("Targeting location: "+Convert.ToString(Game.getMovableCells()[location].getLocation()));
            playerpath = Game.getMovableCells()[location].getShortestPath();
            Console.WriteLine(playerpath.Count());
            int p = playerpath[0];
            Console.WriteLine("p is the direction: " +Convert.ToString(p));
            String msg ="UP#" ;
            int danX = Game.getOurPlayer().getCordinate()[0];
            int danY = Game.getOurPlayer().getCordinate()[1];
            Boolean danger = false;
            if (p == 0){
                msg = "UP#";
                 
                if (danY!=0 && (Map.mapDetail[danX, danY-1] == "S" || Map.mapDetail[danX, danY-1] == "W" || Map.mapDetail[danX, danY-1] == "B0" || Map.mapDetail[danX, danY-1] == "B1" || Map.mapDetail[danX, danY-1] == "B2" || Map.mapDetail[danX, danY-1] == "B3"))
                {
                    //Console.WriteLine("UP DANGER");
                    danger = true;
                }
            }
            else if (p == 1)
            {
                msg = "RIGHT#";
                if (danX != 9 && (Map.mapDetail[danX + 1, danY] == "S" || Map.mapDetail[danX + 1, danY] == "W" || Map.mapDetail[danX + 1, danY] == "B0" || Map.mapDetail[danX + 1, danY] == "B1" || Map.mapDetail[danX + 1, danY] == "B2" || Map.mapDetail[danX + 1, danY] == "B3"))
                {
                    Console.WriteLine("RIGHT DANGER");
                    danger = true;
                }
            }
            else if (p == 2)
            {
                msg = "DOWN#";
                if (danY != 9 && (Map.mapDetail[danX, danY + 1] == "S" || Map.mapDetail[danX, danY + 1] == "W" || Map.mapDetail[danX, danY + 1] == "B0" || Map.mapDetail[danX, danY + 1] == "B1" || Map.mapDetail[danX, danY + 1] == "B2" || Map.mapDetail[danX, danY + 1] == "B3"))
                {
                    Console.WriteLine("DOWN DANGER");
                    danger = true;
                }
            }
            else if (p == 3)
            {
                msg = "LEFT#";
                if (danX != 0 && (Map.mapDetail[danX - 1, danY] == "S" || Map.mapDetail[danX - 1, danY] == "W" || Map.mapDetail[danX - 1, danY] == "B0" || Map.mapDetail[danX - 1, danY] == "B1" || Map.mapDetail[danX - 1, danY] == "B2" || Map.mapDetail[danX - 1, danY] == "B3"))
                {
                    Console.WriteLine("LEFT DANGER");
                    danger = true;
                }
            }           
            //serverConnection.SendData(dataobject);
            
            if (Game.getOurPlayer().Direction != p)
            {
                //Game.getOurPlayer().Direction = p;
                DObject dataobject = new DObject(msg, "127.0.0.1", 6000);
                Console.WriteLine();
                Console.WriteLine("Not equal player direction"+msg);
                //Console.WriteLine(Game.getOurPlayer().Direction);
                cm.SendData(dataobject);

            }
            else
            {
                if(!danger)
                {
                    if (playerpath.Count() > 0)
                    {
                        playerpath.RemoveAt(0);
                    }
                    //Game.getOurPlayer().Direction = p;
                    DObject dataobject = new DObject(msg, "127.0.0.1", 6000);
                    Console.WriteLine();
                    Console.WriteLine("Equal player direction "+ msg);
                   // Console.WriteLine(Game.getOurPlayer().Direction);
                    cm.SendData(dataobject);
                }
                else
                {
                    //There is a danger in next move so avoid it
                    Console.WriteLine("IN DANGER"+Convert.ToString(Game.getOurPlayer().Direction));
                }
                
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
