using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankgameG36ConsoleApp1.Model;
using System.Drawing;
using TankgameG36ConsoleApp1.Model;
using TankgameG36ConsoleApp1.AI;


namespace TankgameG36ConsoleApp1.Network
{
    class MessageHandler
    {

        private static String joinError; // when joing the server error occured : PLAYERS_FULL // ALREADY_ADDED // GAME_ALREADY_STARTED
        private static String movingshootingError; // OBSTACLE // CELL_OCCUPIED //TOO_QUICK // INVALID_CELL // GAME_NOT_STARTED_YET // NOT_A_VALID_CONTESTANT
        private static Boolean gameFinished; // GAME_FINISHED // GAME_HAS_FINISHED
        private static Boolean myPlayerDead; // DEAD //PITFALL 
        private static String error; // other exception 
        private static Boolean errorOccupied; // when exception happen, true
        //private static List<Player> playerList = new List<Player>();
        

        
        public MessageHandler()
        {
            errorOccupied = false;
        }

        public static void decode(string message)
        {
            Map.reduceTime();
            Map.achieveCoinLife();
            char firstL = message[0];
    
            if(message.StartsWith("S:"))  //**********************  Acceptance ******************
            {
                
                Console.WriteLine(message);
                acceptance_decode(message);
                Console.WriteLine("Acceptance Decode");
                
            }
            else if (message.StartsWith("I:")) //********** initialize ************************
            {
                //Console.WriteLine(message);
                GameInit_decode(message);
                Console.WriteLine("Game inition Decode");

            }
            else if(message.StartsWith("C:")) //***************** coins ***********************
            {
                Console.WriteLine("Found Coin");
                coin_popup(message);
            }
            else if (message.StartsWith("L:") ) // ************* life packs *******************
            {  
                Console.WriteLine("Found Life pack");
                lifepack_popup(message);

            }
            
            else if (message.StartsWith("G:")) ///************ update *************************
            {
                
                Console.WriteLine("Client update once a second");
                //Console.WriteLine(message);
                Client_update_decode(message);
            } 

            //******************* ERROR HANDLING ********************//
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

        //int count_init = 0;
        public static void GameInit_decode(string msg)
        {
            Game.gameinit();
            try
            {
                //I:P1: < x>,<y>;< x>,<y>;< x>,<y>…..< x>,<y>:  < x>,<y>;< x>,<y>;< x>,<y>…..< x>,<y>:
                String[] tokernizedGameDetails = msg.Split(':');
                String ourPlayer = tokernizedGameDetails[1];
                Game.setMyname(tokernizedGameDetails[1]);
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
                        // list of points one of wall, stone, water
                        if (count == 1)
                        {
                            //wall
                            Game.setWall(pointSplit);
                        }
                        else if (count == 2)
                        {
                            //stone
                            Game.setStone(pointSplit);
                        }
                        else if (count == 3)
                        {
                            //water
                            Game.setWater(pointSplit);
                        }
                    }
                    count++; // end of the for loop, increase count becouse of next time cordinate list will be next type
                }
                Map.initMap(Game.getWall(),Game.getStone(),Game.getWater());
                Program.printMap();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MessageHandler: Game initialization Failed! \n " + ex.Message);
                Console.WriteLine(ex);
                errorOccupied = true;
            }

        }

        public static void acceptance_decode(string msg)
        {
            
            String[] tokernizedPlayerDetails = msg.Split(':'); // :  p<num> ; X,Y ; direction   :
            tokernizedPlayerDetails = tokernizedPlayerDetails.Except(new String[] { tokernizedPlayerDetails[0] }).ToArray();
            foreach (String playerDetails in tokernizedPlayerDetails)
            {
               
                String[] word = playerDetails.Split(';'); // split into player , point, direction
                Player newPlayer = new Player();
                newPlayer.PlayerNo = word[0]; // set palyer
                //set point cordinates (X,Y)
                try
                {
                    String[] positionSplit = word[1].Split(',');
                   // newPlayer.Position = new Point(int.Parse(positionSplit[0]),int.Parse(positionSplit[1]));
                    newPlayer.setCordinates(int.Parse(positionSplit[0]), int.Parse(positionSplit[1]));

                    newPlayer.Direction = int.Parse(word[2]);
                    Game.setplayers(newPlayer);
                    
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Players initialisinf errror:- "+ e);
                }
                //set direction
                
            }
            Game.setOurPlayer();
            Map.initPlayers(Game.getPlayers());
            Program.printMap();
            
        }
        
        public static void Client_update_decode(string msg)
        {
            //G:P1;< player location  x>,< player location  y>;<Direction>;< whether shot>;<health>;< coins>;< points>: 
            String[] tokernizedGameDetails = msg.Split(':');
            //first element is G , un-wanted data remore that and clear the array
            tokernizedGameDetails = tokernizedGameDetails.Except(new String[] { tokernizedGameDetails[0] }).ToArray(); // remove G

            //************************************************* Brick damage catch *******************************************************
            //brick walls damage level          < x>,<y>,<damage-level> 
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
            foreach (String details in tokernizedGameDetails)
            {
                String[] playerDetails = details.Split(';');
                   
                    foreach (Player p in Game.getPlayers())
                    {
                        if (p.PlayerNo == playerDetails[0])  //in player details there are 6 items in msg
                        {
                            string[] playerXY = playerDetails[1].Split(',');
                            p.setCordinates(int.Parse(playerXY[0]), int.Parse(playerXY[1]));
                            p.Direction = int.Parse(playerDetails[2]);
                            p.Shoot = int.Parse(playerDetails[3]);
                            p.Health = int.Parse(playerDetails[4]);
                            p.Coins = int.Parse(playerDetails[5]);
                            p.Points = int.Parse(playerDetails[6]);
                        }
                    }
               
            }
            Map.updateMap();
            Program.printMap();
            
            /*Console.WriteLine("movablecellArray : ");
            Console.WriteLine(Game.getMovableCells().Count());
            foreach (ReachableCell r in Game.getMovableCells())
            {
                Console.Write(Convert.ToString(r.getLocation())+" ");
            }
            Console.WriteLine();*/
            AiBrain.runMeFindShortestPath();
            /*Console.WriteLine(Game.getMovableCells()[Game.getMovableCells().Count -1].getLocation());
            Console.WriteLine(Game.getMovableCells().Count);
            foreach (int i in Game.getMovableCells()[Game.getMovableCells().Count -1].getShortestPath())
            {
                Console.Write(Convert.ToString(i)+" ");
            }*/
        }

        public static void coin_popup(string msg)
        {
            //C:<x>,<y>:<LT>:<Val>#
            String[] coinDetails = msg.Split(':');
            String[] coinXY = coinDetails[1].Split(',');

            Coin newCoin = new Coin();
            newCoin.setCordinates(int.Parse(coinXY[0]), int.Parse(coinXY[1]));
            newCoin.setLife(int.Parse(coinDetails[2]));
            newCoin.setValue(int.Parse(coinDetails[3]));

            Game.setCoins(newCoin);
            Map.updateMap();

            Program.printMap();

        }

        private static void lifepack_popup(string message)
        {
            // L:<x>,<y>:<LT>#
            String[] lifePackDetails = message.Split(':');
            String[] lifepackXY = lifePackDetails[1].Split(',');

            LifePack newLifePack = new LifePack();
            newLifePack.setCordinates(int.Parse(lifepackXY[0]), int.Parse(lifepackXY[1]));
            newLifePack.setLife(int.Parse(lifePackDetails[2]));

            Game.setLifePacks(newLifePack);
            Map.updateMap();

            Program.printMap();
            
        }


        public Boolean ErrorOccupied()
        {
            return errorOccupied;
        }
     
    }
}
