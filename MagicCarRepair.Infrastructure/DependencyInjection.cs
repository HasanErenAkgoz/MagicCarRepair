using Core.Packages.Application.Repositories;
using Core.Packages.Application.Security.Hashing;
using Core.Packages.Application.Security.JWT;
using Core.Packages.Infrastructure.Repositories;
using Core.Packages.Infrastructure.Security;
using Core.Packages.Infrastructure.Security.Hashing;
using Core.Packages.Infrastructure.Security.JWT;
using Core.Packages.Infrastructure.Security.JWT.Models;
using MagicCarRepair.Application.Interfaces;
using MagicCarRepair.Infrastructure.Caching;
using MagicCarRepair.Infrastructure.Configuration;
using MagicCarRepair.Infrastructure.Persistence;
using MagicCarRepair.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MagicCarRepair.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Cache stratejisini belirle
        var cacheStrategy = configuration.GetValue<string>("CacheConfiguration:Strategy") ?? "memory";

        switch (cacheStrategy.ToLower())
        {
            case "memory":
                services.AddMemoryCache();
                services.AddSingleton<ICacheManager, MemoryCacheManager>();
                break;
            case "hybrid":
                services.AddHybridCache(configuration);
                break;
            case "redis":
                services.AddRedisCache(configuration);
                break;
            default:
                throw new ArgumentException($"Unknown cache strategy: {cacheStrategy}");
        }

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<MagicCarRepairDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(sp.GetRequiredService<AuditableEntitySaveChangesInterceptor>());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork<MagicCarRepairDbContext>>();

        // Register all repositories
        services.Scan(scan => scan
            .FromAssemblyOf<MagicCarRepairDbContext>()
            .AddClasses(classes => classes.AssignableTo(typeof(IAsyncRepository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Core services
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IDateTime, DateTimeService>();

        // Email service configuration
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, EmailService>();

        // Rate limiting configuration
        services.Configure<RateLimitingOptions>(configuration.GetSection("RateLimiting"));

        // Logging configuration
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.SetMinimumLevel(LogLevel.Information);
            builder.AddConfiguration(configuration.GetSection("Logging"));
            builder.AddConsole();
            builder.AddDebug();
        });

        // Cache Configuration
        services.Configure<CacheConfiguration>(configuration.GetSection("CacheConfiguration"));
        services.AddSingleton<ICacheConfiguration>(sp => 
            sp.GetRequiredService<IOptions<CacheConfiguration>>().Value);

        // Security Services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();
        services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));
        services.AddScoped<ITokenHelper, JwtHelper>();

        return services;
    }

    private static IServiceCollection AddHybridCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddStackExchangeRedisCache(options =>
        {
            var redisConfig = configuration.GetSection("CacheConfiguration:Redis");
            options.Configuration = redisConfig["ConnectionString"];
            options.InstanceName = redisConfig["InstanceName"];
        });

        services.AddSingleton<MemoryCacheManager>();
        services.AddSingleton<ICacheManager, FallbackCacheManager>();
        return services;
    }

    private static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            var redisConfig = configuration.GetSection("CacheConfiguration:Redis");
            options.Configuration = redisConfig["ConnectionString"];
            options.InstanceName = redisConfig["InstanceName"];
        });

        return services;
    }
}