namespace GameOfLife.GridMembers
{
    public record LifeCell
    {
        public int X { get; }
        public int Y { get; }

        public LifeCell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}