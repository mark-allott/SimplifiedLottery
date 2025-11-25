using System.Collections.Generic;
using System.Collections.Immutable;
using SimplifiedLottery.Core.Interfaces;
using SimplifiedLottery.Core.Models;

namespace SimplifiedLottery.Core.Services
{
	public class PrizeDefinitionService
		: IPrizeDefinitionService
	{
		private static readonly IPrizeDefinition Tier1 =
			LotteryPrize.WinnersByCount("Grand Prize", "The big one", 50.0f, 1, 1000);

		private static readonly IPrizeDefinition Tier2 =
			LotteryPrize.WinnersByPercentage("Second Tier", "Middling prizes", 30.0f, 10.0f, 100);

		private static readonly IPrizeDefinition Tier3 =
			LotteryPrize.WinnersByPercentage("Third Tier", "Low-value prizes", 10.0f, 20.0f, 100);

		/// <inheritdoc/>
		public IEnumerable<IPrizeDefinition> PrizeDefinitions
		{
			get
			{
				return new List<IPrizeDefinition> { Tier1, Tier2, Tier3 }.ToImmutableList();
			}
		}
	}
}