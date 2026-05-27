using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IInventariosNegocio
    {
        List<Inventarios> Listar();

        Inventarios Guardar(Inventarios entidad);

        Inventarios Modificar(Inventarios entidad);

        bool Borrar(int id);
        //Inventarios Borrar(Inventarios entidad);
    }
}