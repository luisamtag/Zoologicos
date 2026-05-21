using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IEntradasNegocio
    {
        List<Entradas> Listar();

        Entradas Guardar(Entradas entidad);

        Entradas Modificar(Entradas entidad);

        bool Borrar(int id);

    }
}