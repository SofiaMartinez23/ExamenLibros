using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenLibros.Models
{
    [Table("LIBROS")]
    public class Libro
    {
        [Key]
        [Column("IdLibro")]
        public int IdLibro { get; set; }

        [Column("Titulo")]
        public string Titulo { get; set; }

        [Column("Autor")]
        public string Autor { get; set; }

        [Column("Editorial")]
        public string Editorial { get; set; }

        [Column("Portada")]
        public string Portada { get; set; }

        [Column("Resumen")]
        public string Resumen { get; set; }

        [Column("Precio")]
        public int Precio { get; set; }

        [Column("IdGenero")]
        public int IdGenero { get; set; }
    }
}
