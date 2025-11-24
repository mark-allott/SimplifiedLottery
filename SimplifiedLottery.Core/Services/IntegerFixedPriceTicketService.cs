using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using SimplifiedLottery.Core.Interfaces;
using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Core.Services
{
	/// <summary>
	/// Integer-based ticket service which requires all tickets to be priced exactly the same and cannot be zero-cost
	/// </summary>
	/// <remarks>
	/// Class could be extended to support "free" tickets by use of another interface, declaring a "BuyFreeTicket"
	/// method, or similar
	/// </remarks>
	public class IntegerFixedPriceTicketService
		: IFixedPriceTicketService<IntegerTicket, int>
	{
		private readonly List<IntegerTicket> _tickets;

		/// <summary>
		/// Constructor determines the ticket cost and maximum number of tickets that can be bought via <see cref="BuyTickets"/>
		/// </summary>
		/// <param name="ticketCost">The cost of each ticket</param>
		/// <param name="maxTicketsPerTransaction">The maximum number of tickets that can be bought in one transaction</param>
		public IntegerFixedPriceTicketService(int ticketCost, int maxTicketsPerTransaction = 100)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ticketCost);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTicketsPerTransaction);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(maxTicketsPerTransaction, 1000);
			TicketCost = ticketCost;
			MaxTicketsPerTransaction = maxTicketsPerTransaction;
			_tickets = [];
		}

		#region ITicketService implementation

		/// <inheritdoc/>
		public IntegerTicket BuyTicket()
		{
			var ticket = IntegerTicket.Create(TicketCost);
			_tickets.Add(ticket);
			return ticket;
		}

		/// <inheritdoc/>
		/// <remarks>
		/// <para>Must have at least 1 ticket to buy and no negatives!!</para>
		/// <para>In practice, an upper limit is placed on the total number of tickets bought in one go</para>
		/// </remarks>
		public IEnumerable<IntegerTicket> BuyTickets(int count)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(count, MaxTicketsPerTransaction);
			var tickets = new List<IntegerTicket>();
			for (var i = 0; i < count; i++)
				tickets.Add(IntegerTicket.Create(TicketCost));
			_tickets.AddRange(tickets);
			return tickets;
		}

		/// <inheritdoc/>
		public IEnumerable<IntegerTicket> Tickets
		{
			get
			{
				return _tickets.ToImmutableList();
			}
		}

		/// <inheritdoc/>
		public int TicketCost { get; }

		/// <inheritdoc/>
		public int MaxTicketsPerTransaction { get; }

		#endregion
	}
}