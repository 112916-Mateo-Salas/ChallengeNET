using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Domicilios;

namespace UserChallenge.Application.Validators
{
    public class CreateDomicilioValidators : AbstractValidator<DomicilioDto>
    {
        public CreateDomicilioValidators()
        {
            RuleFor(d => d.Calle).CalleRules();
            RuleFor(d => d.Provincia).ProvinciaRules(); 
            RuleFor(d => d.Ciudad).CiudadRules();
        }
    }
}
