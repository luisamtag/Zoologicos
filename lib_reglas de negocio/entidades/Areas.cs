
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Areas
    {
        public int Id { get; set; }

        // 🔗 FKs opcionales
        public int? HabitatId { get; set; }
        public int? JaulaId { get; set; }
        public int? ZonaPublicaId { get; set; }

        // 🔗 Navegación opcional
       
        [ForeignKey("HabitatId")] public Habitats? Habitat { get; set; }
        [ForeignKey("JaulaId")] public Jaulas? Jaula { get; set; }
        [ForeignKey("ZonaPublicaId")] public ZonasPublicas? ZonaPublica { get; set; }
    }
}
