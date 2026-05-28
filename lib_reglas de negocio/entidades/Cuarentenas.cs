using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Cuarentenas
    {
        public int Id { get; set; }

        // 🔗 FKs
        public int AnimalId { get; set; }
        public int VeterinarioId { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }        // NULL mientras está activa

        public string Motivo { get; set; } = null!;    // Animal Nuevo, Enfermedad, Adaptación
        public string Estado { get; set; } = null!;    // Activa, Finalizada
        public string? Observaciones { get; set; }

        // 🔗 Navegación
        [ForeignKey("AnimalId")] public Animales? Animal { get; set; }
        [ForeignKey("VeterinarioId")] public Veterinarios? Veterinario { get; set; }
    }
}
