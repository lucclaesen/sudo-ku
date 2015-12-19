using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Solver 
    {

        private DllCoverMatrix matrix;
        private bool stopSearch;
        private List<Node> CurrentSolution;
        private List<List<int>> AllSolutions;

        public Solver(DllCoverMatrix matrix)
        {
            this.matrix = matrix;
            this.stopSearch = false;
            this.CurrentSolution = new List<Node>();
            this.AllSolutions = new List<List<int>>();
            
        }

        protected void SetGivens(Puzzle puzzle)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var v = puzzle.MatrixData[i, j];

                    // Zero's in the puzzle are empty cells, which are not represented in the dll matrix
                    if (v != 0)
                    {
                        PuzzleInfo given = new PuzzleInfo
                        {
                            RowIndex = i,
                            ColIndex = j,
                            // The dll is 0-based, whereas the puzzle represents values on base 1
                            CellValue = v - 1
                        };
                        int rowIndex = PuzzleMatrixTranslator.PuzzleInfo2MatrixRowIndex(given);
                        var correspondingRowHeader = matrix.RowHeaders[rowIndex];
                        this.CurrentSolution.Add(correspondingRowHeader);
                        this.matrix.RemoveRow(rowIndex);
                    }

                }
            }
        }

     

        public IEnumerable<Puzzle> Solve(Puzzle puzzle, int maxNumberOfSolutions)
        {
            this.SetGivens(puzzle);

            this.Solve(maxNumberOfSolutions);

            List<Puzzle> solutions = new List<Puzzle>();
            foreach (var solution in this.AllSolutions)
            {
                Puzzle convertedSolution = new Puzzle();
                foreach (var matrixRowIndex in solution)
                {
                    PuzzleInfo puzzleInfo = PuzzleMatrixTranslator.MatrixRowIndex2PuzzleInfo(matrixRowIndex);
                    // Again the translation from a zero based value representation to the 1-based representation of the puzzle.
                    convertedSolution.MatrixData[puzzleInfo.RowIndex, puzzleInfo.ColIndex] = (byte)(puzzleInfo.CellValue + 1);
                }
                solutions.Add(convertedSolution);
            }
            return solutions;
        }

        /// <summary>
        /// Recursive depth first and backtracking search for solutions.
        /// </summary>
        /// <param name="maxNumberOfSolutions"></param>
        protected void Solve(int maxNumberOfSolutions)
        {
            //If enough solutions have been found, stop recursing
            if (this.stopSearch)
            {
                return;
            }

            // Solution found, since no unsatisfied constraints are left, i.e. all columns have been covered and start.right == start.
            if (this.matrix.StartNode.Right == this.matrix.StartNode)
            {
                AddSolution();
                if (this.AllSolutions.Count >= maxNumberOfSolutions)
                {
                    this.stopSearch = true;
                }
                return;
            }

            HeaderNode ChosenColumn = ChooseColumnWithSHeuristics();
            this.matrix.CoverColumn(ChosenColumn);

            // For all rows satisfying the chosen column: top - down. We need to introduce randomness when in generation mode
            for (Node i = ChosenColumn.Down; i != ChosenColumn; i = i.Down)
            {
                // add the row to the current solution
                CurrentSolution.Add(i);

                // Iterate over all of the satisfactions of the row's satisfactions. Note that a row is circular. This guarantees that all satisfactions in the row
                // will be visited
                for (Node j = i.Right; j != i; j = j.Right)
                {
                    // Cover the column of the satisfaction, i.e. delete all rows with a satisfaction for the column
                    this.matrix.CoverColumn(j.ColHeader);
                }

                // recursive call -- solve on a reduced matrix;  returns void when a solution is found
                Solve(maxNumberOfSolutions);

                // Backtracking logic
                i = this.CurrentSolution.Last();
                this.CurrentSolution.Remove(i);
                ChosenColumn = i.ColHeader;
                for (Node j = i.Left; j != i; j = j.Left)
                {
                    this.matrix.UncoverColumn(j.ColHeader);
                }
            }
            this.matrix.UncoverColumn(ChosenColumn);
        }

        protected HeaderNode ChooseColumnWithSHeuristics()
        {
            int MinSize = int.MaxValue;
            HeaderNode Output = null;
            for (HeaderNode i = this.matrix.StartNode.Right as HeaderNode; i != this.matrix.StartNode; i = i.Right as HeaderNode)
            {
                if (i.SatisfactionsCount < MinSize)
                {
                    MinSize = i.SatisfactionsCount;
                    Output = i;
                }
            }

            return Output;
        }
      

        //Add a solution to the results vector.
        protected void AddSolution()
        {
            List<int> NewSolution = new List<int>();
            foreach (var i in this.CurrentSolution)
            {
                NewSolution.Add(i.RowNumberInMatrix);
            }
            this.AllSolutions.Add(NewSolution);
        }


    }
}
