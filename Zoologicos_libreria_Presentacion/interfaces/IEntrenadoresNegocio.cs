using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IEntrenadoresNegocio
    {
        List<Entrenadores> Listar();

        Entrenadores Guardar(Entrenadores entidad);

        Entrenadores Modificar(Entrenadores entidad);

        bool Borrar(int id);
        //Entrenadores Borrar(Entrenadores entidad);
    }
}