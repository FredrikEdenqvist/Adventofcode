IList<int> input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8
        .GetString(mem.ToArray())
        .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x))
        .ToList();
}

var stage1 = new List<int>();
for (int i = 0; i < input.Count - 2; i++)
{
    stage1.Add(input[i] + input[i + 1] + input[i + 2]);
}

var arr = stage1.ToArray();
Console.WriteLine($"Total altsum: {arr.Zip(arr[1..], (a, b) => a < b).Count(x => x)}");