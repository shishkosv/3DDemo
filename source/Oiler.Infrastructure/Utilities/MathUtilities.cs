using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oiler.Infrastructure.Utilities
{
    public static class MathUtilities
    {
        public static int GetRandomNumber(int startPos, int endPosition)
        {
            //Random r = new Random();
            //return r.Next(startPos, endPosition);

            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(startPos, endPosition);
        }
    }
}
