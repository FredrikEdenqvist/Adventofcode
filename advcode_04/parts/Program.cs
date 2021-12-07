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
    .Split(new [] { " ", "\n" }, StringSplitOptions.RemoveEmptyEntries)
    .Select(x => Convert.ToInt32(x))
    .ToArray();

var boards = parts.Board.GenerateBoards(bingoboards, 5, 5).ToArray();

int[]? winnerRow = null;
var lastBingoNumber = 0;
var done = false;

for(int i = 0; i < bingonumbers.Length; i++)
{
    lastBingoNumber = bingonumbers[i];
    for(int b = 0; b < boards.Length; b++)
    {
        boards[b].AddBingoNumber(lastBingoNumber);
        var bingoRow = boards[b].GetBingoRow();
        if (bingoRow != null && bingoRow.Length > 0)
        {
            winnerRow = bingoRow;
            done = true;
        }
        if (done) break;
    }
    if (done) break;
}

if (winnerRow == null)
    Console.WriteLine("No winner :(");
else
{
    var quizAnswer1 = winnerRow?.Sum(x => x) * lastBingoNumber;
    Console.WriteLine($"Quiz answer part 1: {quizAnswer1}");
}