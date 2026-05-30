using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zoologicos_libreria.entidades
{
    [Table("PersonalAseo")]
    public class PersonalAseo : Empleados
    {
        public string ZonaAsignada { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public string ProductosAsignados { get; set; } = null!;
    }

}
