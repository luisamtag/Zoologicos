using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IPersonalAseoNegocio
    {
        List<PersonalAseo> Listar();

        PersonalAseo Guardar(PersonalAseo entidad);

        PersonalAseo Modificar(PersonalAseo entidad);

        bool Borrar(int id);
    }
}