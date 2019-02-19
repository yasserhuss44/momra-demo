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
    public class BackGroundFakePingService : IHostedService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private Timer _timer1;
        private readonly ILogger _logger;

        public BackGroundFakePingService(IConfiguration configuration, ILogger<BackGroundFakePingService> logger)
        {
            _logger = logger;
            _configuration = configuration; 
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting for BackGroundFakePingService.");

           _timer1 = new Timer(Ping, null, TimeSpan.Zero, TimeSpan.FromSeconds(25));

            return Task.CompletedTask;
        }

        private void Ping(object state)
        {
            try
            {
                var apisTriggersSection = _configuration.GetSection("ApisTriggersUrls");
                var url = apisTriggersSection.GetValue<string>("Vehicle");
                var http = (HttpWebRequest)WebRequest.Create(url);
                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();

            }
            catch (Exception ex)
            {
                _logger.LogError($"BackGroundFakePingService: {ex.Message}");
            }
        
    }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping for BackGroundFakePingService.");

            _timer1?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer1?.Dispose();
        }
    }

}