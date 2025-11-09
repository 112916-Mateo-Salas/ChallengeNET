using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Domain.Model
{
    public class VehiculoImagen
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; } = default!;

        public int Portada { get; set; }

        public bool Eliminado { get; set;}

        public DateTime FechaCreacion { get; set; }
    }
}
