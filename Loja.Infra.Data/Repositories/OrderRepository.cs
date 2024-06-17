using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Loja.Infra.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public override async Task<Order> Read(int id)
        {
            return await _context
                .Set<Order>()
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
