using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class HabitatsModel : PageModel
    {
        private IHabitatsNegocio iHabitatsNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Habitats>? Lista { get; set; }
        [BindProperty] public Habitats? Habitat { get; set; }

        public HabitatsModel()
        {
            iHabitatsNegocio = new HabitatsNegocio();
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
                if (iHabitatsNegocio == null)
                    return;

                Lista = iHabitatsNegocio.Listar();
                Habitat = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Habitat = new Habitats();
            Habitat.CapacidadMaxima = 1; // Valor inicial seguro por defecto
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Habitat = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Habitat == null)
                    return;

                if (Habitat.Id == 0)
                {
                    Habitat = iHabitatsNegocio.Guardar(Habitat);
                }
                else
                {
                    Habitat = iHabitatsNegocio.Modificar(Habitat);
                }

                if (Habitat == null || Habitat.Id == 0)
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
                if (Habitat == null)
                    return;

                bool eliminado = iHabitatsNegocio.Borrar(Habitat.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el hábitat en el servidor.");

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
                Habitat = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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