using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IAlimentacionesNegocio
    {
        List<Alimentaciones> Listar();

        Alimentaciones Guardar(Alimentaciones entidad);

        Alimentaciones Modificar(Alimentaciones entidad);

        bool Borrar(int id);
        //Alimentaciones Borrar(Alimentaciones entidad);
    }
}