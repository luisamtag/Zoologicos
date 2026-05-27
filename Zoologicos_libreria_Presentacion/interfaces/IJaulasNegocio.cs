using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IJaulasNegocio
    {
        List<Jaulas> Listar();

        Jaulas Guardar(Jaulas entidad);

        Jaulas Modificar(Jaulas entidad);

        bool Borrar(int id);
        //Jaulas Borrar(Jaulas entidad);
    }
}