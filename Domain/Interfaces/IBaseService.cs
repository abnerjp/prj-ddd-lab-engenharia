using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        void Inserir<TValidator>(TEntity obj)
            where TValidator : AbstractValidator<TEntity>;

        void Alterar<TValidator>(TEntity obj)
            where TValidator : AbstractValidator<TEntity>;

        IList<TEntity> Listar();

        TEntity ListarPorId(int id);

        void Excluir(int id);

    }
}
