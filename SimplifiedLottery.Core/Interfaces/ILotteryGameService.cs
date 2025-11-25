using System.Collections.Generic;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface ILotteryGameService<T>
		where T : struct
	{
		/// <summary>
		/// Plays one round of the lottery
		/// </summary>
		/// <param name="minPlayers">Determines the minimum number of players required</param>
		/// <param name="maxPlayers">Determines the maximum number of players possible</param>
		IEnumerable<IWinnerDetail<T>> PlayLottery(int minPlayers, int maxPlayers);

		/// <summary>
		/// Performs the required actions to pay the <paramref name="winners"/> their winnings
		/// </summary>
		/// <param name="winners">Details of the winners</param>
		void Payout(IEnumerable<IWinnerDetail<T>> winners);

		/// <summary>
		/// Details the total number of tickets bought
		/// </summary>
		T Revenue { get; }

		/// <summary>
		/// Details the profit for the house
		/// </summary>
		T Profit { get; }
	}
}