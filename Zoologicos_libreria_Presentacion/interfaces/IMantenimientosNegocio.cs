using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IMantenimientosNegocio
    {
        List<Mantenimientos> Listar();

        Mantenimientos Guardar(Mantenimientos entidad);

        Mantenimientos Modificar(Mantenimientos entidad);

        bool Borrar(int id);
        //Mantenimientos Borrar(Mantenimientos entidad);
    }
}