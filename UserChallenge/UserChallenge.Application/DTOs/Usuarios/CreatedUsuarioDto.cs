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
    public class CreatedUsuarioDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public DateTime FechaCreacion { get; set; }

        public CreatedDomicilioDto? Domicilio { get; set; }
    }
}
