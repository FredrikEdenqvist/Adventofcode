using advcode_02;

string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

var turns = input
    .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(Turn.Create)
    .ToList();

Console.WriteLine("Part 1:");
Console.WriteLine($"Total score: {turns.Select(x => x.CalculateMyOutcome()).Sum()}");

Console.WriteLine("Part 2:");
Console.WriteLine($"Total score: {turns.Select(x => x.CalculateMyOutcomeWithSuggestion()).Sum()}");