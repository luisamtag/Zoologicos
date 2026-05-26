using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface ICuidadorAnimalesNegocio
    {
        List<CuidadorAnimales> Listar();

        CuidadorAnimales Guardar(CuidadorAnimales entidad);

        CuidadorAnimales Modificar(CuidadorAnimales entidad);

        bool Borrar(int id);
        //CuidadorAnimales Borrar(CuidadorAnimales entidad);
    }
}