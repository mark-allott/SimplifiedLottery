using System;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Models
{
	public record LotteryPrize
		: IPrizeDefinition
	{
		/// <inheritdoc/>
		public string Name { get; init; }

		/// <inheritdoc/>
		public string Description { get; init; }

		/// <inheritdoc/>
		public double PrizePercentage { get; init; }

		/// <inheritdoc/>
		public int? WinningPlayerCount { get; init; }

		/// <inheritdoc/>
		public double? WinningTicketsPercentage { get; init; }

		/// <inheritdoc/>
		public int AllocationOrder { get; }

		/// <summary>
		/// Hidden constructor as we need either <see cref="WinningPlayerCount"/> or <see cref="WinningTicketsPercentage"/>
		/// to be set, but not both (or neither!) 
		/// </summary>
		/// <param name="name">The name of the prize definition</param>
		/// <param name="description">A more wordy description for the prize</param>
		/// <param name="prizePercentage">The percentage of the total prize to allocate to this definition</param>
		/// <param name="winningPlayerCount">Optional: prize allocation is determined by the number of players stated</param>
		/// <param name="winningTicketsPercentage">Optional: prize allocation is determined by the percentage of total number of tickets sold</param>
		/// <param name="allocationOrder">The order in which prize definitions should be applied (higher should be applied first)</param>
		private LotteryPrize(string name, string description, double prizePercentage, int? winningPlayerCount,
			double? winningTicketsPercentage, int allocationOrder)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(name);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(prizePercentage);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(prizePercentage, 100.0f);

			Name = name.Trim();
			Description = description.Trim();
			PrizePercentage = prizePercentage;
			WinningPlayerCount = winningPlayerCount;
			WinningTicketsPercentage = winningTicketsPercentage;
			AllocationOrder = allocationOrder;
		}

		/// <summary>
		/// Static constructor creating a prize definition based on the number of players to share the prize allocation
		/// </summary>
		/// <param name="name">The name of the prize definition</param>
		/// <param name="description">A more wordy description for the prize</param>
		/// <param name="prizePercentage">The percentage of the total prize to allocate to this definition</param>
		/// <param name="winningPlayerCount">The number of players who will share the prize pot</param>
		/// <param name="allocationOrder">The order in which prize definitions should be applied (higher should be applied first)</param>
		/// <returns>The prize definition</returns>
		public static LotteryPrize WinnersByCount(string name, string description, double prizePercentage,
			int winningPlayerCount, int allocationOrder)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(winningPlayerCount);

			return new LotteryPrize(name, description, prizePercentage, winningPlayerCount, null, allocationOrder);
		}

		/// <summary>
		/// Static constructor creating a prize definition based on a percentage of the total number of tickets sold 
		/// </summary>
		/// <param name="name">The name of the prize definition</param>
		/// <param name="description">A more wordy description for the prize</param>
		/// <param name="prizePercentage">The percentage of the total prize to allocate to this definition</param>
		/// <param name="winningTicketsPercentage">The percentage of the total number of tickets that shall share the prize pot</param>
		/// <param name="allocationOrder">The order in which prize definitions should be applied (higher should be applied first)</param>
		/// <returns>The prize definition</returns>
		public static LotteryPrize WinnersByPercentage(string name, string description, double prizePercentage,
			double winningTicketsPercentage, int allocationOrder)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(winningTicketsPercentage);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(winningTicketsPercentage, 100.0f);

			return new LotteryPrize(name, description, prizePercentage, null, winningTicketsPercentage,
				allocationOrder);
		}
	}
}