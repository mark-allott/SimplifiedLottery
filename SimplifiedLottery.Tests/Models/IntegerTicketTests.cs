using FluentAssertions;
using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Tests.Models
{
	public class IntegerTicketTests
	{
		[Fact]
		public void IntegerTicketCannotHaveNegativeCost()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => IntegerTicket.Create(-1));
		}

		[Theory]
		[InlineData(0)]
		[InlineData(10)]
		[InlineData(100)]
		public void IntegerTicketHasExpectedCost(int cost)
		{
			var ticket = IntegerTicket.Create(cost);
			ticket.Should().NotBeNull();
			ticket.Cost.Should().Be(cost);
		}
	}
}