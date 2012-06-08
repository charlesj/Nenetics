using System.Collections.Generic;

namespace Nenetics
{
    /// <summary>
    /// Contains a list of genes
    /// </summary>
    public class Genotype
    {
        public List<Gene> Genes { get; set; } 

        public Genotype()
        {
            Genes = new List<Gene>();
        }

        public double SimilarTo(Genotype toTest)
        {
            int count = 0;
            for(int i = 0; i<Genes.Count;i++)
            {
                if (toTest.Genes[i] != null && toTest.Genes[i].Value == Genes[i].Value)
                    count++;
            }
            return (double)count / Genes.Count;
        }
    }
}
