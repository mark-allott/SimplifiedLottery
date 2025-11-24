using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Tests.Models
{
	public class LotteryPrizeTests
	{
		[Theory]
		[InlineData(null, typeof(ArgumentNullException))]
		[InlineData("", typeof(ArgumentException))]
		[InlineData(" ", typeof(ArgumentException))]
		public void LotteryPrizeMustHaveName(string? prizeName, Type expectedType)
		{
			Assert.Throws(expectedType, () => LotteryPrize.WinnersByCount(prizeName!, "", 1, 1));
			Assert.Throws(expectedType, () => LotteryPrize.WinnersByPercentage(prizeName!, "", 1, 1));
		}

		[Theory]
		[InlineData(-1.0f)]
		[InlineData(0f)]
		[InlineData(100.01f)]
		public void LotteryPrizeMustHaveValidPrizePercentage(double prizePercentage)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
				LotteryPrize.WinnersByCount(nameof(LotteryPrize.WinnersByCount), "", prizePercentage, 1));
			Assert.Throws<ArgumentOutOfRangeException>(() =>
				LotteryPrize.WinnersByPercentage(nameof(LotteryPrize.WinnersByPercentage), "", prizePercentage, 1));
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public void LotteryPrizeWinnersByCountMustBeValid(int winningPlayerCount)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
				LotteryPrize.WinnersByCount(nameof(LotteryPrize.WinnersByCount), "", 1, winningPlayerCount));
		}

		[Theory]
		[InlineData(-1.0f)]
		[InlineData(0f)]
		[InlineData(100.01f)]
		public void LotteryPrizeWinnersByPercentageMustBeValid(double winningPlayerPercentage)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
				LotteryPrize.WinnersByPercentage(nameof(LotteryPrize.WinnersByPercentage), "", 1,
					winningPlayerPercentage));
		}
	}
}