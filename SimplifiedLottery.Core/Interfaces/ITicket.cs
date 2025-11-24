using System;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface ITicket<out T>
		where T : struct
	{
		/// <summary>
		/// Internal identifier for the ticket
		/// </summary>
		Guid Id { get; }

		/// <summary>
		/// The cost of the ticket
		/// </summary>
		T Cost { get; }
	}
}