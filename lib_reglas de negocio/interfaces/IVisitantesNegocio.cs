using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IVisitantesNegocio
    {
        List<Visitantes> Listar();

        Visitantes Guardar(Visitantes entidad);

        Visitantes Modificar(Visitantes entidad);

        bool Borrar(int id);
    }
}