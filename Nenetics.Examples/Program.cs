using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Nenetics.Examples
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var currentFolder = "LastRun";
            Directory.CreateDirectory(currentFolder);
            var genotypeSize = 1600;
            var bitmap = new Bitmap("selectionTarget.png");
            var pic = new ImagePhenotype(bitmap);
            var target = pic.Genotype;
            var result = pic.Get();
            var chanceToMutate = .003;
            var populationSize = 100;
            var generationsToRun = 3;
            var minimumFitness = 0.4;
            var promiscuityIndex = 3;

            Func<Genotype, double> fitnessTest = (g) => g.SimilarTo(target);

            var population = Generation.CreateRandomPopulation(populationSize, genotypeSize, chanceToMutate);
            result.Save(Path.Combine(currentFolder, "target.png"), ImageFormat.Png);
            
            // do the run
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
