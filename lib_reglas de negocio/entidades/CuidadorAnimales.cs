

using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    [Table("CuidadorAlimentaciones")]
    public class CuidadorAlimentaciones : Empleados
    {
        public int? EspecieId { get; set; }

        public string Turno { get; set; } = null!;
        public int AñosExperiencia { get; set; }

        // 🔗 Navegación (opcional)
        

        [ForeignKey("EspecieId")] public Especies? Especie { get; set; }
    }
}
