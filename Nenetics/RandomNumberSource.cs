// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomNumberSource.cs" company="Josh Charles">
//   This software is released under a license yet to be determined but of the open source variety
// </copyright>
// <summary>
//   Returns a random integer that is from 0 up to (and including) the max argument
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nenetics
{
	using System;

	/// <summary>
    /// Returns a random integer that is from 0 up to (and including) the max argument
    /// </summary>
    public static class RandomNumberSource
    {
	    /// <summary>
	    /// The random source.
	    /// </summary>
	    private static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Returns a random integer that is from 0 up to (and including) the max argument
        /// </summary>
        /// <param name="max">
        /// The maximum number that should be returned
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetNext(int max)
        {
	        return Rand.Next(max + 1);
        }

	    /// <summary>
	    /// Returns a random boolean.
	    /// </summary>
	    /// <returns>
	    /// The <see cref="bool"/>.
	    /// </returns>
	    public static bool GetRandomBool()
		{
			return GetNext(1) == 1;
		}
    }
}
