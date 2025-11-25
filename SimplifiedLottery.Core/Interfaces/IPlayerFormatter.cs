namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPlayerFormatter<in T, in TT>
		where T : IPlayer<TT>
		where TT : struct
	{
		/// <summary>
		/// Formats the player for display
		/// </summary>
		/// <param name="player">The player to be formatted</param>
		/// <returns>The display string for the player</returns>
		string FormatPlayer(T player);
	}
}