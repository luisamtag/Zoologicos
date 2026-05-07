

using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Diagnosticos
    {
        public int Id { get; set; }

        public DateTime FechaDiagnostico { get; set; }
        public DateTime? FechaCura { get; set; } // NULL en SQL

        // 🔗 FKs
        public int AnimalId { get; set; }
        public int EnfermedadId { get; set; }
        public int VeterinarioId { get; set; }

        // 🔗 Navegación
     

        [ForeignKey("AnimalId")] public  Animales? Animal { get; set; }

        [ForeignKey("EnfermedadId")] public  Enfermedades? Enfermedad { get; set; }

        [ForeignKey("VeterinarioId")] public  Veterinarios? Veterinario { get; set; }
    }
}
