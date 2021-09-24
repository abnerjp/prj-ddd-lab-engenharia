using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class PaisValidator : AbstractValidator<Pais>
    {

        public PaisValidator() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o nome do país!")
                .NotNull().WithMessage("Informe o nome do país!");

            RuleFor(c => c.Sigla)
                .NotEmpty().WithMessage("Informe a sigla do país!")
                .NotNull().WithMessage("Informe a sigla do país!");
        }
    }
}
