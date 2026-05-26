using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IAreasNegocio
    {
        List<Areas> Listar();

        Areas Guardar(Areas entidad);

        Areas Modificar(Areas entidad);

        bool Borrar(int id);
        //Areas Borrar(Areas entidad);
    }
}