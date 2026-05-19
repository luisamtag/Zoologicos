using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IVeterinariosNegocio
    {
        List<Veterinarios> Listar();

        Veterinarios Guardar(Veterinarios entidad);

        Veterinarios Modificar(Veterinarios entidad);

        bool Borrar(int id);

    }
}