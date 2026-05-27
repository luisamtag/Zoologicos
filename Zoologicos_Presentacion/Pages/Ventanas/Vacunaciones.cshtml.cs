using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class VacunacionesModel : PageModel
    {
        private IVacunacionesNegocio iVacunacionesNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Vacunaciones>? Lista { get; set; }
        [BindProperty] public Vacunaciones? Vacunacion { get; set; }

        public VacunacionesModel()
        {
            iVacunacionesNegocio = new VacunacionesNegocio();
        }

        public void OnGet()
        {
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
                if (iVacunacionesNegocio == null)
                    return;

                Lista = iVacunacionesNegocio.Listar();
                Vacunacion = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Vacunacion = new Vacunaciones();
            Vacunacion.FechaAplicacion = DateTime.Today; // Inicializa con el día actual
            Vacunacion.NombreVacuna = "";
            Vacunacion.Dosis = "";
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Vacunacion = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Vacunacion == null)
                    return;

                if (Vacunacion.Id == 0)
                {
                    Vacunacion = iVacunacionesNegocio.Guardar(Vacunacion);
                }
                else
                {
                    Vacunacion = iVacunacionesNegocio.Modificar(Vacunacion);
                }

                if (Vacunacion == null || Vacunacion.Id == 0)
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
                if (Vacunacion == null)
                    return;

                bool eliminado = iVacunacionesNegocio.Borrar(Vacunacion.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el registro de vacunación en el servidor.");

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
                Vacunacion = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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