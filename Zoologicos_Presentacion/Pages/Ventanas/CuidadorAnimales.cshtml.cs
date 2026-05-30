using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades; // Donde se encuentre CuidadorAnimales
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class CuidadorAnimalesModel : PageModel
    {
        private ICuidadorAnimalesNegocio iCuidadorAnimalesNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<CuidadorAnimales>? Lista { get; set; }
        [BindProperty] public CuidadorAnimales? Cuidador { get; set; }

        public CuidadorAnimalesModel()
        {
            iCuidadorAnimalesNegocio = new CuidadorAnimalesNegocio();
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
                if (iCuidadorAnimalesNegocio == null)
                    return;

                Lista = iCuidadorAnimalesNegocio.Listar();
                Cuidador = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Cuidador = new CuidadorAnimales();
            Cuidador.Turno = ""; // Evita problemas de nulos en el select al iniciar
            Cuidador.AñosExperiencia = 0;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Cuidador = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Cuidador == null)
                    return;

                // Validación de Negocio usando la propiedad de texto obligatoria (Turno)
                if (string.IsNullOrEmpty(Cuidador.Turno))
                    throw new Exception("Falta información: El turno es un campo obligatorio.");

                if (Cuidador.Id == 0)
                {
                    Cuidador = iCuidadorAnimalesNegocio.Guardar(Cuidador);
                }
                else
                {
                    Cuidador = iCuidadorAnimalesNegocio.Modificar(Cuidador);
                }

                if (Cuidador == null || Cuidador.Id == 0)
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
                if (Cuidador == null)
                    return;

                bool eliminado = iCuidadorAnimalesNegocio.Borrar(Cuidador.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar al cuidador en el servidor.");

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
                Cuidador = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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