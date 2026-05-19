using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IDiagnosticosNegocio
    {
        List<Diagnosticos> Listar();

        Diagnosticos Guardar(Diagnosticos entidad);

        Diagnosticos Modificar(Diagnosticos entidad);

        bool Borrar(int id);

    }
}