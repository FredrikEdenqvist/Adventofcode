﻿namespace parts
{
    public class Board
    {
        public List<(int number, bool bingo)[]> Sequence { get; private set; }

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

        public static List<(int number, bool bingo)[]> FindLines(int[] sequence, int x, int y)
        {
            var lines = new List<int[]>();

            //Rows
            for(int i = 0;i < y; i++)
            {
                var startindex = i * x;
                var endindex = startindex + x;
                lines.Add(sequence[startindex..endindex]);
            }

            //Cols
            for(int i = 0; i < x; i++)
            {
                var column = new int[y];
                for(int j = 0; j < y; j++)
                {
                    int indexToPick = i + (j * x);
                    column[j] = sequence[indexToPick];
                }
                lines.Add(column);
            }

            return lines.Select(x => x.Select(y => (y, false)).ToArray()).ToList();
        }

        public Board(int[] sequence, int x, int y)
        {
            Sequence = FindLines(sequence, x, y);
        }

        public void AddBingoNumber(int number)
        {
            foreach(var line in Sequence)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i].number == number)
                        line[i].bingo = true;
                }
            }

        }

        public int[]? GetBingoRow()
        {
            return Sequence
                .Where(x => x.All(y => y.bingo))
                .Select(x => x.Select(y => y.number).ToArray())
                .FirstOrDefault();
        }
    }
}
