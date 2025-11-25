using System.Globalization;
using SimplifiedLottery.Core.Interfaces;

namespace SimplifiedLottery.Core.Formatters
{
	public class IntegerWalletFormatter
		: IWalletFormatter<int>

	{
		/// <inheritdoc/>
		public string Format(int value)
		{
			return value.Format();
		}

		/// <inheritdoc/>
		public string Format(int value, CultureInfo cultureInfo)
		{
			return value.Format(cultureInfo);
		}
	}
}