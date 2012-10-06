// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Breeder.cs" company="Josh Charles">
//   This software is released under a license yet to be determined but of the open source variety
// </copyright>
// <summary>
//   Defines the Breeder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nenetics
{
	using System;

	/// <summary>
	/// The breeder.
	/// </summary>
	public class Breeder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Breeder"/> class. 
        /// Breeder takes two genotypes and breeds them, producing an offspring created from their genes.
        /// </summary>
        /// <param name="chanceOfMutation">
        /// The chance a mutation occurs.  Precise up to 3 decimal places
        /// </param>
        /// <param name="couple">
        /// The couple to breed 
        /// </param>
        public Breeder(double chanceOfMutation, Couple couple)
        {
            this.Couple = couple;
            this.ChanceOfMutation = chanceOfMutation;
        }

		/// <summary>
		/// Chance of mutation is a percentage with precision to three decimal places for prevelence of mutations
		/// </summary>
		public double ChanceOfMutation { get; private set; }

		/// <summary>
		/// The couple to breed
		/// </summary>
		public Couple Couple { get; set; }

        /// <summary>
        /// The mother and father produces a child.
        /// </summary>
        /// <returns>The child produced</returns>
        public Genotype GetChild()
        {
            // TODO Add checks for different genotype lengths. 
            // TODO  difference of lengths too great will produce could produce a null offspring
            // TODO There is a random chance of producing a null offspring no matter what
            // TODO There is a random chance of producing a really crazy off spring?
            // TODO Add possibility of multiple children?
            var genotype = new Genotype();

            for (int i = 0; i < Couple.Mother.Genes.Count; i++)
            {
                var mutate = this.ShouldMutate();

                // Handle different genotype lengths
                if (i < Couple.Father.Genes.Count && (Couple.Father.Genes[i] != Couple.Mother.Genes[i] || mutate))
                {
                    if (mutate)
                    {
                        this.Mutate(genotype);
                    }
                    else
                    {
                        // Randomly take mother or father
                        genotype.Genes.Add(RandomNumberSource.GetNext(1) == 1
                                               ? Couple.Mother.Genes[i] 
                                               : Couple.Father.Genes[i]);
                    }
                }
                else if (i < Couple.Father.Genes.Count)
                {
                    // mothers genes are longer
                    genotype.Genes.Add(Couple.Mother.Genes[i]);
                }
                else
                {
                    // they are equal
                    genotype.Genes.Add(Couple.Mother.Genes[i]);
                }
            }

            if (Couple.Father.Genes.Count > Couple.Mother.Genes.Count)
            {
                for (var i = Couple.Father.Genes.Count - Couple.Mother.Genes.Count; i < Couple.Father.Genes.Count; i++)
                {
                    if (this.ShouldMutate())
                    {
                        this.Mutate(genotype);
                    }
                    else
                    {
                        genotype.Genes.Add(Couple.Father.Genes[i]);
                    }
                }
            }

            return genotype;
        }

		/// <summary>
		/// The should mutate.
		/// </summary>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		private bool ShouldMutate()
        {
            var chance = Math.Round(this.ChanceOfMutation, 3);
            var intChance = Convert.ToInt32(chance * 1000);
            var value = RandomNumberSource.GetNext(100000);
            return value < intChance;
        }

		/// <summary>
		/// Performs a mutation on the given genotype
		/// </summary>
		/// <param name="genotype">
		/// The genotype.
		/// </param>
		private void Mutate(Genotype genotype)
        {
            // Delete current gene, add 1 random, or 2 random
            int next = RandomNumberSource.GetNext(2);

            if (next == 2 || next == 1)
            {
                this.AddRandomGene(genotype);
            }
            else if (next == 2)
            {
                this.AddRandomGene(genotype);
            }
        }

		/// <summary>
		/// The add random gene.
		/// </summary>
		/// <param name="genotype">
		/// The genotype.
		/// </param>
		private void AddRandomGene(Genotype genotype)
        {
            genotype.Genes.Add(Gene.GetRandomGene());
        }
    }
}
