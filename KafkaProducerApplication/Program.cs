using Confluent.Kafka;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092",
    BrokerAddressFamily = BrokerAddressFamily.V4
};

var producer = new ProducerBuilder<Null,
    string>(config).Build();
Console.WriteLine("Please enter the message you want to send");
string input = Console.ReadLine();

for (int i=0; i<1000; i++)
{
    var message = new Message<Null, string>
    {
        Value = "Message :: "+ i.ToString(),
    };

    var deliveryReport = await producer.ProduceAsync("myTopic", message);
}


//CreateMessage().Wait();

//static async Task CreateMessage()
//{
//    var config = new ProducerConfig
//    {
//        BootstrapServers = "localhost:9092",
//        ClientId = "my-app",
//        BrokerAddressFamily = BrokerAddressFamily.V4,
//    };
//    using
//    var producer = new ProducerBuilder<Null,
//        string>(config).Build();
//    Console.WriteLine("Please enter the message you want to send");
//    var input = Console.ReadLine();
//    var message = new Message<Null,
//        string>
//    {
//        Value = input
//    };
//    var deliveryReport = await producer.ProduceAsync("my-topic", message);
//    Console.WriteLine($"Message delivered to {deliveryReport.TopicPartitionOffset}");
//}

