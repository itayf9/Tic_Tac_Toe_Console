using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToeConsole.View;
using TicTacToeConsole.Model;

namespace TicTacToeConsole
{
    internal class GameController
    {

        private GameBoard gameBoard;

        public void GameProcedure()
        {

            bool isUserExited = false;
        
            while (!isUserExited)
            {
                ConsoleGameInteraction.ShowMenu();
                BoardSize boardSize = ConsoleGameInteraction.getBoardSize();
                bool isGameModeAgainstMachine = ConsoleGameInteraction.getGameMode();

                this.gameBoard = new GameBoard(boardSize, isGameModeAgainstMachine);
                /* startGame();*/
               
            }
            



        }




        private void startGameSession() 
        {
            /*ConsoleGameInteraction.printGameBoard();*/

            ConsoleGameInteraction.ReadNextMove();

        }

    }
}
