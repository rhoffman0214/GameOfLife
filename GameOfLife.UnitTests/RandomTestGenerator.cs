namespace GameOfLife.UnitTests
{
    public class RandomTestGenerator : IRandomCellStateGenerator
    {
        public int GenerateCellState()
        {
            //we can initialize all test grids to be "dead"
            return 0;
        }
    }
}
