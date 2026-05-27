using Newtonsoft.Json;
using Zoologicos_libreria.entidades;

using Zoologicos_libreria_Presentacion.Implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_libreria_Presentacion.implementaciones
{
    public class MantenimientosNegocio : IMantenimientosNegocio
    {
        private IComunicaciones? iComunicaciones;
        private const string BaseUrl = "http://localhost:5144/Mantenimientos/";
        public List<Mantenimientos> Listar()
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

            return JsonConvert.DeserializeObject<List<Mantenimientos>>(respuesta["Valor"].ToString()!)!;
        }

        public Mantenimientos Guardar(Mantenimientos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Estado))
                throw new Exception("Falta informacion: Debe asignar un estado a la orden de mantenimiento.");


            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = BaseUrl + "Guardar";
            datos["Metodo"] = "POST";       // <- Le avisamos que es un POST
            datos["Entidad"] = entidad;     // <- Pasamos el objeto real para que viaje a la API

            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("No funciono");

            return JsonConvert.DeserializeObject<Mantenimientos>(respuesta["Valor"].ToString()!)!;
        }

        public Mantenimientos Modificar(Mantenimientos entidad)
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

            return JsonConvert.DeserializeObject<Mantenimientos>(respuesta["Valor"].ToString())!;
        }

        public bool Borrar(int id)
        //public Mantenimientos Borrar (Mantenimientos entidad)
        {
            
            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = BaseUrl + "Borrar"; // Asegúrate de que tu API reciba el ID (puede ser "Borrar?id=" + id)
            datos["Metodo"] = "POST";
            // Enviamos un objeto anónimo con el ID para el cuerpo del POST
            datos["Entidad"] = new { Id = id };

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