using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserChallenge.Domain.Model
{
    [ExcludeFromCodeCoverage]
    [Table("Domicilio")]
    public class Domicilio
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        [ForeignKey("usuario")]
        [JsonIgnore]
        public Usuario Usuario { get; set; }

        public string Calle { get; set; }

        public string Numero { get; set; }

        public string Provincia { get; set; }

        public string Ciudad { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
