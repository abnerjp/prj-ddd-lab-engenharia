
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : BaseEntity
    {

        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        private void Validar(TEntity obj, AbstractValidator<TEntity> validador)
        {
            if (obj == null)
                throw new Exception("Objeto vazio!");
            validador.ValidateAndThrow(obj);
        }


        public void Alterar<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validar(obj, Activator.CreateInstance<TValidator>());
            _baseRepository.Alterar(obj);
        }

        public void Excluir(int id) =>
            _baseRepository.Excluir(id);
        

        public void Inserir<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validar(obj, Activator.CreateInstance<TValidator>());
            _baseRepository.Inserir(obj);
        }

        public IList<TEntity> Listar() =>
            _baseRepository.Consultar();

        public TEntity ListarPorId(int id) =>
            _baseRepository.Consultar(id);
    }
}
