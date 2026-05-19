using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IEntrenadoresNegocio
    {
        List<Entrenadores> Listar();

        Entrenadores Guardar(Entrenadores entidad);

        Entrenadores Modificar(Entrenadores entidad);

        bool Borrar(int id);
    }
}