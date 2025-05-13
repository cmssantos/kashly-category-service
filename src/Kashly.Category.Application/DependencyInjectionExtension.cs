using Kashly.Category.Application.Interfaces;
using Kashly.Category.Application.Services;
using Kashly.Category.Application.UseCases.Create;
using Kashly.Category.Application.UseCases.GetById;
using Microsoft.Extensions.DependencyInjection;

namespace Kashly.Category.Application;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddServices(services);
        AddUseCases(services);

        return services;
    }

    private static void AddAutoMapper(IServiceCollection services)
        => services.AddAutoMapper(typeof(DependencyInjectionExtension));

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IValidatorService, ValidationService>();
        services.AddScoped<IUserContext, UserContext>();
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        services.AddScoped<IGetByIdCategoryUseCase, GetByIdCategoryUseCase>();

    }
}
