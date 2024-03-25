
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Game
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool reset = true;
        while(reset == true)
        {
            //TEST - Game reset, check reset value change
            //Console.WriteLine("While loop start "+reset);
            
            //boolean variable to determine when the game will end.
            bool gameOver = false;
            
            //variable used to determine if the cheese in a square has been consumed or not.
            int cheeseCount = 0;
    
            // Define the size of the game map
            Console.WriteLine("Enter the size of the board");
            int size = Convert.ToInt32(Console.ReadLine());
            
            //inputing the number of cheese to be added in the map
            Console.WriteLine("Enter the number of cheese you want in the game");
            int noc = Convert.ToInt32(Console.ReadLine());
    
            //creating 3 seperate arrays to keep track of arrow movements, cheeses and player positions visually
            char[,] map = new char[size, size];
            char[,] cheeseMap = new char[size, size];
            char[,] playerDisplayMap = new char[size, size];
            
            //calling the generateboard function to create the board with random values obeying boundary conditions
            map = GenerateBoard(size);
            
            //assigning the same to cheese map
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    cheeseMap[row, col] = map[row, col];
                }
            }
            
            //generating cheese randomly based on the number of cheese(noc) and distributing it across the map of "size"
            cheeseMap = GenerateCheese(cheeseMap, noc, size);
            
            //assigning the same values in cheese map to player map
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    playerDisplayMap[row, col] = cheeseMap[row, col];
                }
            }
            
            
            //inputing the number of players
            Console.WriteLine("Enter the number of players [Min: 2   Max: 4");
            int playerCount = Convert.ToInt32(Console.ReadLine());
            
            //condition to ensure player count within limits
            while((playerCount<2) || (playerCount>4))
            {
                Console.WriteLine("Minimum of 2 players and a maximum or 9 players needed. Enter again!");
                playerCount = Convert.ToInt32(Console.ReadLine());
            }
    
    
            //creating array to store the X and Y cordinates of each player
            int[,] playerPosition = new int[playerCount, 2];
            
            //array to store player names.
            string[] playerNames = new string[playerCount];
            for(int temp = 0; temp < playerCount; temp++)
            {
                Console.WriteLine("Enter the name of player "+(temp+1));
                playerNames[temp] = Console.ReadLine();
            }
            
            //array that stores icons to show visually for each player
            char[] playerIcon = { '\u2780','\u2781','\u2782','\u2783','\u2784','\u2785','\u2786','\u2787','\u2788','\u2789'};
    
            //setting initial player position in the map.
            for (int i = 0; i < playerCount; i++)
            {
                playerPosition[i, 0] = size - 1;
                playerPosition[i, 1] = i;
                playerDisplayMap[size-1,i] = playerIcon[i];
            }
            
            playerDisplayMap[0,size-1] = '\u2B50';
            
            
    
    
    
            // Display the map with only arrows initially for reference
            Console.WriteLine("=========================");
            Console.WriteLine("HYPER SPACE MAP GENERATED");
            Console.WriteLine("=========================");
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Console.Write(map[row, col] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" ");
            
            
            // display the player map with all the players set at the begining
            Console.WriteLine("====================================");
            Console.WriteLine("WELCOME TO HYPER SPACE CHEESE BATTLE");
            Console.WriteLine("====================================");
            Console.WriteLine(" ");
            
            //Display the game rules
            Console.WriteLine("Game Rules:");
            Console.WriteLine("[1] Each player takes it in turn to throw one dice");
            Console.WriteLine("[2] Every player start of at the last row");
            Console.WriteLine("[3] At each turn the rocket moves in the direction on arrow based on the dice roll");
            Console.WriteLine("[4] If you land on a cheese : ♥");
            Console.WriteLine("      -> Shoot down any player back to initial position");
            Console.WriteLine("      -> Refuel and take another turn to roll the dice");
            Console.WriteLine("[5] If you land on another player, the player moves again to the next square in the arrow direction");
            Console.WriteLine("[6] Each player wins the game by reaching the top right corner with the ♔");
            Console.WriteLine("====================================");
            Console.WriteLine(" ");
            
            
            DisplayMap(playerDisplayMap, size);
            Console.WriteLine(" ");
            //Display the players in the game
            Console.WriteLine("PLAYERS");
            for(int temp = 0; temp < playerCount; temp++)
            {
                Console.WriteLine("["+(temp+1)+"]"+" "+playerNames[temp]);
            }
            Console.WriteLine("====================================");
    
            // while condition to keep iterating until game is not over
            while (gameOver == false)
            {
                //iterating through each player turn
                for (int turn = 0; turn < playerCount; turn++)
                {
                    // initializing local variables to store dice value rolled , x and y cordinates.
                    int diceVal = 0;
                    int playerX;
                    int playerY;
                    string roll;
                    
                    //moving the player based on the arrow and the dice value
                    Console.WriteLine("Player " + (turn + 1) + " dice roll. [yes/no]");
                    roll = Console.ReadLine();
                    if (String.Equals(roll, "yes"))
                    {
                        diceVal = DiceRoll();
                        Console.WriteLine("You have rolled :" + diceVal);
                    }
                    playerX = playerPosition[turn, 0];
                    playerY = playerPosition[turn, 1];
                    
                    //UP movement
                    if (map[playerX, playerY] == '\u219F')
                    {
                        if ((playerX - diceVal) >= 0)
                        {
                            playerX = playerX - diceVal;
                            //-----TEST
                            Console.WriteLine("rocket "+(turn+1)+" moving up to cordinates ["+(size-1-playerX)+","+playerY+"]");
                            //-----TEST
                        }
                        else
                        {
                            Console.WriteLine("Out of boundary. Rocket cannot move up!");
                        }
                    }
                    //DOWN movement
                    else if (map[playerX, playerY] == '\u21A1')
                    {
                        if ((playerX + diceVal) < size)
                        {
                            playerX = playerX + diceVal;
                            //-----TEST
                            Console.WriteLine("rocket "+(turn+1)+" moving down to cordinates ["+(size-1-playerX)+","+playerY+"]");
                            //-----TEST
                        }
                        else
                        {
                            Console.WriteLine("Out of boundary. Rocket cannot move down!");
                        }
                    }
                    //LEFT movement
                    else if (map[playerX, playerY] == '\u219E')
                    {
                        if ((playerY - diceVal) >= 0)
                        {
                            playerY = playerY - diceVal;
                            //-----TEST
                            Console.WriteLine("rocket "+(turn+1)+" moving left to cordinates ["+(size-1-playerX)+","+playerY+"]");
                            //-----TEST
                        }
                        else
                        {
                            Console.WriteLine("Out of boundary. Rocket cannot move left!");
                        }
                    }
                    //RIGHT movement
                    else if (map[playerX, playerY] == '\u21A0')
                    {
                        if ((playerY + diceVal) < size)
                        {
                            playerY = playerY + diceVal;
                            //-----TEST
                            Console.WriteLine("rocket "+(turn+1)+" moving right to cordinates ["+(size-1-playerX)+","+playerY+"]");
                            //-----TEST
                        }
                        else
                        {
                            Console.WriteLine("Out of boundary. Rocket cannot move right!");
                        }
                    }
                    playerPosition[turn, 0] = playerX;
                    playerPosition[turn, 1] = playerY;
                    /* handled the boundary conditions and have checked it the player rolls a dice that may
                       land the player outside the boundary of the map.*/
                    
    
                    //----------------------------------------------------------TEST
                    //Console.WriteLine("turn" + turn);
                    //Console.WriteLine("size" + size);
                    //Console.WriteLine(playerX + " " + playerY);
                    
                    //correcting overlap if occured
                    playerPosition = correctOverlap(map, playerPosition, turn, size);
                    
                    //updating the map and displaying it based on the current turn
                    playerDisplayMap = UpdateMap(playerDisplayMap, playerPosition, cheeseMap, playerIcon, size);
                    DisplayMap(playerDisplayMap, size);
                    
                    //if the player landed on cheese, promting user for the action to be done
                    while (cheeseMap[playerX, playerY] == '\u26F5' && cheeseCount == 0)
                    {
                        cheeseMap[playerX, playerY] = map[playerX, playerY];
                        Console.WriteLine("You have landed on a cheese. Do you want to consume it and...");
                        Console.WriteLine("[1] shoot down a player");
                        Console.WriteLine("[2] Take another turn");
                        int cheeseTurn = Convert.ToInt32(Console.ReadLine());
                        
                        //for shooting down a player
                        if (cheeseTurn == 1)
                        {
                            // prompting user to type in , which player to shoot down
                            Console.WriteLine("Which player do you want to shoot down");
                            int playerShoot = Convert.ToInt32(Console.ReadLine());
                            
                            /*same steps followed above has been done here,
                              player shot down to initial position and the checked 
                              for overlap*/
                              
                            playerPosition[playerShoot - 1, 0] = size-1;
                            playerPosition[playerShoot - 1, 1] = 0;
                            playerPosition = correctOverlap(map, playerPosition, (playerShoot - 1), size);
                            cheeseCount = cheeseCount + 1;
                            Console.WriteLine("F.I.R.E");
                            Console.WriteLine("========================================");
                            Console.WriteLine("Player "+playerShoot+" shot down to co-ordinates "+playerPosition[playerShoot-1,0]+" , "+playerPosition[playerShoot-1,1]);
                            Console.WriteLine("========================================");
                            
                            playerDisplayMap = UpdateMap(playerDisplayMap, playerPosition, cheeseMap, playerIcon, size);
                            DisplayMap(playerDisplayMap, size);
                        }
                        //for another turn
                        else if (cheeseTurn == 2)
                        {
                            //---------------------------------
                            Console.WriteLine("Player " + (turn + 1) + " can roll dice again. [yes/no]");
                            roll = Console.ReadLine();
                            if (String.Equals(roll, "yes"))
                            {
                                diceVal = DiceRoll();
                                Console.WriteLine("You have rolled :" + diceVal);
                            }
                            playerX = playerPosition[turn, 0];
                            playerY = playerPosition[turn, 1];
                            
                            //repeating the same steps again, for another turn
                            if (map[playerX, playerY] == '\u219F')
                            {
                                if ((playerX - diceVal) >= 0)
                                {
                                    playerX = playerX - diceVal;
                                    //-----TEST
                                    Console.WriteLine("rocket "+(turn+1)+" moving up to cordinates ["+(size-1-playerX)+","+playerY+"]");
                                    //-----TEST
                                }
                                else
                                {
                                    Console.WriteLine("Out of boundary. Rocket cannot move up!");
                                }
                            }
                            //DOWN movement
                            else if (map[playerX, playerY] == '\u21A1')
                            {
                                if ((playerX + diceVal) < size)
                                {
                                    playerX = playerX + diceVal;
                                    //-----TEST
                                    Console.WriteLine("rocket "+(turn+1)+" moving down to cordinates ["+(size-1-playerX)+","+playerY+"]");
                                    //-----TEST
                                }
                                else
                                {
                                    Console.WriteLine("Out of boundary. Rocket cannot move down!");
                                }
                            }
                            //LEFT movement
                            else if (map[playerX, playerY] == '\u219E')
                            {
                                if ((playerY - diceVal) >= 0)
                                {
                                    playerY = playerY - diceVal;
                                    //-----TEST
                                    Console.WriteLine("rocket "+(turn+1)+" moving left to cordinates ["+(size-1-playerX)+","+playerY+"]");
                                    //-----TEST
                                }
                                else
                                {
                                    Console.WriteLine("Out of boundary. Rocket cannot move left!");
                                }
                            }
                            //RIGHT movement
                            else if (map[playerX, playerY] == '\u21A0')
                            {
                                if ((playerY + diceVal) < size)
                                {
                                    playerY = playerY + diceVal;
                                    //-----TEST
                                    Console.WriteLine("rocket "+(turn+1)+" moving right to cordinates ["+(size-1-playerX)+","+playerY+"]");
                                    //-----TEST
                                }
                                else
                                {
                                    Console.WriteLine("Out of boundary. Rocket cannot move right!");
                                }
                            }
                            playerPosition[turn, 0] = playerX;
                            playerPosition[turn, 1] = playerY;
                            
                            //correcting overlap , updating map and then displaying it.
                            playerPosition = correctOverlap(map, playerPosition, turn, size);
                            playerDisplayMap = UpdateMap(playerDisplayMap, playerPosition, cheeseMap, playerIcon, size);
                            DisplayMap(playerDisplayMap, size);
                            //---------------------
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice selected. You lost the cheese!");
                        }
                    }
                    //cheesecount assigned back to 0 after the loop ended. A fresh start again untill next cheese is encountered.
                    cheeseCount = 0;
                    
                    // checking at the end of the turn , if the player has reached the win position.
                    if (playerPosition[turn, 0] == 0 && playerPosition[turn, 1] == (size-1))
                    {
                        DisplayMap(playerDisplayMap, size);
                        Console.WriteLine("Player " + (turn + 1) + " wins!");
                        Console.WriteLine("WINNER NAME : "+playerNames[turn]);
                        turn = playerCount;
                        
                        Console.WriteLine("Do you want to start again?[yes/no]");
                        string resetInput = Console.ReadLine();
                        
                        if(string.Equals(resetInput,"yes"))
                        {
                            reset = true;
                        }
                        else
                        {
                            Console.WriteLine("===============================================");
                            Console.WriteLine("Thank you for playing HYPER SPACE CHEESE BATTLE");
                            Console.WriteLine("===============================================");
                            reset = false;
                        }
                        
                        //TEST - Game reset, check reset value change
                        //Console.WriteLine("While loop end "+reset);
                        gameOver = true;
                        break;
                        //exit from loop once any player has won.
                    }
                }
            }
            
        }
        
    }





    //-------------Generate Board--------------//
    static char[,] GenerateBoard(int size)
    {
        char[,] board = new char[size, size];

        // arrow symbols used in this specific order to create the map following the boundary conditions.
        //1. the corners of the maps can have only 2 directions
        //2. the sides of the map can have only 3 directions
        //3. all other squares can have any 4 directions
        char[] arrow1 = new char[4];
        arrow1[0] = '\u219F';
        arrow1[1] = '\u219E';
        arrow1[2] = '\u21A0';
        arrow1[3] = '\u21A1';

        char[] arrow2 = new char[4];
        arrow2[0] = '\u21A0';
        arrow2[1] = '\u219F';
        arrow2[2] = '\u21A1';
        arrow2[3] = '\u219E';

        //random function to randomly pick directions
        Random rand = new Random();
        int randomIndex;

        // map created based on the above boundary rules
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (row == 0)
                {
                    if (col == 0)
                    {
                        randomIndex = rand.Next(2, arrow1.Length);
                        board[row, col] = arrow1[randomIndex];
                    }
                    else if (col == (size - 1))
                    {
                        randomIndex = rand.Next(2, arrow2.Length);
                        board[row, col] = arrow2[randomIndex];
                    }
                    else
                    {
                        randomIndex = rand.Next(1, arrow1.Length);
                        board[row, col] = arrow1[randomIndex];
                    }
                }
                else if (col == 0)
                {
                    if (row == (size - 1))
                    {
                        randomIndex = rand.Next(0, 2);
                        board[row, col] = arrow2[randomIndex];
                    }
                    else
                    {
                        randomIndex = rand.Next(0, 3);
                        board[row, col] = arrow2[randomIndex];
                    }
                }
                else if (row == (size - 1))
                {
                    if (col == (size - 1))
                    {
                        randomIndex = rand.Next(0, 2);
                        board[row, col] = arrow1[randomIndex];
                    }
                    else
                    {
                        randomIndex = rand.Next(0, 3);
                        board[row, col] = arrow1[randomIndex];
                    }
                }
                else if (col == (size - 1))
                {
                    randomIndex = rand.Next(1, arrow2.Length);
                    board[row, col] = arrow2[randomIndex];
                }
                else
                {
                    randomIndex = rand.Next(0, arrow2.Length);
                    board[row, col] = arrow2[randomIndex];
                }
            }
        }
        return board;
    }

    //-------------------Generate Cheese------------------//
    static char[,] GenerateCheese(char[,] map, int NumberOfCheese, int size)
    {
        int cheeseGenI;
        int cheeseGenJ;
        // generating the x and y cordinates randomly for the cheese
        Random rand = new Random();
        char[,] cMap = map;

        for (int x = 0; x < NumberOfCheese; x++)
        {
            cheeseGenI = rand.Next(0, size - 1);
            cheeseGenJ = rand.Next(0, size);
            
            //TEST
            //Console.WriteLine("X cordinate :"+cheeseGenI);
            //Console.WriteLine("Y cordinate :"+cheeseGenJ);
            
            cMap[cheeseGenI, cheeseGenJ] = '\u26F5'; // this symbol used for cheese
        }
        return cMap;
    }






    //-------------------Update Map-------------------------//
    static char[,] UpdateMap(char[,] playerDisplayMap, int[,] playerPosition, char[,] cheeseMap, char[] playerIcon, int size)
    {
        //updating the display map based on any changes in player positions
        int length = playerPosition.Length / 2;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    if( playerDisplayMap[j,k] == playerIcon[i])
                    {
                        if(!((playerPosition[i,0] == j) && (playerPosition[i,1] == k)))
                        {
                            playerDisplayMap[j,k] = cheeseMap[j,k];
                            playerDisplayMap[playerPosition[i,0],playerPosition[i,1]] = playerIcon[i];
                        }
                    }
                }
            }
        }
        return playerDisplayMap;
    }

    //----------------------Dice Roll------------------------//
    static int DiceRoll()
    {
        // generate random value between 1 and 6 inclusive.
        Random rand = new Random();
        return rand.Next(1, 7);
    }

    //-------------------------Handle Overlap-----------------//
    static int[,] correctOverlap(char[,] map, int[,] playerPosition, int lastChangedPlayer, int size)
    {
        int length = playerPosition.Length / 2;
        int count = 2;
        // correcting the overlap of players if encountered.
        // 
        // while loop to keep track of multiple overlap
        while(count>1)
        {
            count = 1;
            //loop through all players to check if any two have the same x and y cordinates
            for (int i = 0; i < length; i++)
            {
                //TEST
                //Console.WriteLine(i);
                if (i != lastChangedPlayer)
                {
                    if((playerPosition[lastChangedPlayer,0] == playerPosition[i,0]) && (playerPosition[lastChangedPlayer,1] == playerPosition[i,1]))
                    {
                        if(map[playerPosition[lastChangedPlayer,0],playerPosition[lastChangedPlayer,1]] == '\u219F')
                        {
                            playerPosition[lastChangedPlayer,0] = playerPosition[lastChangedPlayer,0] - 1;
                            count++;
                            Console.WriteLine("Player landed on a square with another player. Moving UP");
                        }
                        else if(map[playerPosition[lastChangedPlayer,0],playerPosition[lastChangedPlayer,1]] == '\u219E')
                        {
                            playerPosition[lastChangedPlayer,1] = playerPosition[lastChangedPlayer,1] - 1;
                            count++;
                            Console.WriteLine("Player landed on a square with another player. Moving LEFT");
                        }
                        else if(map[playerPosition[lastChangedPlayer,0],playerPosition[lastChangedPlayer,1]] == '\u21A0')
                        {
                            playerPosition[lastChangedPlayer,1] = playerPosition[lastChangedPlayer,1] + 1;
                            count++;
                            Console.WriteLine("Player landed on a square with another player. Moving RIGHT");
                        }
                        else if(map[playerPosition[lastChangedPlayer,0],playerPosition[lastChangedPlayer,1]] == '\u21A1')
                        {
                            playerPosition[lastChangedPlayer,0] = playerPosition[lastChangedPlayer,0] + 1;
                            count++;
                            Console.WriteLine("Player landed on a square with another player. Moving DOWN");
                        }
                    }
                }
                //TEST
                //Console.WriteLine(count);
            }
        }
        
        return playerPosition;
    }
    
    //-----FUNCTION TO DISPLAY THE GAME MAP-----//
    static void DisplayMap(char [,] mapFunc, int size)
    {
        Console.WriteLine(" ");
        
        for (int row = 0; row < size; row++)
        {
            Console.Write((size-row-1) +" |");
            for (int col = 0; col < size; col++)
            {
                Console.Write("| " + mapFunc[row, col] + " |");
            }
            Console.WriteLine();
            Console.Write("   ");
            for (int col = 0; col < size; col++)
            {
                Console.Write("-----");
            }
            Console.WriteLine();
        }
        Console.Write("   ");
        for (int col = 0; col < size; col++)
        {
            Console.Write("  " + col + "  ");
        }
        
        Console.WriteLine(" ");
        Console.WriteLine(" ");
    }
    
}