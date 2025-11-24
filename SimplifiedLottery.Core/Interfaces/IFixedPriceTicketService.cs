using System.Collections.Generic;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface IFixedPriceTicketService<out T, out TT>
		where T : ITicket<TT>
		where TT : struct
	{
		/// <summary>
		/// Buy a single ticket from the service
		/// </summary>
		/// <returns>The ticket bought from the service</returns>
		T BuyTicket();

		/// <summary>
		/// Buys the specified number of tickets from the service
		/// </summary>
		/// <param name="count">The total number of tickets to buy</param>
		/// <returns>The tickets bought</returns>
		IEnumerable<T> BuyTickets(int count);

		/// <summary>
		/// Provides access to the tickets bought through the service
		/// </summary>
		IEnumerable<T> Tickets { get; }

		/// <summary>
		/// Provides the cost per ticket for the service
		/// </summary>
		TT TicketCost { get; }

		/// <summary>
		/// Provides the upper limit on the number of tickets that can be bought via <see cref="BuyTickets"/>
		/// </summary>
		int MaxTicketsPerTransaction { get; }
	}
}