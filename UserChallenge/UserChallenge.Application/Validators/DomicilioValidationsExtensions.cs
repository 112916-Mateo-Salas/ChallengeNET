using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Application.Validators
{
    public static class DomicilioValidationsExtensions
    {
        public static IRuleBuilderOptions<T, string> CalleRules<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty().WithMessage("La calle es obligatoria.")
                .MaximumLength(150).WithMessage("La calle no puede superar 150 caracteres.");
        }

        public static IRuleBuilderOptions<T, string> ProvinciaRules<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty().WithMessage("La provincia es obligatoria.")
                .MaximumLength(100).WithMessage("La provincia no puede superar 100 caracteres.");
        }

        public static IRuleBuilderOptions<T, string> CiudadRules<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty().WithMessage("La ciudad es obligatoria.")
                .MaximumLength(100).WithMessage("La ciudad no puede superar 100 caracteres.");
        }
    }
}
