using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Puzzle
    {
        protected byte[,] values;

        public Puzzle(byte[] linearValues)
        {
            this.LinearData = linearValues;
        }

        public Puzzle(byte[,] matrixValues)
        {
            this.MatrixData = matrixValues;
        }

        public Puzzle()
        {
            var empty = new byte[9 ,9];
            empty.Initialize();
            this.MatrixData = empty;
        }

        public byte[,] MatrixData
        {
            get { return this.values; }
            private set { this.values = value; }
        }

        public byte[] LinearData
        {
            get
            {
                byte[] res = new byte[81];
                for(int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        res[i * 9 + j] = this.values[i, j];
                    }
                }
                return res;
            }
            private set
            {
                this.values = new byte[9, 9];
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        this.values[i, j] = value[i * 9 + j];
                    }
                }
            }
        }


        public IEnumerable<Puzzle> Solve(int maxNumberOfSolutions)
        {
            // set up the solver
            var intermediaryCoverMatrix = new CovermatrixFactory().GetCreateIntermediaryCoverMatrix();
            var doubleLinkedListCoverMatrix = new DllCoverMatrix(intermediaryCoverMatrix);
            Solver s = new Solver(doubleLinkedListCoverMatrix);

            // solve the current instance
            IEnumerable<Puzzle> solutions = s.Solve(this, maxNumberOfSolutions);

            return solutions;
        }

        public virtual bool Validate()
        {
            try
            {
                return this.Solve(1).Count() > 0;
            }
            catch(StackOverflowException)
            {
                // Important: calling solve on an inconsistent puzzle (faulty givens) may lead to infinite recursion. If this
                // occurs, and we trust our solution to never recurse indefintely on consistent givens, the puzzle is certainly invalid.
                // Note that relying on this assumption yet another reason (besides performance) why GeneratedPuzzle.Validate is much better.
                return false;
            }
        }
        

        public string PrettyValues()
        {
            StringBuilder builder = new StringBuilder();
            for (int i= 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    builder.AppendFormat("{0} ", this.values[i, j]);
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}
