using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zoologicos_libreria.entidades
{
    public class Auditorias
    {
        [Key] public int IdAuditorias { get; set; }
        public string Tabla { get; set; } = string.Empty; // Ej: "Bombones"
        public string Accion { get; set; } = string.Empty; // Ej: "Added", "Modified", "Deleted"
        public string Datos { get; set; } = string.Empty; // Guardaremos un JSON con los valores
        public DateTime Fecha { get; set; }

        public string Usuario { get; set; }
    }
}
