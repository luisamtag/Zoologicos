using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IVacunacionesNegocio
    {
        List<Vacunaciones> Listar();

        Vacunaciones Guardar(Vacunaciones entidad);

        Vacunaciones Modificar(Vacunaciones entidad);

        bool Borrar(int id);
        //Vacunaciones Borrar(Vacunaciones entidad);
    }
}