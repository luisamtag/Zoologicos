using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades; // Revisa el nombre exacto de tus entidades
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class DiagnosticosModel : PageModel
    {
        private IDiagnosticosNegocio iDiagnosticosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Diagnosticos>? Lista { get; set; }
        [BindProperty] public Diagnosticos? Diagnostico { get; set; }

        public DiagnosticosModel()
        {
            iDiagnosticosNegocio = new DiagnosticosNegocio();
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
                if (iDiagnosticosNegocio == null)
                    return;

                Lista = iDiagnosticosNegocio.Listar();
                Diagnostico = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Diagnostico = new Diagnosticos();
            // Inicializamos el diagnóstico con la fecha del día actual
            Diagnostico.FechaDiagnostico = DateTime.Today;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Diagnostico = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Diagnostico == null)
                    return;

                if (Diagnostico.Id == 0)
                {
                    Diagnostico = iDiagnosticosNegocio.Guardar(Diagnostico);
                }
                else
                {
                    Diagnostico = iDiagnosticosNegocio.Modificar(Diagnostico);
                }

                if (Diagnostico == null || Diagnostico.Id == 0)
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
                if (Diagnostico == null)
                    return;

                bool eliminado = iDiagnosticosNegocio.Borrar(Diagnostico.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el registro médico en el servidor.");

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
                Diagnostico = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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