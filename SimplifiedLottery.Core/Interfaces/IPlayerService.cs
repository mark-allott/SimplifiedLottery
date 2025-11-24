using System.Collections.Generic;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPlayerService<T>
		where T : struct
	{
		/// <summary>
		/// Returns the players who are ready to play the game
		/// </summary>
		/// <param name="min">The minimum number of players needed</param>
		/// <param name="max">The maximum number of players needed</param>
		/// <returns>A list of players between <paramref name="min"/> and <paramref name="max"/>, inclusive</returns>
		IEnumerable<IPlayer<T>> GetPlayers(int min, int max);
	}
}