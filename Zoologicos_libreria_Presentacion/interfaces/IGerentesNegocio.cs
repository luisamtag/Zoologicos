using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IGerentesNegocio
    {
        List<Gerentes> Listar();

        Gerentes Guardar(Gerentes entidad);

        Gerentes Modificar(Gerentes entidad);

        bool Borrar(int id);
        //Gerentes Borrar(Gerentes entidad);
    }
}