using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Strategies
{
	public class IntegerPrizeCalculationStrategy
		: IPrizeCalculationStrategy<int>
	{
		/// <inheritdoc/>
		public int Calculate(int prizeFund, int numberOfWinners)
		{
			return prizeFund / numberOfWinners;
		}
	}
}