using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades; // Revisa que este namespace sea el de tu clase Areas

using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class AreasModel : PageModel
    {
        private IAreasNegocio iAreasNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Areas>? Lista { get; set; }
        [BindProperty] public Areas? Area { get; set; }

        public AreasModel()
        {
            iAreasNegocio = new AreasNegocio();
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
                if (iAreasNegocio == null)
                    return;

                Lista = iAreasNegocio.Listar();
                Area = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Area = new Areas();
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Area = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Area == null)
                    return;

                if (Area.Id == 0)
                {
                    Area = iAreasNegocio.Guardar(Area);
                }
                else
                {
                    Area = iAreasNegocio.Modificar(Area);
                }

                if (Area == null || Area.Id == 0)
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
                if (Area == null)
                    return;

                bool eliminado = iAreasNegocio.Borrar(Area.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el registro del área en el servidor.");

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
                Area = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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