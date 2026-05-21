using Zoologicos_libreria.entidades;

{
    public interface ICuidadorAnimalesNegocio
    {
        List<CuidadorAnimales> Listar();

        CuidadorAnimales Guardar(CuidadorAnimales entidad);

        CuidadorAnimales Modificar(CuidadorAnimales entidad);

        bool Borrar(int id);
    }

}