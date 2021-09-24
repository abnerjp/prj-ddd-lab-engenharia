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
    public class RestauranteMap : IEntityTypeConfiguration<Restaurante>
    {
        public void Configure(EntityTypeBuilder<Restaurante> builder)
        {
            builder.ToTable("Restaurante");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Nome)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("nvarchar(70)");

            builder.Property(prop => prop.Cep)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Cep")
                .HasColumnType("nvarchar(8)");

            builder.Property(prop => prop.Bairro)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Bairro")
                .HasColumnType("nvarchar(70)");

            builder.Property(prop => prop.Logradouro)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Logradouro")
                .HasColumnType("nvarchar(70)");

            builder.Property(prop => prop.Numero)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Numero")
                .HasColumnType("nvarchar(70)");

            builder.Property(prop => prop.Cozinha.Id)
                .HasConversion(prop => (int)prop, prop => prop)
                .IsRequired()
                .HasColumnName("CozinhaId")
                .HasColumnType("int");

            builder.Property(prop => prop.Cidade.Id)
                .HasConversion(prop => (int)prop, prop => prop)
                .IsRequired()
                .HasColumnName("CidadeId")
                .HasColumnType("int");
     
        }
    }
}
