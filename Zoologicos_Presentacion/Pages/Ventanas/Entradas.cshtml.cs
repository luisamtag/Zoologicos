using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class EntradasModel : PageModel
    {
        private IEntradasNegocio iEntradasNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Entradas>? Lista { get; set; }
        [BindProperty] public Entradas? Entrada { get; set; }

        public EntradasModel()
        {
            iEntradasNegocio = new EntradasNegocio();
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
                if (iEntradasNegocio == null)
                    return;

                Lista = iEntradasNegocio.Listar();
                Entrada = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Entrada = new Entradas();
            Entrada.FechaVisita = DateTime.Today; // Inicializa con el día actual por defecto
            Entrada.TipoEntrada = "";
            Entrada.ValorPagado = 0;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Entrada = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Entrada == null)
                    return;

                if (Entrada.Id == 0)
                {
                    Entrada = iEntradasNegocio.Guardar(Entrada);
                }
                else
                {
                    Entrada = iEntradasNegocio.Modificar(Entrada);
                }

                if (Entrada == null || Entrada.Id == 0)
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
                if (Entrada == null)
                    return;

                bool eliminado = iEntradasNegocio.Borrar(Entrada.Id);

                if (!eliminado)
                    throw new Exception("No se pudo anular la entrada en el servidor.");

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
                Entrada = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
                Borrando = true;
                Lista = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }
    }
}