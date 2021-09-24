using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {

        public PedidoValidator() 
        {
            RuleFor(c => c.DataPedido)
                .NotEmpty().WithMessage("Informe a data do pedido!")
                .NotNull().WithMessage("Informe a data do pedido!");

            RuleFor(c => c.Quantidade)
               .NotEmpty().WithMessage("Informe a quantidade!")
               .NotNull().WithMessage("Informe a quantidade!")
               .LessThan(0.0).WithMessage("A quantidade deve ser igual ou maior do que zero!");

            RuleFor(c => c.Desconto)
               .NotEmpty().WithMessage("Informe o desconto!")
               .NotNull().WithMessage("Informe o desconto!")
               .LessThan(0.0).WithMessage("O desconto deve ser igual ou maior do que zero!");

            RuleFor(c => c.TaxaFrete)
               .NotEmpty().WithMessage("Informe o valor do frete!")
               .NotNull().WithMessage("Informe o valor do frete!")
               .LessThan(0.0).WithMessage("O valor do frete deve ser igual ou maior do que zero!");

            RuleFor(c => c.RestauranteId)
               .NotEmpty().WithMessage("Informe o restaurante!")
               .NotNull().WithMessage("Informe o restaurante!");

            RuleFor(c => c.ProdutoId)
               .NotEmpty().WithMessage("Informe o produto!")
               .NotNull().WithMessage("Informe o produto!");
        }
    }
}
