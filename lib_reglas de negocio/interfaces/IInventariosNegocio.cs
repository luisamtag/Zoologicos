using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IInventariosNegocio
    {
        List<Inventarios> Listar();

        Inventarios Guardar(Inventarios entidad);

        Inventarios Modificar(Inventarios entidad);

        bool Borrar(int id);
    }
}