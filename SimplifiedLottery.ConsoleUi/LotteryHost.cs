using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimplifiedLottery.ConsoleUi.Configuration;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.ConsoleUi
{
	public class LotteryHost
		: IHostedService
	{
		private readonly IPlayerFormatter<IPlayer<int>, int> _playerFormatter;
		private readonly IWalletFormatter<int> _walletFormatter;
		private readonly ILogger<LotteryHost> _logger;
		private IServiceProvider ServiceProvider { get; }
		private IConfiguration Configuration { get; }

		private LotteryGameOptions Options { get; }

		#region ctor

		public LotteryHost(IServiceProvider serviceProvider, IConfiguration configuration,
			IPlayerFormatter<IPlayer<int>, int> playerFormatter, IWalletFormatter<int> walletFormatter,
			ILogger<LotteryHost> logger)
		{
			ServiceProvider = serviceProvider;
			Configuration = configuration;
			_playerFormatter = playerFormatter;
			_walletFormatter = walletFormatter;
			_logger = logger;

			//	Read config and apply settings
			var section = Configuration.GetSection(nameof(LotteryGameOptions));
			var minPlayers = section.GetValue<int>(nameof(LotteryGameOptions.MinimumPlayers));
			var maxPlayers = section.GetValue<int>(nameof(LotteryGameOptions.MaximumPlayers));
			Options = new LotteryGameOptions() { MinimumPlayers = minPlayers, MaximumPlayers = maxPlayers };
		}

		#endregion

		#region IHostedService Implementation

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var lotteryGame = ServiceProvider.GetRequiredService<ILotteryGameService<int>>();
			var winners = lotteryGame.PlayLottery(Options.MinimumPlayers, Options.MaximumPlayers)
				.ToList();
			ShowWinners(winners);
			lotteryGame.Payout(winners);

			return Task.CompletedTask;
		}

		private void ShowWinners(List<IWinnerDetail<int>> winnerDetails)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Ticket Draw results:")
				.AppendLine("");
			foreach (var winningTier in winnerDetails)
			{
				var winners = winningTier.Winners.ToList();
				sb.Append($"{winningTier.PrizeAllocation.PrizeDefinition.Name}: Player")
					.Append(winners.Count != 1 ? "s" : "")
					.AppendJoin(", ", winners.Select(s => $"{_playerFormatter.FormatPlayer(s.Player)}({s.WinningTicketCount})"))
					.Append(" win")
					.Append(winners.Count != 1 ? "" : "s")
					.Append($" {_walletFormatter.Format(winningTier.PrizeAllocation.PrizeAmount)}")
					.Append(winners.Count != 1 ? " per winning ticket" : "")
					.AppendLine("!");
			}

			sb.AppendLine("")
				.AppendLine("Congratulations to the winners!");

			_logger.LogInformation("{message}", sb.ToString());
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		#endregion
	}
}