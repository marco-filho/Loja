using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Services;
using Loja.Domain.Interfaces.Repositories;
using Loja.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Loja.Domain.Services;

namespace Loja.Infra.Common.Initializers
{
    public static class DependencyInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Services
            services.AddScoped<IOrderService, OrderService>();

            //Base Services
            services.AddScoped<IBaseService<Order>, BaseService<Order>>();
            services.AddScoped<IBaseService<OrderItem>, BaseService<OrderItem>>();

            //Repositories
            services.AddTransient<IOrderRepository, OrderRepository>();

            //Base Repositories
            services.AddTransient<IBaseRepository<Order>, BaseRepository<Order>>();
            services.AddTransient<IBaseRepository<OrderItem>, BaseRepository<OrderItem>>();
        }
    }
}
