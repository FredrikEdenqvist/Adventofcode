namespace parts
{
    public class Board
    {
        public List<(int number, bool bingo)[]> Sequence { get; private set; }
        public int Rows { get; }

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
            Rows = y;
        }

        public void AddBingoNumber(int number)
        {
            if (HasBingo)
                return;

            foreach(var line in Sequence)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i].number == number)
                        line[i].bingo = true;
                }
            }
        }

        public int[]? GetWinningBingoBoard()
        {
            if (HasBingo)
                return Sequence.Take(Rows).SelectMany(x => x).Where(x => !x.bingo).Select(x => x.number).ToArray();
            return null;
        }

        public int[]? GetUnMarkedBoardNumbers()
        {
            return Sequence.Take(Rows).SelectMany(x => x).Where(x => !x.bingo).Select(x => x.number).ToArray();
        }

        public bool HasBingo => Sequence.Any(x => x.All(y => y.bingo));
    }
}
