public class SubmarinPosition
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Aim { get; set; }

    public SubmarinPosition()
    {
        X = 0;
        Y = 0;
        Aim = 0;
    }

    public SubmarinPosition(int x, int y, int aim = 0)
    {
        X = x;
        Y = y;
        Aim = aim;
    }
    public int Product => X * Y;
    public static SubmarinPosition Empty => new SubmarinPosition();

    public SubmarinPosition Add(SubmarinPosition newPosition)
    {
        return new SubmarinPosition
        {
            X = X + newPosition.X,
            Y = Y + newPosition.Y
        };
    }

    public SubmarinPosition AddWithAim(SubmarinPosition newPosition)
    {
        if (newPosition.X > 0)
            return new SubmarinPosition
            {
                X = X + newPosition.X,
                Y = Y + (newPosition.X * Aim),
                Aim = Aim
            };

        return new SubmarinPosition
        {
            X = X,
            Y = Y,
            Aim = Aim + newPosition.Y
        };
    }
    
}
