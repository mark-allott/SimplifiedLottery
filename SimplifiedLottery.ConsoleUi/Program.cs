using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimplifiedLottery.ConsoleUi.Configuration;
using SimplifiedLottery.Core.Configuration;
using SimplifiedLottery.Core.Formatters;
using SimplifiedLottery.Core.Interfaces;
using SimplifiedLottery.Core.Models;
using SimplifiedLottery.Core.Services;
using SimplifiedLottery.Core.Strategies;

namespace SimplifiedLottery.ConsoleUi
{
	internal static class Program
	{
		private static int Main(string[] args)
		{
			try
			{
				var host = CreateHost(args);
				host.Run();
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e.Message);
				return 1;
			}
			return 0;
		}

		private static IHost CreateHost(string[] args)
		{
			var hostBuilder = Host.CreateDefaultBuilder(args);
			hostBuilder.ConfigureServices((context, services) =>
			{
				services.Configure<IntegerPlayerServiceOptions>(
					context.Configuration.GetSection(nameof(IntegerPlayerServiceOptions)));
				services.Configure<IntegerFixedPriceTicketServiceOptions>(
					context.Configuration.GetSection(nameof(IntegerFixedPriceTicketServiceOptions)));
				services.Configure<IntegerTicketBuyingStrategyOptions>(
					context.Configuration.GetSection(nameof(IntegerTicketBuyingStrategyOptions)));
				services.Configure<LotteryGameOptions>(context.Configuration.GetSection(nameof(LotteryGameOptions)));
				services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
				services.AddApplicationServices();
				services.AddHostedService<LotteryHost>();
				services.AddSingleton<ILoggerFactory, LoggerFactory>();
			});

			return hostBuilder.Build();
		}

		private static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			return services
				//	Add model relationships
				.AddScoped<IPlayer<int>, PlayerWithIntegerWallet>()
				.AddScoped<IPlayerTicket<int>, IntegerPlayerTicket>()
				.AddScoped<IPrizeAllocation<int>, IntegerPrizeAllocation>()
				.AddScoped<IPrizeDefinition, LotteryPrize>()
				.AddScoped<IPrizeWinner<int>, IntegerPrizeWinner>()
				.AddScoped<ITicket<int>, IntegerTicket>()
				.AddScoped<IWallet<int>, IntegerWallet>()
				.AddScoped<IWinnerDetail<int>, IntegerWinnerDetail>()
				//	Add options
				.AddScoped<IPlayerServiceOptions<int>, IntegerPlayerServiceOptions>()
				.AddScoped<IFixedPriceTicketServiceOptions<int>, IntegerFixedPriceTicketServiceOptions>()
				.AddScoped<ITicketBuyingStrategyOptions<int>, IntegerTicketBuyingStrategyOptions>()
				//	Add formatter services
				.AddScoped<IPlayerFormatter<IPlayer<int>, int>, IntegerPlayerFormatter>()
				.AddScoped<IWalletFormatter<int>, IntegerWalletFormatter>()
				//	Add strategy definitions
				.AddScoped<IPrizeCalculationStrategy<int>, IntegerPrizeCalculationStrategy>()
				.AddScoped<ITicketBuyingStrategy<int>, IntegerTicketBuyingStrategy>()
				//	Add services
				.AddScoped<IFixedPriceTicketService<ITicket<int>, int>, IntegerFixedPriceTicketService>()
				.AddScoped<ILotteryGameService<int>, IntegerLotteryGameService>()
				.AddScoped<IPlayerService<int>, PlayerService>()
				.AddScoped<IPrizeAllocationService<int>, IntegerPrizeAllocationService>()
				.AddScoped<IPrizeDefinitionService, PrizeDefinitionService>();
		}
	}
}