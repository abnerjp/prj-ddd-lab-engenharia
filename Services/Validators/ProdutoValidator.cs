using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {

        public ProdutoValidator() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o nome do produto!")
                .NotNull().WithMessage("Informe o nome do produto!");

            RuleFor(c => c.Preco)
                .NotEmpty().WithMessage("Informe um preço para o produto!")
                .NotNull().WithMessage("Informe um preço para o produto!")
                .LessThan(0.0).WithMessage("Preço deve ser igual ou maior do que zero!");
        }
    }
}
