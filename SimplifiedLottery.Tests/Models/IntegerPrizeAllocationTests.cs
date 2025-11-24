using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Tests.Models
{
	public class IntegerPrizeAllocationTests
	{
		[Fact]
		public void PrizeDefinitionCannotBeNull()
		{
			Assert.Throws<ArgumentNullException>(() => new IntegerPrizeAllocation(null!, 1, 1));
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public void PrizeWinnerCountMustBeValid(int prizeWinnerCount)
		{
			var prize = LotteryPrize.WinnersByCount("test", "a test prize tier", 50.0f, 1, 100);
			Assert.Throws<ArgumentOutOfRangeException>(() => new IntegerPrizeAllocation(prize, prizeWinnerCount, 100));
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public void PrizeAmountMustBeValid(int prizeAmount)
		{
			var prize = LotteryPrize.WinnersByCount("test", "a test prize tier", 50.0f, 1, 100);
			Assert.Throws<ArgumentOutOfRangeException>(() => new IntegerPrizeAllocation(prize, 1, prizeAmount));
		}
	}
}