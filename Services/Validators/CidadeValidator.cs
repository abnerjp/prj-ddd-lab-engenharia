using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class CidadeValidator : AbstractValidator<Cidade>
    {

        public CidadeValidator() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o nome da cidade!")
                .NotNull().WithMessage("Informe o nome da cidade!");

            RuleFor(c => c.PaisId)
                .NotEmpty().WithMessage("Informe o país!")
                .NotNull().WithMessage("Informe o país!");
        }


    }
}
