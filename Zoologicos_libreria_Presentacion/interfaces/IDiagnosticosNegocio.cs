using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IDiagnosticosNegocio
    {
        List<Diagnosticos> Listar();

        Diagnosticos Guardar(Diagnosticos entidad);

        Diagnosticos Modificar(Diagnosticos entidad);

        bool Borrar(int id);
        //Diagnosticos Borrar(Diagnosticos entidad);
    }
}