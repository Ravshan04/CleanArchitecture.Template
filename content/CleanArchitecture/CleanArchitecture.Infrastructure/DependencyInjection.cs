using CleanArchitecture.Application.Interfaces.Auth;
using CleanArchitecture.Infrastructure.Auth;
using CleanArchitecture.Infrastructure.Kafka;
using CleanArchitecture.Persistence;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddTransient<IJwtTokenService, JwtTokenService>();
        services.AddTransient<ILoginService, LoginService>();
        services.AddTransient<IRefreshTokenService, RefreshTokenService>();
        services.AddTransient<IUserRegistrationService, IdentityUserRegistrationService>();
        
        services.AddCaching(configuration);
        services.AddMessaging(configuration);

        return services;
    }

    public static IServiceCollection AddCaching(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis") ?? "localhost:6379";

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "CleanArchitecture_";
        });

        return services;
    }

    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var kafkaSection = configuration.GetSection(KafkaOptions.SectionName);
        services.Configure<KafkaOptions>(kafkaSection);

        var kafkaOptions = kafkaSection.Get<KafkaOptions>() ?? new KafkaOptions();

        services.AddSingleton(new ProducerConfig
        {
            BootstrapServers = kafkaOptions.BootstrapServers
        });

        services.AddSingleton(new ConsumerConfig
        {
            BootstrapServers = kafkaOptions.BootstrapServers,
            GroupId = kafkaOptions.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        });

        return services;
    }
}