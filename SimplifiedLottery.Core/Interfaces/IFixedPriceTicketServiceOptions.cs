namespace SimplifiedLottery.Core.Interfaces
{
	public interface IFixedPriceTicketServiceOptions<out T>
		where T : struct
	{
		/// <summary>
		/// Specifies the cost for the ticket
		/// </summary>
		T TicketCost { get; }

		/// <summary>
		/// Specifies the maximum number of tickets that can be bought in one transaction
		/// </summary>
		int MaxTicketsPerTransaction { get; }
	}
}