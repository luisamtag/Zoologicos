using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Jaulas
    {
        public int Id { get; set; }

        public DateTime FechaCompra { get; set; }

        // 🔗 FK
        public int HabitatId { get; set; }

        [ForeignKey("HabitatId")] public Habitats? Habitat { get; set; }// 🔗 Navegación
       
    }
}