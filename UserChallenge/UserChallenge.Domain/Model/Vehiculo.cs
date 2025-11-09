using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Domain.Model
{
    public class Vehiculo
    {
        public int Id { get; set; }

        // Información básica
        public MarcaAuto MarcaAuto { get; set; } 
        public int MarcaAutoId { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Patente { get; set; } = string.Empty;
        public int Año { get; set; }
        public string Version { get; set; } = string.Empty;    // Ej: "GLI", "Limited", etc.

        // Especificaciones técnicas
        public string Combustible { get; set; } = string.Empty;  // Gasolina, Diesel, Híbrido, Eléctrico
        public string Transmision { get; set; } = string.Empty;  // Manual, Automática
        public int Kilometraje { get; set; }                     // en km
        public int Puertas { get; set; }

        public string Tipo {  get; set; } 
        public int Asientos { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Traccion { get; set; } = string.Empty;     // Delantera, Trasera, 4x4

        // Información adicional
        public string Condicion { get; set; }       // Nuevo, Usado
        public string Estado { get; set; } = string.Empty;     // Excelente, Bueno, Regular
        public decimal Precio { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        // Relación con usuario que lo publica
        public int UsuarioCreadorId { get; set; }
        public Usuario UsuarioCreador { get; set; } = default!;

        // Imagenes del vehículo
        public ICollection<VehiculoImagen> Imagenes { get; set; } = new List<VehiculoImagen>();

        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
