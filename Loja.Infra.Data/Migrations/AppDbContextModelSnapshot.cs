﻿// <auto-generated />
using System;
using Loja.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Loja.Infra.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Loja")
                .HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Loja.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasComment("Nome do cliente");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderedAt")
                        .HasColumnType("TEXT")
                        .HasComment("Data e hora em que o pedido foi feito");

                    b.HasKey("Id");

                    b.ToTable("Pedido", "Loja", t =>
                        {
                            t.HasComment("Registro de pedidos");
                        });
                });

            modelBuilder.Entity("Loja.Domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER")
                        .HasComment("Quantidade do produto no pedido");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT")
                        .HasComment("Preço unitário do produto");

                    b.Property<string>("Product")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasComment("Nome do produto");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("ItemPedido", "Loja", t =>
                        {
                            t.HasComment("Itens de pedidos");
                        });
                });

            modelBuilder.Entity("Loja.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Loja.Domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Loja.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
