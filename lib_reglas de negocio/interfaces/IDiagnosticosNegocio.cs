using Zoologicos_libreria.entidades;

{
    public interface IDiagnosticosNegocio
    {
        List<Diagnosticos> Listar();

        Diagnosticos Guardar(Diagnosticos entidad);

        Diagnosticos Modificar(Diagnosticos entidad);

        bool Borrar(int id);

    }
}