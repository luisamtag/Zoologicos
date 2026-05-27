using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class EntrenadoresModel : PageModel
    {
        private IEntrenadoresNegocio iEntrenadoresNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Entrenadores>? Lista { get; set; }
        [BindProperty] public Entrenadores? Entrenador { get; set; }

        public EntrenadoresModel()
        {
            iEntrenadoresNegocio = new EntrenadoresNegocio();
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
                if (iEntrenadoresNegocio == null)
                    return;

                Lista = iEntrenadoresNegocio.Listar();
                Entrenador = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Entrenador = new Entrenadores();
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Entrenador = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Entrenador == null)
                    return;

                if (Entrenador.Id == 0)
                {
                    Entrenador = iEntrenadoresNegocio.Guardar(Entrenador);
                }
                else
                {
                    Entrenador = iEntrenadoresNegocio.Modificar(Entrenador);
                }

                if (Entrenador == null || Entrenador.Id == 0)
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
                if (Entrenador == null)
                    return;

                bool eliminado = iEntrenadoresNegocio.Borrar(Entrenador.Id);

                if (!eliminado)
                    throw new Exception("No se pudo dar de baja al entrenador en el servidor.");

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
                Entrenador = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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