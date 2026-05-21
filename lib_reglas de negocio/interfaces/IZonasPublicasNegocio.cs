using Zoologicos_libreria.entidades;

{
    public interface IZonasPublicasNegocio
    {

        List<ZonasPublicas> Listar();

        ZonasPublicas Guardar(ZonasPublicas entidad);

        ZonasPublicas Modificar(ZonasPublicas entidad);

        bool Borrar(int id);


    }
}