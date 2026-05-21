using Zoologicos_libreria.entidades;

{
    public interface IEntradasNegocio
    {
        List<Entradas> Listar();

        Entradas Guardar(Entradas entidad);

        Entradas Modificar(Entradas entidad);

        bool Borrar(int id);

    }
}