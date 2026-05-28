var builder = WebApplication.CreateBuilder(args);

// Mapea todo lo que hay en los controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🟢 1. AGREGAR ESTO: Configurar la política de CORS
// Permite que tu frontend de Razor Pages pueda consumir los endpoints de este API
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// 🟢 2. OPTIMIZACIÓN: Mover Swagger al principio del pipeline de desarrollo
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty; // Hace que Swagger cargue directamente al abrir la raíz (localhost:XXXX/)
});

app.UseHttpsRedirection();

// 🟢 3. AGREGAR ESTO: Activar CORS antes de la autorización
app.UseCors("PermitirTodo");

app.UseAuthorization();

app.MapControllers();

app.Run();