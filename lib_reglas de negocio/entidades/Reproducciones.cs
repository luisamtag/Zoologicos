
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Reproducciones
    {
        public int Id { get; set; }
        public int AnimalMadreId { get; set; }      // FK → Animales
        public int AnimalPadreId { get; set; }      // FK → Animales
        public DateTime FechaAppariamiento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int CantidadCrias { get; set; }
        public string Metodo { get; set; } = null!;      // Natural, Asistida, In vitro
        public string Estado { get; set; } = null!;       // En proceso, Exitosa, Fallida
        public string? Observaciones { get; set; }

        [ForeignKey("AnimalMadreId")] public Animales? AnimalMadre { get; set; }
        [ForeignKey("AnimalPadreId")] public Animales? AnimalPadre { get; set; }
    }
}
