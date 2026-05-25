
using Newtonsoft.Json;
using System.Text;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_libreria_Presentacion.Implementaciones
{
    public class Comunicaciones : IComunicaciones
    {
        public async Task<Dictionary<string, object>> Ejecutar(Dictionary<string, object> datos)
        {
            // 1. Extraer la configuración de la petición
            var url = datos["Url"].ToString();
            datos.Remove("Url");
            // Averiguar si es GET o POST (por defecto será GET si no se especifica)
            var metodo = datos.ContainsKey("Metodo") ? datos["Metodo"].ToString().ToUpper() : "GET";

            HttpResponseMessage message;
            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            // 2. Ejecutar según el método HTTP correcto
            if (metodo == "POST")
            {
                // Si es POST, serializamos la Entidad que viene desde el Negocio
                var stringData = datos.ContainsKey("Entidad") ? JsonConvert.SerializeObject(datos["Entidad"]) : "{}";
                var body = new StringContent(stringData, Encoding.UTF8, "application/json");

                message = await httpClient.PostAsync(url, body); // <- AHORA SÍ ENVÍA DATOS
            }
            else
            {
                message = await httpClient.GetAsync(url);
            }

            // 3. Validar respuesta
            if (!message.IsSuccessStatusCode)
                throw new Exception($"Error Comunicación: {message.StatusCode}");

            var resp = await message.Content.ReadAsStringAsync();
            httpClient.Dispose();

            
            resp = Replace(resp);

            return new Dictionary<string, object>() {
                { "Valor", resp }
            };
        }

        private string Replace(string resp)
        {
            return resp.Replace("\\\\r\\\\n", "")
                .Replace("\\r\\n", "")
                .Replace("\\", "")
                .Replace("\\\"", "\"")
                .Replace("\"", "'")
                .Replace("'[", "[")
                .Replace("]'", "]")
                .Replace("'{'", "{'")
                .Replace("\\\\", "\\")
                .Replace("'}'", "'}")
                .Replace("}'", "}")
                .Replace("\\n", "")
                .Replace("\\r", "")
                .Replace("    ", "")
                .Replace("'{", "{")
                .Replace("\"", "")
                .Replace("  ", "")
                .Replace("null", "''");
        }
    }
}