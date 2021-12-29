string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

var grid = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Select(y => (int)char.GetNumericValue(y))
    .ToArray())
    .ToArray();

IEnumerable<int> GetSurrundedValues(int[][] grid, int pX, int pY)
{
    var xMax = grid.Length - 1;
    var yMax = grid[0].Length - 1;
    (int x, int y)[] coords = new[] { (pX - 1, pY), (pX + 1, pY), (pX, pY - 1), (pX, pY + 1) };
    return coords
        .Where(p => p.y >= 0 && p.y <= yMax && 
                    p.x >= 0 && p.x <= xMax && (p.x != pX || p.y != pY))
        .Select(coord => grid[coord.x][coord.y]);
}

IEnumerable<int> GetLowestHeight(int[][] grid)
{
    var xMax = grid.Length - 1;
    var yMax = grid[0].Length - 1;
    for (int x = 0; x < grid.Length; x++)
    {
        for (int y = 0; y < grid[x].Length; y++)
        {
            if (GetSurrundedValues(grid, x, y).All(t => t > grid[x][y]))
                yield return grid[x][y];
        }
    }
}

Console.WriteLine($"Part 1: {GetLowestHeight(grid).Sum(x => x + 1)}");