using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Infra.Data.Mapping
{
    public class OrderItemMap : BaseMap<OrderItem>
    {
        private const string tableName = "OrderItem";

        public override void MapEntity(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(tableName, t => t.HasComment("Itens de pedidos"));

            builder.Property(c => c.Product).HasComment("Nome do produto");
            builder.Property(c => c.Amount).HasComment("Quantidade do produto no pedido");
            builder.Property(c => c.Price).HasComment("Preço unitário do produto");

            builder.HasOne(el => el.Order)
              .WithMany(r => r.OrderItems)
              .HasForeignKey(x => x.OrderId);
        }
    }
}
