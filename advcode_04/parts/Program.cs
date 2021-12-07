string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}
int endpositionRow1 = input.IndexOf('\n');

int[] bingonumbers = input[..endpositionRow1]
    .Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(x => Convert.ToInt32(x))
    .ToArray();

int[] bingoboards = input[endpositionRow1..]
    .Split(new[] { " ", "\n" }, StringSplitOptions.RemoveEmptyEntries)
    .Select(x => Convert.ToInt32(x))
    .ToArray();

var boards = parts.Board.GenerateBoards(bingoboards, 5, 5).ToArray();

var bingoOrder = new List<(parts.Board board, int bingoNumber)>();

for (int i = 0; i < bingonumbers.Length; i++)
{
    for (int b = 0; b < boards.Length; b++)
    {
        if (!boards[b].HasBingo)
        {
            boards[b].AddBingoNumber(bingonumbers[i]);
            if (boards[b].HasBingo)
            {
                bingoOrder.Add((boards[b], bingonumbers[i]));
            }
        }
    }
}

if (!bingoOrder.Any())
    Console.WriteLine("No winner :(");
else
{
    (parts.Board board, int bingonumber) = bingoOrder.First();
    var quizAnswer1 = board.GetWinningBingoBoard()?.Sum(x => x) * bingonumber;
    Console.WriteLine($"Quiz answer part 1: {quizAnswer1}");

    (parts.Board lastBoard, int lastBingonumber) = bingoOrder.Last();
    var quizAnswer2 = lastBoard.GetWinningBingoBoard()?.Sum(x => x) * lastBingonumber;
    Console.WriteLine($"Quiz answer part 2: {quizAnswer2}");
}
