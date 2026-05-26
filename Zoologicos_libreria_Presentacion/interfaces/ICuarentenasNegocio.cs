using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface ICuarentenasNegocio
    {
        List<Cuarentenas> Listar();

        Cuarentenas Guardar(Cuarentenas entidad);

        Cuarentenas Modificar(Cuarentenas entidad);

        bool Borrar(int id);
        //Cuarentenas Borrar(Cuarentenas entidad);
    }
}