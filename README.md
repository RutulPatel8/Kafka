# Kafka Producer-Consumer Implementation

This project demonstrates a basic implementation of Apache Kafka using C# .NET 6.0, featuring both producer and consumer applications.

## Project Structure

The solution consists of two main components:

1. **KafkaProducerApplication**: A console application that produces messages to Kafka topics
2. **KafkaConsumerApplication**: A console application that consumes messages from Kafka topics

## Prerequisites

- .NET 6.0 SDK
- Apache Kafka running locally (default port: 9092)
- Confluent.Kafka NuGet package (version 2.2.0)

## Features

### Producer Application
- Simple console-based message producer
- Configurable message input
- Sends messages to a specified Kafka topic
- Uses localhost:9092 as the default bootstrap server

### Consumer Application
- Multiple consumer implementations:
  - Consumer1: Background service consumer
  - Consumer2: Background service consumer
  - KafkaConsumer: Basic consumer implementation
- Supports message consumption from specified topics
- Implements consumer group functionality
- Auto offset reset configuration
- Background service implementation for continuous message processing

## Configuration

### Producer Configuration
```csharp
var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092",
    BrokerAddressFamily = BrokerAddressFamily.V4
};
```

### Consumer Configuration
```csharp
var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "new-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};
```

## Getting Started

1. Clone the repository
2. Ensure Kafka is running on localhost:9092
3. Build the solution:
   ```bash
   dotnet build
   ```
4. Run the producer application:
   ```bash
   cd KafkaProducerApplication
   dotnet run
   ```
5. Run the consumer application:
   ```bash
   cd KafkaConsumerApplication
   dotnet run
   ```

## Dependencies

- Confluent.Kafka (2.2.0)
- Microsoft.Extensions.Hosting (7.0.1)
- Microsoft.Extensions.Hosting.Abstractions (7.0.0)

## Project Architecture

The solution follows a modular architecture with separate projects for producer and consumer applications. The consumer application implements multiple consumer types to demonstrate different approaches to message consumption:

1. Background Service Consumers (Consumer1, Consumer2)
2. Basic Kafka Consumer
3. Kafka Consumer Service

## Notes

- The default topic used in the implementation is "myTopic"
- The consumer group ID is set to "new-consumer-group"
- Auto offset reset is configured to start from the earliest available message
- The implementation includes error handling and logging capabilities

## Contributing

Feel free to submit issues and enhancement requests.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
