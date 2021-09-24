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

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Restaurante> Restaurantes { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pais>(new PaisMap().Configure);
            modelBuilder.Entity<Cidade>(new CidadeMap().Configure);
            modelBuilder.Entity<Cozinha>(new CozinhaMap().Configure);
            modelBuilder.Entity<Produto>(new ProdutoMap().Configure);
            modelBuilder.Entity<Restaurante>(new RestauranteMap().Configure);
            modelBuilder.Entity<Pedido>(new PedidoMap().Configure);
        }


    }
}
