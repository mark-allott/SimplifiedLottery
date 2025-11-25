namespace SimplifiedLottery.Core.Interfaces
{
	public interface ITicketBuyingStrategyOptions<out T>
		where T : struct
	{
		/// <summary>
		/// The minimum number of tickets you are permitted to buy
		/// </summary>
		T MinimumTickets { get; }

		/// <summary>
		/// The maximum number of tickets you are permitted to buy
		/// </summary>
		T MaximumTickets { get; }
	}
}