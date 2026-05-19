using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IGerentesNegocio
    {
        List<Gerentes> Listar();

        Gerentes Guardar(Gerentes entidad);

        Gerentes Modificar(Gerentes entidad);

        bool Borrar(int id);
    }
}