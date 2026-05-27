using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IVisitantesNegocio
    {
        List<Visitantes> Listar();

        Visitantes Guardar(Visitantes entidad);

        Visitantes Modificar(Visitantes entidad);

        bool Borrar(int id);
        //Visitantes Borrar(Visitantes entidad);
    }
}