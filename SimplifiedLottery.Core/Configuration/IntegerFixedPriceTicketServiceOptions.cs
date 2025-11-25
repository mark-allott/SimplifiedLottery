using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Configuration
{
	public record IntegerFixedPriceTicketServiceOptions
		: IFixedPriceTicketServiceOptions<int>
	{
		public required int TicketCost { get; init;  }
		public required int MaxTicketsPerTransaction { get; init; }
	}
}