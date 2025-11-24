using System;
using System.Collections.Generic;
using System.Linq;
using SimplifiedLottery.Core.Interfaces;
using SimplifiedLottery.Core.Models;
using SimplifiedLottery.Core.Strategies;

namespace SimplifiedLottery.Core.Services
{
	public class IntegerPrizeAllocationService
		: IPrizeAllocationService<int>
	{
		private readonly IPrizeCalculationStrategy<int> _prizeCalculationStrategy;

		//	Default constructor for the allocation service
		public IntegerPrizeAllocationService()
			: this(new IntegerPrizeCalculationStrategy())
		{
		}

		/// <summary>
		/// Alternate constructor, allows specification of the prize calculation strategy to be used 
		/// </summary>
		/// <param name="prizeCalculationStrategy"></param>
		public IntegerPrizeAllocationService(IPrizeCalculationStrategy<int> prizeCalculationStrategy)
		{
			_prizeCalculationStrategy = prizeCalculationStrategy ?? throw new ArgumentNullException(nameof(prizeCalculationStrategy));
		}

		#region IPrizeAllocationService<int> Members

		/// <inheritdoc/>
		public IEnumerable<IPrizeAllocation<int>> CalculatePrizes(IEnumerable<IPrizeDefinition> prizeDefinitions,
			int prizeFund, int ticketCount)
		{
			return CalculateIntegerPrizeAllocations(prizeDefinitions, prizeFund, ticketCount);
		}

		/// <summary>
		/// Internal method to create the prize allocation objects
		/// </summary>
		/// <param name="prizeDefinitions">The definitions for the prizes</param>
		/// <param name="prizeFund">The total funds available for all prizes</param>
		/// <param name="ticketCount">The total number of tickets sold</param>
		/// <returns>A list of <see cref="IntegerPrizeAllocation"/> objects, detailing the prizes to allocate</returns>
		/// <exception cref="ArgumentException"></exception>
		private List<IntegerPrizeAllocation> CalculateIntegerPrizeAllocations(
			IEnumerable<IPrizeDefinition> prizeDefinitions, int prizeFund, int ticketCount)
		{
			ArgumentNullException.ThrowIfNull(prizeDefinitions);
			//	Sort definitions by highest priority first
			var definitions = prizeDefinitions
				.OrderByDescending(o => o.AllocationPriority)
				.ToList();

			if (definitions.Count == 0)
				throw new ArgumentException("There are no prizes defined.");

			//	Prize allocation cannot be 100% or more, otherwise the house makes no money
			var sumOfAllocations = definitions.Sum(s => s.PrizePercentage);
			ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(sumOfAllocations, 100.0f, nameof(prizeDefinitions));

			var prizes = new List<IntegerPrizeAllocation>();
			definitions.ForEach(d =>
			{
				var winnerCount = d.GetWinnerCount(ticketCount);
				var prizeForTier = d.PrizePercentage * prizeFund / 100;
				var prizeValue = _prizeCalculationStrategy.Calculate((int)prizeForTier, winnerCount);
				prizes.Add(new IntegerPrizeAllocation(d, winnerCount, prizeValue));
			});
			return prizes;
		}

		#endregion
	}
}