using Zoologicos_libreria.entidades;

{
    public interface IHabitatsNegocio
    {
        List<Habitats> Listar();

        Habitats Guardar(Habitats entidad);

        Habitats Modificar(Habitats entidad);

        bool Borrar(int id);
    }
}