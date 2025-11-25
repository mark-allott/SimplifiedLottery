using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Formatters
{
	public class IntegerPlayerFormatter
		: IPlayerFormatter<IPlayer<int>, int>
	{
		/// <inheritdoc/>
		public string FormatPlayer(IPlayer<int> player)
		{
			return PlayerFormatter<int>.FormatPlayer(player);
		}
	}
}