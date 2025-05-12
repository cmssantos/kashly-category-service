using Kashly.Category.Domain.Interfaces;
using Kashly.Category.Infrastructure.Data;
using Kashly.Category.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kashly.Category.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        AddDbContext(services);
        AddRepositories(services);
        MigrateDbContext(services.BuildServiceProvider());

        return services;
    }

    private static void AddDbContext(IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

        services.AddDbContext<ReadDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<WriteDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IWriteCategoryRepository, WriteCategoryRepository>();
        services.AddScoped<IReadCategoryRepository, ReadCategoryRepository>();
    }

    private static void MigrateDbContext(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        IServiceProvider scopedServiceProvider = scope.ServiceProvider;

        using WriteDbContext dbContext = scopedServiceProvider.GetRequiredService<WriteDbContext>();
        dbContext.Database.Migrate();
    }
}
