using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TicTacToeConsole.Model;

using TicTacToeConsole.View;
using TicTacToeConsole.Model;

namespace TicTacToeConsole
{
    internal class GameController
    {

        private GameLogic gameLogic;

        public void GameProcedure()
        {

            bool isUserExited = false;
        
            while (!isUserExited)
            {
                ConsoleGameInteraction.ShowMenu();
                BoardSize boardSize = ConsoleGameInteraction.getBoardSizeFromUserInput();
                bool isGameModeAgainstMachine = ConsoleGameInteraction.getGameModeFromUserInput();

                this.gameLogic = new GameLogic(boardSize, isGameModeAgainstMachine);
                startGameSession();
            }
        }


        private void startGameSession() 
        {

            bool gameOver = false;

            while (!gameOver) 
            {
                ConsoleGameInteraction.printGameBoard(gameLogic.GameBoard, gameLogic.Turn);

                Point move = ConsoleGameInteraction.ReadNextMove();
                gameLogic.ApplyMove(move);

                GameState gameStateAfterMove = gameLogic.CheckGameState();        

                switch (gameStateAfterMove)
                {
                    case GameState.RUNNING:
                        continue;
                    break;
                     case GameState.FINISHED_TIE:
                        gameOver = true;
                        ConsoleGameInteraction.PrintGameOverMesseage(GameState.FINISHED_TIE);
                    break;
                     case GameState.FINISHED_P1:
                        gameOver = true;
                        ConsoleGameInteraction.PrintGameOverMesseage(GameState.FINISHED_P1);
                        break;
                     case GameState.FINISHED_P2:
                        gameOver = true;
                        ConsoleGameInteraction.PrintGameOverMesseage(GameState.FINISHED_P2);
                        break;
                }
            }
        }
    }
}
