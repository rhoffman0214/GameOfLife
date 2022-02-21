using GameOfLife.GridMembers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.UnitTests
{
    [TestClass]
    public class LifeCellGridTests
    {
        //1 will be our alive value
        private const int _alive = 1;
        [DataTestMethod]
        [DataRow(15, 15, 225)]
        [DataRow(20, 20, 400)]
        [DataRow(25, 25, 625)]
        public void GetCells_WithSetGridSize_ReturnsCorrectNumberOfCells(int rows, int columns, int cellCount)
        {
            var grid = CreateCellGrid(rows, columns);

            var gridCellCount = grid.GetCellCount();

            Assert.AreEqual(gridCellCount, cellCount);
        }

        [DataTestMethod]
        [DataRow(10, 10)]
        [DataRow(20, 20)]
        public void GetCellState_SetCellState_ReturnsCorrectCellState(int rows, int columns)
        {
            var grid = CreateCellGrid(rows, columns);
            var cell = new LifeCell(8, 8);

            //1 represents alive
            grid.SetCellState(cell, _alive);
            var cellState = grid.GetCellState(cell);

            Assert.AreEqual(_alive, cellState);
        }

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, false)]
        [DataRow(2, true)]
        [DataRow(3, true)]
        [DataRow(4, false)]
        public void CellShouldLive_LiveCell_CellBehaviorIsCorrect(int aliveNeighbors, bool shouldLive)
        {
            var grid = CreateCellGrid(5, 5);

            var cell = new LifeCell(2, 2);
            grid.SetCellState(cell, _alive);

            Assert.AreEqual(shouldLive, grid.WillCellLive(cell, aliveNeighbors));
        }

        private static LifeCellGrid CreateCellGrid(int rows, int columns)
        {
            return LifeCellGrid.Create(rows, columns, new RandomTestGenerator());
        }
    }
}