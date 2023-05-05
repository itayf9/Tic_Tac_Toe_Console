using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Ex02.ConsoleUtils;
using TicTacToeConsole.Model;

namespace TicTacToeConsole.View
{
    public class ConsoleGameInteraction
    {

        public static void printGameBoard(BoardMark[,] m_GameBoard, int m_Turn)
        {

        }

        public static BoardSize getBoardSizeFromUserInput() { return BoardSize.THREE; }

        public static bool getGameModeFromUserInput() { return false; }

        public static void ShowMenu()
        {
            Screen.Clear();
            printMainMenu();
       
        }

        private static void printMainMenu()
        {
            Console.WriteLine("Menu");
        }

        public static Point ReadNextMove()
        {
            return new Point(0,0);
        }

        public static void PrintGameOverMesseage(GameState i_GameState)
        {
            switch (i_GameState)
            {
                case GameState.FINISHED_TIE:
                    Console.WriteLine("Game Over! it's a Tie !!");
                    break;
                case GameState.FINISHED_P1:
                    Console.WriteLine("Game Over! winner is Player 1 !!");
                    break;
                case GameState.FINISHED_P2:
                    Console.WriteLine("Game Over! winner is Player 2 !!");
                    break;         
            }
        }
    }
}
