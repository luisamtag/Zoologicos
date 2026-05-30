using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.Implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_libreria_Presentacion.implementaciones
{
    public class AuditoriasNegocio : IAuditoriasNegocio
    {
        private IComunicaciones? iComunicaciones;
        private const string BaseUrl = "http://localhost:5202/Auditorias/";

        //Convierte el JSON del servidor manejando el campo Datos como string
        private List<Auditorias> Convertir(string json)
        {
            var array = JArray.Parse(json);
            var lista = new List<Auditorias>();

            foreach (var item in array)
            {
                var auditoria = new Auditorias
                {
                    IdAuditorias = item["idAuditorias"]?.Value<int>() ?? 0,
                    Tabla        = item["tabla"]?.Value<string>()    ?? "",
                    Accion       = item["accion"]?.Value<string>()   ?? "",
                    Usuario      = item["usuario"]?.Value<string>(),
                    Fecha        = item["fecha"]?.Value<DateTime>()  ?? DateTime.MinValue,
                    // Datos puede ser string o un objeto JSON — siempre lo guardamos como string
                    Datos        = item["datos"]?.Type == JTokenType.String
                                    ? item["datos"]!.Value<string>()!
                                    : item["datos"]?.ToString(Formatting.None) ?? ""
                };
                lista.Add(auditoria);
            }
            return lista;
        }

        public List<Auditorias> Listar()
        {
            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"]    = BaseUrl + "Listar";
            datos["Metodo"] = "GET";

            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("No se pudo obtener el historial de auditorías.");

            return Convertir(respuesta["Valor"].ToString()!);
        }

        public List<Auditorias> ListarPorTabla(string tabla)
        {
            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"]    = BaseUrl + "ListarPorTabla?tabla=" + tabla;
            datos["Metodo"] = "GET";

            var comunicaciones = new Comunicaciones();
            var task = comunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                throw new Exception("No se pudo filtrar el historial.");

            return Convertir(respuesta["Valor"].ToString()!);
        }
    }
}
