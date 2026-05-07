
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zoologicos_libreria.entidades
{
    public class Inventarios
    {
        public int Id { get; set; }

        // 🔗 FK
        public int ZoologicoId { get; set; }

        public string NombreItem { get; set; } = null!;
        public string TipoItem { get; set; } = null!;
        public decimal CantidadDisponible { get; set; }
        public DateTime? FechaVencimiento { get; set; } // NULL en SQL

        // 🔗 Navegación
      
        [ForeignKey("ZoologicoId")] public Zoologicos? Zoologico { get; set; }
    }
}
