﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Nenetics.Examples
{
    class Program
    {
        static void Main()
        {
            const double    chanceToMutate      = .003;
            const int       populationSize      = 100;
            const int       generationsToRun    = 50;
            const double    minimumFitness      = 0.4;
            const int       promiscuityIndex    = 3;
            const int       genomeSize          = 1600;
            const string    currentFolder       = "LastRun";

            Console.WriteLine("Starting...");
            Directory.CreateDirectory(currentFolder);
            var bitmap = new Bitmap("selectionTarget.png");
            var pic = new ImagePhenotype(bitmap);
            var target = pic.Genotype;
            var result = pic.Get();

            Func<Genotype, double> fitnessTest = (g) => g.SimilarTo(target);

            var population = Generation.CreateRandomPopulation(populationSize, genomeSize, chanceToMutate);
            result.Save(Path.Combine(currentFolder, "target.png"), ImageFormat.Png);
            
            var civ = new ArtificialSelectionCivilization(population,
                           fitnessTest,
                           minimumFitness,
                           generationsToRun,
                           promiscuityIndex,
                           chanceToMutate);
            civ.Process();

            Console.WriteLine("writing total run...");
            for (int index = 0; index < civ.Generations.Count; index++)
            {
                var generation = civ.Generations[index];
                var bestMatch = generation.GetBestMatch(fitnessTest);
                var phenotype = new ImagePhenotype(bestMatch);
                var image = phenotype.Get();
                var fitness = fitnessTest(bestMatch);
                var filename = string.Format("{0}-{1}.png", index, fitness);
                image.Save(Path.Combine(currentFolder, filename), ImageFormat.Png);
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
