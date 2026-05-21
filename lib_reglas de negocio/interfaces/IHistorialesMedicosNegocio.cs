using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria.interfaces
{
    public interface IHistorialesMedicosNegocio
    {
        List<HistorialesMedicos> Listar();

        HistorialesMedicos Guardar(HistorialesMedicos entidad);

        HistorialesMedicos Modificar(HistorialesMedicos entidad);

        bool Borrar(int id);

    }
}