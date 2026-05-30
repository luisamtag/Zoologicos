using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class VisitantesModel : PageModel
    {
        private IVisitantesNegocio iVisitantesNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Visitantes>? Lista { get; set; }
        [BindProperty] public Visitantes? Visitante { get; set; }

        public VisitantesModel()
        {
            iVisitantesNegocio = new VisitantesNegocio();
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
                if (iVisitantesNegocio == null)
                    return;

                Lista = iVisitantesNegocio.Listar();
                Visitante = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Visitante = new Visitantes();
            Visitante.Nombre = "";
            Visitante.TipoDocumento = "";
            Visitante.NumeroDocumento = "";
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Visitante = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Visitante == null)
                    return;

                if (Visitante.Id == 0)
                {
                    Visitante = iVisitantesNegocio.Guardar(Visitante);
                }
                else
                {
                    Visitante = iVisitantesNegocio.Modificar(Visitante);
                }

                if (Visitante == null || Visitante.Id == 0)
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
                if (Visitante == null)
                    return;

                bool eliminado = iVisitantesNegocio.Borrar(Visitante.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el registro del visitante en el servidor.");

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
                Visitante = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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