using System;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPrizeCalculationStrategy<T>
		where T : struct
	{
		/// <summary>
		/// Divides <paramref name="prizeFund"/> by the <paramref name="numberOfWinners"/>, returning the share allocated to each
		/// </summary>
		/// <param name="prizeFund">The total amount to be shared across winners</param>
		/// <param name="numberOfWinners">The total number of winners</param>
		/// <returns>The amount to be allocated to each winner</returns>
		/// <remarks>
		/// Implementations shall determine what process is required to calculate the allocation of funds to the winners.
		/// The simplest example is using something like <see cref="Math.Round(decimal)"/>, which may round up and result
		/// in more prize allocated than was specified in <paramref name="prizeFund"/>
		/// </remarks>
		T Calculate(T prizeFund, int numberOfWinners);
	}
}