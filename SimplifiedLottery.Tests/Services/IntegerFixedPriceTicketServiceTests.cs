using FluentAssertions;
using SimplifiedLottery.Core.Models;
using SimplifiedLottery.Core.Services;

namespace SimplifiedLottery.Tests.Services
{
	public class IntegerFixedPriceTicketServiceTests
	{
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(int.MinValue)]
		public void TicketPriceCannotBeZeroOrNegative(int price)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new IntegerFixedPriceTicketService(price));
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(1, false)]
		[InlineData(10, false)]
		[InlineData(100, false)]
		[InlineData(1000, false)]
		[InlineData(1001)]
		public void MaximumTicketsBoughtRangeMustBeValid(int maxTickets, bool shouldFail = true)
		{
			if (shouldFail)
			{
				Assert.Throws<ArgumentOutOfRangeException>(() => new IntegerFixedPriceTicketService(100, maxTickets));
			}
			else
			{
				var sut = new IntegerFixedPriceTicketService(100, maxTickets);
				sut.Should().NotBeNull();
				sut.TicketCost.Should().Be(100);
				sut.Tickets.Should().HaveCount(0);
				sut.MaxTicketsPerTransaction.Should().Be(maxTickets);
			}
		}

		[Theory]
		[InlineData(10, 11)]
		[InlineData(10, 20)]
		public void TicketsBoughtCannotBeMoreThanMaximum(int maxTickets, int ticketsToBuy)
		{
			var sut = new IntegerFixedPriceTicketService(100, maxTickets);
			sut.Should().NotBeNull();
			sut.TicketCost.Should().Be(100);
			sut.MaxTicketsPerTransaction.Should().Be(maxTickets);

			Assert.Throws<ArgumentOutOfRangeException>(() => sut.BuyTickets(ticketsToBuy));
		}

		[Theory]
		[InlineData(10)]
		[InlineData(15)]
		[InlineData(20)]
		[InlineData(100)]
		public void TicketsBoughtIsCorrectQuantityAndUnique(int maxTickets)
		{
			var sut = new IntegerFixedPriceTicketService(100, maxTickets);
			sut.Should().NotBeNull();

			//	Determine a random number of tickets to buy between 1 and maxTickets
			var ticketsToBuy = Random.Shared.Next(1, maxTickets);
			
			//	Buy the tickets and make sure the quantity is correct
			var ticketsBought = new List<IntegerTicket>(sut.BuyTickets(ticketsToBuy));

			//	Check tickets bought is correct number
			ticketsBought.Should().NotBeNull();
			ticketsBought.Should().HaveCount(ticketsToBuy);

			//	Service should have the correct number of tickets as well
			sut.Tickets.Should().HaveCount(ticketsToBuy);

			//	The service should know about the tickets bought from it
			sut.Tickets.Should().Contain(ticketsBought);
		}
	}
}