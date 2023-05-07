using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TicTacToeConsole.Utillity;

namespace TicTacToeConsole.Model
{
    public class GameLogic
    {
        private BoardMark[,] m_GameBoard;
        private int m_GameBoardSize;
        private List<Player> m_Players;
        private bool m_IsGameAgainstMachine;
        private int m_Turn;
        private GameState m_GameState;
        private int m_CountOfMarkedCells;

        public GameLogic(BoardSize i_BoardSize, bool i_IsGameAgainstMachine)
        {
            this.m_GameBoardSize = (int)i_BoardSize;
            this.m_Players = new List<Player>(2);
            this.m_GameBoard = new BoardMark[m_GameBoardSize, m_GameBoardSize];
            ResetGameBoard();
            this.m_IsGameAgainstMachine = i_IsGameAgainstMachine;
        }

        public BoardMark[,] GameBoard {

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
        

        public GameState GameState
        {
            get 
            { 
                return m_GameState;
            }
        }

        public int GetScoreOfPlayer(int i_Index)
        {
            return m_Players[i_Index].Score;
        }

        public bool IsValidMove(Point i_Move)
        {
            bool isValidMove = true;

            if (i_Move.X >= m_GameBoardSize || i_Move.X < 0 || 
                i_Move.Y >= m_GameBoardSize || i_Move.Y < 0)
            {
                isValidMove = false;
            }
            else
            {
                isValidMove = m_GameBoard[i_Move.Y, i_Move.X] == BoardMark.EmptyCell;
            }

            return isValidMove;
        }

        public Point GenerateMachineMove()
        {
            bool validMove = false;
            Random rand = new Random();
            Point machineMove;

            do
            {
                int x = rand.Next(m_GameBoardSize);
                int y = rand.Next(m_GameBoardSize);

                machineMove = new Point(x, y);

                if (IsValidMove(machineMove))
                {
                    validMove = true;
                }
            }
            while (!validMove);

            return machineMove;
        }

        public void ApplyMove(Point i_Move)
        {
            Player playerWithTheTurn = m_Players[this.Turn];

            GameBoard[i_Move.Y, i_Move.X] = playerWithTheTurn.Symbol;
            m_CountOfMarkedCells++;

            checkAndUpdateGameState(i_Move, playerWithTheTurn);

            this.m_Turn =  (m_Turn+1) % 2;
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
                if (m_GameBoard[row , i_LastMoveOfUser.X] == i_PlayerWithTheTurn.Symbol)
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
                if (i_PlayerWithTheTurn.Symbol == BoardMark.PlayerX)
                {
                    m_GameState = GameState.FinishedP2;
                    m_Players[1].Score++;
                }
                else
                {
                    m_GameState = GameState.FinishedP1;
                    m_Players[0].Score++;
                }
            }
            else if (m_CountOfMarkedCells == Math.Pow(gameBoardHightAndWidth, 2))
            {
                m_GameState = GameState.FinishedTie;
            }
            else 
            {
                m_GameState = GameState.Running;
            }
        }

        public void ResetGameBoard()
        {
            m_GameState = GameState.Running;
            m_CountOfMarkedCells = 0;
            for (int i = 0; i < m_GameBoardSize; i++) 
            {
                for (int j = 0; j < m_GameBoardSize; j++) 
                {
                    m_GameBoard[i, j] = BoardMark.EmptyCell;
                }
            }
        }

        internal void InitPlayers(bool isGameModeAgainstMachine)
        {
            m_Players.Add(new Player(BoardMark.PlayerX, false));
            m_Players.Add(new Player(BoardMark.PlayerO, isGameModeAgainstMachine));
        }
    }
}
