using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IEmpleadosNegocio
    {
        List<Empleados> Listar();

        Empleados Guardar(Empleados entidad);

        Empleados Modificar(Empleados entidad);

        bool Borrar(int id);

    }
}