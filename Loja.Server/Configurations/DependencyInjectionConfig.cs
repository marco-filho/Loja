using Loja.Infra.Common.Initializers;

namespace Loja.Server.Configurations
{
    ///<summary>
    /// Classe responsável pela injeção de dependência
    ///</summary>
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            DependencyInjector.RegisterServices(services);
        }
    }
}