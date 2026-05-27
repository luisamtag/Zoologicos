using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class JaulasModel : PageModel
    {
        private IJaulasNegocio iJaulasNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Jaulas>? Lista { get; set; }
        [BindProperty] public Jaulas? Jaula { get; set; }

        public JaulasModel()
        {
            iJaulasNegocio = new JaulasNegocio();
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
                if (iJaulasNegocio == null)
                    return;

                Lista = iJaulasNegocio.Listar();
                Jaula = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Jaula = new Jaulas();
            Jaula.FechaCompra = DateTime.Today; // Asigna por defecto el día de hoy
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Jaula = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Jaula == null)
                    return;

                if (Jaula.Id == 0)
                {
                    Jaula = iJaulasNegocio.Guardar(Jaula);
                }
                else
                {
                    Jaula = iJaulasNegocio.Modificar(Jaula);
                }

                if (Jaula == null || Jaula.Id == 0)
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
                if (Jaula == null)
                    return;

                bool eliminado = iJaulasNegocio.Borrar(Jaula.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar la jaula en el servidor.");

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
                Jaula = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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