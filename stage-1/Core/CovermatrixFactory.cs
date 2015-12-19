using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CovermatrixFactory
    {
      
        private static IntermediaryCoverMatrix intermediaryCoverMatrix = null;

        public IntermediaryCoverMatrix GetCreateIntermediaryCoverMatrix()
        {
            if (intermediaryCoverMatrix == null)
            {
                IntermediaryCoverMatrix m = new IntermediaryCoverMatrix();
                m.Build();
                intermediaryCoverMatrix = m;
            }
            return intermediaryCoverMatrix;
        }


    }
}
