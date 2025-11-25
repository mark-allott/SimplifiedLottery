namespace SimplifiedLottery.ConsoleUi.Configuration
{
	public record LotteryGameOptions
	{
		/// <summary>
		/// The minimum number of players per game
		/// </summary>
		public required int MinimumPlayers { get; init; }

		/// <summary>
		/// The maximum number of players per game
		/// </summary>
		public required int MaximumPlayers { get; init; }
	}
}