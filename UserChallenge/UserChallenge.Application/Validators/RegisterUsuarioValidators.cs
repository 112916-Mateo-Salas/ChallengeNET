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
    public class RegisterUsuarioValidators : AbstractValidator<RegisterUsuario>
    {
        public RegisterUsuarioValidators()
        {
            RuleFor(u => u.Nombre).NombreRules();
            RuleFor(u => u.Email).EmailRules();
            When(x => x.Domicilio != null, () =>
            {
                RuleFor(u => u.Domicilio).SetValidator(new CreateDomicilioValidators());
            });
        }
    }
}
