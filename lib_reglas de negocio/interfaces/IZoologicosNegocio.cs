using Zoologicos_libreria.entidades;

{
    public interface IZoologicosNegocio
    {
        List<Zoologicos> Listar();

        Zoologicos Guardar(Zoologicos entidad);

        Zoologicos Modificar(Zoologicos entidad);

        bool Borrar(int id);

    }
}