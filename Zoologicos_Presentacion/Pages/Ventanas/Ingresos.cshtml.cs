using Zoologicos_libreria.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class IngresosModel : PageModel
    {
        private IIngresosNegocio iIngresosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Ingresos>? Lista { get; set; }
        [BindProperty] public Ingresos? Ingreso { get; set; }

        public IngresosModel()
        {
            iIngresosNegocio = new IngresosNegocio();
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
                if (iIngresosNegocio == null) return;
                Lista = iIngresosNegocio.Listar();
                Ingreso = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Ingreso = new Ingresos();
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Ingreso = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Ingreso == null) return;

                if (Ingreso!.Id == 0)
                    Ingreso = iIngresosNegocio.Guardar(Ingreso!);
                else
                    Ingreso = iIngresosNegocio.Modificar(Ingreso!);

                if (Ingreso!.Id == 0) return;
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
                Ingreso = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
                Borrando = true;
                Lista = null;
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
                if (Ingreso == null) return;
                if (Ingreso!.Id == 0) return;

                iIngresosNegocio.Borrar(Ingreso!.Id);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }
    }
}
