using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria_Presentacion.implementaciones;
using Zoologicos_libreria_Presentacion.interfaces;

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class HistorialesMedicosModel : PageModel
    {
        private IHistorialesMedicosNegocio iHistorialesMedicosNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<HistorialesMedicos>? Lista { get; set; }
        [BindProperty] public HistorialesMedicos? HistorialMedico { get; set; }

        public HistorialesMedicosModel()
        {
            iHistorialesMedicosNegocio = new HistorialesMedicosNegocio();
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
                if (iHistorialesMedicosNegocio == null)
                    return;

                Lista = iHistorialesMedicosNegocio.Listar();
                HistorialMedico = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            HistorialMedico = new HistorialesMedicos();
            HistorialMedico.FechaControl = DateTime.Today; // Inicializa con la fecha actual
            HistorialMedico.Tratamiento = "";
            HistorialMedico.Medicamento = "";
            HistorialMedico.Dosis = "";
            HistorialMedico.EstadoActual = "";
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                HistorialMedico = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (HistorialMedico == null)
                    return;

                if (HistorialMedico.Id == 0)
                {
                    HistorialMedico = iHistorialesMedicosNegocio.Guardar(HistorialMedico);
                }
                else
                {
                    HistorialMedico = iHistorialesMedicosNegocio.Modificar(HistorialMedico);
                }

                if (HistorialMedico == null || HistorialMedico.Id == 0)
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
                if (HistorialMedico == null)
                    return;

                bool eliminado = iHistorialesMedicosNegocio.Borrar(HistorialMedico.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el registro médico en el servidor.");

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
                HistorialMedico = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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