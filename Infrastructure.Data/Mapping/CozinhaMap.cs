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
    public class CozinhaMap : IEntityTypeConfiguration<Cozinha>
    {
        public void Configure(EntityTypeBuilder<Cozinha> builder)
        {
            builder.ToTable("Cozinha");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Nome)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("nvarchar(70)");

            builder.Property(prop => prop.Observacao)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Descrição")
                .HasColumnType("nvarchar(100)");
     
        }
    }
}
