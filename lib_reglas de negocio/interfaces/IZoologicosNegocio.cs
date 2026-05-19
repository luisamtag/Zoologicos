using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IZoologicosNegocio
    {
        List<Zoologicos> Listar();

        Zoologicos Guardar(Zoologicos entidad);

        Zoologicos Modificar(Zoologicos entidad);

        bool Borrar(int id);

    }
}