using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenLibros.Models
{

    [Table("VISTAPEDIDOS")]

    public class VistaPedido
    {
        [Key]
        [Column("IDVISTAPEDIDOS")]
        public int IdVistaPedido { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("Nombre")]
        public string NombreUser { get; set; }

        [Column("Apellidos")]
        public string Apellidos { get; set; }
        [Column("Titulo")]
        public string Titulo { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
        [Column("Portada")]
        public string Portada { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }

        [Column("PRECIOFINAL")]
        public int PrecioFinal { get; set; }
    }
}
