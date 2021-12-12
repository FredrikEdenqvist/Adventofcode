string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

var listofFish = input
    .Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(x => int.Parse(x))
    .GroupBy(x => x, x => 1)
    .ToDictionary(x => x.Key, x => x.Sum(y => (long)y));

long[] fishList = new long[9];
long[] temp = new long[9];

for (int i = 0; i < 9; i++)
{
    fishList[i] = listofFish.TryGetValue(i, out long count) ? count : 0;
}

const int totalDays = 256;

for (int i = 0; i < totalDays; i++)
{
    temp[0] = fishList[1];
    temp[1] = fishList[2];
    temp[2] = fishList[3];
    temp[3] = fishList[4];
    temp[4] = fishList[5];
    temp[5] = fishList[6];
    temp[6] = fishList[7] + fishList[0];
    temp[7] = fishList[8];
    temp[8] = fishList[0];

    Array.Copy(temp, fishList, 9);
}

Console.WriteLine($"Total number of fish: {fishList.Sum(x => x)}"); // Part1 388419
                                                                    // Part2 1740449478328