using System;

namespace Nenetics
{
    public class Breeder
    {
        /// <summary>
        /// Chance of mutation is a percentage with precision to three decimal places for prevelance of mutations
        /// </summary>
        public double ChanceOfMutation { get; private set; }
        
        /// <summary>
        /// The couple to breed
        /// </summary>
        public Couple Couple { get; set; }

        /// <summary>
        /// Breeder takes two genotypes and breeds them, producing an offspring created from their genes.
        /// </summary>
        /// <param name="chanceOfMutation">The chance a mutation occurs.  Precise up to 3 decimal places</param>
        /// <param name="couple">The couple to breed </param>
        public Breeder(double chanceOfMutation, Couple couple)
        {
            Couple = couple;
            ChanceOfMutation = chanceOfMutation;
        }

        /// <summary>
        /// The mother and father produces a child.
        /// </summary>
        /// <returns>The child produced</returns>
        public Genotype GetChild()
        {
            // TODO Add checks for different genotype lengths. 
            // TODO  difference of lengths too great will produce could produce a null offspring
            // TODO There is a random chance of producing a null offspring no matter what
            // TODO There is a random chance of producing a really crazy off spring?
            // TODO Add possibility of multiple children?
            var genotype = new Genotype();

            for (int i = 0; i < Couple.Mother.Genes.Count; i++ )
            {
                var mutate = ShouldMutate();
                // Handle different genotype lengths
                if (i < Couple.Father.Genes.Count && (Couple.Father.Genes[i] != Couple.Mother.Genes[i] || mutate))
                {
                    if(mutate)
                    {
                        Mutate(genotype);
                    }
                    else
                    {
                        // Randomly take mother or father
                        genotype.Genes.Add(RandomNumberSource.GetNext(1) == 1
                                               ? Couple.Mother.Genes[i] 
                                               : Couple.Father.Genes[i] );
                    }
                }
                else if (i < Couple.Father.Genes.Count)
                {
                    //mothers genes are longer
                    genotype.Genes.Add(Couple.Mother.Genes[i] );
                }
                else
                {
                    //they are equal
                    genotype.Genes.Add(Couple.Mother.Genes[i]);
                }
            }

            if (Couple.Father.Genes.Count > Couple.Mother.Genes.Count)
            {
                for (int i = Couple.Father.Genes.Count - Couple.Mother.Genes.Count; i < Couple.Father.Genes.Count; i++)
                {
                    if( ShouldMutate() )
                    {
                        Mutate(genotype);
                    }
                    else
                    {
                        genotype.Genes.Add(Couple.Father.Genes[i]);
                    }
                }
            }
            return genotype;
        }

        private bool ShouldMutate()
        {
            var chance = Math.Round(ChanceOfMutation, 3);
            var intChance = Convert.ToInt32(chance * 1000);
            var value = RandomNumberSource.GetNext(100000);
            return value < intChance;
        }

        private void Mutate(Genotype genotype)
        {
            // Delete current gene, add 1 random, or 2 random
            int next = RandomNumberSource.GetNext(2);

            if (next == 2 || next == 1)
            {
                AddRandomGene(genotype);
            }
            else if (next == 2)
            {
                AddRandomGene(genotype);
            }
        }

        private void AddRandomGene(Genotype genotype)
        {
            genotype.Genes.Add(Genotype.RandomBool());
        }
    }
}
