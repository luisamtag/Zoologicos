using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IZoologicosNegocio
    {
        List<Zoologicos> Listar();

        Zoologicos Guardar(Zoologicos entidad);

        Zoologicos Modificar(Zoologicos entidad);

        bool Borrar(int id);
        //Zoologicos Borrar(Zoologicos entidad);
    }
}