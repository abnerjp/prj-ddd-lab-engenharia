using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {

        void Inserir(TEntity obj);
        void Alterar(TEntity obj);
        void Excluir(int id);
        IList<TEntity> Consultar();
        TEntity Consultar(int id);

    }
}
