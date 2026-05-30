

using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Alimentaciones
    {
        public int Id { get; set; }

        // 🔗 FK
        public int AnimalId { get; set; }

        public string TipoDieta { get; set; } = null!;
        public decimal CantidadDiaria { get; set; }

        // 🔗 Navegación
        
        [ForeignKey("AnimalId")] public Animales? Animal { get; set; }
    }
}
