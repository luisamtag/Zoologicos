using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IZonasPublicasNegocio
    {

        List<ZonasPublicas> Listar();

        ZonasPublicas Guardar(ZonasPublicas entidad);

        ZonasPublicas Modificar(ZonasPublicas entidad);

        bool Borrar(int id);


    }
}