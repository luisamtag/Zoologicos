using Zoologicos_libreria.entidades;

{
    public interface IEntrenadoresNegocio
    {
        List<Entrenadores> Listar();

        Entrenadores Guardar(Entrenadores entidad);

        Entrenadores Modificar(Entrenadores entidad);

        bool Borrar(int id);
    }
}