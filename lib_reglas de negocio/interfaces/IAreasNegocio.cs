using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IAreasNegocio
    {

        List<Areas> Listar();

        Areas Guardar(Areas entidad);

        Areas Modificar(Areas entidad);

        bool Borrar(int id);
    }

}
