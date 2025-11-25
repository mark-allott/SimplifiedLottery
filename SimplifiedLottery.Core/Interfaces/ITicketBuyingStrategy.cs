namespace SimplifiedLottery.Core.Interfaces
{
	public interface ITicketBuyingStrategy<T>
		where T : struct
	{
		/// <summary>
		/// Determine how many tickets the player wishes to buy 
		/// </summary>
		/// <param name="player">The player making the purchase</param>
		/// <param name="ticketCost">The cost of each ticket</param>
		/// <returns>The number of tickets the player wishes to buy</returns>
		int GetTicketsToBuy(IPlayer<T> player, T ticketCost);
	}
}