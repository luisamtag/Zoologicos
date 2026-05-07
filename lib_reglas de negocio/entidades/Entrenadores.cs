

using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    [Table("Entrenadores")]
    public class Entrenadores : Empleados
    {
        public string Especialidad { get; set; } = null!;
        public string TipoEntrenamiento { get; set; } = null!;
    }
}
