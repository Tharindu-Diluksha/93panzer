﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankgameG36ConsoleApp1.Model
{
    //Copied from Server code
    class DObject
    {
        #region "Variables"
        private string msg = "";
        private string clientMachine = "";
        private int clientPort = -1;
        private List<string> playerIPList = new List<string>();
        private List<int> playerPortList = new List<int>();
        private int considerFrom = 0;
        #endregion

        #region "Properties"
        public string MSG
        {
            get { return msg; }
            set { msg = value; }
        }

        public string ClientMachine
        {
            get { return clientMachine; }
        }

        public int ClientPort
        {
            get { return clientPort; }
        }

        public List<string> PlayerIPList
        {
            get { return playerIPList; }
            set { playerIPList = value; }
        }

        public List<int> PlayerPortList
        {
            get { return playerPortList; }
            set { playerPortList = value; }
        }

        public int ConsiderFrom
        {
            get { return considerFrom; }
            set { considerFrom = value; }
        }


        #endregion

        #region "Public Methods"
        public DObject(string msgP, string clientMachineP, int clientPortP)
        {
            msg = msgP;
            clientMachine = clientMachineP;
            clientPort = clientPortP;
        }

        public DObject(string msgP, List<string> clientMachinesList, List<int> clientPortP)
        {
            msg = msgP;
            playerIPList = clientMachinesList;
            playerPortList = clientPortP;
        }
        #endregion
    }
}
