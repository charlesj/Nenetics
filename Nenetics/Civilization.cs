using System;
using System.Collections.Generic;
using System.Linq;

namespace Nenetics
{
    public class Civilization
    {
        public Generation InitialPopulation;
        public List<Generation> Generations { get; private set; }
        public double MinimumFitness { get; set; }
        public int NumberOfGenerations { get; private set; }
        private readonly Func<Genotype, double> _fitnessTest;
        public int PromiscuityIndex { get; private set; }
        public double ChanceForMutation { get; private set; }
        
        public Civilization(
            Generation initialPopulation, 
            Func<Genotype, double> fitnessTest, 
            double minimumFitness, 
            int numGenerations, 
            int promiscuityIndex, 
            double chanceForMutation)
        {
            Generations = new List<Generation> {initialPopulation};
            _fitnessTest = fitnessTest;
            MinimumFitness = minimumFitness;
            NumberOfGenerations = numGenerations;
            PromiscuityIndex = promiscuityIndex;
            ChanceForMutation = chanceForMutation;
        }

        public void Process()
        {
            var fitForBreeding = new List<Genotype>();
            var currentGeneration = InitialPopulation;
            for (int i = 0; i < NumberOfGenerations; i++)
            {
                foreach (var genotype in currentGeneration.Genotypes)
                {
                    var fitness = _fitnessTest(genotype);
                    if (fitness > MinimumFitness)
                    {
                        fitForBreeding.Add(genotype);
                    }
                }

                // We have the genotypes fit for breeding
                // now find good couples
                var couples = new List<Couple>();
                foreach (var genotype in fitForBreeding)
                {
                    Genotype genotype1 = genotype;
                    var everyOneElse = fitForBreeding.Where(g => g != genotype1);
                    foreach (var other in everyOneElse)
                    {
                        if (genotype.SimilarTo(other) > .9)
                        {
                            if (couples.Count(c => c.Mother == genotype || c.Father == genotype) < PromiscuityIndex)
                            {
                                couples.Add(new Couple {Mother = genotype, Father = other});
                            }
                        }
                    }
                }
                //we have the couples, now breed them
                currentGeneration.CouplesToBreed = couples;
                var nextGeneration = currentGeneration.GetNextGeneration();
                Generations.Add(nextGeneration);
                currentGeneration = nextGeneration;
            }
        }
    }

}
