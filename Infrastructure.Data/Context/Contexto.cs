using Domain.Entities;
using Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Context
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options) {}

        public DbSet<Pais> Paises { get; set; }

        public DbSet<Cidade> Cidades { get; set; }

        public DbSet<Cozinha> Cozinhas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pais>(new PaisMap().Configure);
            modelBuilder.Entity<Cidade>(new CidadeMap().Configure);
            modelBuilder.Entity<Cozinha>(new CozinhaMap().Configure);
        }


    }
}
