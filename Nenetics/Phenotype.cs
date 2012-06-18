using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nenetics
{
    public abstract class Phenotype<T>
    {
        protected Phenotype(Genotype genotype)
        {
            Genotype = genotype;
        }
        protected Phenotype(T phenotype)
        {
            this.Genotype = CreateGenotype(phenotype);
        }
        public Genotype Genotype { get; private set; }
        public abstract T Get();
        protected abstract Genotype CreateGenotype(T phenotype);
    }
}
