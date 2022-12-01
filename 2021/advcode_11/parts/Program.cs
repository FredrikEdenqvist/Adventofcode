string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

int[][] lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Select(y => (int)char.GetNumericValue(y)).ToArray())
    .ToArray();

var xMax = lines.Length - 1;
var yMax = lines[0].Length - 1;
var printingOn = false;

void PrintState(string header)
{
    if (!printingOn)
        return;
    Console.WriteLine();
    Console.WriteLine(header);
    for (int y = 0; y < lines[0].Length; y++)
    {
        for (int x = 0; x < lines.Length; x++)
        {
            Console.Write(lines[x][y]);
        }
        Console.WriteLine();
    }
}

IEnumerable<(int x, int y)> StepFlashPop()
{
    for (int x = 0; x < lines.Length; x++)
    {
        for (int y = 0; y < lines[x].Length; y++)
        {
            lines[x][y] += 1;
            if (lines[x][y] > 9)
            {
                lines[x][y] = 0;
                yield return (x, y);
            }
        }
    }
}

IEnumerable<(int x, int y)> StepFlashPopPoint((int xP, int yP) p)
{
    if (lines[p.xP][p.yP] > 0)
        lines[p.xP][p.yP] += 1;
    if (lines[p.xP][p.yP] > 9)
    {
        lines[p.xP][p.yP] = 0;
        yield return (p.xP, p.yP);
    }
}

IEnumerable<(int x, int y)> GetSurrunded((int x, int y) p)
{
    var surr = new (int x, int y)[]{
        (p.x - 1, p.y - 1),
        (p.x, p.y - 1),
        (p.x + 1, p.y - 1),
        (p.x - 1, p.y),
        (p.x + 1, p.y),
        (p.x - 1, p.y + 1),
        (p.x, p.y + 1),
        (p.x + 1, p.y + 1)}
    .Where(p => p.x >= 0 && p.y >= 0 && p.x <= xMax && p.y <= yMax)
    .ToList();
    return surr;
}

long flashesMade = 0;
bool reportedAllFlashed = false;
PrintState("Before any steps:");

for (int i = 0; i < 300; i++)
{
    var flashPoints = StepFlashPop().ToList();
    flashesMade += flashPoints.Count;
    while (flashPoints.Any())
    {
        flashPoints = flashPoints.SelectMany(x => GetSurrunded(x)).SelectMany(x => StepFlashPopPoint(x)).ToList();
        flashesMade += flashPoints.Count;
    }
    PrintState($"After step {i + 1} (Flashes made: {flashesMade}):");

    if (i == 100)
    {
        Console.WriteLine($"Flashes made at step 100: {flashesMade}");
    }

    if (!reportedAllFlashed && lines.All(x => x.All(y => y == 0)))
    {
        reportedAllFlashed = true;
        Console.WriteLine($"All did flash att step { i + 1}");
    }
}
