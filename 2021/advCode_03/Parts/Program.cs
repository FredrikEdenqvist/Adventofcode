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

(int zeros, int ones) CountOn(string[] input, int position)
{
    int zeros = 0;
    int ones = 0;
    for (int i = 0; i < input.Length; i++)
    {
        var value = Convert.ToInt32(input[i][position].ToString()) == 0;
        zeros += (value ? 1 : 0);
        ones += (value ? 0 : 1);
    }
    return (zeros, ones);
}

string[] GetOnlyValuesAtPosition(string[] input, int value, int position)
{
    return input.Where(item => Convert.ToInt32(item[position].ToString()) == value).ToArray();
}

string GetOxygenRate(string[] input, int length)
{
    string[] currentSet = input;
    for(int i = 0; i < length; i++)
    {
        (int zeros, int ones) = CountOn(currentSet, i);
        currentSet = GetOnlyValuesAtPosition(currentSet, (zeros > ones ? 0 : 1), i);
        if (currentSet.Length == 1) return currentSet[0];
    }

    return "NA";
}

string GetCO2ScrubberRate(string[] input, int length)
{
    string[] currentSet = input;
    for (int i = 0; i < length; i++)
    {
        (int zeros, int ones) = CountOn(currentSet, i);
        currentSet = GetOnlyValuesAtPosition(currentSet, (zeros <= ones ? 0 : 1), i);
        if (currentSet.Length == 1) return currentSet[0];
    }

    return "NA";
}

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
bool[] sameValues= new bool[stringlength];

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

int StringToDecimal(string item)
{
    return ToDecimal(item.Select(x => Convert.ToInt32(x.ToString())).ToArray());
}

Console.WriteLine($"Minvalue: {string.Join("", minValues.Select(x => $"{x}"))}: {ToDecimal(minValues)}");
Console.WriteLine($"Maxvalue: {string.Join("", maxValues.Select(x => $"{x}"))}: {ToDecimal(maxValues)}");

Console.WriteLine($"Product for part 1: {ToDecimal(minValues) * ToDecimal(maxValues)}");

Console.WriteLine($"Oxygen rate: {GetOxygenRate(input, stringlength)}");
Console.WriteLine($"CO2 rate: {GetCO2ScrubberRate(input, stringlength)}");

Console.WriteLine($"Product for part 2: {StringToDecimal(GetOxygenRate(input, stringlength)) * StringToDecimal(GetCO2ScrubberRate(input, stringlength))}");