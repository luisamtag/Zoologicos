using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IAuditoriasNegocio
    {
        List<Auditorias> Listar();
        List<Auditorias> ListarPorTabla(string tabla);
    }
}
