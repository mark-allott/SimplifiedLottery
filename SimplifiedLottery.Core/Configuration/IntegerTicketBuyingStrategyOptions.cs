using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Configuration
{
	public record IntegerTicketBuyingStrategyOptions
		: ITicketBuyingStrategyOptions<int>
	{
		/// <inheritdoc/>
		public required int MinimumTickets { get; init; }

		/// <inheritdoc/>
		public required int MaximumTickets { get; init; }
	}
}