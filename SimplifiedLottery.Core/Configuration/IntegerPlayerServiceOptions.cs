using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Configuration
{
	public record IntegerPlayerServiceOptions
		: IPlayerServiceOptions<int>
	{
		/// <inheritdoc/>
		public required int TotalPlayers { get; init; }

		/// <inheritdoc/>
		public int InitialBalance { get; init; }
	}
}