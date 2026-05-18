


using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class HistorialesMedicos
    {
        public int Id { get; set; }

        // 🔗 FK
        public int AnimalId { get; set; }

        public string Tratamiento { get; set; } = null!;
        public string Medicamento { get; set; } = null!;
        public string Dosis { get; set; } = null!;

        public DateTime FechaControl { get; set; }
        public string EstadoActual { get; set; } = null!;

        // 🔗 Navegación
        
        [ForeignKey("AnimalId")] public Alimentaciones? Animal { get; set; }
    }
}
