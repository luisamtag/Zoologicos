using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;

using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;



namespace Zoologicos_Presentacion.Pages.Ventanas

{
    public class AlimentacionesModel : PageModel
    {
        private IAlimentacionesNegocio iAlimentacionesNegocio;
        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Alimentaciones>? Lista { get; set; }
        [BindProperty] public Alimentaciones? Alimentacion { get; set; }

        public AlimentacionesModel()
        {
            iAlimentacionesNegocio = new AlimentacionesNegocio();
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
                if (iAlimentacionesNegocio == null)
                    return;
                Lista = iAlimentacionesNegocio.Listar();
                Alimentacion = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Alimentacion = new Alimentaciones();
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Alimentacion = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Alimentacion == null)
                    return;
                if (Alimentacion!.Id == 0)
                    Alimentacion = iAlimentacionesNegocio.Guardar(Alimentacion!);
                else
                {
                    Alimentacion = iAlimentacionesNegocio.Modificar(Alimentacion!);
                }
                if (Alimentacion!.Id == 0)
                    return;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        

        public IActionResult OnPostBtBorrar() // 👈 Cambiado de void a IActionResult
        {
            try
            {
                if (Alimentacion == null)

                {
                    ViewData["Mensaje"] = "No se seleccionó ninguna sede para eliminar.";
                    return Page();
                }

                bool eliminado = iAlimentacionesNegocio.Borrar(Alimentacion.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar la sede en el servidor.");
                
                return RedirectToPage();


            }
            catch (Exception ex)
            {
                
                ViewData["Mensaje"] = ex.Message;
                
                OnPostBtRefrescar();

                return Page();
            }
        }


















        public void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Alimentacion = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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