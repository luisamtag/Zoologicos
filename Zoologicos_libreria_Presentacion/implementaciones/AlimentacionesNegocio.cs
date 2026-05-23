using Newtonsoft.Json;
using Zoologicos_libreria.entidades;

using Zoologicos_libreria_Presentacion.Implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_libreria_Presentacion.implementaciones
{
    public class AlimentacionesNegocio : IAlimentacionesNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Alimentaciones> Listar()
        {
            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5144/Alimentaciones/Listar";

            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("No funciono");

            return JsonConvert.DeserializeObject<List<Alimentaciones>>(respuesta["Valor"].ToString()!)!;
        }

        public Alimentaciones Guardar(Alimentaciones entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.TipoDieta))
                throw new Exception("Falta informacion");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5144/Alimentaciones/Guardar";

            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (respuesta.ContainsKey("Valor"))
                throw new Exception("No funciono");

            return JsonConvert.DeserializeObject<Alimentaciones>(respuesta["Valor"].ToString()!)!;
        }

        public Alimentaciones Modificar(Alimentaciones entidad)
        {
            throw new NotImplementedException();
        }

        public bool Borrar(int id)
        {
            throw new NotImplementedException();
        }
    }
}