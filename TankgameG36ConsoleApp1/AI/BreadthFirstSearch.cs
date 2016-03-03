using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.AI
{
    class BreadthFirstSearch
    {
        public static int[] edgeTo;
        public static int[] distTo;
        public static int s;

        public static BreadthFirstSearch(int start)
        {
            edgeTo = new int[GraphAdjList.VertexCount];
            distTo = new int[GraphAdjList.VertexCount];

            for (int i = 0; i < GraphAdjList.VertexCount; i++)
            {
                distTo[i] = -1;
                edgeTo[i] = -1;
            }

            //this.s = s;

            BFS(start);
        }

        static void BFS(int s)
        {
            var queue = new Queue<int>();
            queue.Enqueue(s);
            distTo[s] = 0;

            while (queue.Count != 0)
            {
                int v = queue.Dequeue();

                foreach (var w in GraphAdjList.GetAdj(v))
                {
                    if (distTo[w] == -1)
                    {
                        queue.Enqueue(w);
                        distTo[w] = distTo[v] + 1;
                        edgeTo[w] = v;
                    }
                }
            }
        }
    }
}
