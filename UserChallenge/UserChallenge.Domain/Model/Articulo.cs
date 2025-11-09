using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Domain.Model
{
    public class Articulo
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        // Relación con Categoria
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = default!;

        // Relación con Marca
        public int MarcaId { get; set; }
        public Marca Marca { get; set; } = default!;

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        // Usuario creador (si lo querés relacionar)
        public int UsuarioCreadorId { get; set; }   // FK
        public Usuario UsuarioCreador { get; set; } = default!;

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; } = true;
    }

}
