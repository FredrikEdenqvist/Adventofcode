using advcode_05;

string input;
using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}
var splitarr = input.Split('\n', StringSplitOptions.None);
int moveStartIndex = 0;
for (moveStartIndex = 0; moveStartIndex < splitarr.Length && splitarr[moveStartIndex] != string.Empty; moveStartIndex++) { }


var listOfMoves = new List<Move>();
for (int i = moveStartIndex; i < splitarr.Length; i++)
{
    if (splitarr[i] != string.Empty)
        listOfMoves.Add(Move.Parse(splitarr[i]));
}
var stacks = ListStack.GetStackList();

foreach (var move in listOfMoves)
{
    for (int i = 0; i < move.Count; i++)
    {
        stacks[move.To - 1].Push(stacks[move.From - 1].Pop());
    }
}

Console.WriteLine("Part1:");
Console.WriteLine($"{string.Join(string.Empty, stacks.Select(x => x.Peek()))}");

var stacks2 = ListStack.GetStackList();
foreach (var move in listOfMoves)
{
    Stack<string> revList = new Stack<string>();
    for (int i = 0; i < move.Count; i++)
    {
        revList.Push(stacks2[move.From - 1].Pop());
    }
    for (int i = 0; i < move.Count; i++)
    {
        stacks2[move.To - 1].Push(revList.Pop());
    }
}

Console.WriteLine("Part2:");
Console.WriteLine($"{string.Join(string.Empty, stacks2.Select(x => x.Peek()))}");