using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Domain.Model
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }


        public Usuario UsuarioCreador { get; set; }
        public DateTime FechaCreacion { get; set; }

        public ICollection<Articulo> Articulos { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    }
}
