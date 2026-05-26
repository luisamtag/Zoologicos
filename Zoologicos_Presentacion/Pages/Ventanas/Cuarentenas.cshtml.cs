using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades; // Namespace donde esté tu clase Cuarentena
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class CuarentenasModel : PageModel
    {
        private ICuarentenasNegocio iCuarentenasNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Cuarentenas>? Lista { get; set; }
        [BindProperty] public Cuarentenas? Cuarentena { get; set; }

        public CuarentenasModel()
        {
            iCuarentenasNegocio = new CuarentenasNegocio();
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
                if (iCuarentenasNegocio == null)
                    return;

                Lista = iCuarentenasNegocio.Listar();
                Cuarentena = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Cuarentena = new Cuarentenas();
            // Asignamos por defecto el día de hoy al abrir el formulario de nuevo registro
            Cuarentena.FechaInicio = DateTime.Today;
            Cuarentena.Estado = "Activa"; // Estado inicial lógico
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Cuarentena = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Cuarentena == null)
                    return;

                if (Cuarentena.Id == 0)
                {
                    Cuarentena = iCuarentenasNegocio.Guardar(Cuarentena);
                }
                else
                {
                    Cuarentena = iCuarentenasNegocio.Modificar(Cuarentena);
                }

                if (Cuarentena == null || Cuarentena.Id == 0)
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
                if (Cuarentena == null)
                    return;

                bool eliminado = iCuarentenasNegocio.Borrar(Cuarentena.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el registro de cuarentena en el servidor.");

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
                Cuarentena = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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