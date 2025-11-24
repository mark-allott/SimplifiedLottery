using SimplifiedLottery.Core.Interfaces;
using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Tests.Helpers
{
	public static class PrizeHelper
	{
		public static readonly IPrizeDefinition Tier1 =
			LotteryPrize.WinnersByCount("Grand Prize", "The big one", 50.0f, 1, 1000);

		public static readonly IPrizeDefinition Tier2 =
			LotteryPrize.WinnersByPercentage("Second Tier", "Middling prizes", 30.0f, 10.0f, 100);

		public static readonly IPrizeDefinition Tier3 =
			LotteryPrize.WinnersByPercentage("Third Tier", "Low-value prizes", 10.0f, 20.0f, 100);
	}
}