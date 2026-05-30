using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IAuditoriasNegocio
    {
        List<Auditorias> Listar();
        List<Auditorias> ListarPorTabla(string tabla);
    }
}
