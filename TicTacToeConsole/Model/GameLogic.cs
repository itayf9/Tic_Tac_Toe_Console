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
        private List<Player> m_Players;
        private bool m_IsGameAgainstMachine;
        private int m_Turn;
        private GameState m_GameState;
        private int m_CountOfMarkedCells;

        public GameLogic(BoardSize i_BoardSize, bool i_IsGameAgainstMachine)
        {
            int boardSizeValue = (int)i_BoardSize;
            this.m_Players = new List<Player>(2);
            this.m_GameBoard = new BoardMark[boardSizeValue, boardSizeValue];
            this.m_IsGameAgainstMachine = i_IsGameAgainstMachine;
            this.m_CountOfMarkedCells = 0;
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
            return m_GameBoard[i_Move.Y, i_Move.X] == BoardMark.EMPTY_CELL;
        }

        public Point GenerateMachineMove()
        {
            bool validMove = false;
            Random rand = new Random();
            Point machineMove;

            do
            {
                int x = rand.Next(m_GameBoard.GetLength(0));
                int y = rand.Next(m_GameBoard.GetLength(0));

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
            Player playerWithTheTurn = m_Players[Turn];

            GameBoard[i_Move.Y, i_Move.X] = playerWithTheTurn.Symbol;
            m_CountOfMarkedCells++;

            checkGameState(i_Move, playerWithTheTurn);

            this.m_Turn = m_Turn++ % 2;
        }

        private GameState checkGameState(Point i_LastMoveOfUser, Player i_PlayerWithTheTurn)
        {
            int gameBoardHightAndWidth = this.m_GameBoard.GetLength(0);
            int countOfMarksPlayer = 0;
            int maxSequentialSymbolCount = 0;
            GameState currentGameState;

            for (int col=0; col < gameBoardHightAndWidth; col++)
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
            for (int i = 0, j = gameBoardHightAndWidth; i < gameBoardHightAndWidth; i++, j--)
            {
                if (m_GameBoard[i, j] == i_PlayerWithTheTurn.Symbol)
                {
                    countOfMarksPlayer++;
                }
            }
            maxSequentialSymbolCount = Math.Max(maxSequentialSymbolCount, countOfMarksPlayer);

            if (maxSequentialSymbolCount == gameBoardHightAndWidth)
            {
                if (i_PlayerWithTheTurn.Symbol == BoardMark.PLAYER_X)
                {
                    currentGameState = GameState.FINISHED_P2;
                }
                else
                {
                    currentGameState = GameState.FINISHED_P1;
                }
            }
            else if (m_CountOfMarkedCells == Math.Pow(gameBoardHightAndWidth, 2))
            {
                currentGameState = GameState.FINISHED_TIE;
            }
            else 
            {
                currentGameState = GameState.RUNNING;
            }

            return currentGameState;
        }

        public void ResetGameBoard()
        {
            for (int i = 0; i < m_GameBoard.GetLength(0); i++) 
            {
                for (int j = 0; j < m_GameBoard.GetLength(1); j++) 
                {
                    m_GameBoard[i, j] = BoardMark.EMPTY_CELL;
                }
            }
        }
    }
}
