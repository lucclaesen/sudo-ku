using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System.Linq;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class PuzzleTest
    {
        [TestMethod]
        public void PuzzleCorrectlyInitializes()
        {
            byte[] testdata = new byte[81]
            {
                5, 3, 0, 0, 7, 0, 0, 0, 0,
                6, 0, 0, 1, 9, 5, 0, 0, 0,
                0, 9, 8, 0, 0, 0, 0, 6, 0,
                8, 0, 0, 0, 6, 0, 0, 0, 3,
                4, 0, 0, 8, 0, 3, 0, 0, 1,
                7, 0, 0, 0, 2, 0, 0, 0, 6,
                0, 6, 0, 0, 0, 0, 2, 8, 0,
                0, 0, 0, 4, 1, 9, 0, 0, 5,
                0, 0, 0, 0, 8, 0, 0, 7, 9
            };

            Puzzle puzzle = new Puzzle(testdata);
            Assert.AreEqual(9, puzzle.MatrixData[8, 8]);
            Assert.AreEqual(5, puzzle.MatrixData[0, 0]);
        }

        [TestMethod]
        public void TestCreatePuzzleInfo()
        {

            var rowIndex = 93;
            var puzzleInfo = PuzzleMatrixTranslator.MatrixRowIndex2PuzzleInfo(rowIndex);
            Assert.AreEqual(1, puzzleInfo.RowIndex);
            Assert.AreEqual(1, puzzleInfo.ColIndex);
            Assert.AreEqual(3, puzzleInfo.CellValue);

            rowIndex = 168;
            puzzleInfo = PuzzleMatrixTranslator.MatrixRowIndex2PuzzleInfo(rowIndex);
            Assert.AreEqual(2, puzzleInfo.RowIndex);
            Assert.AreEqual(0, puzzleInfo.ColIndex);
            Assert.AreEqual(6, puzzleInfo.CellValue);

            rowIndex = 728;
            puzzleInfo = PuzzleMatrixTranslator.MatrixRowIndex2PuzzleInfo(rowIndex);
            Assert.AreEqual(8, puzzleInfo.RowIndex);
            Assert.AreEqual(8, puzzleInfo.ColIndex);
            Assert.AreEqual(8, puzzleInfo.CellValue);
        }

        [TestMethod]
        public void TestSolveOnPuzzleWithSingleSolution()
        {
            byte[] challenge = {
                5, 3, 0, 0, 7, 0, 0, 0, 0,
                6, 0, 0, 1, 9, 5, 0, 0, 0,
                0, 9, 8, 0, 0, 0, 0, 6, 0,
                8, 0, 0, 0, 6, 0, 0, 0, 3,
                4, 0, 0, 8, 0, 3, 0, 0, 1,
                7, 0, 0, 0, 2, 0, 0, 0, 6,
                0, 6, 0, 0, 0, 0, 2, 8, 0,
                0, 0, 0, 4, 1, 9, 0, 0, 5,
                0, 0, 0, 0, 8, 0, 0, 7, 9
            };

            byte[] expectedSolution = new byte[81]
            {
                5, 3, 4, 6, 7, 8, 9, 1, 2,
                6, 7, 2, 1, 9, 5, 3, 4, 8,
                1, 9, 8, 3, 4, 2, 5, 6, 7,
                8, 5, 9, 7, 6, 1, 4, 2, 3,
                4, 2, 6, 8, 5, 3, 7, 9, 1,
                7, 1, 3, 9, 2, 4, 8, 5, 6,
                9, 6, 1, 5, 3, 7, 2, 8, 4,
                2, 8, 7, 4, 1, 9, 6, 3, 5,
                3, 4, 5, 2, 8, 6, 1, 7, 9
            };
            Puzzle p = new Puzzle(challenge);
            var solutions = p.Solve(2);
            Assert.AreEqual(1, solutions.Count());

            for(int i = 0; i < 81; i++)
            {
                Assert.AreEqual(expectedSolution[i], solutions.First().LinearData[i]);
            }
        }

        [TestMethod]
        public void CanCreateTenSolutionsOutOfEmptyPuzzle()
        {
            Puzzle p = new Puzzle();
            var solutions = p.Solve(10);
            Assert.AreEqual(10, solutions.Count());

            // Test that all of these are different
            foreach (var sourceSolution in solutions)
            {
                foreach (var targetSolution in solutions)
                {
                    if (sourceSolution != targetSolution)
                    {
                        bool theyAreTheSame = true;
                        for(int i = 0; i < 81; i++)
                        {
                            if (sourceSolution.LinearData[i] != targetSolution.LinearData[i])
                            {
                                theyAreTheSame = false;
                                break;
                            }
                        }
                        Assert.IsFalse(theyAreTheSame);
                    }
                }
            }
        }

        [TestMethod]
        public void TestGenerationProducedDifferentPuzzles()
        {
            Generator generator = new Generator();
            List<Puzzle> generatedPuzzles = new List<Puzzle>();
            for (int i = 0; i < 10; i++)
            {
                generatedPuzzles.Add(generator.CreatePuzzle());
            }

            foreach (var leftPuzzle in generatedPuzzles)
            {
                foreach (var rightPuzzle in generatedPuzzles)
                {
                    if (leftPuzzle != rightPuzzle)
                    {
                        // compare
                        bool areDifferent = false;
                        for (int i = 0; i < 81; i++)
                        {
                            if (leftPuzzle.LinearData[i] != rightPuzzle.LinearData[i])
                            {
                                areDifferent = true;
                                break;
                            }
                        }
                        Assert.IsTrue(areDifferent);
                    }
                }
            }
        }

        [TestMethod]
        public void GeneratedPuzzleValidateWorks()
        {
            Generator generator = new Generator();
            GeneratedPuzzle puzzle = generator.CreatePuzzle();

            // First: expectations of validation negatives
            bool ok = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puzzle.MatrixData[i, j] == 0)
                    {
                        puzzle.MatrixData[i, j] = (byte) (puzzle.Solution.MatrixData[i, j] == 9 ? 1 : puzzle.Solution.MatrixData[i, j] + 1);
                        if (puzzle.Validate())
                        {
                            ok = false;
                            break;
                        }
                    }
                }
            }

            Assert.IsTrue(ok, "The Validation works as expected");

            // Expect validation positives
            puzzle = generator.CreatePuzzle();
            ok = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puzzle.MatrixData[i, j] == 0)
                    {
                        puzzle.MatrixData[i, j] = (byte) puzzle.Solution.MatrixData[i, j];
                        if (!puzzle.Validate())
                        {
                            ok = false;
                            break;
                        }
                    }
                }
            }
            Assert.IsTrue(ok, "The Validation works as expected");
        }
    }
}
