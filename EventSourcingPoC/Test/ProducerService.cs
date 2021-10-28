using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Test
{
    public class PublisherService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }
    }
}