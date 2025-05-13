using KafkaConsumerApplication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderAPI.Services;

await CreateHostBuilder(args).RunConsoleAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseConsoleLifetime()
        .ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Warning))
        .ConfigureServices((hostContext, services) =>
        {
            //Create singleton object of Consumer-1/2
            services.AddSingleton<Consumer1>();
            services.AddSingleton<Consumer2>();
            services.AddHostedService<Consumer1>();
            services.AddHostedService<Consumer2>();

            //services.AddSingleton<Consumer2>();
            //services.AddSingleton<KafkaConsumer>();
            //services.AddSingleton<KafkaConsumerService>();
            //services.AddHostedService<KafkaConsumerService>();
        });
