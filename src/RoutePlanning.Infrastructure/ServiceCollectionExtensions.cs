using Microsoft.Extensions.DependencyInjection;
using RoutePlanning.Infrastructure.Database;
using Netcompany.Net.DomainDrivenDesign;
using Netcompany.Net.UnitOfWork;
using Netcompany.Net.UnitOfWork.AmbientTransactions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace RoutePlanning.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRoutePlanningInfrastructure(this IServiceCollection services)
    {
        //var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
        //keepAliveConnection.Open();
        services.AddDbContext<RoutePlanningDatabaseContext>(builder =>
        {
            //builder.UseSqlite(keepAliveConnection);
            builder.UseSqlServer(
                "Data Source=tcp:dbs-tl-dk1.database.windows.net,1433;Initial Catalog=db-tl-dk1;User ID=admin-tl-dk1;Password=telStarRox16");
            builder.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
        });

        services.AddDomainDrivenDesign(options => options.UseDbContext<RoutePlanningDatabaseContext>());
        services.AddUnitOfWork(builder => builder.UseAmbientTransactions().With<RoutePlanningDatabaseContext>());

        return services;
    }
}
