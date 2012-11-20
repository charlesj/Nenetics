// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Generation.cs" company="Josh Charles">
//   This software is released under a license yet to be determined but of the open source variety
// </copyright>
// <summary>
//   Defines the Generation type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nenetics
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Represent a generation for use in artificial selection, or other Civilizations that have well defined generations.
	/// </summary>
	public class Generation
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Generation"/> class.
		/// </summary>
		/// <param name="chanceOfMutation">
		/// The chance of mutation.
		/// </param>
		/// <param name="genotypes">
		/// The genotypes.
		/// </param>
		public Generation(double chanceOfMutation, List<Genotype> genotypes)
        {
            this.ChanceOfMutation = chanceOfMutation;
            this.Genotypes = genotypes;
        }

		/// <summary>
		/// Gets the the chance of mutation happening when breeding.
		/// </summary>
		public double ChanceOfMutation { get; private set; }

		/// <summary>
		/// Gets the genotypes that a part of this civilization.
		/// </summary>
		public List<Genotype> Genotypes { get; private set; }

		/// <summary>
		/// Gets or sets the couples to breed.  These are the couples that are generated using the fitness test.
		/// </summary>
		public List<Couple> CouplesToBreed { get; set; }

		/// <summary>
		/// The create random population.
		/// </summary>
		/// <param name="populationSize">
		/// The population size.
		/// </param>
		/// <param name="genotypeSize">
		/// The genotype size.
		/// </param>
		/// <param name="chanceOfMutation">
		/// The chance of mutation.
		/// </param>
		/// <returns>
		/// The <see cref="Generation"/>.
		/// </returns>
		public static Generation CreateRandomPopulation(int populationSize, int genotypeSize, double chanceOfMutation)
		{
			var genotypes = new List<Genotype>();

			for (int i = 0; i < populationSize; i++)
			{
				genotypes.Add(Genotype.GetRandomGenotype(genotypeSize));
			}

			return new Generation(chanceOfMutation, genotypes);
		}

        /// <summary>
        /// Breeds each of the couples together to get their child
        /// </summary>
        /// <returns>The next generation, given the couples in the list.</returns>
        public Generation GetNextGeneration()
        {
            var genotypes = this.CouplesToBreed
                .Select(couple => new Breeder(this.ChanceOfMutation, couple))
                .Select(breeder => breeder.GetChild())
                .ToList();

            return new Generation(this.ChanceOfMutation, genotypes);
        }

		/// <summary>
		/// The get best match.
		/// </summary>
		/// <param name="fitnessTest">
		/// The fitness test.
		/// </param>
		/// <returns>
		/// The <see cref="Genotype"/>.
		/// </returns>
		public Genotype GetBestMatch(Func<Genotype, double> fitnessTest)
        {
			return this.Genotypes.OrderByDescending(fitnessTest).FirstOrDefault();
        }
    }
}
