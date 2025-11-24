using System;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Models
{
	public record IntegerWallet
		: IWallet<int>
	{
		/// <summary>
		/// Constructor ensures balance is positive
		/// </summary>
		/// <param name="initialBalance">The starting amount for the wallet</param>
		public IntegerWallet(int initialBalance)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(initialBalance, 0);
			Balance = initialBalance;
		}

		/// <inheritdoc/>
		public int Balance { get; private set; }

		/// <inheritdoc/>
		public void AddFunds(int amount)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);
			Balance += amount;
		}

		/// <inheritdoc/>
		public void Withdraw(int amount)
		{
			//	amount to withdraw must be a positive amount and cannot be more than current balance
			ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(amount, Balance);
			Balance -= amount;
		}
	}
}