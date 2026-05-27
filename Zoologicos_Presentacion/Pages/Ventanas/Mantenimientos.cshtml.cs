using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class MantenimientosModel : PageModel
    {
        private IMantenimientosNegocio iMantenimientosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Mantenimientos>? Lista { get; set; }
        [BindProperty] public Mantenimientos? Mantenimiento { get; set; }

        public MantenimientosModel()
        {
            iMantenimientosNegocio = new MantenimientosNegocio();
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
                if (iMantenimientosNegocio == null)
                    return;

                Lista = iMantenimientosNegocio.Listar();
                Mantenimiento = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Mantenimiento = new Mantenimientos();
            Mantenimiento.FechaReporte = DateTime.Today;     // Hoy se levanta el reporte
            Mantenimiento.FechaProgramada = DateTime.Today;  // Por defecto se asigna para hoy
            Mantenimiento.Estado = "";
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Mantenimiento = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Mantenimiento == null)
                    return;

                if (Mantenimiento.Id == 0)
                {
                    Mantenimiento = iMantenimientosNegocio.Guardar(Mantenimiento);
                }
                else
                {
                    Mantenimiento = iMantenimientosNegocio.Modificar(Mantenimiento);
                }

                if (Mantenimiento == null || Mantenimiento.Id == 0)
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
                if (Mantenimiento == null)
                    return;

                bool eliminado = iMantenimientosNegocio.Borrar(Mantenimiento.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar la orden en el servidor.");

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
                Mantenimiento = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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