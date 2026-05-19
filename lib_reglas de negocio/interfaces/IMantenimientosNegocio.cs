using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IMantenimientosNegocio
    {
        List<Mantenimientos> Listar();

        Mantenimientos Guardar(Mantenimientos entidad);

        Mantenimientos Modificar(Mantenimientos entidad);

        bool Borrar(int id);



    }
}