using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface ICuarentenaNegocio
    {
        List<Cuarentena> Listar();

        Cuarentena Guardar(Cuarentena entidad);

        Cuarentena Modificar(Cuarentena entidad);

        bool Borrar(int id);
    }
}
