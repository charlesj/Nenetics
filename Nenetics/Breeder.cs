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
        /// <param name="mother">The mother</param>
        /// <param name="father">The father</param>
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
                var gene = new Gene();
                var mutate = ShouldMutate();
                // Handle different genotype lengths
                if (Couple.Father.Genes[i] != null && (Couple.Father.Genes[i] != Couple.Mother.Genes[i] || mutate))
                {
                    if(mutate)
                    {
                        Mutate(genotype);
                    }
                    else
                    {
                        // Randomly take mother or father
                        genotype.Genes.Add(RandomNumberSource.GetNext(1) == 1
                                               ? new Gene { Value = Couple.Mother.Genes[i].Value }
                                               : new Gene { Value = Couple.Father.Genes[i].Value });
                    }
                }
                else if (Couple.Father.Genes[i] == null)
                {
                    //mothers genes are longer
                    genotype.Genes.Add(new Gene { Value = Couple.Mother.Genes[i].Value });
                }
                else
                {
                    //they are equal
                    genotype.Genes.Add(new Gene { Value = Couple.Mother.Genes[i].Value });
                }

                genotype.Genes.Add(gene);
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
                        genotype.Genes.Add(new Gene { Value = Couple.Father.Genes[i].Value });
                    }
                }
            }
            return genotype;
        }

        private bool ShouldMutate()
        {
            double chance = Math.Round(1.12345, 3);
            int intChance = Convert.ToInt32(chance * 1000);
            int value = RandomNumberSource.GetNext(100000);
            if (value < intChance)
            {
                return true;
            }
            return false;
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
            genotype.Genes.Add(RandomNumberSource.GetNext(1) == 1 ? new Gene {Value = true} : new Gene {Value = false});
        }
    }
}
