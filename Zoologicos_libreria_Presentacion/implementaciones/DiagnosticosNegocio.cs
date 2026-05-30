using Newtonsoft.Json;
using Zoologicos_libreria.entidades;

using Zoologicos_libreria_Presentacion.Implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_libreria_Presentacion.implementaciones
{
    public class DiagnosticosNegocio : IDiagnosticosNegocio
    {
        private IComunicaciones? iComunicaciones;
        private const string BaseUrl = "http://localhost:5202/Diagnosticos/";
        public List<Diagnosticos> Listar()
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

            return JsonConvert.DeserializeObject<List<Diagnosticos>>(respuesta["Valor"].ToString()!)!;
        }

        public Diagnosticos Guardar(Diagnosticos entidad)
        {
            // 1. Validar que no sea un registro ya existente
            if (entidad.Id != 0)
                throw new Exception("Ya se guardó");

            //  VALIDACIÓN NUMÉRICA: Aseguramos que se hayan seleccionado IDs válidos
            if (entidad.AnimalId <= 0 || entidad.EnfermedadId <= 0 || entidad.VeterinarioId <= 0)
            {
                throw new Exception("Falta información: Debe especificar un Animal, una Enfermedad y un Veterinario válidos.");
            }

            //  VALIDACIÓN DE FECHA: Aseguramos que no venga la fecha vacía por defecto de C#
            if (entidad.FechaDiagnostico == DateTime.MinValue)
            {
                throw new Exception("Falta información: La fecha de diagnóstico no es válida.");
            }

            // Preparamos el paquete de datos para la API
            var datos = new Dictionary<string, object>();
            datos["Url"] = BaseUrl + "Guardar";
            datos["Metodo"] = "POST";
            datos["Entidad"] = entidad;

            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("No funcionó la comunicación con el servidor.");

            // 🛠️ CONTROL DE NULOS: Extraemos y validamos el JSON de forma segura para evitar advertencias
            string? jsonRespuesta = respuesta["Valor"]?.ToString();

            if (string.IsNullOrEmpty(jsonRespuesta))
                throw new Exception("El servidor respondió con éxito pero devolvió un resultado vacío.");

            // Retornamos el objeto mapeado limpiamente
            return JsonConvert.DeserializeObject<Diagnosticos>(jsonRespuesta)!;
        }

        public Diagnosticos Modificar(Diagnosticos entidad)
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

            return JsonConvert.DeserializeObject<Diagnosticos>(respuesta["Valor"].ToString())!;
        }

        public bool Borrar(int id)
        //public Diagnosticos Borrar (Diagnosticos entidad)
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