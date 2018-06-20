using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TzMazeScraper.Models;

namespace TzMazeScraper.Services
{
    public class ScraperRunner
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ScraperRunner> _logger;
        private readonly object _thisLock = new object();
        private CancellationTokenSource _cancellationTokenSource;

        public bool IsRunning { get; set; }

        public ScraperRunner(IServiceScopeFactory serviceScopeFactory, ILogger<ScraperRunner> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public bool Run(bool reset)
        {
            CancellationToken token;
            lock (_thisLock)
            {
                if (IsRunning)
                {
                    return false;
                }

                IsRunning = true;
                _cancellationTokenSource = new CancellationTokenSource();
                token = _cancellationTokenSource.Token;
            }

            try
            {
                Task.Run(() => DoRun(reset, token));
                return true;
            }
            catch (Exception)
            {
                lock (_thisLock)
                {
                    IsRunning = false;
                    _cancellationTokenSource = null;
                }
                throw;
            }
           
        }

        public bool Cancel()
        {
            lock (_thisLock)
            {
                if (!IsRunning) return false;
                _cancellationTokenSource.Cancel();
                return true;
            }
        }

        private async Task DoRun(bool reset, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scraperService = scope.ServiceProvider.GetRequiredService<ScraperService>();
                    await scraperService.Run(reset, cancellationToken, OnShowAdded);
                }
            }
            catch (TaskCanceledException)
            {
                _logger.LogWarning("Scraper was cancelled");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Scraper run failed");
            }
            finally
            {
                lock (_thisLock)
                {
                    IsRunning = false;
                    _cancellationTokenSource = null;
                }
            }

        }

        private void OnShowAdded(Show show)
        {
            _logger.LogInformation($"New show added: {show.Id} - {show.Name}");
        }
    }
}
