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
int stringlength = input[0].Length;

var results = input.Aggregate(new (int zeros, int ones)[stringlength], (aggr, item) =>
{
    for (int i = 0; i < item.Length; i++)
    {
        var value = Convert.ToInt32(item[i].ToString()) == 0;
        aggr[i] = (aggr[i].zeros + (value ? 1 : 0), aggr[i].ones + (value ? 0 : 1));
    }

    return aggr;
});

int[] maxValues = new int[stringlength];
int[] minValues = new int[stringlength];

for (int i = 0; i < stringlength; i++)
{
    maxValues[i] = results[i].zeros > results[i].ones ? 0 : 1;
    minValues[i] = results[i].zeros > results[i].ones ? 1 : 0;
}

int ToDecimal(int[] item)
{
    int sum = 0;
    for (int i = 0; i < item.Length; i++)
    {
        sum += (int)Math.Pow(2, i) * item[item.Length - i - 1];
    }

    return sum;
}

Console.WriteLine($"Minvalue: {string.Join("", minValues.Select(x => $"{x}"))}: {ToDecimal(minValues)}");
Console.WriteLine($"Maxvalue: {string.Join("", maxValues.Select(x => $"{x}"))}: {ToDecimal(maxValues)}");

Console.WriteLine($"Product for part 1: {ToDecimal(minValues) * ToDecimal(maxValues)}");

