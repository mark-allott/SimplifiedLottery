using System;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPlayer<T>
		where T : struct
	{
		/// <summary>
		/// Internal identifier for the player
		/// </summary>
		Guid Id { get; }

		/// <summary>
		/// Friendly name for the player
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Determines whether the player has funds available
		/// </summary>
		bool HasFunds { get; }

		/// <summary>
		/// Represents the wallet belonging to the player
		/// </summary>
		IWallet<T> Wallet { get; }
	}
}