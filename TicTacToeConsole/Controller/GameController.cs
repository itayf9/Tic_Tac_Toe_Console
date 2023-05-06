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
                    ConsoleGameInteraction.PrintGameBoard(gameLogic.GameBoard, gameLogic.Turn, scoreOfFirstPlayer, scoreOfsecondPlayer);

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
                    
                    switch (gameStateAfterMove)
                    {
                        case GameState.Running:
                            continue;
                        case GameState.FinishedTie:
                            isGameOver = true;
                            userChoiceAboutFinishingSession = ConsoleGameInteraction.PrintGameOverMesseageAndAskUserIfFinishSession(GameState.FinishedTie, gameLogic.GetScoreOfPlayer(0), gameLogic.GetScoreOfPlayer(1));
                            if (userChoiceAboutFinishingSession == true)
                            {
                                isSessionOver = true;
                            }
                        break;
                        case GameState.FinishedP1:
                            isGameOver = true;
                            userChoiceAboutFinishingSession = ConsoleGameInteraction.PrintGameOverMesseageAndAskUserIfFinishSession(GameState.FinishedP1, gameLogic.GetScoreOfPlayer(0), gameLogic.GetScoreOfPlayer(1));
                            if (userChoiceAboutFinishingSession == true)
                            {
                                isSessionOver = true;
                            }
                            break;
                        case GameState.FinishedP2:
                            isGameOver = true;
                            userChoiceAboutFinishingSession = ConsoleGameInteraction.PrintGameOverMesseageAndAskUserIfFinishSession(GameState.FinishedP2, gameLogic.GetScoreOfPlayer(0), gameLogic.GetScoreOfPlayer(1));
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
