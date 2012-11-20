// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Phenotype.cs" company="Josh Charles">
//   This software is released under a license yet to be determined but of the open source variety
// </copyright>
// <summary>
//   Defines the phenotype base type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nenetics
{
	/// <summary>
	/// The abstract phenotype. Used for converting a genotype into another type.
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
            this.Genotype = genotype;
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
		/// Gets the actual implementation object for this phenotype.
		/// </summary>
		/// <returns>
		/// The <see cref="T"/>.
		/// </returns>
		public abstract T Get();

		/// <summary>
		/// Creates a genotype from the given phenotype.  Must be implemented on the client.
		/// </summary>
		/// <param name="phenotype">
		/// The phenotype to get the genotype of.
		/// </param>
		/// <returns>
		/// The <see cref="Genotype"/>.
		/// </returns>
		protected abstract Genotype CreateGenotype(T phenotype);
    }
}
