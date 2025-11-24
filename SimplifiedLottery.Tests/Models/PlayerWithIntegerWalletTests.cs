using FluentAssertions;
using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Tests.Models
{
	public class PlayerWithIntegerWalletTests
	{
		[Theory]
		[InlineData(null, typeof(ArgumentNullException))]
		[InlineData("", typeof(ArgumentException))]
		[InlineData(" ", typeof(ArgumentException))]
		public void PlayerShouldBeNamed(string? name, Type expectedException)
		{
			Assert.Throws(expectedException, () => new PlayerWithIntegerWallet(name!, 100));
		}

		[Theory]
		[InlineData(0, false)]
		[InlineData(100, true)]
		public void PlayerWalletShouldHaveFunds(int amount, bool hasFunds)
		{
			var sut = new PlayerWithIntegerWallet("player", amount);
			sut.Should().NotBeNull();
			sut.Id.Should().NotBe(Guid.Empty);
			sut.Name.Should().Be("player");
			sut.Wallet.Should().NotBeNull();
			sut.Wallet.Balance.Should().Be(amount);
			sut.HasFunds.Should().Be(hasFunds);
		}
	}
}