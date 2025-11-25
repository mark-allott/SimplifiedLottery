using FluentAssertions;
using SimplifiedLottery.Core.Services;
using SimplifiedLottery.Tests.Helpers;

namespace SimplifiedLottery.Tests.Services
{
	public class IntegerPrizeAllocationServiceTests
	{
		[Theory]
		[InlineData(100, 5000)]
		[InlineData(1000, 50000)]
		[InlineData(50, 2500)]
		[InlineData(10, 500)]
		public void CalculatePrizesTestForTier1(int ticketsSold, int expectedPrize)
		{
			var sut = new IntegerPrizeAllocationService();
			sut.Should().NotBeNull();

			var totalPrizeFund = ticketsSold * 100;
			var actualPrizes = sut.CalculatePrizes(new[] { PrizeHelper.Tier1 }, totalPrizeFund, ticketsSold)
				.ToList();
			actualPrizes.Should().NotBeNull();
			actualPrizes.Should().HaveCount(1);
			var actualPrize = actualPrizes[0].Value;
			actualPrize.PrizeWinnerCount.Should().Be(PrizeHelper.Tier1.WinningPlayerCount);
			actualPrize.PrizeAmount.Should().Be(expectedPrize);
		}

		[Theory]
		[InlineData(100, 300)]
		[InlineData(1000, 300)]
		[InlineData(50, 300)]
		[InlineData(10, 300)]
		public void CalculatePrizesTestForTier2(int ticketsSold, int expectedPrize)
		{
			var sut = new IntegerPrizeAllocationService();
			sut.Should().NotBeNull();

			var totalPrizeFund = ticketsSold * 100;
			var actualPrizes = sut.CalculatePrizes(new[] { PrizeHelper.Tier2 }, totalPrizeFund, ticketsSold)
				.ToList();
			actualPrizes.Should().NotBeNull();
			actualPrizes.Should().HaveCount(1);
			var actualPrize = actualPrizes[0].Value;
			actualPrize.PrizeWinnerCount.Should().Be(PrizeHelper.Tier2.GetWinnerCount(ticketsSold));
			actualPrize.PrizeAmount.Should().Be(expectedPrize);
		}

		[Theory]
		[InlineData(100, 20, 50)]
		[InlineData(1000, 200, 50)]
		[InlineData(1004, 200, 50)]
		[InlineData(50, 10, 50)]
		[InlineData(10, 2, 50)]
		[InlineData(11, 2, 55)]
		public void CalculatePrizesTestForTier3(int ticketsSold, int expectedWinners, int expectedPrize)
		{
			var sut = new IntegerPrizeAllocationService();
			sut.Should().NotBeNull();

			var totalPrizeFund = ticketsSold * 100;
			var actualPrizes = sut.CalculatePrizes(new[] { PrizeHelper.Tier3 }, totalPrizeFund, ticketsSold)
				.ToList();
			actualPrizes.Should().NotBeNull();
			actualPrizes.Should().HaveCount(1);
			var actualPrize = actualPrizes[0].Value;
			actualPrize.PrizeWinnerCount.Should().Be(expectedWinners);
			actualPrize.PrizeAmount.Should().Be(expectedPrize);
		}

		[Theory]
		[InlineData(100, new int[] { 1, 10, 20}, new int[]{ 5000, 300, 50 }, 1000)]
		[InlineData(101, new int[] { 1, 10, 20}, new int[]{ 5050, 303, 50 }, 1020)]
		[InlineData(102, new int[] { 1, 10, 20}, new int[]{ 5100, 306, 51 }, 1020)]
		[InlineData(103, new int[] { 1, 10, 20}, new int[]{ 5150, 309, 51 }, 1040)]
		[InlineData(250, new int[] { 1, 25, 50}, new int[]{ 12500, 300, 50 }, 2500)]
		[InlineData(251, new int[] { 1, 25, 50}, new int[]{ 12550, 301, 50 }, 2525)]
		public void CalculatePrizesForAllTiersYieldsExpected(int ticketsSold, int[] winnerCount, int[] tierPrizes, int expectedHouseProfit)
		{
			//	Check the tiers are setup correctly
			winnerCount.Should().NotBeNull();
			tierPrizes.Should().NotBeNull();
			winnerCount.Should().HaveCount(tierPrizes.Length);

			var sut = new IntegerPrizeAllocationService();
			sut.Should().NotBeNull();
			
			//	Determine the prizes
			var totalPrizeFund = ticketsSold * 100;
			var actualPrizes = sut.CalculatePrizes(PrizeHelper.AllPrizeTiers, totalPrizeFund, ticketsSold)
				.ToList();
			actualPrizes.Should().NotBeNull();
			actualPrizes.Should().HaveCount(winnerCount.Length);
			for (var i = 0; i < actualPrizes.Count; i++)
			{
				var tierPrize =  actualPrizes[i].Value;
				tierPrize.PrizeWinnerCount.Should().Be(winnerCount[i]);
				tierPrize.PrizeAmount.Should().Be(tierPrizes[i]);
				totalPrizeFund -= (tierPrize.PrizeAmount * tierPrize.PrizeWinnerCount);
			}
			totalPrizeFund.Should().Be(expectedHouseProfit);
		}
	}
}