using Loja.Server.AutoMapper;

namespace Loja.Server.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddAutoMapper(typeof(DtoToEntityMapping));
            services.AddAutoMapper(typeof(EntityToDtoMapping));
        }
    }
}
