using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TankgameG36ConsoleApp1.Model;
using TankgameG36ConsoleApp1.Network;

namespace TankgameG36ConsoleApp1
{
    class Program
    {
        public static Communicator serverConnection = Communicator.GetInstance();
        public static MessageHandler messagehandler = new MessageHandler();

        static void Main(string[] args)
        {
            System.Console.WriteLine("In the main");

            DObject dataobject = new DObject("JOIN#", "127.0.0.1", 6000);
            serverConnection.SendData(dataobject);


            Thread receive = new Thread(new ThreadStart(serverConnection.ReceiveData)); // creating thread for listner
            receive.Start();
            Thread.Sleep(1000);
            
            /*Thread print = new Thread(new ThreadStart(printMap));
            print.Start();
            Thread.Sleep(1000);*/
            
            // This is used in methods to print
            

            
            
           
        }

        public static void printMap()
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {

                        if (Map.mapDetail[j, i] == null)
                        {
                            Console.Write("N ");
                        }
                        else
                        {
                            Console.Write(Map.mapDetail[j, i] + " ");
                        }


                    }
                    Console.WriteLine();
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("nothing to update");
            }
        }

       

    }
}
