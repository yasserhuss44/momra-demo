using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Common.BackGroundJobs.Handlers
{
    public class TriggerCustomerToPullFromQueueJob : IHostedService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private Timer _timer;
        private readonly ILogger _logger;

        public TriggerCustomerToPullFromQueueJob(ILogger<TriggerCustomerToPullFromQueueJob> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting for TriggerCustomerToPullFromQueueJob.");

           _timer = new Timer(Trigger, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));

            return Task.CompletedTask;
        }

        private void Trigger(object state)
        {
            try
            {
                var apisTriggersSection = _configuration.GetSection("ApisTriggersUrls");
                var url = apisTriggersSection.GetValue<string>("Customer");
                var http = (HttpWebRequest)WebRequest.Create(url);
                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();

            }
            catch (Exception ex)
            {
                _logger.LogError($"TriggerCustomerToPullFromQueueJob: {ex.Message}");
            }
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping for TriggerCustomerToPullFromQueueJob.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}