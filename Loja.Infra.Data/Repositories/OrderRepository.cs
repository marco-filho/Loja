using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Loja.Infra.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public override async Task<Order> Read(int id)
        {
            var entity = await _context
                .Set<Order>()
                .IgnoreDeleted()
                .SingleAsync(x => x.Id == id);

            await _context.Entry(entity)
                .Collection(b => b.OrderItems)
                .Query()
                .Where(x => x.DeletedAt == null)
                .LoadAsync();

            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }
    }
}
