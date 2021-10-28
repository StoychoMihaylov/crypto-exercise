using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BackendChallange.Services
{
    public class StatisticsService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<StatisticsService> _logger;

        // We assume only this coins can be owned
        private string[] Coins = new string[] {"BTC", "ADA", "ETH", "DOT"};

        public StatisticsService(IServiceScopeFactory scopeFactory, ILogger<StatisticsService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
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
            //using var scope = _scopeFactory.CreateScope();
            //var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

            // Fetching prices

            // Fetching portfolio entries for each user from DB

            // Calculating coin values and portfolio values


            //await context.SaveChangesAsync();   
        }

    }
}