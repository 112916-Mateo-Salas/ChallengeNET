using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Application.DTOs.Usuarios
{
    [ExcludeFromCodeCoverage]
    public class FiltersUsuario
    {
        public string? Nombre { get; set; }

        public string? Provincia { get; set; }

        public string? Ciudad { get; set; }
    }
}
