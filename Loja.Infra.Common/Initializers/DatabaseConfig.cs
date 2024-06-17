using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Loja.Infra.Data;
using Loja.Domain.Config;

namespace Loja.Infra.Common.Initializers
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            var connectionString = Constants.ConnectionStrings.Database;

            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlite(connectionString,
               providerOptions =>
               {
                   providerOptions.MigrationsHistoryTable("__EFMigrationsHistory", AppDbContext.Schema);
               }));

        }
    }
}
