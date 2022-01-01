string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

var lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

var open =  "([{<";
var close = ")]}>";

var score = new Dictionary<char, long> {
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 },
    { '(', 1 },
    { '[', 2 },
    { '{', 3 },
    { '<', 4 }
};

long sumIllegalPoints = 0;
var sumLegalPoints = new List<long>();
var parseCache = new Stack<char>();

foreach (var line in lines)
{
    parseCache.Clear();
    bool success = true;
    foreach (var par in line)
    {
        if (open.Contains(par))
        {
            parseCache.Push(par);
        }
        else if (close.Contains(par))
        {
            var openPar = open[close.IndexOf(par)];
            if (!parseCache.Any() || parseCache.Pop() != openPar)
            {
                sumIllegalPoints += score[par];
                success = false;
            }
        }
    }
    if (success && parseCache.Any())
    {
        long legalPoints = 0;
        while (parseCache.Any())
        {
            legalPoints *= 5;
            legalPoints += score[parseCache.Pop()];
        }
        sumLegalPoints.Add(legalPoints);
    }
}

var z = sumLegalPoints.OrderBy(x => x).ToArray();
var middleIndex = (z.Length / 2);


Console.WriteLine($"Part 1: {sumIllegalPoints}");
Console.WriteLine($"Part 2: {z[middleIndex]}");