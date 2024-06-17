using System.Reflection;
using Microsoft.Extensions.Configuration;
using Loja.Domain.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Loja.Infra.Common.Initializers
{
    public static class EnvironmentInitializer
    {
        public static void ConfigureEnvironment(this IHostApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true);

            // Constants
            Constants.Project = builder.Configuration.GetSection(nameof(ProjectConfig)).Get<ProjectConfig>();
            Constants.Project.Name = Assembly.GetEntryAssembly()?.GetName().Name;
            Constants.ConnectionStrings = builder.Configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        }
    }
}
