using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Ingresos
    {
        public int Id { get; set; }

        // 🔗 FKs
        public int AnimalId { get; set; }
        public int ZoologicoId { get; set; }

        public DateTime FechaIngreso { get; set; }
        public string TipoIngreso { get; set; } = null!;  // Donación, Rescate, Traslado
        public string Procedencia { get; set; } = null!;  // País, región o institución
        public string Estado { get; set; } = null!;       // Pendiente, Aceptado, Rechazado
        public string? Observaciones { get; set; }

        // 🔗 Navegación
        [ForeignKey("AnimalId")] public Animales? Animal { get; set; }
        [ForeignKey("ZoologicoId")] public Zoologicos? Zoologico { get; set; }
    }
}
