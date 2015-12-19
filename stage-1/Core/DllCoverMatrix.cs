using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    // Represents a 324 x 739 2D data structure that corresponds to a 9  x 9 puzzle. The representation is designed for performant application of the dancing links algorithm.
    // First of all, the matrix is a sparse one: if a cell value (row) does not satisfy a constraint (column), there simply is no node for that row and column. In the cover
    // matrix for sudoku's, the majority of cells can thus be simply not represented.
    // Applying backtracking within a depth-first algorithm implies that tentative coverings (deletions of rows) can easily be undone (uncovering). The best format for supporting
    // this is a double linked list (dll).
    public class DllCoverMatrix
    {
        private IntermediaryCoverMatrix intermediaryConverMatrix;
        public HeaderNode StartNode
        {
            get; private set;
        }
        public Node[] RowHeaders { get; private set; }

        public DllCoverMatrix(IntermediaryCoverMatrix intermediaryCoverMatrix)
        {
            this.RowHeaders = new Node[Constants.COVERMATRIX_ROW_SIZE];
            this.intermediaryConverMatrix = intermediaryCoverMatrix;
            this.Build();
        }

        protected void Build()
        {
            HeaderNode[] headerNodes = null;
            BuildHeaders(out headerNodes);
            BuildRows(headerNodes);
        }
        
        public void RemoveRow(int rowIndex)
        {
            // set the nodePointer to the first cell of the row
            Node nodePointer = this.RowHeaders[rowIndex];
            do
            {
                // Cover every column for which the current row has a satisfaction
                CoverColumn(nodePointer.ColHeader);
                nodePointer = nodePointer.Right;
            } while (nodePointer != this.RowHeaders[rowIndex]);
        }


        protected void BuildHeaders(out HeaderNode[] headerNodes)
        {
            headerNodes = new HeaderNode[Constants.COVERMATRIX_COL_SIZE];
            this.StartNode = new HeaderNode();
            this.StartNode.Name = "StartNode";
            for (int i = 0; i < Constants.COVERMATRIX_COL_SIZE; i++)
            {
                HeaderNode columnHeader = new HeaderNode();
                columnHeader.Name = ConstraintDescription(i);
                headerNodes[i] = columnHeader;
                InsertLeft(this.StartNode, columnHeader);
            }
        }

        protected void BuildRows(HeaderNode[] headerNodes)
        {
            for(int i = 0; i < this.intermediaryConverMatrix.NumberOfRows; i++)
            {
                var row = this.intermediaryConverMatrix[i];
                for(int j = 0; j < row.Length; j ++)
                {
                    // This is were the sparsity of the DLL representation is determined. Satisfaction == false, means no node in the DLL representation
                    if (row[j])
                    {
                        Node node = new Node();
                        node.ColHeader = headerNodes[j];
                        node.RowNumberInMatrix = i;
                        InsertUp(headerNodes[j], node);

                        if (this.RowHeaders[i] == null)
                        {
                            this.RowHeaders[i] = node;
                        }
                        else
                        {
                            InsertLeft(this.RowHeaders[i], node);
                        }
                    }


                }        
            }
        }

        

        protected string ConstraintDescription(int i)
        {
            string description = null;
            if (i < 81)
            {
                description = string.Format("Row - col constraint for cell ({0}, {1})", i / 9, i % 9) ;
            }
            // Check Row - Number constraints
            else if (i < 162)
            {
                var index = i - 81;
                description = string.Format("Row - num constraint for row {0} and value {1}", index / 9, index % 9);
            }
            // Check Column - Number constraints
            else if (i < 243)
            {
                var index = i - 162;
                description = string.Format("Col - num constraint for col {0} and value {1}", index / 9, index % 9);
            }
            // Check block - number constraints
            else
            {
                var index = i - 243;
                description = string.Format("Block - num constraint for block {0} and value {1}", index / 9, index % 9);
            }
            return description;
        }


        public void CoverColumn(HeaderNode TargetNode)
        {
            RemoveLeftRight(TargetNode);
            for (Node i = TargetNode.Down; i != TargetNode; i = i.Down)
            {
                for (Node j = i.Right; j != i; j = j.Right)
                {
                    RemoveUpDown(j);
                    j.ColHeader.SatisfactionsCount--;
                }
            }
        }

        public void UncoverColumn(HeaderNode TargetNode)
        {
            for (Node i = TargetNode.Up; i != TargetNode; i = i.Up)
            {
                for (Node j = i.Left; j != i; j = j.Left)
                {
                    RestoreUpDown(j);
                    j.ColHeader.SatisfactionsCount++;
                }
            }
            RestoreLeftRight(TargetNode);
        }



        //Auxiliary functions for linked list manipulation
        public void InsertRight(Node TargetNode, Node NewNode)
        {
            NewNode.Right = TargetNode.Right;
            NewNode.Left = TargetNode;
            TargetNode.Right.Left = NewNode;
            TargetNode.Right = NewNode;
        }

        public void InsertLeft(Node TargetNode, Node NewNode)
        {
            NewNode.Left = TargetNode.Left;
            NewNode.Right = TargetNode;
            TargetNode.Left.Right = NewNode;
            TargetNode.Left = NewNode;
        }

        public void InsertUp(Node TargetNode, Node NewNode)
        {
            NewNode.Up = TargetNode.Up;
            NewNode.Down = TargetNode;
            TargetNode.Up.Down = NewNode;
            TargetNode.Up = NewNode;
        }

        public void InsertDown(Node TargetNode, Node NewNode)
        {
            NewNode.Down = TargetNode.Down;
            NewNode.Up = TargetNode;
            TargetNode.Down.Up = NewNode;
            TargetNode.Down = NewNode;
        }

        protected void RemoveLeftRight(Node TargetNode)
        {
            TargetNode.Left.Right = TargetNode.Right;
            TargetNode.Right.Left = TargetNode.Left;
        }

        protected void RestoreLeftRight(Node TargetNode)
        {
            TargetNode.Left.Right = TargetNode;
            TargetNode.Right.Left = TargetNode;
        }

        protected void RemoveUpDown(Node TargetNode)
        {
            TargetNode.Up.Down = TargetNode.Down;
            TargetNode.Down.Up = TargetNode.Up;
        }

        public void RestoreUpDown(Node TargetNode)
        {
            TargetNode.Up.Down = TargetNode;
            TargetNode.Down.Up = TargetNode;
        }
    }
}
