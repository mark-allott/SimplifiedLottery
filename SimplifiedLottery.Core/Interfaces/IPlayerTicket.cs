namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPlayerTicket<T>
		where T : struct
	{
		/// <summary>
		/// The player buying the ticket
		/// </summary>
		IPlayer<T> Player { get; }

		/// <summary>
		/// The ticket
		/// </summary>
		ITicket<T> Ticket { get; }
	}
}