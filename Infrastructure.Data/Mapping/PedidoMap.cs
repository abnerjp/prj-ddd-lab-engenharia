using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mapping
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.DataPedido)
                .HasConversion(prop => (DateTime) prop, prop => prop)
                .IsRequired()
                .HasColumnName("DataPedido")
                .HasColumnType("DateTime");

            builder.Property(prop => prop.Quantidade)
               .HasConversion(prop => (float)prop, prop => prop)
               .IsRequired()
               .HasColumnName("Quantidade")
               .HasColumnType("float");

            builder.Property(prop => prop.TaxaFrete)
               .HasConversion(prop => (float)prop, prop => prop)
               .IsRequired()
               .HasColumnName("TaxaFrete")
               .HasColumnType("float");

            builder.Property(prop => prop.Desconto)
              .HasConversion(prop => (float)prop, prop => prop)
              .IsRequired()
              .HasColumnName("Desconto")
              .HasColumnType("float");

            builder.Property(prop => prop.ValorTotal)
              .HasConversion(prop => (float)prop, prop => prop)
              .IsRequired()
              .HasColumnName("ValorTotal")
              .HasColumnType("float");

            builder.Property(prop => prop.RestauranteId)
                .HasConversion(prop => (int)prop, prop => prop)
                .IsRequired()
                .HasColumnName("RestauranteId")
                .HasColumnType("int");

            builder.Property(prop => prop.ProdutoId)
                .HasConversion(prop => (int)prop, prop => prop)
                .IsRequired()
                .HasColumnName("ProdutoId")
                .HasColumnType("int");

        }
    }
}
