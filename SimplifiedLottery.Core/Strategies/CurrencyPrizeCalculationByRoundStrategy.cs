using System;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Strategies
{
	public class CurrencyPrizeCalculationByRoundStrategy
		: IPrizeCalculationStrategy<double>
	{
		private readonly int _decimalPlaces;
		private readonly MidpointRounding _roundingMode;

		/// <summary>
		/// Constructor applies the number of decimal places and rounding rule to apply during the calculation
		/// </summary>
		/// <param name="decimalPlaces">The number of decimal places for the currency</param>
		/// <param name="roundingMode">The mode of rounding to be used</param>
		public CurrencyPrizeCalculationByRoundStrategy(int decimalPlaces = 2,
			MidpointRounding roundingMode = MidpointRounding.AwayFromZero)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(decimalPlaces);
			_decimalPlaces = decimalPlaces;
			_roundingMode = roundingMode;
		}

		#region IPrizeCalculationStrategy<double> Members

		/// <inheritdoc/>
		public double Calculate(double prizeFund, int numberOfWinners)
		{
			return Math.Round(prizeFund / numberOfWinners, _decimalPlaces, _roundingMode);
		}

		#endregion
	}
}