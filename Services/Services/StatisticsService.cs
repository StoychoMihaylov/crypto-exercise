using Database.interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class StatisticsService : IStatiscticsService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IFetchService fetchService;
        private readonly ILogger<StatisticsService> logger;
        private readonly IPortfolioService portfolioService;

        // We assume only this coins can be owned
        private string[] coins = new string[] { "BTC", "ADA", "ETH", "DOT" };

        public StatisticsService(
            IServiceScopeFactory scopeFactory, 
            ILogger<StatisticsService> logger, 
            IFetchService fetchService,
            IPortfolioService c
            )
        {
            this.scopeFactory = scopeFactory;
            this.logger = logger;
            this.fetchService = fetchService;
            this.portfolioService = portfolioService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Start the timer here

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop the timer here

            return Task.CompletedTask;
        }

        private async void AlignedTimer_Elapsed(object sender, DateTime time)
        {
            // This function should be called by the timer
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IApplicationDBContext>();

            // Fetching prices
            var prices = await this.fetchService.RequestGetCoinPrices(coins);

            // Fetching portfolio entries for each user from DB
            //var portfolioEntities = await this.portfolioService.GetPortfolioEntities();

            // Calculating coin values and portfolio values

            await context.SaveChangesAsync();
        }
    }
}
