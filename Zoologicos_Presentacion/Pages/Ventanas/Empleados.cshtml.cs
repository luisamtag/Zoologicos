using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class EmpleadosModel : PageModel
    {
        private IEmpleadosNegocio iEmpleadosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Empleados>? Lista { get; set; }
        [BindProperty] public Empleados? Empleado { get; set; }

        public EmpleadosModel()
        {
            iEmpleadosNegocio = new EmpleadosNegocio();
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
                if (iEmpleadosNegocio == null)
                    return;

                Lista = iEmpleadosNegocio.Listar();
                Empleado = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Empleado = new Empleados();
            Empleado.FechaContratacion = DateTime.Today; // Inicializa con el día de hoy
            Empleado.Salario = 0;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Empleado = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Empleado == null)
                    return;

                if (Empleado.Id == 0)
                {
                    Empleado = iEmpleadosNegocio.Guardar(Empleado);
                }
                else
                {
                    Empleado = iEmpleadosNegocio.Modificar(Empleado);
                }

                if (Empleado == null || Empleado.Id == 0)
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
                if (Empleado == null)
                    return;

                bool eliminado = iEmpleadosNegocio.Borrar(Empleado.Id);

                if (!eliminado)
                    throw new Exception("No se pudo procesar la baja del empleado en el servidor.");

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
                Empleado = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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