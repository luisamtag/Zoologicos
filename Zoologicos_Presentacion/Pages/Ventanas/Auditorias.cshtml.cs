using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;
using System.Text;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasNegocio iAuditoriasNegocio;

        [BindProperty] public List<Auditorias>? Lista    { get; set; }
        [BindProperty] public string?           FiltroTabla { get; set; }

        public AuditoriasModel()
        {
            iAuditoriasNegocio = new AuditoriasNegocio();
        }

        public void OnGet()
        {
            var sesion = HttpContext.Session.GetString("UsuarioSede");
            if (string.IsNullOrEmpty(sesion))
            {
                Response.Redirect("/Index");
                return;
            }

            OnPostBtRefrescar();
        }

        public IActionResult OnPostBtCerrar()
        {
            return RedirectToPage();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista       = iAuditoriasNegocio.Listar();
                FiltroTabla = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtFiltrar()
        {
            try
            {
                Lista = string.IsNullOrEmpty(FiltroTabla)
                    ? iAuditoriasNegocio.Listar()
                    : iAuditoriasNegocio.ListarPorTabla(FiltroTabla);
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public IActionResult OnPostBtExportar()
        {
            try
            {
                var datos = string.IsNullOrEmpty(FiltroTabla)
                    ? iAuditoriasNegocio.Listar()
                    : iAuditoriasNegocio.ListarPorTabla(FiltroTabla);

                // Generar CSV (compatible con Excel)
                var sb = new StringBuilder();
                sb.AppendLine("ID;Tabla;Accion;Usuario;Fecha;Datos");

                foreach (var a in datos)
                {
                    var datos_escaped = a.Datos.Replace("\"", "\"\"");
                    sb.AppendLine($"{a.IdAuditorias};" +
                                  $"{a.Tabla};" +
                                  $"{a.Accion};" +
                                  $"{a.Usuario ?? "Sistema"};" +
                                  $"{a.Fecha:dd/MM/yyyy HH:mm:ss};" +
                                  $"\"{datos_escaped}\"");
                }

                var bytes  = Encoding.UTF8.GetPreamble().Concat(
                                 Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
                var nombre = $"Auditorias_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                return File(bytes, "text/csv; charset=utf-8", nombre);
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
                OnPostBtRefrescar();
                return Page();
            }
        }
    }
}
