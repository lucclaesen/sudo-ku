using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class PuzzleMatrixTranslator
    {
        /// <summary>
        /// In the cover matrix, every row represents a candidate solution, i.e. a value in a cell (identified by a row and column index). E.g. the row with index 0 represents
        /// a cell value 0 in cell (0, 0) in block 0. There are 9 (possible values in a cell) * 81 (number of cells) such candidate solutions.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static PuzzleInfo MatrixRowIndex2PuzzleInfo(int matrixRowIndex)
        {
            var rowIndex = matrixRowIndex / 81;
            var colIndex = (matrixRowIndex % 81) / 9;
            var cellValue = matrixRowIndex % 9;
            var blockIndex = (3 * (rowIndex / 3)) + (colIndex / 3);

            return new PuzzleInfo
            {
                RowIndex = rowIndex,
                ColIndex = colIndex,
                CellValue = cellValue,
                BlockIndex = blockIndex
            };
        }

        public static int PuzzleInfo2MatrixRowIndex(PuzzleInfo puzzleInfo)
        {
            return puzzleInfo.CellValue + puzzleInfo.ColIndex * 9 + puzzleInfo.RowIndex * 81;
        }
    }
}
