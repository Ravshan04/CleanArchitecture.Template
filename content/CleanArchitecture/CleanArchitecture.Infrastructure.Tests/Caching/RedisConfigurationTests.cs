using CleanArchitecture.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Tests.Caching;

public class RedisConfigurationTests
{
    [Fact]
    public void AddCaching_ShouldRegisterIDistributedCache()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "ConnectionStrings:Redis", "localhost:6379" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var services = new ServiceCollection();

        // Act
        services.AddCaching(configuration);
        var provider = services.BuildServiceProvider();

        // Assert
        var cache = provider.GetService<IDistributedCache>();
        cache.Should().NotBeNull();
    }
}
