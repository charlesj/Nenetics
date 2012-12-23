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
		/// Processes the entire civilization.
		/// </summary>
		/// <remarks>
		/// This could be a long running and memory intensive process.  Don't be surprised if it runs out of memory if you have given it very large values to generate.
		/// </remarks>
		public void Process()
        {
			// Prime the currentGeneration variable.
            var currentGeneration = this.InitialPopulation;
			
			// We need to process each generation for the total number of generations.
            for (int i = 0; i < this.NumberOfGenerations; i++)
            {
				// This line is pretty dense.  We are ordering all the genotypes by their fitness.  The most fit will be at the top.
				// Then we take the top number that matches the current population size.  This is important, because otherwise the population
				// size will grow exponentially.  Finally, ToList() is called so that we can call count() on it in a few lines.
                var fitForBreeding = currentGeneration.Genotypes.OrderByDescending(g => this.fitnessTest(g)).Take(this.InitialPopulation.Genotypes.Count).ToList();
                var couples = new List<Couple>();
				
				// This is where we breed.  We start with what we call the 'mothers' but that is just a useful label.
                for (var motherIterator = 0; motherIterator < fitForBreeding.Count; motherIterator++)
                {
                    var mother = fitForBreeding[motherIterator];

					// We need to match the mother with each possible father.
	                var eligibleFathers =
										// start by getting the rest of the array
						fitForBreeding	.Skip(motherIterator + 1)
										// order the eligible fathers by their similarity to the mother.
										.OrderByDescending(mother.SimilarTo)
										// Take the maximum number of fathers
										.Take(this.PromiscuityIndex);

					// Add the mother and her matches to the couples.
					couples.AddRange(eligibleFathers.Select( father => new Couple{Mother = mother, Father = father}));
                }

                // we have the couples, now breed them, limiting them to the same size population they started with.
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
