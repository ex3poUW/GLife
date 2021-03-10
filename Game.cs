using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer --- Description
// 2021-01-30 - CJ Cummings - Wrote part of GLife   
// 2021-02-04 - CJ Cummings - Wrote the rules to GLife and refined the board
// 2021-02-04 - CJ Cummings - Wrote Gosper's Gun situation
// 2021-02-04 - CJ Cummings - Wrote the dirty puffer preset
/////////////////////////////////////////////////////////////////////////////
// TINFO 200 A, Winter 2021
// UWTacoma SET, CJ Cummings
// Due 2021-02-06, L5Life
// GLife.Game is a simulation of Conway's Game of Life with 4 different starting
// presets for the user to view as well as decide how many generations the simulation
// can last.
namespace GLife
{
    internal class Game
    {
        public readonly int ROW_SIZE = 50;
        public readonly int COL_SIZE = 80;

        public const char LIVE = '■'; //Space represents alive to contrast with the white rectangle char
        public const char DEAD = '`'; //♀ ☼ ■ █ × <- Symbols to play with
        public const char SPACE = ' ';

        private char[,] gameBoard;
        private char[,] resultsBoard;

        //Creates the arrays representing the game boards in the size specified by
        //ROW_SIZE and COL_SIZE
        public Game()
        {
            gameBoard = new char[ROW_SIZE, COL_SIZE];
            resultsBoard = new char[ROW_SIZE, COL_SIZE];

            InitializeBoards();
            InsertStartupPatterns();
        }
        //Allows the user to specify which preset will be inserted into the gameBoard.
        private void InsertStartupPatterns()
        {
            Console.WriteLine("***Starting Patterns Available***");
            Console.WriteLine("[1] - Linear arrangement of cells - longer period before repeat");
            Console.WriteLine("[2] - Glider arrangement of cells - moves off the board");
            Console.WriteLine("[3] - Puffer arrangement of cells - produces an output stream of cells");
            Console.WriteLine("[4] - Gosper arrangement of cells - produces Gosper's glider gun");
            Console.Write("ENTER the starting pattern choice: ");
            int startPattern = int.Parse(Console.ReadLine());

            switch (startPattern)
            {
                case 1:
                    InsertPattern1(10, 15);
                    InsertPattern1(30, 20);
                    break;
                case 2:
                    InsertPattern2(10, 2);
                    InsertPattern2(4, 10);
                    InsertPattern2(20, 13);
                    InsertPattern2(10, 18);
                    InsertPattern2(30, 5);
                    break;
                case 3:
                    InsertPattern3(1, 20);
                    break;
                case 4:
                    InsertPattern4(5, 19);
                    break;
                default:
                    InsertPattern1(45, 35);
                    break;
            }
        }
        //Linear Preset
        private void InsertPattern1(int r, int c)
        {
            //Insert 8 LIVE cells for first segment
            gameBoard[r, c + 1] = LIVE;
            gameBoard[r, c + 2] = LIVE;
            gameBoard[r, c + 3] = LIVE;
            gameBoard[r, c + 4] = LIVE;
            gameBoard[r, c + 5] = LIVE;
            gameBoard[r, c + 6] = LIVE;
            gameBoard[r, c + 7] = LIVE;
            gameBoard[r, c + 8] = LIVE;

            // 1 DEAD cell
            // 5 LIVE cells
            gameBoard[r, c + 10] = LIVE;
            gameBoard[r, c + 11] = LIVE;
            gameBoard[r, c + 12] = LIVE;
            gameBoard[r, c + 13] = LIVE;
            gameBoard[r, c + 14] = LIVE;

            // 3 DEAD cell
            // 3 LIVE cells
            gameBoard[r, c + 18] = LIVE;
            gameBoard[r, c + 19] = LIVE;
            gameBoard[r, c + 20] = LIVE;

            // 6 DEAD cell
            // 7 LIVE cells
            gameBoard[r, c + 27] = LIVE;
            gameBoard[r, c + 28] = LIVE;
            gameBoard[r, c + 29] = LIVE;
            gameBoard[r, c + 30] = LIVE;
            gameBoard[r, c + 31] = LIVE;
            gameBoard[r, c + 32] = LIVE;
            gameBoard[r, c + 33] = LIVE;

            // 1 DEAD cell
            // 5 LIVE cells
            gameBoard[r, c + 35] = LIVE;
            gameBoard[r, c + 36] = LIVE;
            gameBoard[r, c + 37] = LIVE;
            gameBoard[r, c + 38] = LIVE;
            gameBoard[r, c + 39] = LIVE;
            // END of pattern
        }
        //Glider Preset
        private void InsertPattern2(int r, int c)
        {
            gameBoard[r - 1, c - 1] = LIVE; 
            gameBoard[r - 1, c + 1] = LIVE;
            gameBoard[r, c] = LIVE;
            gameBoard[r, c + 1] = LIVE;
            gameBoard[r + 1, c] = LIVE;
        }
        //Puffer Preset
        private void InsertPattern3(int r, int c)
        {
            //West Section
            gameBoard[r + 3, c] = LIVE;
            gameBoard[r + 4, c + 1] = LIVE;
            gameBoard[r, c + 2] = LIVE;
            gameBoard[r + 4, c + 2] = LIVE;
            gameBoard[r + 1, c + 3] = LIVE;
            gameBoard[r + 2, c + 3] = LIVE;
            gameBoard[r + 3, c + 3] = LIVE;
            gameBoard[r + 4, c + 3] = LIVE;
            //Middle Section
            gameBoard[r, c + 7] = LIVE;
            gameBoard[r + 1, c + 8] = LIVE;
            gameBoard[r + 2, c + 8] = LIVE;
            gameBoard[r + 2, c + 9] = LIVE;
            gameBoard[r + 2, c + 10] = LIVE;
            gameBoard[r + 1, c + 11] = LIVE;
            //East Section
            gameBoard[r + 3, c + 14] = LIVE;
            gameBoard[r + 4, c + 15] = LIVE;
            gameBoard[r, c + 16] = LIVE;
            gameBoard[r + 4, c + 16] = LIVE;
            gameBoard[r + 1, c + 17] = LIVE;
            gameBoard[r + 2, c + 17] = LIVE;
            gameBoard[r + 3, c + 17] = LIVE;
            gameBoard[r + 4, c + 17] = LIVE;
        }
        //Gosper Preset
        private void InsertPattern4(int r, int c)
        {
            //https://en.wikipedia.org/wiki/Gun_(cellular_automaton)#/media/File:Game_of_life_glider_gun.svg (Creates a glider gun in a 11x36 grid)
            //Warning!!!! Do not place anywhere left than [4, 18], [5, 19] is recommended for maximum glider production

            gameBoard[r, c - 18] = LIVE;
            gameBoard[r + 1, c - 18] = LIVE;
            gameBoard[r, c - 17] = LIVE;
            gameBoard[r + 1, c - 17] = LIVE;
            gameBoard[r, c - 8] = LIVE;
            gameBoard[r + 1, c - 8] = LIVE;
            gameBoard[r + 2, c - 8] = LIVE;
            gameBoard[r - 1, c - 7] = LIVE;
            gameBoard[r + 3, c - 7] = LIVE;
            gameBoard[r - 2, c - 6] = LIVE;
            gameBoard[r + 4, c - 6] = LIVE;
            gameBoard[r - 2, c - 5] = LIVE;
            gameBoard[r + 4, c - 5] = LIVE;
            gameBoard[r + 1, c - 4] = LIVE;
            gameBoard[r - 1, c - 3] = LIVE;
            gameBoard[r + 3, c - 3] = LIVE;
            gameBoard[r, c - 2] = LIVE;
            gameBoard[r + 1, c - 2] = LIVE;
            gameBoard[r + 2, c - 2] = LIVE;
            gameBoard[r + 1, c - 1] = LIVE;
            gameBoard[r - 2, c + 2] = LIVE;
            gameBoard[r - 1, c + 2] = LIVE;
            gameBoard[r, c + 2] = LIVE;
            gameBoard[r - 2, c + 3] = LIVE;
            gameBoard[r - 1, c + 3] = LIVE;
            gameBoard[r, c + 3] = LIVE;
            gameBoard[r - 3, c + 4] = LIVE;
            gameBoard[r + 1, c + 4] = LIVE;
            gameBoard[r - 4, c + 6] = LIVE;
            gameBoard[r - 3, c + 6] = LIVE;
            gameBoard[r + 1, c + 6] = LIVE;
            gameBoard[r + 2, c + 6] = LIVE;
            gameBoard[r - 2, c + 16] = LIVE;
            gameBoard[r - 1, c + 16] = LIVE;
            gameBoard[r - 2, c + 17] = LIVE;
            gameBoard[r - 1, c + 17] = LIVE;

        }
        //Creates a board of entirely dead cells
        private void InitializeBoards()
        {
            for (int r = 0; r < ROW_SIZE; r++)
            {
                for (int c = 0; c < COL_SIZE; c++)
                {
                    gameBoard[r, c] = DEAD;
                    resultsBoard[r, c] = DEAD;
                }
            }
        }
        //Starts the actual process of the game. Contains a for loop ending when the loop
        //reaches the user's specified generation
        public void PlayTheGame()
        {
            Console.Write("Number of generations for this simulation run: ");
            int numGenerations = int.Parse(Console.ReadLine());

            for (int generation = 1; generation <= numGenerations; generation++)
            {
                DisplayCurrentGameBoard(generation);

                ProcessGameBoard();

                SwapGameBoards();
            }
        }
        //Makes the gameBoard = resultsBoard and resultsBoard = OLD gameBoard
        private void SwapGameBoards()
        {
            char[,] tmp = gameBoard;
            gameBoard = resultsBoard;
            resultsBoard = tmp;
        }
        //Applies the rules to the gameBoard cells
        private void ProcessGameBoard()
        {
            for (int r = 0; r < ROW_SIZE; r++)
            {
                for (int c = 0; c < COL_SIZE; c++)
                {
                    resultsBoard[r, c] = DaRules(r, c);
                }
            }
        }
        //Ruleset for Game of Life
        private char DaRules(int r, int c)
        {
            char stateOfCell = DEAD;
            int count = GetNeighborCount(r, c);

            //Case 1: Any live cell with two or three live neighbours survives.
            //Case 2: Any dead cell with three live neighbours becomes a live cell.
            //Case 3: All other live cells die in the next generation. Similarly, all other dead cells stay dead.

            if (gameBoard[r, c] == LIVE && (count == 2 || count == 3)) stateOfCell = LIVE; //Stays alive
            else if (gameBoard[r, c] == DEAD && count == 3) stateOfCell = LIVE; //Replication

            return stateOfCell;
        }
        //Finds the amount of neighbors each cell has
        private int GetNeighborCount(int r, int c)
        {
            int neighbors = 0;

            if (r == 0 && c == 0)
            {
                //North West (3 Neighbors)
                if (gameBoard[r, c + 1] == LIVE) neighbors++;
                if (gameBoard[r + 1, c] == LIVE) neighbors++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighbors++;
            }
            else if (r == 0 && c == COL_SIZE - 1)
            {
                //North East (3 Neighbors)
                if (gameBoard[r, c - 1] == LIVE) neighbors++;
                if (gameBoard[r + 1, c] == LIVE) neighbors++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighbors++;
            }
            else if (r == 0)
            {
                //North (5 Neighbors)
                if (gameBoard[r, c - 1] == LIVE) neighbors++;
                if (gameBoard[r, c + 1] == LIVE) neighbors++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighbors++;
                if (gameBoard[r + 1, c] == LIVE) neighbors++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighbors++;
            }
            else if (r == ROW_SIZE - 1 && c == 0)
            {
                //South West (3 Neighbors)
                if (gameBoard[r - 1, c] == LIVE) neighbors++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighbors++;
                if (gameBoard[r, c + 1] == LIVE) neighbors++;
            }
            else if (r == ROW_SIZE - 1 && c == COL_SIZE - 1)
            {
                //South East (3 Neighbors)
                if (gameBoard[r, c - 1] == LIVE) neighbors++;
                if (gameBoard[r - 1, c] == LIVE) neighbors++;
                if (gameBoard[r - 1, c - 1] == LIVE) neighbors++;
            }
            else if (r == ROW_SIZE - 1)
            {
                //South (5 Neighbors)
                if (gameBoard[r - 1, c - 1] == LIVE) neighbors++;
                if (gameBoard[r - 1, c] == LIVE) neighbors++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighbors++;
                if (gameBoard[r, c - 1] == LIVE) neighbors++;
                if (gameBoard[r, c + 1] == LIVE) neighbors++;
            }
            else if (c == 0)
            {
                //West (5 Neighbors)
                if (gameBoard[r - 1, c + 1] == LIVE) neighbors++;
                if (gameBoard[r - 1, c] == LIVE) neighbors++;
                if (gameBoard[r, c + 1] == LIVE) neighbors++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighbors++;
                if (gameBoard[r + 1, c] == LIVE) neighbors++;
            }
            else if (c == COL_SIZE - 1)
            {
                //East (5 Neighbors)
                if (gameBoard[r - 1, c - 1] == LIVE) neighbors++;
                if (gameBoard[r - 1, c] == LIVE) neighbors++;
                if (gameBoard[r, c - 1] == LIVE) neighbors++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighbors++;
                if (gameBoard[r + 1, c] == LIVE) neighbors++;
            }
            else
            {
                //Nominal Case - No EDGE or CORNER (8 Neighbors)
                //
                //North
                if (gameBoard[r - 1, c - 1] == LIVE) neighbors++;
                if (gameBoard[r - 1, c] == LIVE) neighbors++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighbors++;
                //Middle
                if (gameBoard[r, c - 1] == LIVE) neighbors++;
                if (gameBoard[r, c + 1] == LIVE) neighbors++;
                //South
                if (gameBoard[r + 1, c - 1] == LIVE) neighbors++;
                if (gameBoard[r + 1, c] == LIVE) neighbors++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighbors++;
            }

            return neighbors;
        }
        //Prints the gameBoard
        private void DisplayCurrentGameBoard(int gen)
        {
            Console.WriteLine($"Displaying Generation No.{gen}");

            //Draws the Top Border
            Console.Write("  ─");
            for (int r = 0; r <= COL_SIZE - 1; r++)
            {
                Console.Write("──");
            }
            Console.WriteLine();

            //Draws the Gameboard
            for (int r = 0; r < ROW_SIZE; r++)
            {
                Console.Write(" │"); // Draws the Left Border
                for (int c = 0; c < COL_SIZE; c++)
                {
                    Console.Write($"{SPACE}{gameBoard[r, c]}");
                }
                Console.Write(" │"); // Draws the Right Border
                Console.WriteLine();
            }

            //Draws the Bottom Border
            Console.Write("  ─");
            for (int r = 0; r <= COL_SIZE - 1; r++)
            {
                Console.Write("──");
            }
            Console.WriteLine("     Hit ENTER to Continue...");

            Console.ReadLine(); //Steps through generations at the user's pace
        }
    }
}