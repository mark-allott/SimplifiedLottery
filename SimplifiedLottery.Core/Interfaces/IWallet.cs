namespace SimplifiedLottery.Core.Interfaces
{
	public interface IWallet<T>
		where T : struct
	{
		/// <summary>
		/// The current amount in the wallet
		/// </summary>
		T Balance { get; }

		/// <summary>
		/// Adds funds to the wallet
		/// </summary>
		/// <param name="amount">The amount to credit the wallet with</param>
		/// <remarks>
		/// Negative amounts are not permitted!!
		/// </remarks>
		void AddFunds(T amount);

		/// <summary>
		/// Removes funds from the wallet, subject to availability
		/// </summary>
		/// <param name="amount">The funds to be withdrawn</param>
		/// <remarks>
		/// Negative quantities are not permitted!!
		/// </remarks>
		void Withdraw(T amount);
	}
}