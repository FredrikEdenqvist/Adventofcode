using parts;
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


IEnumerable<GridConnection> GenerateConnections(int[][] grid)
{
    for (int x = 0; x < grid.Length; x++)
    {
        for (int y = 0; y < grid[x].Length; y++)
        {
            yield return new GridConnection { Point = (x, y), Value = grid[x][y] };
        }
    }
}

var connections = GenerateConnections(grid)
    .Where(x => x.Value != 9)
    .ToDictionary(x => x.Point, x => x);

foreach (var connection in connections)
{
    var point = connection.Key;
    var list = new[] { (point.x, point.y - 1), (point.x, point.y + 1), (point.x - 1, point.y), (point.x + 1, point.y) }.ToHashSet();
    connection.Value.Connections = connections.Where(x => list.Contains(x.Key)).Select(x => x.Value).ToList();
}

var connectionManager = new GridClusterConnections();
var listOfClusters = new List<List<GridConnection>>();
do
{
    var cluster = connectionManager.GetCluster(connections.First().Value);
    listOfClusters.Add(cluster);
    foreach (var item in cluster)
    {
        var ok = connections.Remove(item.Point);
    }

} while (connections.Any());

var countclusterHeat = listOfClusters
    .Select(x => x.Count)
    .OrderByDescending(x => x)
    .Take(3)
    .Aggregate(1, (a, b) => a * b);

Console.WriteLine($"Part 2: {countclusterHeat}");