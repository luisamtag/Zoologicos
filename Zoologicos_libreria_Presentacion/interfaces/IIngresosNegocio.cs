using Zoologicos_libreria.entidades;

namespace Zoologicos_libreria_Presentacion.interfaces
{
    public interface IIngresosNegocio
    {
        List<Ingresos> Listar();

        Ingresos Guardar(Ingresos entidad);

        Ingresos Modificar(Ingresos entidad);

        bool Borrar(int id);
    }
}
