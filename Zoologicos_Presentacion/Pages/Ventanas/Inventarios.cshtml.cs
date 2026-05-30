using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class InventariosModel : PageModel
    {
        private IInventariosNegocio iInventariosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Inventarios>? Lista { get; set; }
        [BindProperty] public Inventarios? Inventario { get; set; }

        public InventariosModel()
        {
            iInventariosNegocio = new InventariosNegocio();
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
                if (iInventariosNegocio == null)
                    return;

                Lista = iInventariosNegocio.Listar();
                Inventario = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Inventario = new Inventarios();
            Inventario.NombreItem = "";
            Inventario.TipoItem = "";
            Inventario.CantidadDisponible = 0;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Inventario = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Inventario == null)
                    return;

                if (Inventario.Id == 0)
                {
                    Inventario = iInventariosNegocio.Guardar(Inventario);
                }
                else
                {
                    Inventario = iInventariosNegocio.Modificar(Inventario);
                }

                if (Inventario == null || Inventario.Id == 0)
                    return;

                OnPostBtRefrescar();
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
                if (Inventario == null)
                    return;

                bool eliminado = iInventariosNegocio.Borrar(Inventario.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el artículo del inventario en el servidor.");

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
                Inventario = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
                Borrando = true;
                Lista = null;
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

                var datos = Lista ?? new List<Inventarios>();
                var sb = new StringBuilder();
                sb.AppendLine("Id;ZoologicoId;Nombre Item;Tipo Item;Cantidad Disponible;Fecha Vencimiento");
                foreach (var e in datos)
                    sb.AppendLine($"{e.Id};{e.ZoologicoId};{e.NombreItem};{e.TipoItem};{e.CantidadDisponible};{(e.FechaVencimiento.HasValue ? e.FechaVencimiento.Value.ToString("dd/MM/yyyy") : "")}");

                var bytes = System.Text.Encoding.UTF8.GetPreamble()
                    .Concat(System.Text.Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
                var nombre = $"Inventarios_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
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