using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class ReproduccionesModel : PageModel
    {
        private IReproduccionesNegocio iReproduccionesNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Reproducciones>? Lista { get; set; }
        [BindProperty] public Reproducciones? Reproduccion { get; set; }

        public ReproduccionesModel()
        {
            iReproduccionesNegocio = new ReproduccionesNegocio();
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
                if (iReproduccionesNegocio == null)
                    return;

                Lista = iReproduccionesNegocio.Listar();
                Reproduccion = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Reproduccion = new Reproducciones();
            Reproduccion.FechaAppariamiento = DateTime.Today; // Inicializa con la fecha actual por comodidad
            Reproduccion.Metodo = "";
            Reproduccion.Estado = "";
            Reproduccion.CantidadCrias = 0;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Reproduccion = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Reproduccion == null)
                    return;

                if (Reproduccion.Id == 0)
                {
                    Reproduccion = iReproduccionesNegocio.Guardar(Reproduccion);
                }
                else
                {
                    Reproduccion = iReproduccionesNegocio.Modificar(Reproduccion);
                }

                if (Reproduccion == null || Reproduccion.Id == 0)
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
                if (Reproduccion == null)
                    return;

                bool eliminado = iReproduccionesNegocio.Borrar(Reproduccion.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el registro de reproducción en el servidor.");

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
                Reproduccion = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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