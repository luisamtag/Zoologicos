using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class PersonalAseoModel : PageModel
    {
        private IPersonalAseoNegocio iPersonalAseoNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<PersonalAseo>? Lista { get; set; }
        [BindProperty] public PersonalAseo? Personal { get; set; }

        public PersonalAseoModel()
        {
            iPersonalAseoNegocio = new PersonalAseoNegocio();
        }

        public void OnGet()
        {
            var sesion = HttpContext.Session.GetString("UsuarioSede");
            if (string.IsNullOrEmpty(sesion))
            {
                Response.Redirect("/Index");
                return;
            }

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
                if (iPersonalAseoNegocio == null)
                    return;

                Lista = iPersonalAseoNegocio.Listar();
                Personal = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Personal = new PersonalAseo();
            Personal.ZonaAsignada = "";
            Personal.Turno = "";
            Personal.ProductosAsignados = "";
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Personal = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Personal == null)
                    return;

                if (Personal.Id == 0)
                {
                    Personal = iPersonalAseoNegocio.Guardar(Personal);
                }
                else
                {
                    Personal = iPersonalAseoNegocio.Modificar(Personal);
                }

                if (Personal == null || Personal.Id == 0)
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
                if (Personal == null)
                    return;

                bool eliminado = iPersonalAseoNegocio.Borrar(Personal.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar al empleado de aseo en el servidor.");

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
                Personal = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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