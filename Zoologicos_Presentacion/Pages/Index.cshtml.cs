using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Zoologicos_Presentacion.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty] public bool EstaLogueado { get; set; } = false;

        [BindProperty]
        public string? Usuario { get; set; }

        [BindProperty]
        public string? Contrasena { get; set; }

        public void OnGet()
        {
            // Validar si el pasaporte digital ya existe en el servidor
            var sesionActiva = HttpContext.Session.GetString("UsuarioSede");
            if (!string.IsNullOrEmpty(sesionActiva))
            {
                EstaLogueado = true;
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                Usuario = string.Empty;
                Contrasena = string.Empty;
            }
            catch (Exception ex)
            {
                // Reutiliza tu clase de logs nativa del proyecto
                // LogConversor.Log(ex, ViewData!); 
            }
        }

        public IActionResult OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Contrasena))
                {
                    OnPostBtClean();
                    ViewData["Mensaje"] = "Por favor diligencie todos los campos.";
                    return Page();
                }

                // Validación estricta con credenciales fijas (Quemadas)
                if (Usuario != "LuLu" || Contrasena != "CopitodeNieve")
                {
                    OnPostBtClean();
                    ViewData["Mensaje"] = "Credenciales incorrectas. Intente de nuevo.";
                    return Page();
                }

                // Registrar el "Pasaporte" en la sesión del servidor
                HttpContext.Session.SetString("UsuarioSede", Usuario);

                // Redirección limpia al mismo Index para mutar la interfaz
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }

        public IActionResult OnPostBtClose()
        {
            try
            {
                // Destruir por completo la sesión del zoológico
                HttpContext.Session.Clear();
                EstaLogueado = false;

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }
    }
}
