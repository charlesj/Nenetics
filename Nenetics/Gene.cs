// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Gene.cs" company="Josh Charles">
//   This software is released under a license yet to be determined but of the open source variety
// </copyright>
// <summary>
//   Contains a single value, true or false.  Can be treated as a 1 or 0
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nenetics
{
    /// <summary>
    /// Contains a single value, true or false.  Can be treated as a 1 or 0
    /// </summary>
    public class Gene 
    {
	    /// <summary>
	    /// Initializes a new instance of the <see cref="Gene"/> class.
	    /// </summary>
	    /// <param name="value">
	    /// The value.
	    /// </param>
	    public Gene(bool value)
        {
            this.Value = value;
        }

		/// <summary>
		/// Gets or sets the value of the Gene.   True can be treated as a 1.  False can be treated as a zero.
		/// </summary>
		public bool Value { get; set; }

	    /// <summary>
	    /// Gets a random gene. 
	    /// </summary>
	    /// <example>
	    /// genotype.Add(Gene.GetRandomGene());
	    /// </example>
	    /// <returns>
	    /// A random gene <see cref="Gene"/>.
	    /// </returns>
	    public static Gene GetRandomGene()
        {
            return RandomNumberSource.GetNext(1) == 1 ? new Gene(true) : new Gene(false);
        }

	    /// <summary>
	    /// Returns either a "0" or a "1" based on the value of the Gene
	    /// </summary>
	    /// <returns>
	    /// The <see cref="string"/>.
	    /// </returns>
	    public override string ToString()
        {
            return this.Value ? "1" : "0";
        }
    }
}
