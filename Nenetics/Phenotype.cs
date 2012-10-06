// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Phenotype.cs" company="Josh Charles">
//   This software is released under a license yet to be determined but of the open source variety
// </copyright>
// <summary>
//   The phenotype.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nenetics
{
	/// <summary>
	/// The phenotype.
	/// </summary>
	/// <typeparam name="T">
	/// The type this phenotype represents
	/// </typeparam>
	public abstract class Phenotype<T>
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Phenotype{T}"/> class.
		/// </summary>
		/// <param name="genotype">
		/// The genotype.
		/// </param>
		protected Phenotype(Genotype genotype)
        {
            Genotype = genotype;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="Phenotype{T}"/> class.
		/// </summary>
		/// <param name="phenotype">
		/// The phenotype.
		/// </param>
		protected Phenotype(T phenotype)
        {
            this.Genotype = this.CreateGenotype(phenotype);
        }

		/// <summary>
		/// Gets the genotype.
		/// </summary>
		public Genotype Genotype { get; private set; }

		/// <summary>
		/// The get.
		/// </summary>
		/// <returns>
		/// The <see cref="T"/>.
		/// </returns>
		public abstract T Get();

		/// <summary>
		/// The create genotype.
		/// </summary>
		/// <param name="phenotype">
		/// The phenotype.
		/// </param>
		/// <returns>
		/// The <see cref="Genotype"/>.
		/// </returns>
		protected abstract Genotype CreateGenotype(T phenotype);
    }
}
