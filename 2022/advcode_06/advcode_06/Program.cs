string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}


int index = 0;
bool done = false;
while (index < input.Length && !done)
{
    for(int j = 0; j < 3; j++)
    {
        if (input[(index + 1 + j)..(index + 4)].Contains(input[index + j]))
        {
            done = false;
            break;
        }
        done = true;
    }
    index++;
}

Console.WriteLine("Part1:");
Console.WriteLine($"Item: {index + 3}: {input[(index - 1)..(index + 3)]}");

done = false;
while (index < input.Length && !done)
{
    for (int j = 0; j < 13; j++)
    {
        if (input[(index + 1 + j)..(index + 14)].Contains(input[index + j]))
        {
            done = false;
            break;
        }
        done = true;
    }
    index++;
}


Console.WriteLine("Part2:");
Console.WriteLine($"Item: {index + 13}: {input[(index - 1)..(index + 13)]}");