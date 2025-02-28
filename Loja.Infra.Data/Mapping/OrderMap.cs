﻿using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Infra.Data.Mapping
{
    public class OrderMap : BaseMap<Order>
    {
        private const string tableName = "Order";

        public override void MapEntity(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(tableName, t => t.HasComment("Registro de pedidos"));

            builder.Property(c => c.ClientName).HasComment("Nome do cliente");
            builder.Property(c => c.OrderedAt).HasComment("Data e hora em que o pedido foi feito");
            builder.Property(c => c.Total).HasComment("Valor somado dos itens no pedido");

            builder.HasMany(el => el.OrderItems)
              .WithOne(r => r.Order)
              .HasForeignKey(x => x.OrderId);
        }
    }
}
