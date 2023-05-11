using System;
using System.Drawing;
using System.Text;
using Ex02.ConsoleUtils;
using TicTacToeConsole.Utillity;

namespace TicTacToeConsole.View
{
    public class ConsoleGameInteraction
    {
        public static void PrintGameBoard(eBoardMark[,] i_GameBoard, int i_Turn)
        {
            int boardHightAndWidth = i_GameBoard.GetLength(0);

            StringBuilder boardPrintBuilder = new StringBuilder();

            boardPrintBuilder.Append("  ");
            for (int i = 1; i <= boardHightAndWidth; i++)
            {
                boardPrintBuilder.Append(i).Append("   ");
            }

            boardPrintBuilder.Append("\n");

            for (int i = 0; i < boardHightAndWidth; i++)
            {
                boardPrintBuilder.Append(i + 1).Append("|");

                for (int j = 0; j < boardHightAndWidth; j++)
                {
                    boardPrintBuilder.Append(" ").Append((char)i_GameBoard[i, j]).Append(" |");
                }

                boardPrintBuilder.Append("\n");

                boardPrintBuilder.Append(" =");
                for (int j = 0; j < boardHightAndWidth; j++)
                {
                    boardPrintBuilder.Append("====");
                }

                boardPrintBuilder.Append("\n");
            }

            Screen.Clear();
            Console.WriteLine(boardPrintBuilder.ToString());
            string playerNameThatHasTheTurn = i_Turn == 0 ? "Player 1 (X)" : "Player 2 (O)";
            Console.WriteLine("\nTurn Of: " + playerNameThatHasTheTurn);
        }

        public static void PrintGameScore(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            Console.WriteLine("Score: Player 1 (X) : " + i_FirstPlayerScore);
            Console.WriteLine("       Player 2 (O) : " + i_SecondPlayerScore);
        }

        public static void GetBoardSizeFromUserInput(out eBoardSize o_BoardSize)
        {
            displayBoardSizesMenu();
            bool validInput = false;
            o_BoardSize = eBoardSize.Three;
            while (!validInput)
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int choice) && choice >= 3 && choice <= 9)
                {
                    o_BoardSize = (eBoardSize)choice;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a number between 3 and 9.");
                }
            }
        }

        private static void displayBoardSizesMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Choose the size of the board:");
            sb.AppendLine("3. 3x3");
            sb.AppendLine("4. 4x4");
            sb.AppendLine("5. 5x5");
            sb.AppendLine("6. 6x6");
            sb.AppendLine("7. 7x7");
            sb.AppendLine("8. 8x8");
            sb.AppendLine("9. 9x9");
            Console.WriteLine(sb.ToString());
        }

        private static void displayGameModeMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Choose game mode:");
            sb.AppendLine("1. vs PC");
            sb.AppendLine("2. vs Human Player");
            Console.WriteLine(sb.ToString());
        }

        public static void GetGameModeFromUserInput(out bool o_GameMode)
        {
            displayGameModeMenu();
            bool validInput = false;
            o_GameMode = false;
            while (!validInput)
            {
                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    o_GameMode = true;
                    validInput = true;
                }
                else if (userInput == "2")
                {
                    o_GameMode = false;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter 1 or 2.");
                }
            }
        }

        public static void ShowMenu()
        {
            Screen.Clear();
            printMainMenu();
        }

        public static eMainMenuUserChoice GetStartOrExitUserInput()
        {
            eMainMenuUserChoice choice = eMainMenuUserChoice.Invalid;
            while (choice == eMainMenuUserChoice.Invalid)
            {
                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    choice = eMainMenuUserChoice.StartNewGame;
                }
                else if (userInput == "2")
                {
                    choice = eMainMenuUserChoice.ExitProgram;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter 1 or 2.");
                }
            }

            return choice;
        }

        private static void printMainMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Welcome to Reverse Tic-Tac-Toe, please enter your choice (1 or 2): ");
            sb.AppendLine("1. Start New Game.");
            sb.AppendLine("2. Exit.");
            Console.WriteLine(sb.ToString());
        }

        public static Point ReadNextMove(int i_BoarderHightAndWidth, ref bool o_IsSessionOver)
        {
            bool isValidMove = false;

            Console.WriteLine("\nPlease enter two numbers separated by a space. (e.g -> 1 2)");

            string playerDesiredMove = Console.ReadLine();
            int x = 0, y = 0;
            string[] playerDesiredSeperatedMove = playerDesiredMove.Trim().Split(' ');
            isValidMove = playerDesiredSeperatedMove.Length == 2 &&
                    int.TryParse(playerDesiredSeperatedMove[0], out y) &&
                    int.TryParse(playerDesiredSeperatedMove[1], out x) &&
                    y >= 1 && y <= i_BoarderHightAndWidth &&
                    x >= 1 && x <= i_BoarderHightAndWidth;

            while (!isValidMove)
            {
                if (playerDesiredSeperatedMove.Length == 1 &&
                    playerDesiredSeperatedMove[0].ToUpper() == "Q")
                {
                    o_IsSessionOver = true;
                    x = -1;
                    y = -1;
                    break;
                }

                Console.WriteLine("Invalid move input! Please enter two numbers separated by a space, each between 1 and the board size.");
                playerDesiredMove = Console.ReadLine();

                playerDesiredSeperatedMove = playerDesiredMove.Trim().Split(' ');
                isValidMove = playerDesiredSeperatedMove.Length == 2 &&
                        int.TryParse(playerDesiredSeperatedMove[0], out y) &&
                        int.TryParse(playerDesiredSeperatedMove[1], out x) &&
                        y >= 1 && y <= i_BoarderHightAndWidth &&
                        x >= 1 && x <= i_BoarderHightAndWidth;
            }

            return new Point(x, y);
        }

        public static bool PrintGameOverMesseageAndAskUserIfFinishSession(eGameState i_GameState, int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            bool isValidAnswerToYesOrNoQuestion;
            string userAnswerToYesOrNoQuestion;

            switch (i_GameState)
            {
                case eGameState.FinishedTie:
                    Console.WriteLine("Game Over! it's a Tie !!");
                    break;
                case eGameState.FinishedP1:
                    Console.WriteLine("Game Over! winner is Player 1 !!");
                    break;
                case eGameState.FinishedP2:
                    Console.WriteLine("Game Over! winner is Player 2 !!");
                    break;
            }

            PrintGameScore(i_FirstPlayerScore, i_SecondPlayerScore);

            Console.WriteLine("\nDo you want to start another game or end this game session?");
            Console.WriteLine("Enter 'Y' for Yes, 'N' for No: ");

            userAnswerToYesOrNoQuestion = Console.ReadLine().ToUpper();
            isValidAnswerToYesOrNoQuestion = userAnswerToYesOrNoQuestion == "Y" || userAnswerToYesOrNoQuestion == "N";

            while (!isValidAnswerToYesOrNoQuestion)
            {
                Console.WriteLine("Invalid answer ! Try Again with (Y Or N)");

                userAnswerToYesOrNoQuestion = Console.ReadLine().ToUpper();
                isValidAnswerToYesOrNoQuestion = userAnswerToYesOrNoQuestion == "Y" || userAnswerToYesOrNoQuestion == "N";
            }

            return userAnswerToYesOrNoQuestion == "N";
        }

        public static void DisplayInvalidMoveMessage()
        {
            Console.WriteLine("Invalid move! Please try again.");
        }
    }
}
