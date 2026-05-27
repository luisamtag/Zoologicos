using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;

using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class ZonasPublicasModel : PageModel
    {
        private IZonasPublicasNegocio iZonasPublicasNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<ZonasPublicas>? Lista { get; set; }
        [BindProperty] public ZonasPublicas? ZonaPublica { get; set; }

        public ZonasPublicasModel()
        {
            iZonasPublicasNegocio = new ZonasPublicasNegocio();
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
                if (iZonasPublicasNegocio == null)
                    return;

                Lista = iZonasPublicasNegocio.Listar();
                ZonaPublica = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            ZonaPublica = new ZonasPublicas();
            ZonaPublica.Nombre = "";
            ZonaPublica.Tipo = "";
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                ZonaPublica = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (ZonaPublica == null)
                    return;

                if (ZonaPublica.Id == 0)
                {
                    ZonaPublica = iZonasPublicasNegocio.Guardar(ZonaPublica);
                }
                else
                {
                    ZonaPublica = iZonasPublicasNegocio.Modificar(ZonaPublica);
                }

                if (ZonaPublica == null || ZonaPublica.Id == 0)
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
                if (ZonaPublica == null)
                    return;

                bool eliminado = iZonasPublicasNegocio.Borrar(ZonaPublica.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar la zona pública en el servidor.");

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
                ZonaPublica = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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