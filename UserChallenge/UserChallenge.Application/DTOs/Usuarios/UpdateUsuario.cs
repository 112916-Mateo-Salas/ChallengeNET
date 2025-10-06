using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Domicilios;

namespace UserChallenge.Application.DTOs.Usuarios
{
    [ExcludeFromCodeCoverage]
    public class UpdateUsuario
    {
        public string? Nombre { get; set; }

        public string? Email { get; set; }

        public DomicilioDto? Domicilio { get; set; }
    }
}
