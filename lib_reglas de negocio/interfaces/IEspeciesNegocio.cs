using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IEspeciesNegocio
    {
        List<Especies> Listar();

        Especies Guardar(Especies entidad);

        Especies Modificar(Especies entidad);

        bool Borrar(int id);
    }
}