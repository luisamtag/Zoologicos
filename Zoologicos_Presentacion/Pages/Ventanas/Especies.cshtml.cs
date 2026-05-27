using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class EspeciesModel : PageModel
    {
        private IEspeciesNegocio iEspeciesNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Especies>? Lista { get; set; }
        [BindProperty] public Especies? Especie { get; set; }

        public EspeciesModel()
        {
            iEspeciesNegocio = new EspeciesNegocio();
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
                if (iEspeciesNegocio == null)
                    return;

                Lista = iEspeciesNegocio.Listar();
                Especie = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Especie = new Especies();
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Especie = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Especie == null)
                    return;

                if (Especie.Id == 0)
                {
                    Especie = iEspeciesNegocio.Guardar(Especie);
                }
                else
                {
                    Especie = iEspeciesNegocio.Modificar(Especie);
                }

                if (Especie == null || Especie.Id == 0)
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
                if (Especie == null)
                    return;

                bool eliminado = iEspeciesNegocio.Borrar(Especie.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar la especie en el servidor.");

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
                Especie = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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