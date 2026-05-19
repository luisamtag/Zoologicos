using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IJaulasNegocio
    {
        List<Jaulas> Listar();

        Jaulas Guardar(Jaulas entidad);

        Jaulas Modificar(Jaulas entidad);

        bool Borrar(int id);

    }
}