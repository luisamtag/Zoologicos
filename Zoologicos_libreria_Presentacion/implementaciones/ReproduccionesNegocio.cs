using Newtonsoft.Json;
using Zoologicos_libreria.entidades;

using Zoologicos_libreria_Presentacion.Implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_libreria_Presentacion.implementaciones
{
    public class ReproduccionesNegocio : IReproduccionesNegocio
    {
        private IComunicaciones? iComunicaciones;
        private const string BaseUrl = "http://localhost:5202/Reproducciones/";
        public List<Reproducciones> Listar()
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

            return JsonConvert.DeserializeObject<List<Reproducciones>>(respuesta["Valor"].ToString()!)!;
        }

        public Reproducciones Guardar(Reproducciones entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Metodo))
                throw new Exception("Falta informacion: Debe seleccionar el método reproductivo utilizado.");

            if (string.IsNullOrEmpty(entidad.Estado))
                throw new Exception("Falta informacion: Debe registrar el estado del proceso actual.");

            if (entidad.AnimalMadreId <= 0 || entidad.AnimalPadreId <= 0)
                throw new Exception("Falta informacion: Los identificadores de los animales progenitores deben ser válidos.");

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

            return JsonConvert.DeserializeObject<Reproducciones>(respuesta["Valor"].ToString()!)!;
        }

        public Reproducciones Modificar(Reproducciones entidad)
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

            return JsonConvert.DeserializeObject<Reproducciones>(respuesta["Valor"].ToString())!;
        }

        public bool Borrar(int id)
        //public Reproducciones Borrar (Reproducciones entidad)
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