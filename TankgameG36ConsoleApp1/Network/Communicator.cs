﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

using TankgameG36ConsoleApp1.Model;

namespace TankgameG36ConsoleApp1.Network
{
    /// <summary>
    /// Is responsible for communication handling
    /// </summary>
    class Communicator
    {
        #region "Variables"
        private NetworkStream clientStream; //Stream - outgoing
        private TcpClient client; //To talk back to the client
        private BinaryWriter writer; //To write to the clients

        private NetworkStream serverStream; //Stream - incoming        
        private TcpListener listener; //To listen to the clinets        
        public string reply = ""; //The message to be written

        private static Communicator comm = new Communicator();
        #endregion

        private Communicator()
        {
            
        }

        public static Communicator GetInstance()
        {
            
            return comm;
        }

        public void ReceiveData()
        {
            MessageHandler messagehandler = new MessageHandler();
            bool errorOcurred = false;
            Socket connection = null; //The socket that is listened to       
            try
            {
                //Creating listening Socket
                this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);
                //Starts listening
                this.listener.Start();
                //Establish connection upon client request
                DObject dataObj;
                while (true)
                {
                
                    //connection is connected socket
                    connection = listener.AcceptSocket();
                    if (connection.Connected)
                    {
                        
                        //To read from socket create NetworkStream object associated with socket
                        this.serverStream = new NetworkStream(connection);

                        SocketAddress sockAdd = connection.RemoteEndPoint.Serialize();
                        string s = connection.RemoteEndPoint.ToString();
                        List<Byte> inputStr = new List<byte>();

                        int asw = 0;
                        while (asw != -1)
                        {
                            asw = this.serverStream.ReadByte();
                            inputStr.Add((Byte)asw);
                        }

                        reply = Encoding.UTF8.GetString(inputStr.ToArray());
                        //Console.WriteLine("reply " + reply);
                        this.serverStream.Close();
                        string ip = s.Substring(0, s.IndexOf(":"));
                       int port = 6000; // Constant.CLIENT_PORT;
                        try
                        {
                            string ss = reply.Substring(0, reply.IndexOf(";"));
                            port = Convert.ToInt32(ss);
                        }
                        catch (Exception)
                        {
                            port = 6000; //Constant.CLIENT_PORT;
                        }
                        //Console.WriteLine("connected to server");
                        //Console.WriteLine(ip + ": " + reply.Substring(0, reply.Length - 1));
                       
                       //Thread.Sleep(1000);
                        messagehandler.decode(reply.Substring(0, reply.Length - 2));
                   }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (RECEIVING) Failed! \n " + e.Message);
                errorOcurred = true;
                Console.ReadLine();
                
            }
            finally
            {
                if (connection != null)
                    if (connection.Connected)
                        connection.Close();
                if (errorOcurred)
                   this.ReceiveData();
                    
            }
        }

        public void SendData(object stateInfo)
        {
            DObject dataObj = (DObject)stateInfo;
            //Opening the connection
            this.client = new TcpClient();

            try
            {
                if (dataObj.ClientPort == 6000)
                {

                    this.client.Connect(dataObj.ClientMachine, dataObj.ClientPort);

                    if (this.client.Connected)
                    {
                        //To write to the socket
                        this.clientStream = client.GetStream();

                        //Create objects for writing across stream
                        this.writer = new BinaryWriter(clientStream);
                        Byte[] tempStr = Encoding.ASCII.GetBytes(dataObj.MSG);

                        //writing to the port                
                        this.writer.Write(tempStr);
                        Console.WriteLine("\t Data: " + dataObj.MSG + " is written to " + dataObj.ClientMachine + " on " + dataObj.ClientPort);
                        this.writer.Close();
                        this.clientStream.Close();

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (WRITING) to " + dataObj.ClientMachine + " on " + dataObj.ClientPort + "Failed! \n " + e.Message);

            }
            finally
            {
                this.client.Close();
            }
        }

       

    }
}
