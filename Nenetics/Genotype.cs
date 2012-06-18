using System;
using System.Collections.Generic;
using System.Text;

namespace Nenetics
{
    /// <summary>
    /// Contains a list of genes
    /// </summary>
    public class Genotype
    {
        //public List<Gene> Genes { get; set; }
        public List<bool> Genes { get; set; }

        public Genotype()
        {
            Genes = new List<bool>();
        }

        public double SimilarTo(Genotype toTest)
        {
            int count = 0;
            for(int i = 0; i<Genes.Count;i++)
            {
                if (i < toTest.Genes.Count && toTest.Genes[i] == Genes[i]) count++;
            }
            return (double)count / Math.Max(Genes.Count,toTest.Genes.Count);
        }

        public static Genotype GetRandomGenotype(int size)
        {
            var genotype = new Genotype();
            for (int j = 0; j < size; j++)
            {
                genotype.Genes.Add(RandomBool());
            }
            return genotype;
        }

        public static bool RandomBool()
        {
            return RandomNumberSource.GetNext(1) == 1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var gene in Genes)
                sb.Append(gene ? "1" : "0");
            return sb.ToString();
        }
    }
}
