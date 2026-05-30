using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class GerentesModel : PageModel
    {
        private IGerentesNegocio iGerentesNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Gerentes>? Lista { get; set; }
        [BindProperty] public Gerentes? Gerente { get; set; }

        public GerentesModel()
        {
            iGerentesNegocio = new GerentesNegocio();
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
                if (iGerentesNegocio == null)
                    return;

                Lista = iGerentesNegocio.Listar();
                Gerente = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Gerente = new Gerentes();
            Gerente.FechaContratacion = DateTime.Today;
            Gerente.Salario = 0;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Gerente = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Gerente == null)
                    return;

                if (Gerente.Id == 0)
                {
                    Gerente = iGerentesNegocio.Guardar(Gerente);
                }
                else
                {
                    Gerente = iGerentesNegocio.Modificar(Gerente);
                }

                if (Gerente == null || Gerente.Id == 0)
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
                if (Gerente == null)
                    return;

                bool eliminado = iGerentesNegocio.Borrar(Gerente.Id);

                if (!eliminado)
                    throw new Exception("No se pudo procesar la baja del cargo gerencial en el servidor.");

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
                Gerente = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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