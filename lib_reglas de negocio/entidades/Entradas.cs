

using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Entradas
    {
        public int Id { get; set; }

        // 🔗 FK
        public int VisitanteId { get; set; }

        public DateTime FechaVisita { get; set; }
        public string TipoEntrada { get; set; } = null!;
        public decimal ValorPagado { get; set; }

        // 🔗 Navegación
       
        [ForeignKey("VisitanteId")] public Visitantes? Visitante { get; set; }
    }
}
