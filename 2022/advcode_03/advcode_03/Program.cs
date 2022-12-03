using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

string input;
using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

var characterList = new List<char>();
for (int i = 0; i + 'a' <= 'z'; i++)
{
    characterList.Add((char)('a' + i));
}
for (int i = 0; i + 'A' <= 'Z'; i++)
{
    characterList.Add((char)('A' + i));
}

var ryggsacks = input
    .Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
    .Select(x => (x[..(x.Length / 2)], x[(x.Length / 2)..], x))
    .ToList();

int sum = 0;
foreach (var item in ryggsacks)
{
    var c = item.Item1.First(x => item.Item2.Any(y => y == x));
    sum += characterList.IndexOf(c) + 1;
}

Console.WriteLine("Part1:");
Console.WriteLine($"Sum of the priorities {sum}");


var sum2 = 0;
for (int i = 0; ryggsacks.Count > i; i += 3)
{
    var firstItem = ryggsacks[i].Item3!;
    var items = ryggsacks.Select(x => x.Item3).Skip(i + 1).Take(2).ToList();
    var a = firstItem.First(x => items[0].Any(y => y == x && items[1].Any(z => x == z)));

    sum2 += characterList.IndexOf(a) + 1;
}


Console.WriteLine("Part2:");
Console.WriteLine($"Sum of the priorities {sum2}");