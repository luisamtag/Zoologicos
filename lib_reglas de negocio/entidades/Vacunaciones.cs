

using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Vacunaciones
    {
        public int Id { get; set; }

        // 🔗 FKs
        public int AnimalId { get; set; }
        public int VeterinarioId { get; set; }

        public string NombreVacuna { get; set; } = null!;
        public string Dosis { get; set; } = null!;

        public DateTime FechaAplicacion { get; set; }
        public DateTime? FechaProximaDosis { get; set; } // NULL en SQL

        // 🔗 Navegación
       
        [ForeignKey("AnimalId")] public Animales? Animal { get; set; }
        [ForeignKey("VeterinarioId")] public Veterinarios? Veterinario { get; set; }
    }
}
