using System.Drawing;
using TicTacToeConsole.Model;
using TicTacToeConsole.Utillity;

using TicTacToeConsole.View;

namespace TicTacToeConsole
{
    public class GameController
    {
        private GameLogic m_GameLogic;

        public void GameProcedure()
        {
            bool isUserExited = false;

            while (!isUserExited)
            {
                ConsoleGameInteraction.ShowMenu();
                eMainMenuUserChoice mainMenuUserChoice = ConsoleGameInteraction.GetStartOrExitUserInput();
                if (mainMenuUserChoice == eMainMenuUserChoice.ExitProgram)
                {
                    isUserExited = true;
                    continue;
                }

                eBoardSize boardSize;
                ConsoleGameInteraction.GetBoardSizeFromUserInput(out boardSize);
                bool isGameModeAgainstMachine;
                ConsoleGameInteraction.GetGameModeFromUserInput(out isGameModeAgainstMachine);
                this.m_GameLogic = new GameLogic(boardSize, isGameModeAgainstMachine);
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
                    int scoreOfFirstPlayer = m_GameLogic.GetScoreOfPlayer(0);
                    int scoreOfsecondPlayer = m_GameLogic.GetScoreOfPlayer(1);
                    ConsoleGameInteraction.PrintGameBoard(m_GameLogic.GameBoard, m_GameLogic.Turn);
                    ConsoleGameInteraction.PrintGameScore(scoreOfFirstPlayer, scoreOfsecondPlayer);

                    Point nextGameMove;
                    if (m_GameLogic.Turn == 0 || !m_GameLogic.IsGameAgainstMachine)
                    {
                        nextGameMove = ConsoleGameInteraction.ReadNextMove(m_GameLogic.GameBoardSize, ref isSessionOver);
                        if (nextGameMove == new Point(-1, -1))
                        {
                            break;
                        }

                        nextGameMove.X--;
                        nextGameMove.Y--;
                        while (!m_GameLogic.IsValidMove(nextGameMove))
                        {
                            ConsoleGameInteraction.PrintGameBoard(m_GameLogic.GameBoard, m_GameLogic.Turn);
                            ConsoleGameInteraction.PrintGameScore(scoreOfFirstPlayer, scoreOfsecondPlayer);
                            ConsoleGameInteraction.DisplayInvalidMoveMessage();
                            nextGameMove = ConsoleGameInteraction.ReadNextMove(m_GameLogic.GameBoardSize, ref isSessionOver);
                            if (nextGameMove == new Point(-1, -1))
                            {
                                break;
                            }

                            nextGameMove.X--;
                            nextGameMove.Y--;
                        }
                    }
                    else
                    {
                        nextGameMove = m_GameLogic.GenerateMachineMove();
                    }

                    m_GameLogic.ApplyMove(nextGameMove);

                    eGameState gameStateAfterMove = m_GameLogic.GameState;

                    if (gameStateAfterMove != eGameState.Running)
                    {
                        isGameOver = true;
                        ConsoleGameInteraction.PrintGameBoard(m_GameLogic.GameBoard, m_GameLogic.Turn);
                        bool userChoiceAboutFinishingSession = ConsoleGameInteraction.PrintGameOverMesseageAndAskUserIfFinishSession(gameStateAfterMove, m_GameLogic.GetScoreOfPlayer(0), m_GameLogic.GetScoreOfPlayer(1));
                        if (userChoiceAboutFinishingSession == true)
                        {
                            isSessionOver = true;
                        }
                    }
                }

                m_GameLogic.ResetGameBoard();
            }
        }
    }
}
