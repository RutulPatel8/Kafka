using KafkaConsumerApplication;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OrderAPI.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly KafkaConsumer _consumer;

        public KafkaConsumerService(KafkaConsumer consumer, ILogger<KafkaConsumerService> logger)
        {
            _logger = logger;
            _consumer = consumer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OrderProcessing Service Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                string orderRequest = _consumer.ReadMessage();

                if (!string.IsNullOrWhiteSpace(orderRequest))
                {
                    Console.WriteLine(orderRequest);
                    ////Deserilaize 
                    //OrderRequest order = JsonConvert.DeserializeObject<OrderRequest>(orderRequest);

                    ////TODO:: Process Order
                    //_logger.LogInformation($"Info: OrderHandler => Processing the order for {order.productname}");
                    //order.status = OrderStatus.COMPLETED;

                    ////Write to ReadyToShip Queue

                    //await _producer.WriteMessage(JsonConvert.SerializeObject(order), "readytoship");
                    
                }
                else
                {
                    await Task.Delay(2000);
                }
            }
        }
    }
}
