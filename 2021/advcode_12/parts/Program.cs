string input;

using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"), FileMode.Open))
using (MemoryStream mem = new MemoryStream())
{
    fs.CopyTo(mem);
    input = System.Text.Encoding.UTF8.GetString(mem.ToArray());
}

var list = input.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(new char[] { '-' }));
const string start = nameof(start);
const string end = nameof(end);