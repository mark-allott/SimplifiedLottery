using FluentAssertions;
using SimplifiedLottery.Core.Formatters;

namespace SimplifiedLottery.Tests.Formatters
{
	public class WalletFormatterTests
	{
		[Theory]
		[InlineData(0, "$0.00")]
		[InlineData(1, "$0.01")]
		[InlineData(10, "$0.10")]
		[InlineData(100, "$1.00")]
		[InlineData(1000, "$10.00")]
		[InlineData(10000, "$100.00")]
		public void WalletFormatterProvidesExpectedResultForDefault(int value, string expected)
		{
			var actual = value.Format();
			actual.Should().NotBeNullOrWhiteSpace();
			actual.Should().Be(expected);
		}

		[Theory]
		[InlineData(0, "£0.00")]
		[InlineData(1, "£0.01")]
		[InlineData(10, "£0.10")]
		[InlineData(100, "£1.00")]
		[InlineData(1000, "£10.00")]
		[InlineData(10000, "£100.00")]
		public void WalletFormatterProvidesExpectedResultForBritish(int value, string expected)
		{
			var actual = value.Format(WalletFormatter.BritishCulture);
			actual.Should().NotBeNullOrWhiteSpace();
			actual.Should().Be(expected);
		}
	}
}