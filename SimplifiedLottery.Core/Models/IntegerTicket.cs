using System;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Models
{
	public record IntegerTicket
		: ITicket<int>
	{
		#region ITicket<int> Members

		/// <inheritdoc/>
		public Guid Id { get; }

		/// <inheritdoc/>
		public int Cost { get; }

		#endregion

		/// <summary>
		/// Hide the constructor to help prevent the possibility of creating tickets with duplicate <see cref="Id"/>,
		/// leaving the creation of the internal ID to the constructor itself, rather than having it passed as a parameter
		/// </summary>
		/// <param name="cost">The cost of the ticket</param>
		/// <remarks>
		/// Obviously, tickets cannot cost less than zero "credits", but can be "free"
		/// </remarks>
		private IntegerTicket(int cost)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(cost);
			Cost = cost;
			Id = Guid.NewGuid();
		}

		/// <summary>
		/// Static constructor, allows caller to specify the cost of the ticket
		/// </summary>
		/// <param name="cost">The cost of the individual ticket</param>
		/// <returns>The newly minted ticket</returns>
		public static IntegerTicket Create(int cost)
		{
			return new IntegerTicket(cost);
		}
	}
}