using System;

namespace GameOfLife
{
    public class RandomCellStateGenerator : IRandomCellStateGenerator
    {
        private readonly Random random = new Random((int)DateTime.Now.Millisecond);
        public int GenerateCellState()
        {
            //1 is alive state
            //0 is dead state
            return random.Next() % 2 == 1 ? 1 : 0;
        }
    }
}