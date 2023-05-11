using System;
using System.Collections.Generic;
using System.Drawing;
using TicTacToeConsole.Utillity;

namespace TicTacToeConsole.Model
{
    public class GameLogic
    {
        private eBoardMark[,] m_GameBoard;
        private int m_GameBoardSize;
        private List<Player> m_Players;
        private bool m_IsGameAgainstMachine;
        private int m_Turn;
        private eGameState m_GameState;
        private int m_CountOfMarkedCells;

        public GameLogic(eBoardSize i_BoardSize, bool i_IsGameAgainstMachine)
        {
            this.m_GameBoardSize = (int)i_BoardSize;
            this.m_Players = new List<Player>(2);
            initPlayers();
            this.m_GameBoard = new eBoardMark[m_GameBoardSize, m_GameBoardSize];
            ResetGameBoard();
            this.m_IsGameAgainstMachine = i_IsGameAgainstMachine;
        }

        public eBoardMark[,] GameBoard
        {
            get
            {
                return m_GameBoard;
            }
        }

        public bool IsGameAgainstMachine
            {
            get
            {
                return m_IsGameAgainstMachine;
            }

            set
            {
                m_IsGameAgainstMachine = value;
            }
        }

        public int Turn
        {
            get
            {
                return m_Turn;
            }
        }

        public int GameBoardSize
        {
            get
            {
                return m_GameBoardSize;
            }
        }

        public eGameState GameState
        {
            get
            {
                return m_GameState;
            }
        }

        public int GetScoreOfPlayer(int i_IndexOfPlayer)
        {
            return m_Players[i_IndexOfPlayer].Score;
        }

        public bool IsValidMove(Point i_GameMove)
        {
            bool isValidMove = true;

            if (i_GameMove.X >= m_GameBoardSize || i_GameMove.X < 0 ||
                i_GameMove.Y >= m_GameBoardSize || i_GameMove.Y < 0)
            {
                isValidMove = false;
            }
            else
            {
                isValidMove = m_GameBoard[i_GameMove.Y, i_GameMove.X] == eBoardMark.EmptyCell;
            }

            return isValidMove;
        }

        public Point GenerateMachineMove()
        {
            bool isValidMove = false;
            Random randomGenerator = new Random();
            Point machineGameMove;

            do
            {
                int x = randomGenerator.Next(m_GameBoardSize);
                int y = randomGenerator.Next(m_GameBoardSize);

                machineGameMove = new Point(x, y);

                if (IsValidMove(machineGameMove))
                {
                    isValidMove = true;
                }
            }
            while (!isValidMove);

            return machineGameMove;
        }

        public void ApplyMove(Point i_GameMove)
        {
            Player playerWithTheTurn = m_Players[m_Turn];

            m_GameBoard[i_GameMove.Y, i_GameMove.X] = playerWithTheTurn.Symbol;
            m_CountOfMarkedCells++;

            checkAndUpdateGameState(i_GameMove, playerWithTheTurn);

            this.m_Turn = (m_Turn + 1) % 2;
        }

        private void checkAndUpdateGameState(Point i_LastMoveOfUser, Player i_PlayerWithTheTurn)
        {
            int gameBoardHightAndWidth = this.m_GameBoardSize;
            int countOfMarksPlayer = 0;
            int maxSequentialSymbolCount = 0;

            for (int col = 0; col < gameBoardHightAndWidth; col++)
            {
                if (m_GameBoard[i_LastMoveOfUser.Y, col] == i_PlayerWithTheTurn.Symbol)
                {
                    countOfMarksPlayer++;
                }
            }

            maxSequentialSymbolCount = Math.Max(maxSequentialSymbolCount, countOfMarksPlayer);

            countOfMarksPlayer = 0;
            for (int row = 0; row < gameBoardHightAndWidth; row++)
            {
                if (m_GameBoard[row, i_LastMoveOfUser.X] == i_PlayerWithTheTurn.Symbol)
                {
                    countOfMarksPlayer++;
                }
            }

            maxSequentialSymbolCount = Math.Max(maxSequentialSymbolCount, countOfMarksPlayer);

            countOfMarksPlayer = 0;
            for (int i = 0; i < gameBoardHightAndWidth; i++)
            {
                if (m_GameBoard[i, i] == i_PlayerWithTheTurn.Symbol)
                {
                    countOfMarksPlayer++;
                }
            }

            maxSequentialSymbolCount = Math.Max(maxSequentialSymbolCount, countOfMarksPlayer);

            countOfMarksPlayer = 0;
            for (int i = 0, j = gameBoardHightAndWidth - 1; i < gameBoardHightAndWidth; i++, j--)
            {
                if (m_GameBoard[i, j] == i_PlayerWithTheTurn.Symbol)
                {
                    countOfMarksPlayer++;
                }
            }

            maxSequentialSymbolCount = Math.Max(maxSequentialSymbolCount, countOfMarksPlayer);

            if (maxSequentialSymbolCount == gameBoardHightAndWidth)
            {
                if (i_PlayerWithTheTurn.Symbol == eBoardMark.PlayerX)
                {
                    m_GameState = eGameState.FinishedP2;
                    m_Players[1].Score++;
                }
                else
                {
                    m_GameState = eGameState.FinishedP1;
                    m_Players[0].Score++;
                }
            }
            else if (m_CountOfMarkedCells == Math.Pow(gameBoardHightAndWidth, 2))
            {
                m_GameState = eGameState.FinishedTie;
            }
            else
            {
                m_GameState = eGameState.Running;
            }
        }

        public void ResetGameBoard()
        {
            m_GameState = eGameState.Running;
            m_CountOfMarkedCells = 0;

            for (int i = 0; i < m_GameBoardSize; i++)
            {
                for (int j = 0; j < m_GameBoardSize; j++)
                {
                    m_GameBoard[i, j] = eBoardMark.EmptyCell;
                }
            }
        }

        public void initPlayers()
        {
            m_Players.Add(new Player(eBoardMark.PlayerX));
            m_Players.Add(new Player(eBoardMark.PlayerO));
        }
    }
}
