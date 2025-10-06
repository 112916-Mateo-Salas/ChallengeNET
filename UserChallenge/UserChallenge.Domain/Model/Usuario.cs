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
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]

        public string Email { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Domicilio Domicilio { get; set; }
    }
}
