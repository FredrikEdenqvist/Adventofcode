namespace advcode_05
{
    internal class Move
    {
        public int Count { get; set; }
        public int From { get; set; }
        public int To { get; set; }

        internal static Move Parse(string row)
        {
            var moving = row.Split(new[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            return new Move { Count = moving[0], From = moving[1], To = moving[2] };
        }
    }
}
