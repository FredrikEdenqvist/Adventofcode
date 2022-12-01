static IEnumerable<int> SumGroups(string[] arr)
{
    int count = 0;
    for (int i = 0; arr.Length > i; i++)
    {
        if (string.IsNullOrEmpty(arr[i]))
        {
            yield return count;
            count = 0;
        }
        else if (int.TryParse(arr[i], out int countValue))
        {
            count += countValue;
        }
    }

    yield return count;
};

string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

var sumList = SumGroups(input.Split("\r\n", StringSplitOptions.None)).ToList();
var part1 = sumList.Max();
var part2 = sumList.OrderByDescending(x => x).Take(3).Sum();

Console.WriteLine($"Part 1");
Console.WriteLine($"Total Calories: {part1}");

Console.WriteLine($"Part 2");
Console.WriteLine($"Total Calories: {part2}");