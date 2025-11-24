using System;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Models
{
	public record IntegerPrizeAllocation
		: IPrizeAllocation<int>
	{
		public IntegerPrizeAllocation(IPrizeDefinition prizeDefinition, int prizeWinnerCount, int prizeAmount)
		{
			ArgumentNullException.ThrowIfNull(prizeDefinition);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(prizeWinnerCount);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(prizeAmount);
			PrizeDefinition = prizeDefinition;
			PrizeWinnerCount = prizeWinnerCount;
			PrizeAmount = prizeAmount;
		}

		#region IPrizeAllocation Members

		/// <inheritdoc/>
		public IPrizeDefinition PrizeDefinition { get; init; }

		/// <inheritdoc/>
		public int PrizeWinnerCount { get; init; }

		/// <inheritdoc/>
		public int PrizeAmount { get; init; }

		#endregion
	}
}