using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IEnfermedadesNegocio
    {

        List<Enfermedades> Listar();

        Enfermedades Guardar(Enfermedades entidad);

        Enfermedades Modificar(Enfermedades entidad);

        bool Borrar(int id);


    }
}