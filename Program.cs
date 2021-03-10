using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer --- Description
// 2021-01-30 - CJ Cummings - Wrote driver for GLife   

////////////////////////////////////////////////////////////////
// TINFO 200 A, Winter 2021
// UWTacoma SET, CJ Cummings
// Due 2021-02-06, L5Life
// This is the driver for the class Game. It just creates the game
// and then calls its run method.
namespace GLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Game theGame = new Game();
            theGame.PlayTheGame();
        }
    }
}
