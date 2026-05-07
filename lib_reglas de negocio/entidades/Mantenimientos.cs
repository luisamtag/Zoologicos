

using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Mantenimientos
    {
        public int Id { get; set; }

        // 🔗 FKs obligatorias
        public int AreaId { get; set; }
        public int EmpleadoResponsableId { get; set; }

        public DateTime FechaReporte { get; set; }
        public DateTime FechaProgramada { get; set; }
        public string Estado { get; set; } = null!;

        // 🔗 Navegación
       
        [ForeignKey("AreaId")] public Areas? Area { get; set; }
        [ForeignKey("EmpleadoResponsableId")] public Empleados? Empleado { get; set; }
    }
}
