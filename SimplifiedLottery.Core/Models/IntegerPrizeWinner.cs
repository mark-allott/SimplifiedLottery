using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Models
{
	public record IntegerPrizeWinner
		: IPrizeWinner<int>
	{
		/// <inheritdoc/>
		public required IPlayer<int> Player { get; init; }

		/// <inheritdoc/>
		public required int WinningTicketCount { get; init; }
	}
}