using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeConsole.Model
{
    public class GameBoard
    {
        private BoardMark[,] board;
        private bool isGameAgainstMachine;

        public GameBoard(BoardSize i_boardSize, bool isGameAgainstMachine)
        {
            int boardSizeValue = (int)i_boardSize;

            this.board= new BoardMark[boardSizeValue, boardSizeValue];
            this.isGameAgainstMachine=isGameAgainstMachine;
        }
    }
}
