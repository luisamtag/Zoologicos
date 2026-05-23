namespace Zoologicos_libreria_Presentacion.interfaces

{
    public interface IComunicaciones
    {
        Task<Dictionary<string, object>> Ejecutar(Dictionary<string, object> datos);
    }
}