using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Models
{
	public record IntegerPlayerTicket
		: IPlayerTicket<int>
	{
		/// <inheritdoc/>
		public required IPlayer<int> Player { get; init; }

		/// <inheritdoc/>
		public required ITicket<int> Ticket { get; init; }
	}
}