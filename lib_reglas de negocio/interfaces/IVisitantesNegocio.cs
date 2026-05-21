using Zoologicos_libreria.entidades;

{
    public interface IVisitantesNegocio
    {
        List<Visitantes> Listar();

        Visitantes Guardar(Visitantes entidad);

        Visitantes Modificar(Visitantes entidad);

        bool Borrar(int id);
    }
}