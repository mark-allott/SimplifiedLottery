using FluentAssertions;
using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Tests.Models
{
	public class IntegerWalletTests
	{
		[Fact]
		public void IntegerWalletCannotHaveNegativeStartingBalance()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new IntegerWallet(-1));
		}

		[Fact]
		public void IntegerWalletCanHaveZeroStartingBalance()
		{
			var sut = new IntegerWallet(0);
			sut.Should().NotBeNull();
			sut.Balance.Should().Be(0);
		}

		[Fact]
		public void IntegerWalletAddFundsCannotBeNegative()
		{
			var sut = new IntegerWallet(0);
			sut.Should().NotBeNull();
			sut.Balance.Should().Be(0);
			Assert.Throws<ArgumentOutOfRangeException>(() => sut.AddFunds(-1));
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 10, 10)]
		[InlineData(5, 5, 10)]
		[InlineData(10, 100, 110)]
		public void IntegerWalletAddFundsYieldsExpectedAmount(int startingBalance, int fundsToAdd, int expectedAmount)
		{
			var sut = new IntegerWallet(startingBalance);
			sut.Should().NotBeNull();
			sut.AddFunds(fundsToAdd);
			sut.Balance.Should().Be(expectedAmount);
		}

		[Fact]
		public void IntegerWalletWithdrawCannotBeNegative()
		{
			var sut = new IntegerWallet(0);
			sut.Should().NotBeNull();
			sut.Balance.Should().Be(0);
			Assert.Throws<ArgumentOutOfRangeException>(() => sut.Withdraw(-1));
		}

		[Fact]
		public void IntegerWalletWithdrawCannotBeMoreThanBalance()
		{
			var sut = new IntegerWallet(0);
			sut.Should().NotBeNull();
			sut.Balance.Should().Be(0);
			Assert.Throws<ArgumentOutOfRangeException>(() => sut.Withdraw(1));
		}

		[Theory]
		[InlineData(10, 5, 5)]
		[InlineData(15, 15, 0)]
		[InlineData(50, 10, 40)]
		public void IntegerWalletWithdrawYieldsExpectedAmount(int startingBalance, int withdrawal, int expectedAmount)
		{
			var sut = new IntegerWallet(startingBalance);
			sut.Should().NotBeNull();
			sut.Withdraw(withdrawal);
			sut.Balance.Should().Be(expectedAmount);
		}
	}
}