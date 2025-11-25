namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPlayerServiceOptions<out T>
		where T : struct
	{
		/// <summary>
		/// The total number of players for the service
		/// </summary>
		int TotalPlayers { get; }

		/// <summary>
		/// The initial balance allocated to players
		/// </summary>
		T InitialBalance { get; }
	}
}