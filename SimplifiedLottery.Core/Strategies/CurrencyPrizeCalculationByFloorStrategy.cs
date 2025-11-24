using System;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Strategies
{
	public class CurrencyPrizeCalculationByFloorStrategy
		: IPrizeCalculationStrategy<double>
	{
		private readonly int _decimalPlaces;
		private readonly double _multiplier;

		public CurrencyPrizeCalculationByFloorStrategy(int decimalPlaces = 2)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(decimalPlaces);
			_decimalPlaces = decimalPlaces;
			_multiplier = Math.Pow(10, _decimalPlaces);
		}

		#region IPrizeCalculationStrategy<double> Members

		public double Calculate(double prizeFund, int numberOfWinners)
		{
			var intermediate = Math.Floor((prizeFund * _multiplier) / numberOfWinners);
			return Math.Round(intermediate / _multiplier, _decimalPlaces);
		}

		#endregion
	}
}