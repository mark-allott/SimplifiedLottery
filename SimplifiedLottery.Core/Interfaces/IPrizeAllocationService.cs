using System.Collections.Generic;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPrizeAllocationService<T>
		where T : struct
	{
		/// <summary>
		/// Determines the prize allocation for each of the prize tiers 
		/// </summary>
		/// <param name="prizeDefinitions">The definitions for the applicable prize tiers</param>
		/// <param name="prizeFund">The total amount allocated to the prize fund</param>
		/// <param name="ticketCount">The total number of tickets sold</param>
		/// <returns>The prize allocated to each of the <paramref name="prizeDefinitions"/></returns>
		IEnumerable<IPrizeAllocation<T>> CalculatePrizes(IEnumerable<IPrizeDefinition> prizeDefinitions, T prizeFund, int ticketCount);
	}
}