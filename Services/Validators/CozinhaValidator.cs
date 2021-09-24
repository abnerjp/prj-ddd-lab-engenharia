using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class CozinhaValidator : AbstractValidator<Cozinha>
    {

        public CozinhaValidator() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o nome/especialidade da cozinha!")
                .NotNull().WithMessage("Informe o nome/especialidade da cozinha!");

        }
    }
}
