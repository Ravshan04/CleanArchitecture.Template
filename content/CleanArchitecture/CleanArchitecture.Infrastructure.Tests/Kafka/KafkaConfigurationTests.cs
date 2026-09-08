using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Kafka;
using Confluent.Kafka;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Tests.Kafka;

public class KafkaConfigurationTests
{
    [Fact]
    public void AddMessaging_ShouldRegisterKafkaConfigsAndOptions()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Kafka:BootstrapServers", "localhost:9092" },
            { "Kafka:GroupId", "test-group" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var services = new ServiceCollection();

        // Act
        services.AddMessaging(configuration);
        var provider = services.BuildServiceProvider();

        // Assert
        var options = provider.GetService<IOptions<KafkaOptions>>();
        options.Should().NotBeNull();
        options!.Value.BootstrapServers.Should().Be("localhost:9092");
        options.Value.GroupId.Should().Be("test-group");

        var producerConfig = provider.GetService<ProducerConfig>();
        producerConfig.Should().NotBeNull();
        producerConfig!.BootstrapServers.Should().Be("localhost:9092");

        var consumerConfig = provider.GetService<ConsumerConfig>();
        consumerConfig.Should().NotBeNull();
        consumerConfig!.BootstrapServers.Should().Be("localhost:9092");
        consumerConfig.GroupId.Should().Be("test-group");
    }
}
