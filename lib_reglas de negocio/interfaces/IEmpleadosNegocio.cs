using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IEmpleadosNegocio
    {
        List<Empleados> Listar();

        Empleados Guardar(Empleados entidad);

        Empleados Modificar(Empleados entidad);

        bool Borrar(int id);

    }
}