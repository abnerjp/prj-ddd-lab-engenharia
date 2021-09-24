using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class RestauranteValidator : AbstractValidator<Restaurante>
    {

        public RestauranteValidator() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o nome do restaurante!")
                .NotNull().WithMessage("Informe o nome do restaurante!");

            RuleFor(c => c.Logradouro)
               .NotEmpty().WithMessage("Informe o logradouro!")
               .NotNull().WithMessage("Informe o logradouro!");

            RuleFor(c => c.CozinhaId)
               .NotEmpty().WithMessage("Informe a cozinha!")
               .NotNull().WithMessage("Informe a cozinha!");

            RuleFor(c => c.CidadeId)
               .NotEmpty().WithMessage("Informe a cidade!")
               .NotNull().WithMessage("Informe a cidade!");

        }
    }
}
