using ControledeSalaFEMASS.Application.Behaviors;
using ControledeSalaFEMASS.Application.Services.AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ControledeSalaFEMASS.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddMediatR(services, configuration);
        AddAutoMapper(services, configuration); 
    }

    private static void AddAutoMapper(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
        {
            autoMapperOptions.AddProfile(new AutoMapping());
        }).CreateMapper());
    }

    private static void AddMediatR(IServiceCollection services, IConfiguration configuration)
    {
        var handlers = AppDomain.CurrentDomain.Load("ControledeSalaFEMASS.Application");

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(handlers);
            //cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(new[] { Assembly.Load("ControledeSalaFEMASS.Application") });
    }
}