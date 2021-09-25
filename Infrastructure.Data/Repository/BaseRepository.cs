using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {

        protected readonly Contexto _contexto;
        
        public BaseRepository(Contexto contexto)
        {
            _contexto = contexto;
        }
        
        public void Alterar(TEntity obj)
        {
            _contexto.Entry(obj).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
        }

        public IList<TEntity> Consultar() =>
            _contexto.Set<TEntity>().ToList();

        public TEntity Consultar(int id) =>
            _contexto.Set<TEntity>().Find(id);
        
        public void Excluir(int id)
        {
            _contexto.Set<TEntity>().Remove(Consultar(id));
            _contexto.SaveChanges();
        }

        public void Inserir(TEntity obj)
        {
            _contexto.Set<TEntity>().Add(obj);
            _contexto.SaveChanges();
        }
    }
}
