using System.Text.Json.Serialization;
using Cms.AspNetCore.JsonLocalizer.Extensions;
using Kashly.Category.Api.Filters;
using Kashly.Category.Application;
using Kashly.Category.Infrastructure;

namespace Kashly.Category.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddApplicationServices();
        services.AddInfrastructureServices();
        services.AddCustomCors();
        services.AddCustomLocalization();
        services.AddCustomControllers();

        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddMvc(options => options.Filters.Add<ExceptionFilter>());

        return services;
    }

    private static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Enable enum as string
            });

        return services;
    }

    private static IServiceCollection AddCustomLocalization(this IServiceCollection services)
    {
        services.AddJsonLocalizer(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Localization"));
        return services;
    }

    private static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        return services;
    }
}
