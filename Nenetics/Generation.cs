using System.Collections.Generic;
using System.Linq;

namespace Nenetics
{
    public class Generation
    {
        public double ChanceOfMutation { get; private set; }

        public List<Genotype> Genotypes { get; private set; }

        public List<Couple> CouplesToBreed { get; set; }

        public Generation(double chanceOfMutation, List<Genotype> genotypes )
        {
            ChanceOfMutation = chanceOfMutation;
            Genotypes = genotypes;
        }

        /// <summary>
        /// Breeds each of the couples together to get their child
        /// </summary>
        /// <returns></returns>
        public Generation GetNextGeneration()
        {
            var genotypes = CouplesToBreed
                .Select(couple => new Breeder(ChanceOfMutation, couple))
                .Select(breeder => breeder.GetChild())
                .ToList();

            // TODO Add variability in ChanceOfMutation?
            return new Generation(ChanceOfMutation, genotypes);
        }
    }
}
