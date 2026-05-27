using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IReproduccionesNegocio
    {
        List<Reproducciones> Listar();

        Reproducciones Guardar(Reproducciones entidad);

        Reproducciones Modificar(Reproducciones entidad);

        bool Borrar(int id);
    }
}
