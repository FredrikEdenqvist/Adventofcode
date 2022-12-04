string input;
using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

var list = input
    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)
                  .Select(y => y.Split('-', StringSplitOptions.RemoveEmptyEntries))
                                .Select(y => (Convert.ToInt32(y[0]), Convert.ToInt32(y[1])))
                                .ToArray())
    .Select(x => (x[0].Item1, x[0].Item2, x[1].Item1, x[1].Item2))
    .ToList();

var sum1 = list.Count(x => (x.Item1 <= x.Item3 && x.Item2 >= x.Item4) || (x.Item3 <= x.Item1 && x.Item4 >= x.Item2));

Console.WriteLine("Part1:");
Console.WriteLine($"Pares: {sum1}");

var sum2 = list.Count(x => (x.Item1 <= x.Item3 && x.Item3 <= x.Item2) || 
                           (x.Item1 <= x.Item4 && x.Item4 <= x.Item2) ||
                           (x.Item3 <= x.Item1 && x.Item1 <= x.Item4) ||
                           (x.Item3 <= x.Item2 && x.Item2 <= x.Item4));

Console.WriteLine("Part2:");
Console.WriteLine($"Pares: {sum2}");