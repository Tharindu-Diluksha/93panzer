using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.AI
{
    class GraphAdjList
    {
        /*private static readonly int V;
        private static readonly List<int>[] Adj;*/
        private static int V;
        private static List<int>[] Adj;

        public static  void setGraphAdjList(int v)
        {
            V = v;
            Adj = new List<int>[V];

            for (int i = 0; i < V; i++)
            {
                Adj[i] = new List<int>();
            }
        }

        public static  void AddEdge(int v, int w)
        {
            Adj[v].Add(w);
            Adj[w].Add(v);
        }

        public static  List<int> GetAdj(int v)
        {
            return Adj[v];
        }

        public static  int VertexCount
        {
            get
            {
                return V;
            }
        }
    }
}
