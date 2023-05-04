using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ex02.ConsoleUtils;


namespace TicTacToeConsole.View
{
    public class ConsoleGameInteraction
    {

        public static void printGameBoard(object[,] board, int scoreOfPlayer1, int scoreOfAdversaryPlayer)
        {

        }

        public static BoardSize getBoardSize() { return BoardSize.THREE; }

        public static bool getGameMode() { return false; }

        public static void ShowMenu()
        {
            Screen.Clear();
            printMainMenu();
       
        }

        private static void printMainMenu()
        {
            Console.WriteLine("Menu");
        }

        internal static void ReadNextMove()
        {
            throw new NotImplementedException();
        }
    }
}
