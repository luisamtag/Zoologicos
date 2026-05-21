using Zoologicos_libreria.entidades;

{
    public interface IEspeciesNegocio
    {
        List<Especies> Listar();

        Especies Guardar(Especies entidad);

        Especies Modificar(Especies entidad);

        bool Borrar(int id);
    }
}