using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Usuarios;

namespace UserChallenge.Application.Validators
{
    public class UpdateUsuarioValidators : AbstractValidator<UpdateUsuario>
    {
        public UpdateUsuarioValidators()
        {
            When(x => x.Nombre != null, () =>
            {
                RuleFor(x => x.Nombre!).NombreRules();
            });

            When(x => x.Email != null, () =>
            {
                RuleFor(x => x.Email!).EmailRules();
            });

            When(x => x.Domicilio != null, () =>
            {
                RuleFor(x => x.Domicilio).SetValidator(new CreateDomicilioValidators());
            });
        }
    }
}
