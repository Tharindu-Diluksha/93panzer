using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankgameG36ConsoleApp1.Model;
using System.Drawing;


namespace TankgameG36ConsoleApp1.Network
{
    class MessageHandler
    {

        private String joinError; // when joing the server error occured : PLAYERS_FULL // ALREADY_ADDED // GAME_ALREADY_STARTED
        private String movingshootingError; // OBSTACLE // CELL_OCCUPIED //TOO_QUICK // INVALID_CELL // GAME_NOT_STARTED_YET // NOT_A_VALID_CONTESTANT
        private Boolean gameFinished; // GAME_FINISHED // GAME_HAS_FINISHED
        private Boolean myPlayerDead; // DEAD //PITFALL 
        private String error; // other exception 
        private Boolean errorOccupied; // when exception happen, true

        

        
        public MessageHandler()
        {
            errorOccupied = false;
        }

        public void decode(string message)
        {

            char firstL = message[0];
    
            if(message.StartsWith("S:"))  //**********************  Acceptance ******************
            {
                Console.WriteLine("Acceptance Decode");
                Console.WriteLine(message);
                //acceptance_decode(message);
                
            }
            else if(message.StartsWith("C:")) //***************** coins ***********************
            {
                //coin_popup(message);
                Console.WriteLine("Found Coin");
            }
            else if (message.StartsWith("L:") ) // ************* life packs *******************
            {
               // lifepack_popup(message);
                Console.WriteLine("Found Life pack");

            }
            else if (message.StartsWith("I:")) //********** initialize ************************
            {
                GameInit_decode(message);
                Console.WriteLine("Game inition Decode");
                //Console.WriteLine(message);
            }
            else if (message.StartsWith("G:")) ///************ update *************************
            {
               // Client_update_decode(message);
                Console.WriteLine("Client update once a second");
            } //**************************************************************** ERROR HANDLING ****************************
            else if (message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_CONTESTANTSFULL) || message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_ALREADYADDED) || message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_GAMESTARTED))
            {   // PLAYERS_FULL // ALREADY_ADDED // GAME_ALREADY_STARTED
                joinError = message;
            }
            else if (message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_HITONOBSTACLE) || message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_CELLOCCUPIED) ||
                message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_TOOEARLY) || message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_INVALIDCELL) || message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_NOTSTARTED) || message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_NOTACONTESTANT))
            {   // OBSTACLE // CELL_OCCUPIED //TOO_QUICK // INVALID_CELL // GAME_NOT_STARTED_YET // NOT_A_VALID_CONTESTANT
                movingshootingError = message;
            }
            else if (message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_GAMEJUSTFINISHED) || message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_GAMEOVER))
            {   // GAME_FINISHED // GAME_HAS_FINISHED
                gameFinished = true;
                Console.WriteLine("Game Finished");
            }
            else if (message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_NOTALIVE) || message.Equals(TankgameG36ConsoleApp1.Controller.Constant.S2C_FALLENTOPIT))
            {   // DEAD //PITFALL 
                myPlayerDead = true;
            }
            else
            {
                error = message;
            }
        }

        

        public void acceptance_decode(string msg)
        {
            String[] tokernizedPlayerDetails = msg.Split(':'); // :  p<num> ; X,Y ; direction   :
            tokernizedPlayerDetails = tokernizedPlayerDetails.Except(new String[] { tokernizedPlayerDetails[0] }).ToArray();
            foreach (String playerDetails in tokernizedPlayerDetails)
            {
               
                String[] word = playerDetails.Split(';'); // split into player , point, direction
               // Player newPlayer = new Player();
               // newPlayer.PlayerNo = word[0]; // set palyer
                //set point cordinates (X,Y)
                try
                {
                    String[] positionSplit = word[1].Split(',');
                   // newPlayer.Position = new Point(int.Parse(positionSplit[0]),int.Parse(positionSplit[1]));

                  //  newPlayer.Direction = int.Parse(word[2]);
                    //**************** PLAYER SET IN THE GAMEBOARD ************
                
                  //    gameBoard.ServerAccepted(newPlayer);
                    //*********************************************************
                }
                catch (IndexOutOfRangeException e)
                {

                }
                //set direction
                
            }

        }
        //int count_init = 0;
        public void GameInit_decode(string msg)
        {
            List<Array> walllist = new List<Array>();
            List<Array> stonelist = new List<Array>();
            List<Array> waterlist = new List<Array>();
            
            try
            {
                //I:P1: < x>,<y>;< x>,<y>;< x>,<y>…..< x>,<y>:  < x>,<y>;< x>,<y>;< x>,<y>…..< x>,<y>:
                String[] tokernizedGameDetails = msg.Split(':');
                String ourPlayer = tokernizedGameDetails[1];

                //first item is player, by removing that we can easily listed cordinates of walls, stone and water cells
                tokernizedGameDetails = tokernizedGameDetails.Except(new String[] { ourPlayer, tokernizedGameDetails[0] }).ToArray();
                int count = 1;
                //List<Cell> newList = new List<Cell>();
                foreach (String details in tokernizedGameDetails)
                {
                    
                    String[] points = details.Split(';');
                    foreach (string word in points)
                    {
                        string[] pointSplit = word.Split(',');
                       /*Point p = new Point(int.Parse(pointSplit[0]),int.Parse(pointSplit[1]));
                        Cell newCell = new Cell(count, p, 4);
                        newList.Add(newCell);*/ // list of points one of wall, stone, water
                        if (count == 1)
                        {
                            //wall
                            walllist.Add(pointSplit);
                        }
                        else if (count == 2)
                        {
                            //stone
                            stonelist.Add(pointSplit);
                        }
                        else if (count == 3)
                        {
                            //water
                            waterlist.Add(pointSplit);
                        }
                    }
                    count++; // end of the for loop, increase count becouse of next time cordinate list will be next type
                }
                Map.initMap(walllist, stonelist, waterlist);

            }
            catch(Exception ex)
            {
                Console.WriteLine("MessageHandler: Game initialization Failed! \n " + ex.Message);
                errorOccupied = true;
            }
            
        }

        public void Client_update_decode(string msg)
        {
           // List<Player> gameClients = new List<Player>();
            //G:P1;< player location  x>,< player location  y>;<Direction>;< whether shot>;<health>;< coins>;< points>: 
            String[] tokernizedGameDetails = msg.Split(':');
            //first element is G , un-wanted data remore that and clear the array
            tokernizedGameDetails = tokernizedGameDetails.Except(new String[] { tokernizedGameDetails[0] }).ToArray(); // remove G

            //************************************************* Brick damage catch *******************************************************
            //brick walls damage level          < x>,<y>,<damage-level> 
           // List<Cell> brickDamage = new List<Cell>();
            String brickDamageStr = tokernizedGameDetails[tokernizedGameDetails.Length - 1]; // get last string in array( brick damage cordinates)
            String[] brickDetails = brickDamageStr.Split(';');
            foreach (String s in brickDetails)
            {
                String[] brick = s.Split(',');
               /* Point p = new Point(int.Parse(brick[0]), int.Parse(brick[1]));
                Cell c = new Cell(1, p, int.Parse(brick[2]));
                brickDamage.Add(c);*/
            }
            //*************************************************** players detais catch *******************************************************
            tokernizedGameDetails = tokernizedGameDetails.Except(new String[] { brickDamageStr }).ToArray(); // remove brick details
            int count = 1;
            foreach (String details in tokernizedGameDetails)
            {
                String[] playerDetails = details.Split(';');
                
                {   //in player details there are 6 items in msg
                   /* Player p = new Player();

                    p.PlayerNo = playerDetails[0];
                    // set player location (element 1 [0] )
                    String[] positionSplit_G = playerDetails[1].Split(',');
                    Point p_location = new Point(int.Parse(positionSplit_G[0]),int.Parse(positionSplit_G[1]));
                        
                    p.Position = p_location;
                    // set player direction 
                    p.Direction = int.Parse(playerDetails[2]);
                    //set player shots
                    p.Shots = int.Parse(playerDetails[3]);  //?????????????????????????????????????????  ERROR: CANT UNDERSTANT WHAT?     ????????????????????????
                    //set player health ( element 4 [3]
                    p.Health = int.Parse(playerDetails[4]);
                    // set player coins (elements 5 [4])
                    p.Coins = int.Parse(playerDetails[5]);
                    //set player points (element 6 [5]
                    p.Points = int.Parse(playerDetails[6]);
                    count++;// player number
                    gameClients.Add(p);*/
                }
                
                
            }


            //return gameClients and brick damages
           /* gameBoard.updatePlayerLocation(gameClients);
            gameBoard.updateDamageBricks(brickDamage);*/
        }

        public void coin_popup(string msg)
        {
            //C:<x>,<y>:<LT>:<Val>#
            String[] coinDetails = msg.Split(':');
            String[] cointSplit = coinDetails[1].Split(',');
           /* Point p = new Point(int.Parse(cointSplit[0]), int.Parse(cointSplit[1]));
            Coin c = new Coin(p, int.Parse(coinDetails[2]), int.Parse(coinDetails[3]));
            gameBoard.setCoins(c);*/

        }

        private void lifepack_popup(string message)
        {
            // L:<x>,<y>:<LT>#
            String[] lifePackDetails = message.Split(':');
            String[] lifeSplit = lifePackDetails[1].Split(',');
            /* Point p = new Point(int.Parse(lifeSplit[0]), int.Parse(lifeSplit[1]));
             LifePack L = new LifePack(p, int.Parse(lifePackDetails[2]));
             gameBoard.setLifePack(L);*/
        }


        public Boolean ErrorOccupied()
        {
            return errorOccupied;
        }
     
    }
}
