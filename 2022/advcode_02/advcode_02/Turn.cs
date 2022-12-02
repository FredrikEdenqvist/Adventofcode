namespace advcode_02
{
    internal enum Shape
    {
        Rock = 1,
        Paper = 2,
        Scissor = 3
    }

    internal class Turn
    {
        public required Shape OpponentShape { get; init; }
        public required Shape MyShape { get; init; }

        internal int CalculateMyOutcome()
        {
            int score = 0;
            if (MyShape == Shape.Rock && OpponentShape == Shape.Scissor ||
                MyShape == Shape.Paper && OpponentShape == Shape.Rock ||
                MyShape == Shape.Scissor && OpponentShape == Shape.Paper)
            {
                score = 6;
            }
            if (MyShape == OpponentShape)
            {
                score = 3;
            }

            return (int)MyShape + score;
        }

        internal int CalculateMyOutcomeWithSuggestion()
        {
            int score = 0;
            Shape NewShape = Shape.Rock;

            switch (MyShape)
            {
                case Shape.Rock:
                    if (OpponentShape == Shape.Rock)
                        NewShape = Shape.Scissor;
                    else if (OpponentShape == Shape.Paper)
                        NewShape = Shape.Rock;
                    else
                        NewShape = Shape.Paper;
                    break;
                case Shape.Paper:
                    score = 3;
                    NewShape = OpponentShape;
                    break;
                case Shape.Scissor:
                    score = 6;
                    if (OpponentShape == Shape.Rock)
                        NewShape = Shape.Paper;
                    else if (OpponentShape == Shape.Paper)
                        NewShape = Shape.Scissor;
                    else
                        NewShape = Shape.Rock;
                    break;
                default: 
                    break;
            }

            return (int)NewShape + score;
        }

        internal static Shape GetShape(char shape)
        {
            return shape switch
            {
                'A' => Shape.Rock,
                'B' => Shape.Paper,
                'C' => Shape.Scissor,
                'X' => Shape.Rock,
                'Y' => Shape.Paper,
                'Z' => Shape.Scissor,
                _ => throw new NotSupportedException()
            };
        }

        internal static Turn Create(string turn)
        {
            return new Turn { MyShape = GetShape(turn[2]), OpponentShape = GetShape(turn[0]) };
        }
    }
}
