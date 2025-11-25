using System.Collections.Generic;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Models
{
	public record IntegerWinnerDetail
		: IWinnerDetail<int>
	{
		/// <inheritdoc/>
		public required IPrizeAllocation<int> PrizeAllocation { get; init; }

		/// <inheritdoc/>
		public required IEnumerable<IPrizeWinner<int>> Winners { get; init; }
	}
}