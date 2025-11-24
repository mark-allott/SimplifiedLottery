using FluentAssertions;
using SimplifiedLottery.Core.Services;

namespace SimplifiedLottery.Tests.Services
{
	public class PlayerServiceTests
	{
		private readonly PlayerService _playerService = new PlayerService(50, 1000);

		[Theory]
		[InlineData(10, 20)]
		[InlineData(10, 15)]
		[InlineData(20, 30)]
		public void GetPlayers_ReturnsExpectedNumberOfPlayers(int min, int max)
		{
			var players = _playerService.GetPlayers(min, max).ToList();
			players.Count.Should().BeInRange(min, max);

			//	Human player (player 1) should always be present
			var human = players.First(q => q.Name == "1");
			human.Should().NotBeNull();

			//	Unique players should be same as number returned from service
			var uniquePlayers = players.DistinctBy(q => q.Id).ToList();
			uniquePlayers.Should().HaveCount(players.Count);

			players.All(p => p.HasFunds).Should().BeTrue();
		}
	}
}