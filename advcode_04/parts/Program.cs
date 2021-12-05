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

Console.WriteLine("");