using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Test
{
    class Program
    {
        private static IHostBuilder CreateHostBuilder(string[] arguments)
        {
            return Host.CreateDefaultBuilder().ConfigureServices((context, collection) =>
            {
                collection.AddHostedService<ConsumerService>();
                collection.AddHostedService<ProducerService>();
            });
        }
        
        public static void Main(string[] arguments)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();  
            
            CreateHostBuilder(arguments).Build().Run();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole()).AddTransient<ProducerService>();
            services.AddLogging(configure => configure.AddConsole()).AddTransient<ConsumerService>();
        }
    }
}