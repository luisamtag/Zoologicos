using Zoologicos_libreria.entidades;

{
    public interface IEnfermedadesNegocio
    {

        List<Enfermedades> Listar();

        Enfermedades Guardar(Enfermedades entidad);

        Enfermedades Modificar(Enfermedades entidad);

        bool Borrar(int id);


    }
}