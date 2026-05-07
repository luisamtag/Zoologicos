using System.ComponentModel.DataAnnotations.Schema;

namespace Zoologicos_libreria.entidades
{
    public class Habitats
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public int CapacidadMaxima { get; set; }
        public string Estado { get; set; } = null!;

        
        public int ZoologicoId { get; set; }

        
       

        [ForeignKey("ZoologicoId")] public Zoologicos? Zoologico { get; set; }
        
    }
}