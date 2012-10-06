// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Couple.cs" company="Josh Charles">
//   This software is released under a license yet to be determined but of the open source variety
// </copyright>
// <summary>
//   Defines the Couple type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nenetics
{
	/// <summary>
	/// The couple.
	/// </summary>
	public class Couple
    {
		/// <summary>
		/// Gets or sets the mother.
		/// </summary>
		public Genotype Mother { get; set; }

		/// <summary>
		/// Gets or sets the father.
		/// </summary>
		public Genotype Father { get; set; }
    }
}
