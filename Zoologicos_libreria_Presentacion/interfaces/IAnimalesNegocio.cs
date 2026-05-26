using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IAnimalesNegocio
    {
        List<Animales> Listar();

        Animales Guardar(Animales entidad);

        Animales Modificar(Animales entidad);

        bool Borrar(int id);
        //Animales Borrar(Animales entidad);
    }
}