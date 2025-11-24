using System;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPlayer
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
	}
}