using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class VeterinariosModel : PageModel
    {
        private IVeterinariosNegocio iVeterinariosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Veterinarios>? Lista { get; set; }
        [BindProperty] public Veterinarios? Veterinario { get; set; }

        public VeterinariosModel()
        {
            iVeterinariosNegocio = new VeterinariosNegocio();
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
                if (iVeterinariosNegocio == null)
                    return;

                Lista = iVeterinariosNegocio.Listar();
                Veterinario = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Veterinario = new Veterinarios();
            Veterinario.Especialidad = "";
            Veterinario.AñosExperiencia = 0;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Veterinario = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Veterinario == null)
                    return;

                if (Veterinario.Id == 0)
                {
                    Veterinario = iVeterinariosNegocio.Guardar(Veterinario);
                }
                else
                {
                    Veterinario = iVeterinariosNegocio.Modificar(Veterinario);
                }

                if (Veterinario == null || Veterinario.Id == 0)
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
                if (Veterinario == null)
                    return;

                bool eliminado = iVeterinariosNegocio.Borrar(Veterinario.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar al veterinario en el servidor.");

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
                Veterinario = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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