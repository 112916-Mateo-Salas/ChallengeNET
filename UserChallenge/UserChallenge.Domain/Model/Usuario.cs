using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Domain.Model
{
    [ExcludeFromCodeCoverage]
    [Table("Usuario")]
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Genero { get; set; }

        public int Edad { get; set; }

        public string Email { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Domicilio Domicilio { get; set; }

        public string Contraseña { get; set; }

        // FK a Rol
        public int RolId { get; set; }               // <-- necesario
        public Rol Rol { get; set; } = default!;     // navegación

        // Navegación 1:N: artículos creados (si los usás)
        public ICollection<Articulo> ArticulosCreados { get; set; } = new List<Articulo>();
        public ICollection<Vehiculo> VehiculosCreados { get; set; } = new List<Vehiculo>();

    }
}
