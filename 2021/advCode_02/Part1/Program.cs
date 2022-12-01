string[] input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8
        .GetString(mem.ToArray())
        .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
        .ToArray();
}

const string FORWARD = "forward";
const string UP = "up";
const string DOWN = "down";

IEnumerable<SubmarinPosition> wayfinder = input.Select(x =>
{
    if (x.StartsWith(FORWARD))
        return new SubmarinPosition(int.Parse(x.Replace(FORWARD, string.Empty).Replace(" ", string.Empty)), 0);
    if (x.StartsWith(UP))
        return new SubmarinPosition(0, -1 * int.Parse(x.Replace(UP, string.Empty).Replace(" ", string.Empty)));
    if (x.StartsWith(DOWN))
        return new SubmarinPosition(0, int.Parse(x.Replace(DOWN, string.Empty).Replace(" ", string.Empty)));

    return SubmarinPosition.Empty;

}).ToList();

SubmarinPosition sumPart1 = wayfinder.Aggregate((p1, p2) => p1.Add(p2));
SubmarinPosition sumPart2 = wayfinder.Aggregate((p1, p2) => p1.AddWithAim(p2));

Console.WriteLine($"Part 1 Product is { sumPart1.Product }");
Console.WriteLine($"Part 2 Product is { sumPart2.Product }");