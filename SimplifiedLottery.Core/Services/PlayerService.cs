using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using SimplifiedLottery.Core.Configuration;
using SimplifiedLottery.Core.Interfaces;
using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Core.Services
{
	public class PlayerService
		: IPlayerService<int>
	{
		private readonly List<PlayerWithIntegerWallet> _players;
		private readonly PlayerWithIntegerWallet _human;

		/// <summary>
		/// Creates the players for the games, initialising each with <paramref name="initialBalance"/>
		/// </summary>
		/// <param name="totalPlayers">The total number of players in the game</param>
		/// <param name="initialBalance">The initial balance for the player wallet</param>
		public PlayerService(int totalPlayers, int initialBalance)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalPlayers);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(initialBalance);

			_players = [];
			//	Add the required number of players
			for (var i = 1; i <= totalPlayers; i++)
				_players.Add(new PlayerWithIntegerWallet($"{i}", initialBalance));
			_human = _players[0];
		}

		/// <summary>
		/// Alternate constructor for use with config options and DI
		/// </summary>
		/// <param name="options">The number of players and initial balance from configuration</param>
		public PlayerService(IOptions<IntegerPlayerServiceOptions> options)
			: this(options.Value.TotalPlayers, options.Value.InitialBalance)
		{
		}

		/// <inheritdoc/>
		public IEnumerable<IPlayer<int>> GetPlayers(int min, int max)
		{
			var availablePlayers = new List<PlayerWithIntegerWallet>(_players);
			availablePlayers.Remove(_human);

			//	Players playing always has the human player in the list
			var playing = new List<PlayerWithIntegerWallet>() { _human };

			//	pick a random number of players
			var requiredPlayers = Random.Shared.Next(min, max + 1);

			//	Loop until required number of players is present
			do
			{
				//	Pick a random player
				var playerIndex = Random.Shared.Next(0, availablePlayers.Count);
				//	Add to the playing list and remove from available so it is picked twice
				playing.Add(availablePlayers[playerIndex]);
				availablePlayers.RemoveAt(playerIndex);
			} while (playing.Count < requiredPlayers);

			return playing;
		}
	}
}