using System.Text;
using Zoologicos_libreria.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class IngresosModel : PageModel
    {
        private IIngresosNegocio iIngresosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Ingresos>? Lista { get; set; }
        [BindProperty] public Ingresos? Ingreso { get; set; }

        public IngresosModel()
        {
            iIngresosNegocio = new IngresosNegocio();
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

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iIngresosNegocio == null) return;
                Lista = iIngresosNegocio.Listar();
                Ingreso = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Ingreso = new Ingresos();
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Ingreso = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
                Lista = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Ingreso == null) return;

                if (Ingreso!.Id == 0)
                    Ingreso = iIngresosNegocio.Guardar(Ingreso!);
                else
                    Ingreso = iIngresosNegocio.Modificar(Ingreso!);

                if (Ingreso!.Id == 0) return;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Ingreso = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
                Borrando = true;
                Lista = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Ingreso == null) return;
                if (Ingreso!.Id == 0) return;

                iIngresosNegocio.Borrar(Ingreso!.Id);
                OnPostBtRefrescar();
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
                if (Lista == null || Lista.Count == 0)
                    OnPostBtRefrescar();

                var datos = Lista ?? new List<Ingresos>();
                var sb = new StringBuilder();
                sb.AppendLine("Id;AnimalId;ZoologicoId;Fecha Ingreso;Tipo Ingreso;Procedencia;Estado;Observaciones");
                foreach (var e in datos)
                    sb.AppendLine($"{e.Id};{e.AnimalId};{e.ZoologicoId};{e.FechaIngreso:dd/MM/yyyy};{e.TipoIngreso};{e.Procedencia};{e.Estado};{e.Observaciones ?? \"\")}");

                var bytes = System.Text.Encoding.UTF8.GetPreamble()
                    .Concat(System.Text.Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
                var nombre = $"Ingresos_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
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
