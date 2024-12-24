using Microsoft.Extensions.Logging;

namespace AgricultureManager.Module.Accounting.Services
{
    public class AccountingHostedService(ILogger<AccountingHostedService> logger, IServiceProvider serviceProvider) : IAccountingHostedService
    {
        public bool IsEnabled { get; set; } = true;
        private Timer? _timer = null;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(MainLoop, cancellationToken, MilliSecondsUntilMidnight(), Timeout.Infinite);

            return Task.CompletedTask;
        }
        private void MainLoop(object? state)
        {
            CancellationToken cancellationToken = CancellationToken.None;

            if (state != null && state is CancellationToken token)
            {
                cancellationToken = token;
            }

            if (!IsEnabled)
            {
                logger.LogInformation($"Timed Hosted Service is disabled.");
                return;
            }

            logger.LogInformation("Timed Hosted Service is working.");

            try
            {
                //var service = serviceProvider.GetRequiredService<IBankMouvementsService>();
                //service.GetData(cancellationToken);
            }
            finally
            {
                // set timer to fire off again
                _timer?.Change(MilliSecondsUntilMidnight(), Timeout.Infinite);
            }

        }

        private static int MilliSecondsUntilMidnight()
            => (int)(DateTime.Today.AddDays(1.0) - DateTime.Now).TotalMilliseconds;

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Hosted Service is Stopping");
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
