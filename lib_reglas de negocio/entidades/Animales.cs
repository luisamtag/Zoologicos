
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Animales
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Naturaleza { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }

        public string? Alimentacion { get; set; }
        public string Genero { get; set; } = null!;
        // 🔗 FKs
        public int EspecieId { get; set; }
        public int JaulaId { get; set; }

        // 🔗 Navegación
        
        [ForeignKey("EspecieId")] public Especies? Especie { get; set; }
        [ForeignKey("JaulaId")] public Jaulas? Jaula { get; set; }
    }
}
