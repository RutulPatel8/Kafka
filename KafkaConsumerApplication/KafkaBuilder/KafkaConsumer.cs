using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace KafkaConsumerApplication
{
    public class KafkaConsumer 
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<KafkaConsumer> _logger;

        public KafkaConsumer(ILogger<KafkaConsumer> logger)
        {
            _logger = logger;
            _consumer = KafkaUtils.CreateConsumer("localhost:9092", new List<string> { "myTopic" });
        }

        public string ReadMessage()
        {
            var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(0.5));
            if (consumeResult == null || string.IsNullOrWhiteSpace(consumeResult.Value))
            {
                return string.Empty;
            }
            else
            {
                return consumeResult.Value;
            }            
        }
    }
}
