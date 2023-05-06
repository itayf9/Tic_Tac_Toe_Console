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
                if (mainMenuUserChoice == MainMenuUserChoice.ExitProgram) 
                {
                    isUserExited = true;
                    continue;
                }
                BoardSize boardSize;
                ConsoleGameInteraction.GetBoardSizeFromUserInput(out boardSize);
                bool isGameModeAgainstMachine;
                ConsoleGameInteraction.GetGameModeFromUserInput(out isGameModeAgainstMachine);
                this.gameLogic = new GameLogic(boardSize, isGameModeAgainstMachine);
                gameLogic.InitAdversary(isGameModeAgainstMachine);
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
                    ConsoleGameInteraction.PrintGameBoard(gameLogic.GameBoard, gameLogic.Turn);
                    ConsoleGameInteraction.PrintGameScore(scoreOfFirstPlayer, scoreOfsecondPlayer);

                    Point move;
                    if (gameLogic.Turn == 0 || !gameLogic.IsGameAgainstMachine) 
                    {
                        move = ConsoleGameInteraction.ReadNextMove(gameLogic.GameBoard.Length);
                        move.X--;
                        move.Y--;
                        while(!gameLogic.IsValidMove(move)) 
                        {
                            ConsoleGameInteraction.DisplayInvalidMoveMessage();
                            move = ConsoleGameInteraction.ReadNextMove(gameLogic.GameBoard.Length);
                        }
                    }
                    else
                    {
                        move = gameLogic.GenerateMachineMove();
                    }
                    gameLogic.ApplyMove(move);

                    GameState gameStateAfterMove = gameLogic.GameState;

                    bool userChoiceAboutFinishingSession;


                    if (gameStateAfterMove != GameState.Running)
                    {
                        isGameOver = true;
                        ConsoleGameInteraction.PrintGameBoard(gameLogic.GameBoard, gameLogic.Turn);
                        userChoiceAboutFinishingSession = ConsoleGameInteraction.PrintGameOverMesseageAndAskUserIfFinishSession(gameStateAfterMove, gameLogic.GetScoreOfPlayer(0), gameLogic.GetScoreOfPlayer(1));
                        if (userChoiceAboutFinishingSession == true)
                        {
                            isSessionOver = true;
                        }
                    }

                }
            }
            gameLogic.ResetGameBoard(); 
        }
    }
}
