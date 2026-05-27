using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IVeterinariosNegocio
    {
        List<Veterinarios> Listar();

        Veterinarios Guardar(Veterinarios entidad);

        Veterinarios Modificar(Veterinarios entidad);

        bool Borrar(int id);
        //Veterinarios Borrar(Veterinarios entidad);
    }
}