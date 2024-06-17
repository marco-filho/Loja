using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Loja.Domain.Entities;

namespace Loja.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public const string Schema = "Loja";

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        private IDbContextTransaction? _currentTransaction;
        public IDbTransaction Transaction => _currentTransaction?.GetDbTransaction();

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Defaults
            modelBuilder.HasDefaultSchema(Schema);

            //Mappings
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
