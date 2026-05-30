var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.ConfigureFilter(new VerificacionSesionFilter());
});

// Configuración del servicio de sesiones en memoria
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tiempo de expiración por inactividad
    options.Cookie.HttpOnly = true;                 // Seguridad extra para la cookie de sesión
    options.Cookie.IsEssential = true;              // Obligatoria para el funcionamiento del Login
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Habilitar el Middleware de sesiones
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();


app.Run();




public class VerificacionSesionFilter : Microsoft.AspNetCore.Mvc.Filters.IPageFilter
{
    public void OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext context)
    {
        // Detecta en qué página está el usuario actualmente
        var rutaActual = context.ActionDescriptor.ViewEnginePath;

        // Si el usuario está en el Login (Index), lo dejamos pasar sin validar
        if (rutaActual == "/Index" || rutaActual == "/")
        {
            return;
        }

        // Si intenta entrar a cualquier otra página, revisamos la sesión
        var variableSession = context.HttpContext.Session.GetString("UsuarioSede");

        if (string.IsNullOrEmpty(variableSession))
        {
            // Si está vacía, lo expulsamos al Login
            context.HttpContext.Response.Redirect("/");

            // Le avisamos a .NET que detenga la carga de la página inmediatamente
            context.Result = new Microsoft.AspNetCore.Mvc.EmptyResult();
        }
    }


    public void OnPageHandlerExecuted(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext context) { }
    public void OnPageHandlerSelected(Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext context) { }
}