namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPrizeWinner<T>
		where T : struct
	{
		/// <summary>
		/// The winning player
		/// </summary>
		IPlayer<T> Player { get; }

		/// <summary>
		/// The number of winning tickets the player has
		/// </summary>
		int WinningTicketCount { get; }
	}
}