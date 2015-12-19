using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Extensions
    {
        public static string ToPrettyPrintString(this bool[][] intermediaryCoverMatrix)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var row in intermediaryCoverMatrix)
            {
                foreach (var v in row)
                {
                    builder.Append(v ? "x" : " ");
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}
