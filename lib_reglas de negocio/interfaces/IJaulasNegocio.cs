using Zoologicos_libreria.entidades;

{
    public interface IJaulasNegocio
    {
        List<Jaulas> Listar();

        Jaulas Guardar(Jaulas entidad);

        Jaulas Modificar(Jaulas entidad);

        bool Borrar(int id);

    }
}