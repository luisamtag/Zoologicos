


using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Empleados
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;

        public decimal Salario { get; set; }
        public DateTime FechaContratacion { get; set; }

        // 🔗 FK
        public int ZoologicoId { get; set; }

        // 🔗 Navegación
        
        [ForeignKey("ZoologicoId")] public Zoologicos? Zoologico { get; set; }
    }
}
