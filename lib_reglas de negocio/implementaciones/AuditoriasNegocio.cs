using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;

namespace Zoologicos_libreria.implementaciones
{
    public class AuditoriasNegocio : IAuditoriasNegocio
    {
        private IConexion? iConexion;

        public List<Auditorias> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Auditorias!.ToList();
        }

        public List<Auditorias> ListarPorTabla(string tabla)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Auditorias!
                .Where(a => a.Tabla == tabla)
                .ToList();
        }
    }
}
