using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface ICuidadorAnimalesNegocio
    {
        List<CuidadorAnimales> Listar();

        CuidadorAnimales Guardar(CuidadorAnimales entidad);

        CuidadorAnimales Modificar(CuidadorAnimales entidad);

        bool Borrar(int id);
    }

}