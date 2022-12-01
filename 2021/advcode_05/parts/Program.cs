using parts;
string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

(int x1, int y1, int x2, int y2)[] directions = input
    .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Split(new [] { " -> ", "," }, StringSplitOptions.RemoveEmptyEntries))
    .Select(x => (int.Parse(x[0]), int.Parse(x[1]), int.Parse(x[2]), int.Parse(x[3])))
    .ToArray();

var part1Filtered = directions.Where(v => v.x1 == v.x2 || v.y1 == v.y2).ToArray();
var part2Filtered = directions.Where(v => v.x1 == v.x2 || v.y1 == v.y2 || v.Is45Degree()).ToArray();
var maxX = new[] { directions.Max(v => v.x1), directions.Max(v => v.x2) }.Max();
var maxY = new[] { directions.Max(v => v.y1), directions.Max(v => v.y2) }.Max();


int FindAllOverlapps(int maxX, int maxY, (int x1, int y1, int x2, int y2)[] source)
{
    int[,] grid = new int[maxX, maxY];

    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxX; x++)
        {
            grid[x, y] = source.CountPosition(x, y);
        }
    }

    int countMoreIntersec = 0;
    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxX; x++)
        {
            if (grid[x, y] > 1)
                countMoreIntersec++;
        }
    }

    return countMoreIntersec;
}



Console.WriteLine($"Part1 {FindAllOverlapps(maxX, maxY, part1Filtered)} no of overlaps."); // 6564
Console.WriteLine($"Part2 {FindAllOverlapps(maxX, maxY, part2Filtered)} no of overlaps."); // 19172