using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cons
{
    class Program
    {
        static void Main(string[] args)
        {

            Generator g = new Generator();
            GeneratedPuzzle puzzle = g.CreatePuzzle();

            Console.WriteLine("The challenge:");
            Console.WriteLine(puzzle.PrettyValues());

            Console.WriteLine("And its solution");
            Console.WriteLine(puzzle.Solution.PrettyValues());
            
        
            Console.ReadKey();
        }

        //    Console.WriteLine("Beginning");
        //    var m = new CovermatrixFactory().GetCreateIntermediaryCoverMatrix();
        //    byte[] challenge = {
        //        5, 3, 0, 0, 7, 0, 0, 0, 0,
        //        6, 0, 0, 1, 9, 5, 0, 0, 0,
        //        0, 9, 8, 0, 0, 0, 0, 6, 0,
        //        8, 0, 0, 0, 6, 0, 0, 0, 3,
        //        4, 0, 0, 8, 0, 3, 0, 0, 1,
        //        7, 0, 0, 0, 2, 0, 0, 0, 6,
        //        0, 6, 0, 0, 0, 0, 2, 8, 0,
        //        0, 0, 0, 4, 1, 9, 0, 0, 5,
        //        0, 0, 0, 0, 8, 0, 0, 7, 9
        //    };
        //    // Puzzle p = new Puzzle(challenge);
        //    Puzzle p = new Puzzle();
        //    var solutions = p.Solve(10);
        //    foreach(var solution in solutions)
        //    {
        //        Console.WriteLine(solution.PrettyValues());
        //    }

        //    Console.WriteLine("Done. Press key to exit ...");
        //    Console.ReadKey();
        //}

        public static void OutputArray(string path, string prettified)
        {
            System.IO.File.WriteAllText(path, prettified);
        }
    }
}
