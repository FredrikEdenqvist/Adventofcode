using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parts
{
    public class Board
    {
        public int[] Sequence { get; }
        public int X { get; }
        public int Y { get; }

        public static IEnumerable<Board> GenerateBoards(int[] boardSequence, int x, int y)
        {
            var sequenceLenght = x * y;
            var boards = boardSequence.Length / sequenceLenght;
            for(int i = 0; i < boards; i++)
            {
                var start = i * sequenceLenght;
                var end = start + sequenceLenght;
                yield return new Board(boardSequence[start..end], x, y);
            }
        }

        public Board(int[] sequence, int x, int y)
        {
            Sequence = sequence;
            X = x;
            Y = y;
        }
    }
}
