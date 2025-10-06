using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Application.DTOs.Domicilios
{
    [ExcludeFromCodeCoverage]
    public class DomicilioDto
    {

        public string Calle { get; set; }

        public string? Numero { get; set; }

        public string Provincia { get; set; }

        public string Ciudad { get; set; }
    }
}
