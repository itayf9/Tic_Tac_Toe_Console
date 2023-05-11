using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeConsole.Utillity;

namespace TicTacToeConsole.Model
{
    public class Player
    {
        private int m_Score;
        private eBoardMark m_Symbol;

        public Player(eBoardMark i_Symbol)
        {
            this.m_Score = 0;
            this.m_Symbol = i_Symbol;
        }

        public eBoardMark Symbol
        {
            get
            {
                 return m_Symbol;
            }
        }

        public int Score
        {
            get
            {
                 return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }
    }
}
