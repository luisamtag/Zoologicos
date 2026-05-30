using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class EnfermedadesModel : PageModel
    {
        private IEnfermedadesNegocio iEnfermedadesNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Enfermedades>? Lista { get; set; }
        [BindProperty] public Enfermedades? Enfermedad { get; set; }

        public EnfermedadesModel()
        {
            iEnfermedadesNegocio = new EnfermedadesNegocio();
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
                if (iEnfermedadesNegocio == null)
                    return;

                Lista = iEnfermedadesNegocio.Listar();
                Enfermedad = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Enfermedad = new Enfermedades();
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Enfermedad = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Enfermedad == null)
                    return;

                if (Enfermedad.Id == 0)
                {
                    Enfermedad = iEnfermedadesNegocio.Guardar(Enfermedad);
                }
                else
                {
                    Enfermedad = iEnfermedadesNegocio.Modificar(Enfermedad);
                }

                if (Enfermedad == null || Enfermedad.Id == 0)
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
                if (Enfermedad == null)
                    return;

                bool eliminado = iEnfermedadesNegocio.Borrar(Enfermedad.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar la enfermedad en el servidor.");

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
                Enfermedad = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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