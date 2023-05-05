using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TicTacToeConsole.Model;
using TicTacToeConsole.Utillity;

using TicTacToeConsole.View;


namespace TicTacToeConsole
{
    public class GameController
    {
        private GameLogic gameLogic;

        public void GameProcedure()
        {

            bool isUserExited = false;
        
            while (!isUserExited)
            {
                ConsoleGameInteraction.ShowMenu();
                MainMenuUserChoice mainMenuUserChoice = ConsoleGameInteraction.GetStartOrExitUserInput();
                if (mainMenuUserChoice == MainMenuUserChoice.EXIT_PROGRAM) 
                {
                    isUserExited = true;
                    continue;
                }
                BoardSize boardSize = ConsoleGameInteraction.GetBoardSizeFromUserInput();
                bool isGameModeAgainstMachine = ConsoleGameInteraction.GetGameModeFromUserInput();

                this.gameLogic = new GameLogic(boardSize, isGameModeAgainstMachine);
                startGameSession();
            }
        }


        private void startGameSession() 
        {
            bool isSessionOver = false;

            while (!isSessionOver)
            {
                bool isGameOver = false;

                while (!isGameOver) 
                {
                    int scoreOfFirstPlayer = gameLogic.GetScoreOfPlayer(0);
                    int scoreOfsecondPlayer = gameLogic.GetScoreOfPlayer(1);
                    ConsoleGameInteraction.PrintGameBoard(gameLogic.GameBoard, gameLogic.Turn, scoreOfFirstPlayer, scoreOfsecondPlayer);

                    Point move;
                    if (!gameLogic.IsGameAgainstMachine & gameLogic.Turn != 0) 
                    {
                        move = ConsoleGameInteraction.ReadNextMove();
                        while(!gameLogic.IsValidMove(move)) 
                        {
                            ConsoleGameInteraction.DisplayInvalidMoveMessage();
                            move = ConsoleGameInteraction.ReadNextMove();
                        }
                    }
                    else
                    {
                        move = gameLogic.GenerateMachineMove();
                    }
                    gameLogic.ApplyMove(move);

                    GameState gameStateAfterMove = gameLogic.GameState;

                    bool userChoiceAboutFinishingSession;

                    switch (gameStateAfterMove)
                    {
                        case GameState.RUNNING:
                            continue;
                        case GameState.FINISHED_TIE:
                            isGameOver = true;
                            userChoiceAboutFinishingSession = ConsoleGameInteraction.PrintGameOverMesseageAndAskUserIfFinishSession(GameState.FINISHED_TIE);
                            if (userChoiceAboutFinishingSession == true)
                            {
                                isSessionOver = true;
                            }
                        break;
                        case GameState.FINISHED_P1:
                            isGameOver = true;
                            userChoiceAboutFinishingSession = ConsoleGameInteraction.PrintGameOverMesseageAndAskUserIfFinishSession(GameState.FINISHED_P1);
                            if (userChoiceAboutFinishingSession == true)
                            {
                                isSessionOver = true;
                            }
                            break;
                        case GameState.FINISHED_P2:
                            isGameOver = true;
                            userChoiceAboutFinishingSession = ConsoleGameInteraction.PrintGameOverMesseageAndAskUserIfFinishSession(GameState.FINISHED_P2);
                            if (userChoiceAboutFinishingSession == true)
                            {
                                isSessionOver = true;
                            }
                            break;
                    }
                }
            }
            gameLogic.ResetGameBoard(); 
        }
    }
}
