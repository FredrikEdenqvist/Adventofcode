namespace parts
{
    public static class CountPositionExtensions
    {
        public static int CountPosition(this (int x1, int y1, int x2, int y2)[] list, int x, int y)
        {
            return list.Count(item => item.IsPassingThru(x, y));
        }

        public static bool IsPassingThru(this (int x1, int y1, int x2, int y2) item, int x, int y)
        {
            var maxY = item.y1 > item.y2 ? item.y1 : item.y2;
            var minY = item.y1 > item.y2 ? item.y2 : item.y1;
            var maxX = item.x1 > item.x2 ? item.x1 : item.x2;
            var minX = item.x1 > item.x2 ? item.x2 : item.x1;

            if (minY > y || y > maxY || minX > x || x > maxX)
                return false;

            if (item.x2 == item.x1)
                return item.x1 == x && minY <= y && y <= maxY;

            decimal k = (item.y2 - item.y1) / (item.x2 - item.x1);
            decimal m = item.y1 - (k * item.x1);

            return y == (k * x) + m;
        }

        public static bool Is45Degree(this (int x1, int y1, int x2, int y2) item)
        {
            if (item.x2 == item.x1) return false;
            return 1m == Math.Abs((decimal)(item.y2 - item.y1) / (item.x2 - item.x1));
        }
    }
}
