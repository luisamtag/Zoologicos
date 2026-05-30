


using Newtonsoft.Json;
using Zoologicos_libreria.entidades;

using Zoologicos_libreria_Presentacion.Implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_libreria_Presentacion.implementaciones
{
    public class AreasNegocio : IAreasNegocio
    {
        private IComunicaciones? iComunicaciones;
        private const string BaseUrl = "http://localhost:5202/Areas/";
        public List<Areas> Listar()
        {
            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = BaseUrl + "Listar";
            datos["Metodo"] = "GET";
            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("No funciono");

            return JsonConvert.DeserializeObject<List<Areas>>(respuesta["Valor"].ToString()!)!;
        }

        public Areas Guardar(Areas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // 🛠️ ERROR CORREGIDO: Validamos si todos los IDs opcionales vienen nulos
            if (entidad.HabitatId == null && entidad.JaulaId == null && entidad.ZonaPublicaId == null)
            {
                throw new Exception("Falta información: El área debe estar asociada al menos a un Hábitat, Jaula o Zona Pública.");
            }

            var datos = new Dictionary<string, object>();
            datos["Url"] = BaseUrl + "Guardar";
            datos["Metodo"] = "POST";
            datos["Entidad"] = entidad;

            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("No funciono");

            return JsonConvert.DeserializeObject<Areas>(respuesta["Valor"].ToString()!)!;
        }

        public Areas Modificar(Areas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El registro no existe para ser modificado.");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = BaseUrl + "Modificar"; // Asegúrate de que este endpoint exista en tu API
            datos["Metodo"] = "POST";
            datos["Entidad"] = entidad;

            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos);
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("No se pudo modificar.");

            return JsonConvert.DeserializeObject<Areas>(respuesta["Valor"].ToString())!;
        }

        public bool Borrar(int id)
        //public Areas Borrar (Areas entidad)
        {
            
            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = BaseUrl + "Borrar/" + id;
            datos["Metodo"] = "DELETE";
            // Enviamos un objeto anónimo con el ID para el cuerpo del POST

            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos);
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("No se pudo eliminar.");

            // Si la API responde con éxito, asumimos true
            return true;
        }
    }
}