﻿using System;
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
            InitialPopulation = initialPopulation;
        }

        public void Process()
        {
            var fitForBreeding = new List<Genotype>();
            var currentGeneration = InitialPopulation;
            for (int i = 0; i < NumberOfGenerations; i++)
            {
                var start = DateTime.Now;
                fitForBreeding = currentGeneration.Genotypes.OrderByDescending(g => _fitnessTest(g)).Take(InitialPopulation.Genotypes.Count).ToList();
                // We have the genotypes fit for breeding
                // now find good couples
                var couples = new List<Couple>();
                for (int j = 0; j < fitForBreeding.Count; j++)
                {
                    var genotype = fitForBreeding[j];
                    Genotype genotype1 = genotype;

                    for (int k = j + 1; k < fitForBreeding.Count;k++ )
                    {
                        var other = fitForBreeding[k];
                        if (genotype.SimilarTo(other) >= MinimumFitness)
                        {
                            if (couples.Count(c => c.Mother == genotype || c.Father == genotype) < PromiscuityIndex)
                            {
                                couples.Add(new Couple { Mother = genotype, Father = other });
                            }
                        }
                    }
                }
                //we have the couples, now breed them
                var newCouples = couples.OrderByDescending(c => _fitnessTest(c.Mother)).Take(InitialPopulation.Genotypes.Count).ToList();
                currentGeneration.CouplesToBreed = newCouples;
                var nextGeneration = currentGeneration.GetNextGeneration();
                Generations.Add(nextGeneration);
                var latestfitness = _fitnessTest(currentGeneration.GetBestMatch(_fitnessTest));
                var end = DateTime.Now;
                var diff = end - start;
                Console.WriteLine("Generation {0}: {1} best fitness", i, latestfitness);
                currentGeneration = nextGeneration;
            }
        }
    }

}