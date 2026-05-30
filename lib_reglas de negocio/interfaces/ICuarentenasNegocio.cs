using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface ICuarentenasNegocio
    {
        List<Cuarentenas> Listar();

        Cuarentenas Guardar(Cuarentenas entidad);

        Cuarentenas Modificar(Cuarentenas entidad);

        bool Borrar(int id);
    }
}
