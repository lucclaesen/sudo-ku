using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class GeneratedPuzzle : Puzzle
    {
        public Puzzle Solution { get; set; }

        public GeneratedPuzzle(byte[,] matrixValues, Puzzle solution) : base(matrixValues)
        {
            this.Solution = solution;
        }

        public override bool Validate()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    byte v = this.values[i, j];
                    if (v != 0)
                    {
                        if (v != this.Solution.MatrixData[i, j])
                            return false;
                    } 
                }
            }
            return true;
        }
    }
}
