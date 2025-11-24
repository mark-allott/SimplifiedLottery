using FluentAssertions;
using SimplifiedLottery.Core.Strategies;

namespace SimplifiedLottery.Tests.Strategies
{
	public class IntegerPrizeCalculationStrategyTests
	{
		[Theory]
		[InlineData(100, 2, 50)]
		[InlineData(100, 9, 11)]
		[InlineData(1000, 9, 111)]
		[InlineData(1000, 3, 333)]
		[InlineData(1000, 333, 3)]
		public void PrizeStrategyRoundingYieldsExpectedResult(int prizeFund, int numberOfWinners, int expectedPrizeAmount)
		{
			var sut = new IntegerPrizeCalculationStrategy();
			sut.Should().NotBeNull();
			var actual = sut.Calculate(prizeFund, numberOfWinners);
			actual.Should().Be(expectedPrizeAmount);
		}
	}
}