using Kashly.Category.Domain.Interfaces;
using Kashly.Category.Infrastructure.Data;
using Kashly.Category.Infrastructure.Data.Configurations;
using Kashly.Category.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Kashly.Category.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(options =>
        {
            configuration.GetSection("DatabaseSettings").Bind(options);
        });

        AddDbContexts(services, configuration);
        AddRepositories(services);

        MigrateDbContext(services.BuildServiceProvider());

        return services;
    }

    private static void AddDbContexts(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(sp =>
        {
            IOptions<DatabaseSettings> options = sp.GetRequiredService<IOptions<DatabaseSettings>>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return new ReadDbContext(
                new DbContextOptionsBuilder<ReadDbContext>()
                    .UseNpgsql(connectionString)
                    .Options,
                options
            );
        });

        services.AddScoped(sp =>
        {
            IOptions<DatabaseSettings> options = sp.GetRequiredService<IOptions<DatabaseSettings>>();
            IConfiguration configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return new WriteDbContext(
                new DbContextOptionsBuilder<WriteDbContext>()
                    .UseNpgsql(connectionString)
                    .Options,
                options
            );
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IWriteCategoryRepository, WriteCategoryRepository>();
        services.AddScoped<IReadCategoryRepository, ReadCategoryRepository>();
    }

    private static void MigrateDbContext(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        WriteDbContext dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        dbContext.Database.Migrate();
    }
}
