using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IReproduccionesNegocio
    {
        List<Reproducciones> Listar();

        Reproducciones Guardar(Reproducciones entidad);

        Reproducciones Modificar(Reproducciones entidad);

        bool Borrar(int id);
        //Reproducciones Borrar(Reproducciones entidad);
    }
}