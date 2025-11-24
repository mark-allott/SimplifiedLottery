namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPrizeAllocation<out T>
		where T : struct
	{
		/// <summary>
		/// The prize definition applied to the allocation
		/// </summary>
		IPrizeDefinition PrizeDefinition { get; }

		/// <summary>
		///	Details the total number of winners for the prize
		/// </summary>
		int PrizeWinnerCount { get; }

		/// <summary>
		/// Shows the amount won by each ticket
		/// </summary>
		T PrizeAmount { get; }
	}
}