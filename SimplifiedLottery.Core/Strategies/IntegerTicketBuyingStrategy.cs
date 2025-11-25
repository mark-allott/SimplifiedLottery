using System;
using SimplifiedLottery.Core.Formatters;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Strategies
{
	public class IntegerTicketBuyingStrategy
		: ITicketBuyingStrategy<int>
	{
		private int MinimumTickets { get; }
		private int MaximumTickets { get; }

		/// <summary>
		/// Ensures class is set up with a minimum and maximum number of tickets to buy
		/// </summary>
		/// <param name="minimumTickets">The lowest number of tickets that can be bought</param>
		/// <param name="maximumTickets">The highest number of tickets that can be bought</param>
		public IntegerTicketBuyingStrategy(int minimumTickets, int maximumTickets)
		{
			MinimumTickets = Math.Min(minimumTickets, maximumTickets);
			MaximumTickets = Math.Max(minimumTickets, maximumTickets);
		}

		/// <inheritdoc/>
		public int GetTicketsToBuy(IPlayer<int> player, int ticketCost)
		{
			var ticketsDesired = player.Name == "1"
				? GetTicketsToBuyForHuman(player, ticketCost)
				: GetTicketsToBuyForComputer(player, ticketCost);
			CheckTicketQuantityAllowed(ticketsDesired);
			return ticketsDesired;
		}

		/// <summary>
		/// Verifies the number of tickets bought is within valid range
		/// </summary>
		/// <param name="ticketsDesired">The number of tickets the player wishes to buy</param>
		private void CheckTicketQuantityAllowed(int ticketsDesired)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(ticketsDesired);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(ticketsDesired, MaximumTickets);
		}

		/// <summary>
		/// Implements a Console.ReadLine variant requesting the human player to select a number of tickets to buy
		/// </summary>
		/// <param name="player">The human player details</param>
		/// <param name="ticketCost">The cost of each ticket</param>
		/// <returns>The number of tickets the human player wishes to buy</returns>
		private int GetTicketsToBuyForHuman(IPlayer<int> player, int ticketCost)
		{
			//	Check for sufficient balance to purchase 1 ticket
			if (player.Wallet.Balance < ticketCost)
				return 0;

			var maxTickets = Math.Min(10, player.Wallet.Balance / ticketCost);
			while (true)
			{
				Console.Write($"How many tickets do you want to buy, {PlayerFormatter<int>.FormatPlayer(player)}? Choose between 1 and {maxTickets}: ");
				var input = Console.ReadLine();
				if (int.TryParse(input, out var ticketsToBuy) && ticketsToBuy >= 0 && ticketsToBuy <= maxTickets)
					return ticketsToBuy;
				Console.WriteLine($"Invalid input: '{input}'! Please enter a number between 1 and {maxTickets}.");
				Console.WriteLine();
			}
		}

		/// <summary>
		/// Determines the number of tickets a computer player wishes to buy
		/// </summary>
		/// <param name="player">The computer player details</param>
		/// <param name="ticketCost">The cost of each ticket</param>
		/// <returns>The number of tickets the computer player wishes to buy</returns>
		private int GetTicketsToBuyForComputer(IPlayer<int> player, int ticketCost)
		{
			//	Check for sufficient balance to purchase 1 ticket
			if (player.Wallet.Balance < ticketCost)
				return 0;
			var maxTickets = Math.Min(10, player.Wallet.Balance / ticketCost);
			return Random.Shared.Next(1, maxTickets + 1);
		}
	}
}