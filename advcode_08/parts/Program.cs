string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

(string[] encoding, string[] value)[] lines = input
    .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(l =>
    {
        var split = l.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        return (split[0].Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries), split[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
    }).ToArray();

var sum = 0;
foreach (var line in lines)
{
    sum += line.value.Count(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7);
}

Console.WriteLine($"Part 1: {sum}");

bool IncludesIn(string value, string includes)
{
    return value.All(x => includes.Any(y => y == x));
}

string Subtract(string value1, string value2)
{
    return string.Join(string.Empty, value1.Where(x => !value2.Contains(x)));
}

string[] DecodeList(string[] encoded)
{
    var decoded = new string[10];
    decoded[1] = encoded.Single(x => x.Length == 2);
    decoded[4] = encoded.Single(x => x.Length == 4);
    decoded[7] = encoded.Single(x => x.Length == 3);
    decoded[8] = encoded.Single(x => x.Length == 7);

    var encodedList = encoded.Where(x => x.Length == 6 || x.Length == 5).ToList();

    decoded[6] = encodedList.Single(x => x.Length == 6 && !IncludesIn(decoded[1], x));
    encodedList.Remove(decoded[6]);
    decoded[0] = encodedList.Single(x => x.Length == 6 && !IncludesIn(decoded[4], x));
    encodedList.Remove(decoded[0]);
    decoded[9] = encodedList.Single(x => x.Length == 6);
    encodedList.Remove(decoded[9]);

    decoded[3] = encodedList.Single(x => x.Length == 5 && IncludesIn(decoded[1], x));
    encodedList.Remove(decoded[3]);
    decoded[5] = encodedList.Single(x => x.Length == 5 && IncludesIn(Subtract(decoded[4], decoded[1]), x));
    encodedList.Remove(decoded[5]);
    decoded[2] = encodedList.Single(x => x.Length == 5);
    encodedList.Remove(decoded[2]);

    return decoded;
}

int[] DecodeValue(string[] encodedValue, string[] decodeList)
{
    var list = new List<int>();
    foreach(string value in encodedValue)
    {
        for(int i = 0; i < decodeList.Length; i++)
        {
            if (IncludesIn(decodeList[i], value) && IncludesIn(value, decodeList[i]))
            {
                list.Add(i);
                break;
            }
        }
    }
    return list.ToArray();
}

int Combine(int[] value)
{
    int result = 0;
    for(int i = 0; i < value.Length; i++)
    {
        result += value[i] * (int)Math.Pow(10, value.Length - i - 1);
    }

    return result;
}

var decodeLinesSum = lines.Sum(x => Combine(DecodeValue(x.value, DecodeList(x.encoding))));
Console.WriteLine($"Part 2: {decodeLinesSum}");