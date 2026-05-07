var builder = WebApplication.CreateBuilder(args);

// mápea todo de lo que hay en controller 

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
