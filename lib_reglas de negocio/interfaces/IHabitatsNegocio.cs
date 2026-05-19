using Zoologicos_libreria.entidades;

namespace Zoologicos_servicios.Controllers
{
    public interface IHabitatsNegocio
    {
        List<Habitats> Listar();

        Habitats Guardar(Habitats entidad);

        Habitats Modificar(Habitats entidad);

        bool Borrar(int id);
    }
}