using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChallenge.Domain.Model
{
    public class MarcaAuto
    {
        public int Id { get; set; }

        public string Marca {  get; set; }

        public bool Activo { get; set; }

        public string Tipo { get; set; } //Puede Ser Nacional o Importado

        public string Clase { get; set; } //Clase alta, media, baja, Deportivos, Deluxe


        public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    }
}
