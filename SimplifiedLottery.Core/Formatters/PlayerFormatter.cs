using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Formatters
{
	public static class PlayerFormatter<T>
		where T : struct
	{
		public static string FormatPlayer(IPlayer<T> player)
		{
			return $"Player {player.Name}";
		}
	}
}