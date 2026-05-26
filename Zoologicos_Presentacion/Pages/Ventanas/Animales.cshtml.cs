using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zoologicos_libreria.entidades; // Asegúrate de que apunte a la carpeta de tus entidades

using Zoologicos_libreria_Presentacion.implementaciones; // Donde tengas AnimalesNegocio
using Zoologicos_libreria_Presentacion.interfaces; // Donde tengas IAnimalesNegocio

namespace Zoologicos_Presentacion.Pages.Ventanas
{
    public class AnimalesModel : PageModel
    {
        private IAnimalesNegocio iAnimalesNegocio;

        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Animales>? Lista { get; set; }
        [BindProperty] public Animales? Animal { get; set; }

        public AnimalesModel()
        {
            // Inicializamos la capa de negocio de Animales
            iAnimalesNegocio = new AnimalesNegocio();
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
                if (iAnimalesNegocio == null)
                    return;

                Lista = iAnimalesNegocio.Listar();
                Animal = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Animal = new Animales();
            // Opcional: Puedes setear la fecha de hoy por defecto para el input HTML5 de tipo date
            Animal.FechaNacimiento = DateTime.Today;
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Animal = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                if (Animal == null)
                    return;

                if (Animal.Id == 0)
                {
                    // Crear un nuevo animal
                    Animal = iAnimalesNegocio.Guardar(Animal);
                }
                else
                {
                    // Modificar un animal existente
                    Animal = iAnimalesNegocio.Modificar(Animal);
                }

                if (Animal == null || Animal.Id == 0)
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
                if (Animal == null)
                    return;

                // Enviamos el Id numérico del animal a eliminar
                bool eliminado = iAnimalesNegocio.Borrar(Animal.Id);

                if (!eliminado)
                    throw new Exception("No se pudo eliminar el registro del animal en el servidor.");

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
                Animal = Lista!.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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