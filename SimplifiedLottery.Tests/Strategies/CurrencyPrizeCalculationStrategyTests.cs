using FluentAssertions;
using SimplifiedLottery.Core.Strategies;

namespace SimplifiedLottery.Tests.Strategies
{
	public class CurrencyPrizeCalculationStrategyTests
	{
		[Theory]
		[InlineData(100.0f, 9, 11.11f)]
		[InlineData(100.0f, 3, 33.33f)]
		[InlineData(100.0f, 2, 50.0f)]
		[InlineData(1000.0f, 300, 3.33f)]
		[InlineData(30.0f, 10, 3.0f)]
		[InlineData(10.0f, 20, 0.5f)]
		public void PrizeStrategyRoundingYieldsExpectedResultForDefaults(double prizeFund, int numberOfWinners,
			double expectedPrizeAmount)
		{
			var sut = new CurrencyPrizeCalculationByRoundStrategy();
			sut.Should().NotBeNull();
			var actual = sut.Calculate(prizeFund, numberOfWinners);
			actual.Should().BeApproximately(expectedPrizeAmount, 0.0001f);
		}

		[Theory]
		[InlineData(100.0f, 9, 11.1f)]
		[InlineData(100.0f, 3, 33.3f)]
		[InlineData(100.0f, 2, 50.0f)]
		[InlineData(5000.0f, 300, 16.7f)]
		[InlineData(30.0f, 10, 3.0f)]
		[InlineData(10.0f, 20, 0.5f)]
		public void PrizeStrategyRoundingYieldsExpectedResultForToEvenOnePlace(double prizeFund, int numberOfWinners,
			double expectedPrizeAmount)
		{
			var sut = new CurrencyPrizeCalculationByRoundStrategy(1, MidpointRounding.ToEven);
			sut.Should().NotBeNull();
			var actual = sut.Calculate(prizeFund, numberOfWinners);
			actual.Should().BeApproximately(expectedPrizeAmount, 0.0001f);
		}

		[Theory]
		[InlineData(100.0f, 9, 11.11f)]
		[InlineData(100.0f, 3, 33.33f)]
		[InlineData(100.0f, 2, 50.0f)]
		[InlineData(5000.0f, 300, 16.67f)]
		[InlineData(30.0f, 10, 3.0f)]
		[InlineData(10.0f, 20, 0.5f)]
		public void PrizeStrategyRoundingYieldsExpectedResultForToEvenTwoPlaces(double prizeFund, int numberOfWinners,
			double expectedPrizeAmount)
		{
			var sut = new CurrencyPrizeCalculationByRoundStrategy(roundingMode: MidpointRounding.ToEven);
			sut.Should().NotBeNull();
			var actual = sut.Calculate(prizeFund, numberOfWinners);
			actual.Should().BeApproximately(expectedPrizeAmount, 0.0001f);
		}

		[Theory]
		[InlineData(100.0f, 9, 11.1f)]
		[InlineData(100.0f, 3, 33.3f)]
		[InlineData(100.0f, 2, 50.0f)]
		[InlineData(5000.0f, 300, 16.7f)]
		[InlineData(30.0f, 10, 3.0f)]
		[InlineData(10.0f, 20, 0.5f)]
		public void PrizeStrategyRoundingYieldsExpectedResultForAwayFromZeroOnePlace(double prizeFund, int numberOfWinners,
			double expectedPrizeAmount)
		{
			var sut = new CurrencyPrizeCalculationByRoundStrategy(1);
			sut.Should().NotBeNull();
			var actual = sut.Calculate(prizeFund, numberOfWinners);
			actual.Should().BeApproximately(expectedPrizeAmount, 0.0001f);
		}

		[Theory]
		[InlineData(100.0f, 9, 11.11f)]
		[InlineData(100.0f, 3, 33.33f)]
		[InlineData(100.0f, 2, 50.0f)]
		[InlineData(5000.0f, 300, 16.67f)]
		[InlineData(30.0f, 10, 3.0f)]
		[InlineData(10.0f, 20, 0.5f)]
		public void PrizeStrategyRoundingYieldsExpectedResultForAwayFromZeroTwoPlaces(double prizeFund, int numberOfWinners,
			double expectedPrizeAmount)
		{
			var sut = new CurrencyPrizeCalculationByRoundStrategy();
			sut.Should().NotBeNull();
			var actual = sut.Calculate(prizeFund, numberOfWinners);
			actual.Should().BeApproximately(expectedPrizeAmount, 0.0001f);
		}

		[Theory]
		[InlineData(100.0f, 9, 11.11f)]
		[InlineData(100.0f, 3, 33.33f)]
		[InlineData(100.0f, 2, 50.0f)]
		[InlineData(1000.0f, 300, 3.33f)]
		[InlineData(30.0f, 10, 3.0f)]
		[InlineData(10.0f, 20, 0.5f)]
		public void PrizeStrategyFloorYieldsExpectedResultForDefaults(double prizeFund, int numberOfWinners,
			double expectedPrizeAmount)
		{
			var sut = new CurrencyPrizeCalculationByFloorStrategy();
			sut.Should().NotBeNull();
			var actual = sut.Calculate(prizeFund, numberOfWinners);
			actual.Should().BeApproximately(expectedPrizeAmount, 0.0001f);
		}

		[Theory]
		[InlineData(100.0f, 9, 11.1f)]
		[InlineData(100.0f, 3, 33.3f)]
		[InlineData(100.0f, 2, 50.0f)]
		[InlineData(1000.0f, 300, 3.3f)]
		[InlineData(30.0f, 10, 3.0f)]
		[InlineData(10.0f, 20, 0.5f)]
		public void PrizeStrategyFloorYieldsExpectedResultForOnePlace(double prizeFund, int numberOfWinners,
			double expectedPrizeAmount)
		{
			var sut = new CurrencyPrizeCalculationByFloorStrategy(1);
			sut.Should().NotBeNull();
			var actual = sut.Calculate(prizeFund, numberOfWinners);
			actual.Should().BeApproximately(expectedPrizeAmount, 0.0001f);
		}
	}
}