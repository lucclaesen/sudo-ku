using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// A simple wrapper around the cover matrix in an intermediary format, i.e. not optimized for the dancing links algorithm.
    /// </summary>
    public class IntermediaryCoverMatrix
    {
        private bool[][] data;

        public IntermediaryCoverMatrix()
        {
            data = new bool[Constants.COVERMATRIX_ROW_SIZE][];
        }
        
        public void Build()
        {
            for (int i = 0; i < Constants.COVERMATRIX_ROW_SIZE; i++)
            {
                var puzzleInfo = PuzzleMatrixTranslator.MatrixRowIndex2PuzzleInfo(i);
                data[i] = CreateMatrixRow(puzzleInfo);
            }
        }

        public int NumberOfRows {  get { return this.data.Length; } }

        public bool[] this[int i]
        {
            get
            {
                return this.data[i];
            }
        }

        public string ToPrettyString()
        {
            return this.data.ToPrettyPrintString();
        }

        #region Helpers
        protected bool[] CreateMatrixRow(PuzzleInfo puzzleInfo)
        {
            var row = new bool[Constants.COVERMATRIX_COL_SIZE];
            for (int i = 0; i < Constants.COVERMATRIX_COL_SIZE; i++)
            {
                // Check Row-Column constraints
                if (i < 81)
                {
                    row[i] = RowColumnConstraintIsSatisfiedBy(i, puzzleInfo);
                }
                // Check Row - Number constraints
                else if (i < 162)
                {
                    row[i] = RowNumberConstraintIsSatisfiedBy(i - 81, puzzleInfo);
                }
                // Check Column - Number constraints
                else if (i < 243)
                {
                    row[i] = ColumnNumberConstraintIsSatisfiedBy(i - 162, puzzleInfo);
                }
                // Check block - number constraints
                else
                {
                    row[i] = BlockNumberConstraintIsSatisfiedBy(i - 243, puzzleInfo);
                }
            }
            return row;
        }


        // A row-columnconstraint expresses that the given cell should have some value.
        protected bool RowColumnConstraintIsSatisfiedBy(int constraintIndex, PuzzleInfo puzzleInfo)
        {
            return puzzleInfo.RowIndex == constraintIndex / 9 &&
                puzzleInfo.ColIndex == constraintIndex % 9;
        }

        // A row - number constraint expresses that in a particular row, a particular value should occur.
        protected bool RowNumberConstraintIsSatisfiedBy(int constraintIndex, PuzzleInfo puzzleInfo)
        {
            return puzzleInfo.RowIndex == constraintIndex / 9 &&
                puzzleInfo.CellValue == constraintIndex % 9;
        }

        // A column - number constraint expresses that a particular value should occur in a particular column.
        protected bool ColumnNumberConstraintIsSatisfiedBy(int constraintIndex, PuzzleInfo puzzleInfo)
        {
            return puzzleInfo.ColIndex == constraintIndex / 9 &&
                puzzleInfo.CellValue == constraintIndex % 9;
        }

        // A block - number constraint expresses that a particular value should occur in a particular block.
        protected bool BlockNumberConstraintIsSatisfiedBy(int constraintIndex, PuzzleInfo puzzleInfo)
        {
            return puzzleInfo.BlockIndex == constraintIndex / 9 &&
                puzzleInfo.CellValue == constraintIndex % 9;
        }
        #endregion


    }
}
