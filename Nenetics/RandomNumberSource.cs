using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nenetics
{
    /// <summary>
    /// Returns a random integer that is from 0 up to (and including) the max argument
    /// </summary>
    public static class RandomNumberSource
    {
        private static Random rand = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Returns a random integer that is from 0 up to (and including) the max argument
        /// </summary>
        /// <param name="max">The maximum number that should be returned</param>
        public static int GetNext(int max)
        {
            return rand.Next(max+1);
        }
    }
}
