using Zoologicos_libreria.entidades;

{
    public interface IAreasNegocio
    {

        List<Areas> Listar();

        Areas Guardar(Areas entidad);

        Areas Modificar(Areas entidad);

        bool Borrar(int id);
    }

}
