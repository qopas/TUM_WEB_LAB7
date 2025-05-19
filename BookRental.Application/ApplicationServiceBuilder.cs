using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceBuilder
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly(); 
        services.AddValidatorsFromAssembly(assembly);
        
        services.AddFluentValidation(fv => 
        {
            fv.RegisterValidatorsFromAssembly(assembly);
            fv.ImplicitlyValidateChildProperties = true;
        });
        
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);
        });
            
        return services;
    }
}