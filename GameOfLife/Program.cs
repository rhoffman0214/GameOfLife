using System;
using GameOfLife.GridMembers;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            const int rows = 50;
            const int columns = 100;
            const int maxRuns = 100;

            var grid = new LifeCellGrid(rows, columns);
            grid = LifeCellGrid.Create(rows, columns, new RandomCellStateGenerator());
            Console.SetWindowSize(columns + 10, rows + 10);

            var runs = 0;
            do
            {
                PrintGameOfLife(grid);
                grid = LifeCellGrid.Update(grid);
                //quick sleep to make the updates a little easier to see
                System.Threading.Thread.Sleep(100);
            } while (runs++ < maxRuns);
        }

        private static void PrintGameOfLife(LifeCellGrid grid)
        {
            Console.Clear();

            for (int x = 0; x < grid.Rows; x++)
            {
                for (int y = 0; y < grid.Columns; y++)
                {
                    Console.Write(grid.GetCellState(new LifeCell(x, y)) == 1 ? "#" : ' ');
                    if (y == grid.Columns - 1) Console.WriteLine();
                }
            }
        }
    }
}