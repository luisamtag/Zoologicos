

using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    [Table("Veterinarios")]
    public class Veterinarios : Empleados
    {
        public string Especialidad { get; set; } = null!;
        public int AñosExperiencia { get; set; }
    }
}
