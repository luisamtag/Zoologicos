var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// 🟢 1. AGREGAR ESTO: Configuración del servicio de sesiones en memoria
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// 🟢 2. AGREGAR ESTO: Habilitar el Middleware de sesiones
// NOTA CRÍTICA: Debe ir exactamente AQUÍ (Después de UseRouting y ANTES de UseAuthorization)
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();