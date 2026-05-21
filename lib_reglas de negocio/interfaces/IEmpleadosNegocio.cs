using Zoologicos_libreria.entidades;

{
    public interface IEmpleadosNegocio
    {
        List<Empleados> Listar();

        Empleados Guardar(Empleados entidad);

        Empleados Modificar(Empleados entidad);

        bool Borrar(int id);

    }
}