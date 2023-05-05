using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Ex02.ConsoleUtils;
using TicTacToeConsole.Utillity;

namespace TicTacToeConsole.View
{
    public class ConsoleGameInteraction
    {

        public static void PrintGameBoard(BoardMark[,] m_GameBoard, int m_Turn, int i_FirstPlayerScore, int i_SecondPlayerScore)
        {

        }

        public static BoardSize GetBoardSizeFromUserInput() { return BoardSize.THREE; }

        public static bool GetGameModeFromUserInput() { return false; }

        public static void ShowMenu()
        {
            Screen.Clear();
            printMainMenu();
        }

        internal static MainMenuUserChoice GetStartOrExitUserInput()
        {
            throw new NotImplementedException();
        }

        private static void printMainMenu()
        {
            Console.WriteLine("Menu");
        }

        public static Point ReadNextMove()
        {
            return new Point(0,0);
        }

        public static bool PrintGameOverMesseageAndAskUserIfFinishSession(GameState i_GameState)
        {
            bool isPlayerWantToContinuePlaying = true;

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

            // ask user to decide continue of finnish
            // isPlayerWantToContinuePlaying

            return isPlayerWantToContinuePlaying;
        }

        internal static void DisplayInvalidMoveMessage()
        {
            throw new NotImplementedException();
        }
    }
}
