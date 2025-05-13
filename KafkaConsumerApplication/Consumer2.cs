namespace KafkaConsumerApplication
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Microsoft.Extensions.Hosting;

    public class Consumer2 : IHostedService, IDisposable //BackgroundService
    {
        private Timer _timer;
        private readonly IConsumer<string, string> _consumer;
        private bool isBackgroundProcess = false;

        public Consumer2()
        {
            //_consumer = KafkaUtils.CreateConsumer("localhost:9092", new List<string> { "myTopic" });
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "new-consumer-group",
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe("myTopic");
            _consumer = consumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            //var config = new ConsumerConfig
            //{
            //    BootstrapServers = "localhost:9092",
            //    AutoOffsetReset = AutoOffsetReset.Earliest,
            //    ClientId = "Consumer-2",
            //    GroupId = "new-consumer-group",
            //    BrokerAddressFamily = BrokerAddressFamily.V4,
            //    PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin
            //};

            //var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            //consumer.Subscribe("myTopic");
            if (!isBackgroundProcess)
            {
                isBackgroundProcess = true;
                try
                {
                    while (true)
                    {
                        var consumeResult = _consumer.Consume();
                        Console.WriteLine($"Consumer - 2 :: Message received from {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
                    }

                }
                catch (OperationCanceledException)
                {
                    // The consumer was stopped via cancellation token.
                }
                finally
                {
                    _consumer.Close();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (true)
                {
                    var consumeResult = _consumer.Consume();
                    Console.WriteLine($"Consumer - 2 :: Message received from {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                // The consumer was stopped via cancellation token.
            }
            finally
            {
                _consumer.Close();
            }
        }
    }
}
