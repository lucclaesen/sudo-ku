using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Generator
    {
        protected Puzzle CreateRandomSolution()
        {
            Random firstRowRandomizer = new Random();
            int[] firstRowValues = Enumerable.Range(1, 9).OrderBy(x => firstRowRandomizer.Next()).ToArray();

            Puzzle emptyPuzzle = new Puzzle();
            for (int i = 0; i < firstRowValues.Count(); i++)
            {
                emptyPuzzle.MatrixData[0,i] = (byte)firstRowValues[i];
            }

            return emptyPuzzle.Solve(1).First();
        }

        public GeneratedPuzzle CreatePuzzle()
        {
            // Step one: as a starting point we create a solved puzzle which is generated randomly
            Puzzle randomPuzzle = this.CreateRandomSolution();

            // Save a clone for attaching it as the solution for the generated puzzle
            Puzzle solution = new Puzzle(randomPuzzle.LinearData);

            // Generate a random permutation of the puzzle cells
            Random randomizer = new Random();
            int[] rowIndices = Enumerable.Range(0, 9).OrderBy(x => randomizer.Next()).ToArray();
            int[] colIndices = Enumerable.Range(0, 9).OrderBy(x => randomizer.Next()).ToArray();

            int nbrCellsRemoved = 0;

            for(int i = 0; i < 9; i++)
            {
                for(int j=0; j< 9; j++)
                { 
                    int rowIndex = rowIndices[i];
                    int colIndex = colIndices[j];

                    // tentatively set the value to 0 (i.e. removal of the value)
                    var originalCellValue = randomPuzzle.MatrixData[rowIndex, colIndex];
                    randomPuzzle.MatrixData[rowIndex, colIndex] = 0;

                    var nbrSolutions = randomPuzzle.Solve(2).Count();
                    if (nbrSolutions > 1)
                    {
                        // if taking the value away introduces multiple solutions, put the value back
                        randomPuzzle.MatrixData[rowIndex, colIndex] = originalCellValue;
                    }
                    else
                    {
                        nbrCellsRemoved++;
                    }
                }
            }

            return new GeneratedPuzzle(randomPuzzle.MatrixData, solution);
        }
    }
}
