// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArtificialSelectionCivilization.cs" company="Josh Charles">
//   This software is released under a license yet to be determined but of the open source variety
// </copyright>
// <summary>
//   Defines the ArtificialSelectionCivilization type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nenetics
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// The artificial selection civilization.
	/// </summary>
	public class ArtificialSelectionCivilization
    {
		/// <summary>
		/// The _fitness test.
		/// </summary>
		private readonly Func<Genotype, double> fitnessTest;

		/// <summary>
		/// Initializes a new instance of the <see cref="ArtificialSelectionCivilization"/> class.
		/// </summary>
		/// <param name="initialPopulation">
		/// The initial population.
		/// </param>
		/// <param name="fitnessTest">
		/// The fitness test.
		/// </param>
		/// <param name="minimumFitness">
		/// The minimum fitness.
		/// </param>
		/// <param name="numGenerations">
		/// The num generations.
		/// </param>
		/// <param name="promiscuityIndex">
		/// The promiscuity index.
		/// </param>
		/// <param name="chanceForMutation">
		/// The chance for mutation.
		/// </param>
		public ArtificialSelectionCivilization(
            Generation initialPopulation, 
            Func<Genotype, double> fitnessTest, 
            double minimumFitness, 
            int numGenerations, 
            int promiscuityIndex, 
            double chanceForMutation)
        {
            this.Generations = new List<Generation> { initialPopulation };
            this.fitnessTest = fitnessTest;
            this.MinimumFitness = minimumFitness;
            this.NumberOfGenerations = numGenerations;
            this.PromiscuityIndex = promiscuityIndex;
            this.ChanceForMutation = chanceForMutation;
            this.InitialPopulation = initialPopulation;
        }

		/// <summary>
		/// The initial population.
		/// </summary>
		public Generation InitialPopulation { get; private set; }

		/// <summary>
		/// Gets the generations.
		/// </summary>
		public List<Generation> Generations { get; private set; }

		/// <summary>
		/// Gets or sets the minimum fitness.
		/// </summary>
		public double MinimumFitness { get; set; }

		/// <summary>
		/// Gets the number of generations.
		/// </summary>
		public int NumberOfGenerations { get; private set; }

		/// <summary>
		/// Gets the promiscuity index.
		/// </summary>
		public int PromiscuityIndex { get; private set; }

		/// <summary>
		/// Gets the chance for mutation.
		/// </summary>
		public double ChanceForMutation { get; private set; }

		/// <summary>
		/// The process.
		/// </summary>
		public void Process()
        {
            var currentGeneration = this.InitialPopulation;
            for (int i = 0; i < this.NumberOfGenerations; i++)
            {
                List<Genotype> fitForBreeding = currentGeneration.Genotypes.OrderByDescending(g => this.fitnessTest(g)).Take(this.InitialPopulation.Genotypes.Count).ToList();
                var couples = new List<Couple>();
                for (int j = 0; j < fitForBreeding.Count; j++)
                {
                    var genotype = fitForBreeding[j];

                    for (int k = j + 1; k < fitForBreeding.Count; k++)
                    {
                        var other = fitForBreeding[k];
                        if (genotype.SimilarTo(other) >= this.MinimumFitness)
                        {
                            if (couples.Count(c => c.Mother == genotype || c.Father == genotype) < this.PromiscuityIndex)
                            {
                                couples.Add(new Couple { Mother = genotype, Father = other });
                            }
                        }
                    }
                }

                // we have the couples, now breed them
                var newCouples = couples.OrderByDescending(c => this.fitnessTest(c.Mother)).Take(this.InitialPopulation.Genotypes.Count).ToList();
                currentGeneration.CouplesToBreed = newCouples;
                var nextGeneration = currentGeneration.GetNextGeneration();
                this.Generations.Add(nextGeneration);
                Console.WriteLine("Generation {0} - Best match: {1}", i, this.fitnessTest(currentGeneration.GetBestMatch(this.fitnessTest)));
                currentGeneration = nextGeneration;
            }
        }
    }
}
