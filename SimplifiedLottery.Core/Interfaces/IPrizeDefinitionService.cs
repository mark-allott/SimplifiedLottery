using System.Collections.Generic;

namespace SimplifiedLottery.Core.Interfaces
{
	public interface IPrizeDefinitionService
	{
		/// <summary>
		/// Provides a number of prize definitions for the lottery
		/// </summary>
		IEnumerable<IPrizeDefinition> PrizeDefinitions { get; }
	}
}