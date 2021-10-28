using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Test
{
    public class ConsumerService : IHostedService
    {
        private const string Topic = "morsley_users"; 
        
        private readonly ILogger<ConsumerService> _logger;
        private readonly IConsumer<string, string> _consumer;

        public ConsumerService(ILogger<ConsumerService> logger)
        {
            _logger = logger;
            
            var configuration = new ConsumerConfig
            {
                AutoOffsetReset = AutoOffsetReset.Earliest,
                BootstrapServers = "localhost:9092",
                GroupId = "morsley"
            };
            _consumer = new ConsumerBuilder<string, string>(configuration).Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(Topic);

            try
            {
                while (true)
                {
                    var result = _consumer.Consume(cancellationToken);

                }
            }
            catch (Exception e)
            {
                _consumer.Close();
            }

            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer?.Dispose();
            return Task.CompletedTask;
        }
    }
}