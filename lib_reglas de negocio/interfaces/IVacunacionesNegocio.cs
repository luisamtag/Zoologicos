using Zoologicos_libreria.entidades;

{
    public interface IVacunacionesNegocio
    {
        List<Vacunaciones> Listar();

        Vacunaciones Guardar(Vacunaciones entidad);

        Vacunaciones Modificar(Vacunaciones entidad);

        bool Borrar(int id);

    }
}