using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TicTacToeConsole.Model
{
    public class GameLogic
    {
        private BoardMark[,] m_GameBoard;
        private List<Player> m_Players;
        private bool m_IsGameAgainstMachine;
        private int m_Turn;
        

        public GameLogic(BoardSize i_BoardSize, bool i_IsGameAgainstMachine)
        {
            int boardSizeValue = (int)i_BoardSize;
            this.m_Players = new List<Player>(2);
            this.m_GameBoard = new BoardMark[boardSizeValue, boardSizeValue];
            this.m_IsGameAgainstMachine = i_IsGameAgainstMachine;
        }

        public BoardMark[,] GameBoard {

            get
            {
                return m_GameBoard;
            }
        }

        public List<Player> Players
        {
            get
            {
                return m_Players;
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

        public void ApplyMove(Point move)
        {

        }

        public GameState CheckGameState()
        {
            return GameState.RUNNING;
        }
    }
}
