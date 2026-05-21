using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IReproduccionNegocio
    {
        List<Reproduccion> Listar();

        Reproduccion Guardar(Reproduccion entidad);

        Reproduccion Modificar(Reproduccion entidad);

        bool Borrar(int id);
    }
}
