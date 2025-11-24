using System;
using System.Globalization;

namespace SimplifiedLottery.Core.Formatters
{
	public static class WalletFormatter
	{
		public static readonly CultureInfo DefaultCulture = CultureInfo.GetCultureInfo("en-US");
		public static readonly CultureInfo BritishCulture = CultureInfo.GetCultureInfo("en-GB");

		extension(int value)
		{
			/// <summary>
			/// Formats an integer amount in the default culture currency
			/// </summary>
			/// <returns>The value as expressed as a currency in the default culture</returns>
			public string Format()
			{
				//	Uses the culture defined as default above, but could be changed to pull the current culture in use on the system instead
				return value.Format(DefaultCulture);
			}

			/// <summary>
			/// Formats an integer amount in the specified culture's currency
			/// </summary>
			/// <param name="culture">The culture to use for currency information</param>
			/// <returns>The value expressed in the specified culture</returns>
			public string Format(CultureInfo culture)
			{
				var nfi = culture.NumberFormat;
				var decimalPlaces = nfi.CurrencyDecimalDigits;
				var divisor = Math.Pow(10, decimalPlaces);
				var quotient = Math.Round(value / divisor, decimalPlaces);
				return string.Format("{0}{1:N" + decimalPlaces + "}", nfi.CurrencySymbol, quotient);
			}
		}
	}
}