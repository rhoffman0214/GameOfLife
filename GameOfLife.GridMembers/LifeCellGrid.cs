namespace GameOfLife.GridMembers
{
    public sealed class LifeCellGrid
    {
        private int[,] Grid;
        public int Rows { get; }
        public int Columns { get; }

        public LifeCellGrid(LifeCellGrid lifeCellGrid)
        {
            Grid = lifeCellGrid.Grid.Clone() as int[,] ?? throw new System.Exception();
            Rows = lifeCellGrid.Rows;
            Columns = lifeCellGrid.Columns;
        }
        public LifeCellGrid(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            Grid = new int[Rows, Columns];
        }

        public int GetCellState(LifeCell cell)
        {
            if (cell.X >= 0 && cell.X < this.Rows
            && cell.Y >= 0 && cell.Y < this.Columns)
                return this.Grid[cell.X, cell.Y];

            //if we're out of bounds, we don't want to count that
            return 0;
        }

        public void SetCellState(LifeCell cell, int state)
        {
            if (cell.X >= 0 && cell.X < this.Rows
            && cell.Y >= 0 && cell.Y < this.Columns)
                this.Grid[cell.X, cell.Y] = state;
        }
        public static LifeCellGrid Create(int rowSize, int colSize
        , IRandomCellStateGenerator randomNumber)
        {
            var localGrid = new LifeCellGrid(rowSize, colSize);

            for (int x = 0; x < rowSize; x++)
            {
                for (int y = 0; y < colSize; y++)
                {
                    localGrid.SetCellState(new LifeCell(x, y), randomNumber.GenerateCellState());
                }
            }

            return localGrid;
        }

        public static LifeCellGrid Update(LifeCellGrid oldGrid)
        {
            var updateGrid = new LifeCellGrid(oldGrid);

            for (int mainRow = 0; mainRow < oldGrid.Rows; mainRow++)
            {
                for (int mainCol = 0; mainCol < oldGrid.Columns; mainCol++)
                {
                    var aliveNeighbors = 0;
                    var updateCell = new LifeCell(mainRow, mainCol);

                    for (int row = -1; row <= 1; row++)
                    {
                        for (int col = -1; col <= 1; col++)
                        {
                            aliveNeighbors += updateGrid.GetCellState(new LifeCell(mainRow + row, mainCol + col));
                        }
                    }

                    var currentSquare = updateGrid.GetCellState(updateCell);
                    aliveNeighbors -= currentSquare;

                    //cell is alive and has correct number of neighbors - Live
                    if (currentSquare == 1 && (aliveNeighbors == 2 || aliveNeighbors == 3))
                    {
                        updateGrid.SetCellState(updateCell, 1);
                    }
                    //cell is dead and has exact neighbors, revive - Live
                    else if (currentSquare == 0 && aliveNeighbors == 3)
                    {
                        updateGrid.SetCellState(updateCell, 1);
                    }
                    //otherwise all other cases die - Die
                    else
                    {
                        updateGrid.SetCellState(updateCell, 0);
                    }
                }
            }

            return updateGrid;
        }

        public bool WillCellLive(LifeCell cell, int aliveNeighbors)
        {
            //cell is alive and has correct number of neighbors - Live
            if (GetCellState(cell) == 1 && (aliveNeighbors == 2 || aliveNeighbors == 3))
            {
                return true;
            }
            //cell is dead and has exact neighbors, revive - Live
            else if (GetCellState(cell) == 0 && aliveNeighbors == 3)
            {
                return true;
            }
            //otherwise all other cases die - Die
            return false;
        }

        public int GetCellCount()
        {
            return Rows * Columns;
        }
    }
}