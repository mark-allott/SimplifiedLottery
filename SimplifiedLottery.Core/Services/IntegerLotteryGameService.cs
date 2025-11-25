using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using SimplifiedLottery.Core.Interfaces;
using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Core.Services
{
	public class IntegerLotteryGameService
		: ILotteryGameService<int>
	{
		private ILogger Logger { get; }
		private IPlayerService<int> PlayerService { get; }
		private IFixedPriceTicketService<ITicket<int>, int> TicketService { get; }
		private ITicketBuyingStrategy<int> BuyingStrategy { get; }
		private IPrizeDefinitionService PrizeDefinitionService { get; }
		private IPrizeAllocationService<int> PrizeAllocationService { get; }
		private IPlayerFormatter<IPlayer<int>, int> PlayerFormatter { get; }
		private IWalletFormatter<int> WalletFormatter { get; }

		public IntegerLotteryGameService(ILogger logger, IPlayerService<int> playerService,
			IFixedPriceTicketService<ITicket<int>, int> ticketService, ITicketBuyingStrategy<int> buyingStrategy,
			IPrizeDefinitionService prizeDefinitionService, IPrizeAllocationService<int> prizeAllocationService,
			IPlayerFormatter<IPlayer<int>, int> playerFormatter, IWalletFormatter<int> walletFormatter)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PlayerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
			TicketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
			BuyingStrategy = buyingStrategy ?? throw new ArgumentNullException(nameof(buyingStrategy));
			PrizeDefinitionService =
				prizeDefinitionService ?? throw new ArgumentNullException(nameof(prizeDefinitionService));
			PrizeAllocationService =
				prizeAllocationService ?? throw new ArgumentNullException(nameof(prizeAllocationService));
			PlayerFormatter = playerFormatter ?? throw new ArgumentNullException(nameof(playerFormatter));
			WalletFormatter = walletFormatter ?? throw new ArgumentNullException(nameof(walletFormatter));
		}

		#region ILotteryGameService<int> Members

		/// <inheritdoc/>
		public IEnumerable<IWinnerDetail<int>> PlayLottery(int minPlayers, int maxPlayers)
		{
			Logger.LogInformation("Starting lottery game for between {MinPlayers} and {MaxPlayers}", minPlayers,
				maxPlayers);
			var players = PlayerService.GetPlayers(minPlayers, maxPlayers).ToList();
			var human = players[0];
			ShowWelcomeMessage(human);

			//	Get tickets for all players taking part
			var playerTickets = GetPlayerTickets(players);

			//	Determine total prize fund and add to revenue
			var prizeFund = playerTickets.Sum(t => t.Ticket.Cost);
			Revenue += prizeFund;

			//	Determine prize allocations
			var prizes = PrizeAllocationService
				.CalculatePrizes(PrizeDefinitionService.PrizeDefinitions, prizeFund, playerTickets.Count);

			//	Determine winning tickets for each of the prize tiers
			var winningTickets = GetWinningTickets(PrizeDefinitionService.PrizeDefinitions, playerTickets)
				.OrderByDescending(o => o.Key.AllocationPriority)
				.ToDictionary();

			var winnerDetails = winningTickets
				.Select(s => new IntegerWinnerDetail()
				{
					PrizeAllocation = prizes[s.Key],
					Winners = s.Value.GroupBy(g => g.Player).Select(w =>
						new IntegerPrizeWinner() { Player = w.Key, WinningTicketCount = w.Count() }).ToList()
				})
				.OrderByDescending(o => o.PrizeAllocation.PrizeDefinition.AllocationPriority)
				.ToList();

			//	Work out total payout and house profit
			var totalPrizeCost = prizes.Sum(s => s.Value.PrizeAmount * s.Value.PrizeWinnerCount);
			var drawProfit = prizeFund - totalPrizeCost;
			Profit += drawProfit;

			Logger.LogDebug("Total Prize Fund = {fund}; Total Revenue = {revenue}", WalletFormatter.Format(prizeFund),
				WalletFormatter.Format(Revenue));
			Logger.LogDebug("Draw profit = {drawProfit}, Total Profit = {profit}", WalletFormatter.Format(drawProfit),
				WalletFormatter.Format(Profit));
			return winnerDetails;
		}

		/// <inheritdoc/>
		public void Payout(IEnumerable<IWinnerDetail<int>> winners)
		{
			var winnerList = winners.ToList();
			if (winnerList.Count == 0)
				throw new ArgumentException("No winners defined", nameof(winners));

			//	Before paying out, verify each tier winner details match expected
			var winCheck = winnerList.Select(s => new
				{
					s.PrizeAllocation.PrizeDefinition.Name,
					s.PrizeAllocation.PrizeWinnerCount,
					ActualWinnerCount = s.Winners.Sum(c => c.WinningTicketCount)
				})
				.ToList();

			//	Are there any mismatches? If so, report and halt payout immediately
			if (winCheck.Any(w => w.PrizeWinnerCount != w.ActualWinnerCount))
			{
				var errors = winCheck.Where(q => q.PrizeWinnerCount != q.ActualWinnerCount)
					.Select(s =>
						new ArgumentException(
							$"Mismatch in winning numbers for '{s.Name}': allocation = {s.PrizeWinnerCount}, actual = {s.ActualWinnerCount}"))
					.ToList();
				throw new AggregateException(errors);
			}

			//	Reward the winners
			foreach (var winnerTier in winnerList)
			{
				var prizeValue = winnerTier.PrizeAllocation.PrizeAmount;
				foreach (var winner in winnerTier.Winners)
				{
					var payoutValue = winner.WinningTicketCount * prizeValue;
					Logger.LogDebug("{player} wins on '{tier}' with {ticketCount} ticket{tp}, receiving {amount}",
						PlayerFormatter.FormatPlayer(winner.Player), winnerTier.PrizeAllocation.PrizeDefinition.Name,
						winner.WinningTicketCount, winner.WinningTicketCount != 1 ? "s" : "",
						WalletFormatter.Format(payoutValue));
					winner.Player.Wallet.AddFunds(payoutValue);
				}
			}
		}

		private void ShowWelcomeMessage(IPlayer<int> player)
		{
			Logger.LogInformation("Welcome to the Simplified Lottery {Player}", PlayerFormatter.FormatPlayer(player));
			Logger.LogInformation("Your current balance is: {balance}", WalletFormatter.Format(player.Wallet.Balance));
			Logger.LogInformation("Tickets are priced at: {ticketPrice} each",
				WalletFormatter.Format(TicketService.TicketCost));
		}

		private List<IntegerPlayerTicket> GetPlayerTickets(IEnumerable<IPlayer<int>> players)
		{
			var playerTickets = new List<IntegerPlayerTicket>();
			var ticketCount = 0;
			foreach (var player in players)
			{
				var ticketsToBuy = BuyingStrategy.GetTicketsToBuy(player, TicketService.TicketCost);

				Logger.LogDebug("{player} chooses to buy {count} ticket{plural}", PlayerFormatter.FormatPlayer(player), ticketsToBuy,
					ticketsToBuy != 1 ? "s" : "");
				//	If the player does not wish to buy (or cannot), skip to next player
				if (ticketsToBuy == 0)
					continue;

				//	Remove the funds from the wallet first
				player.Wallet.Withdraw(ticketsToBuy * TicketService.TicketCost);
				//	Buy tickets from the ticket service
				var ticketsBought = TicketService.BuyTickets(ticketsToBuy).ToList();
				//	Allocate bought tickets to the player
				ticketsBought.ForEach(t =>
					playerTickets.Add(new IntegerPlayerTicket() { Player = player, Ticket = t }));
				ticketCount += ticketsToBuy;
			}

			var playersBuyingTickets = playerTickets.GroupBy(g => g.Player)
				.Select(s => new { Player = s.Key, TicketCount = s.Count() })
				.Count(q => q.TicketCount > 0);
			Logger.LogDebug("A total of {players} bought {ticketCount} tickets", playersBuyingTickets, ticketCount);
			return playerTickets;
		}

		private Dictionary<IPrizeDefinition, List<IntegerPlayerTicket>> GetWinningTickets(
			IEnumerable<IPrizeDefinition> prizes,
			IEnumerable<IntegerPlayerTicket> tickets)
		{
			var ticketList = tickets.ToList();
			var totalTickets = ticketList.Count;
			var winningTickets = new Dictionary<IPrizeDefinition, List<IntegerPlayerTicket>>();
			foreach (var prize in prizes)
			{
				(var winners, ticketList) = PickWinningTickets(prize, totalTickets, ticketList);
				winningTickets[prize] = winners;
			}

			return winningTickets;
		}

		private static (List<IntegerPlayerTicket> winners, List<IntegerPlayerTicket> remaining) PickWinningTickets(
			IPrizeDefinition prize,
			int totalTickets, IEnumerable<IntegerPlayerTicket> tickets)
		{
			var ticketList = tickets.ToList();
			var winningTickets = new List<IntegerPlayerTicket>();
			for (var i = 0; i < prize.GetWinnerCount(totalTickets); i++)
			{
				var index = Random.Shared.Next(0, ticketList.Count);
				winningTickets.Add(ticketList[index]);
				ticketList.RemoveAt(index);
			}

			return (winningTickets, ticketList);
		}

		/// <inheritdoc/>
		public int Revenue { get; private set; }

		/// <inheritdoc/>
		public int Profit { get; private set; }

		#endregion
	}
}