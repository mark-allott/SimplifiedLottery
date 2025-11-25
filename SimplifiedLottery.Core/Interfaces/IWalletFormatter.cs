using System.Globalization;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface IWalletFormatter<in T>
		where T : struct
	{
		/// <summary>
		/// Formats the <paramref name="value"/> to requirements
		/// </summary>
		/// <param name="value">The value to format</param>
		/// <returns>The value expressed in the correct format</returns>
		string Format(T value);

		/// <summary>
		/// Formats the <paramref name="value"/> to requirements
		/// </summary>
		/// <param name="value">The value to format</param>
		/// <param name="cultureInfo">The culture-specific rules for formatting</param>
		/// <returns>The value expressed in the correct format</returns>
		string Format(T value, CultureInfo cultureInfo);
	}
}