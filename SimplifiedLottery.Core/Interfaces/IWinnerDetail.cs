using System.Collections.Generic;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface IWinnerDetail<T>
		where T : struct
	{
		/// <summary>
		/// Details of the winning prize allocation
		/// </summary>
		IPrizeAllocation<T> PrizeAllocation { get; }

		IEnumerable<IPrizeWinner<T>> Winners { get; }
	}
}