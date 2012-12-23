// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Genotype.cs" company="Josh Charles">
//   This software is released under a license yet to be determined but of the open source variety
// </copyright>
// <summary>
//   Contains a list of genes
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nenetics
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
    /// Contains a list of genes
    /// </summary>
    public class Genotype
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Genotype"/> class.
		/// </summary>
		public Genotype()
        {
			this.Genes = new List<Gene>();
        }

		/// <summary>
		/// Gets or sets the genes.
		/// </summary>
		public List<Gene> Genes { get; set; }

		/// <summary>
		/// Constructs a random genotype of the requested size.  
		/// </summary>
		/// <param name="size">The length of the genome to construct</param>
		/// <returns>The constructed Genotype</returns>
		public static Genotype GetRandomGenotype(int size)
		{
			var genotype = new Genotype();
			for (var j = 0; j < size; j++)
			{
				genotype.Genes.Add(Gene.GetRandomGene());
			}

			return genotype;
		}

		/// <summary>
		/// Will compare two instances of genotypes with one another.  The comparison is gene by gene. 
		/// </summary>
		/// <remarks>
		/// One consequence of how this method is currently implemented is that if there is a random mutation that is a deletion, it will be 'off by one'.  In this case, a couple could have a radically different child.  
		/// This may or may not be a good thing, but it's certainly something to keep in mind.
		/// </remarks>
		/// <param name="comparison">The genotype to compare with.</param>
		/// <returns>The percentage of similarity.</returns>
        public double SimilarTo(Genotype comparison)
        {
			// get the number of genes that are exactly equal in value.
			int count = this.Genes.Where((target, iterator) => iterator < comparison.Genes.Count && comparison.Genes[iterator].Value == target.Value).Count();
			// return the number of genes that are equal divided by the total number.
	        return (double)count / Math.Max(this.Genes.Count, comparison.Genes.Count);
        }

		/// <summary>
		/// Returns a string representation of this genotype
		/// </summary>
		/// <returns>A string representation of this genotype</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var gene in this.Genes)
            {
	            sb.Append(gene);
            }

            return sb.ToString();
        }
    }
}
