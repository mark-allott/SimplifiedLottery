namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPrizeDefinition
	{
		/// <summary>
		/// The short name for the prize definition
		/// </summary>
		string Name { get; }

		/// <summary>
		/// A description for the prize
		/// </summary>
		string Description { get; }

		/// <summary>
		/// The percentage of the total prize fund allocated to the prize
		/// </summary>
		double PrizePercentage { get; }

		/// <summary>
		/// Optional: defines the total number of winners that can win this prize
		/// </summary>
		int? WinningPlayerCount { get; }

		/// <summary>
		/// Optional: defines the total number of winners as a percentage of tickets bought
		/// </summary>
		double? WinningTicketsPercentage { get; }
	}
}