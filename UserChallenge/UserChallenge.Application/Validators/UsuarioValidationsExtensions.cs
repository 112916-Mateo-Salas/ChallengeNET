using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Application.Validators
{
    public static class UsuarioValidationsExtensions
    {
        public static IRuleBuilderOptions<T, string> NombreRules<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotEmpty().WithMessage("El nombre es obligatorio")
                       .MaximumLength(100).WithMessage("Máx. 100 caracteres");
        }

        public static IRuleBuilderOptions<T, string> EmailRules<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("Formato de email inválido")
                .MaximumLength(250).WithMessage("Máx. 250 caracteres");
        }
    }
}
