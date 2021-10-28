using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Test
{
    public class ProducerService : IHostedService
    {
        private const string Topic = "morsley_users";
        
        private readonly ILogger _logger;
        private readonly IProducer<string, string> _producer;

        public ProducerService(ILogger<ProducerService> logger)
        {
            _logger = logger;
            
            var configuration = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName()
            };
            _producer = new ProducerBuilder<string, string>(configuration).Build();
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var message = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = "Aardvark"
            };
            _producer.Produce(Topic, message);
            _logger.LogInformation("Sending Message");
            //_producer.Flush(TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}