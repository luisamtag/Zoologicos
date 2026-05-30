


using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class ZonasPublicas
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Tipo { get; set; } = null!;

        // 🔗 FK
        public int ZoologicoId { get; set; }

        // 🔗 Navegación
        
        [ForeignKey("ZoologicoId")] public Zoologicos? Zoologico { get; set; }
    }
}
