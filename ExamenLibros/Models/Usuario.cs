using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenLibros.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Apellidos")]
        public string Apellidos { get; set; }

        [Column("Email")]
        public string Email { get; set; }
        [Column("Pass")]
        public string Pass { get; set; }
        [Column("Foto")]
        public string Foto { get; set; }
    }
}
