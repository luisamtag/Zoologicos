using Zoologicos_libreria.entidades;

{
    public interface IVeterinariosNegocio
    {
        List<Veterinarios> Listar();

        Veterinarios Guardar(Veterinarios entidad);

        Veterinarios Modificar(Veterinarios entidad);

        bool Borrar(int id);

    }
}