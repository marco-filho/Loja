using Microsoft.OpenApi.Models;
using Loja.Domain.Config;
using System.Reflection;

namespace Loja.Server.Configurations
{
    ///<summary>
    /// Classe responsável pela configuração da documentação com Swagger
    ///</summary>
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = Constants.Project.Name,
                    Version = "v1",
                    Description = "API Loja Swagger",
                    Contact = new OpenApiContact
                    {
                        Name = Constants.Project.ResponsibleDevName,
                        Email = Constants.Project.ResponsibleDevEmail,
                        Url = new Uri(Constants.Project.ResponsibleDevCompanyWebSite),
                    }
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                option.IncludeXmlComments(xmlPath);
            });
        }
    }
}
