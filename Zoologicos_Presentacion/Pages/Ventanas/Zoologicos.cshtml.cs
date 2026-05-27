using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class ZoologicosModel : PageModel
    {
        private IZoologicosNegocio iZoologicosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Zoologicos>? Lista { get; set; }
        [BindProperty] public Zoologicos? Zoologico { get; set; }

        public ZoologicosModel()
        {
            iZoologicosNegocio = new ZoologicosNegocio();
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
                if (iZoologicosNegocio == null)
                    return;

                Lista = iZoologicosNegocio.Listar();
                Zoologico = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Zoologico = new Zoologicos();
            Zoologico.Nombre = "";
            Zoologico.Ubicacion = "";
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Zoologico = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Zoologico == null)
                    return;

                if (Zoologico.Id == 0)
                {
                    Zoologico = iZoologicosNegocio.Guardar(Zoologico);
                }
                else
                {
                    Zoologico = iZoologicosNegocio.Modificar(Zoologico);
                }

                if (Zoologico == null || Zoologico.Id == 0)
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
                if (Zoologico == null)
                    return;

                bool eliminado = iZoologicosNegocio.Borrar(Zoologico.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar la sede en el servidor.");

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
                Zoologico = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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