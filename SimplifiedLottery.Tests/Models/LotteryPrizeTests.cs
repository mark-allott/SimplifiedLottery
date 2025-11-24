using FluentAssertions;
using SimplifiedLottery.Core.Models;
using SimplifiedLottery.Tests.Helpers;

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
			Assert.Throws(expectedType, () => LotteryPrize.WinnersByCount(prizeName!, "", 1, 1, 1));
			Assert.Throws(expectedType, () => LotteryPrize.WinnersByPercentage(prizeName!, "", 1, 1, 1));
		}

		[Theory]
		[InlineData(-1.0f)]
		[InlineData(0f)]
		[InlineData(100.01f)]
		public void LotteryPrizeMustHaveValidPrizePercentage(double prizePercentage)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
				LotteryPrize.WinnersByCount(nameof(LotteryPrize.WinnersByCount), "", prizePercentage, 1, 1));
			Assert.Throws<ArgumentOutOfRangeException>(() =>
				LotteryPrize.WinnersByPercentage(nameof(LotteryPrize.WinnersByPercentage), "", prizePercentage, 1, 1));
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public void LotteryPrizeWinnersByCountMustBeValid(int winningPlayerCount)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
				LotteryPrize.WinnersByCount(nameof(LotteryPrize.WinnersByCount), "", 1, winningPlayerCount, 1));
		}

		[Theory]
		[InlineData(-1.0f)]
		[InlineData(0f)]
		[InlineData(100.01f)]
		public void LotteryPrizeWinnersByPercentageMustBeValid(double winningPlayerPercentage)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
				LotteryPrize.WinnersByPercentage(nameof(LotteryPrize.WinnersByPercentage), "", 1,
					winningPlayerPercentage, 1));
		}

		[Theory]
		[InlineData(1, 100, 1)]
		[InlineData(1, 1000, 1)]
		[InlineData(2, 100, 10)]
		[InlineData(2, 106, 10)]
		[InlineData(3, 100, 20)]
		[InlineData(3, 101, 20)]
		[InlineData(3, 105, 21)]
		[InlineData(3, 1002, 200)]
		[InlineData(3, 1005, 201)]
		public void LotteryPrizeGetWinnerCountYieldsExpectedResult(int prizeTier, int ticketsSold, int expectedWinners)
		{
			var prizeDefinition = prizeTier switch
			{
				1 => PrizeHelper.Tier1,
				2 => PrizeHelper.Tier2,
				3 => PrizeHelper.Tier3,
				_ => throw new ArgumentOutOfRangeException(nameof(prizeTier))
			};

			var actualWinners = prizeDefinition.GetWinnerCount(ticketsSold);
			actualWinners.Should().Be(expectedWinners);
		}
	}
}