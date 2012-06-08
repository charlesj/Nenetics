using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nenetics.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var genotypeSize = 4000;
            var target = Genotype.GetRandomGenotype(genotypeSize);

            var chanceToMutate = .003;
            var populationSize = 100;
            var generationsToRun = 100;
            Func<Genotype,double> fitnessTest = (g) => g.SimilarTo(target);
            var minimumFitness = 0.4;
            var promiscuityIndex = 3;

            
            var civ = new Civilization(Generation.CreateRandomPopulation(populationSize, genotypeSize, chanceToMutate),
                                       fitnessTest,
                                       minimumFitness,
                                       generationsToRun,
                                       promiscuityIndex,
                                       chanceToMutate);
            civ.Process();
            //foreach (var generation in civ.Generations)
            //{
            //    var bestMatch = generation.GetBestMatch(fitnessTest);
            //    Console.WriteLine("Target: {0}", target);
            //    Console.WriteLine("Best: {0}", bestMatch);
            //    Console.WriteLine("Similarity: {0}%", bestMatch.SimilarTo(target));
            //}
            Console.ReadLine();
        }
    }
}
