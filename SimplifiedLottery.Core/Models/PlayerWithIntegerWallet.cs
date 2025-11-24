using System;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Models
{
	public record PlayerWithIntegerWallet
		: IPlayer
	{
		/// <summary>
		/// Represents the wallet belonging to the player
		/// </summary>
		public IntegerWallet Wallet { get; }

		public PlayerWithIntegerWallet(string name, int initialFunds)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(name);
			ArgumentOutOfRangeException.ThrowIfNegative(initialFunds);
			Id = Guid.NewGuid();
			Name = name.Trim();
			Wallet = new IntegerWallet(initialFunds);
		}

		#region IPlayer implementation

		/// <inheritdoc/>
		public Guid Id { get; init; }

		/// <inheritdoc/>
		public string Name { get; init; }

		/// <inheritdoc/>
		public bool HasFunds
		{
			get
			{
				return Wallet.Balance > 0;
			}
		}

		#endregion
	}
}