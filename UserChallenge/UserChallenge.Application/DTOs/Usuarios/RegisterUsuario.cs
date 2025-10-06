using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Domicilios;
using UserChallenge.Domain.Model;

namespace UserChallenge.Application.DTOs.Usuarios
{
    [ExcludeFromCodeCoverage]
    public class RegisterUsuario
    {
        public string Nombre { get; set; }

        public string Email { get; set; }

        public DomicilioDto? Domicilio { get; set; }
    }
}
