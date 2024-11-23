using ControledeSalaFEMASS.Domain.Repositories;
using ControledeSalaFEMASS.Domain.Services.QuestPDF;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using ControledeSalaFEMASS.Infrastructure.DataAccess.Repositories;
using ControledeSalaFEMASS.Infrastructure.Services.QuestPDF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;

namespace ControledeSalaFEMASS.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddQuestPDF(services);
    }

	private static void AddQuestPDF(IServiceCollection services)
	{
		QuestPDF.Settings.License = LicenseType.Community;
		services.AddScoped<IDocumentGenerator, DocumentGenerator>();
	}

	private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<ISalaRepository, SalaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITurmaRepository, TurmaRepository>();
        services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<AppDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }
}