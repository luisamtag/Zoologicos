using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class InventariosModel : PageModel
    {
        private IInventariosNegocio iInventariosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Inventarios>? Lista { get; set; }
        [BindProperty] public Inventarios? Inventario { get; set; }

        public InventariosModel()
        {
            iInventariosNegocio = new InventariosNegocio();
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
                if (iInventariosNegocio == null)
                    return;

                Lista = iInventariosNegocio.Listar();
                Inventario = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Inventario = new Inventarios();
            Inventario.NombreItem = "";
            Inventario.TipoItem = "";
            Inventario.CantidadDisponible = 0;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Inventario = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Inventario == null)
                    return;

                if (Inventario.Id == 0)
                {
                    Inventario = iInventariosNegocio.Guardar(Inventario);
                }
                else
                {
                    Inventario = iInventariosNegocio.Modificar(Inventario);
                }

                if (Inventario == null || Inventario.Id == 0)
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
                if (Inventario == null)
                    return;

                bool eliminado = iInventariosNegocio.Borrar(Inventario.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el artículo del inventario en el servidor.");

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
                Inventario = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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