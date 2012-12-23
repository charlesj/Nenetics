using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Nenetics.Examples
{
    class Program
    {
        static void Main()
        {
			// Setup our variables
            const double    ChanceToMutate      = .003;
            const int       PopulationSize      = 100;
            const int       GenerationsToRun    = 50;
			// Minimum fitness is the minimum value to be eligible for breeding.
            const double    MinimumFitness      = 0.4;
			// PromiscuityIndex is how many times a specific genotype is allowed to breed.
            const int       PromiscuityIndex    = 3;
			// this is determined by our target image size: 40 pixels by 40 pixels
            const int       GenomeSize          = 1600;
			// where we want to write our output to.
            const string    CurrentFolder       = "LastRun";

			// Used for getting our target phenotype and setting up a place to write our output 
			// data.
            Console.WriteLine("Starting...");
            Directory.CreateDirectory(CurrentFolder);
            var bitmap = new Bitmap("selectionTarget.png");
            var pic = new ImagePhenotype(bitmap);
            var target = pic.Genotype;
            var result = pic.Get();
			result.Save(Path.Combine(CurrentFolder, "target.png"), ImageFormat.Png);

			// The fitness test is the most important part. In this case, we are artificially 
			// selecting towards the target so we want genotypes that are similar to our target 
			// and only breed those.
            Func<Genotype, double> fitnessTest = gene => gene.SimilarTo(target);

			// Starting with a random population is good.  If we're lucky, some of the randomness
			// will have a high similarity towards our target.
            var population = Generation.CreateRandomPopulation(PopulationSize, GenomeSize, ChanceToMutate);
            
			// This is where the magic happens.  We create a civilization with an artificial target
			var civ = new ArtificialSelectionCivilization(population,
                           fitnessTest,
                           MinimumFitness,
                           GenerationsToRun,
                           PromiscuityIndex,
                           ChanceToMutate);
			
			// All the running actually happens in this method.  Everything else was just setup.
            civ.Process();

			// Now it's time to write out the results.
            Console.WriteLine("writing total run...");
            for (int index = 0; index < civ.Generations.Count; index++)
            {
                var generation = civ.Generations[index];
                var bestMatch = generation.GetBestMatch(fitnessTest);
                var phenotype = new ImagePhenotype(bestMatch);
                var image = phenotype.Get();
                var fitness = fitnessTest(bestMatch);
                var filename = string.Format("{0}-{1}.png", index, fitness);
                image.Save(Path.Combine(CurrentFolder, filename), ImageFormat.Png);
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
