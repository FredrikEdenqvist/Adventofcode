string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

var horisontalPositions = input.Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(x => int.Parse(x))
    .ToList();

// Part 1
var minSum = int.MaxValue;
for (int i = 0; i < 100_000; i++)
{
    var t = horisontalPositions.Sum(x => Math.Abs(x - i));
    if (t < minSum)
    {
        minSum = t;
    }
    else
    {
        break;
    }
}

Console.WriteLine($"Part 1 min sum: {minSum}");

// Part 2

minSum = int.MaxValue;
for (int i = 0; i < 100_000; i++)
{
    var t = horisontalPositions.Sum(x => (Math.Abs(x - i) * (Math.Abs(x - i) + 1)) / 2);
    if (t < minSum)
    {
        minSum = t;
    }
    else
    {
        break;
    }
}

Console.WriteLine($"Part 2 min sum: {minSum}");