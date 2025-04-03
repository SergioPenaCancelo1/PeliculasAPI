using Microsoft.OpenApi.Models;
using MovieServiceAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configurar que la aplicación escuche en todas las interfaces dentro del contenedor
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // HTTP
    options.ListenAnyIP(8081); // HTTPS
});

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddScoped<TmdbService>();

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔥 Agregar esta línea para asegurarnos de que se pueda acceder desde Docker
app.Urls.Add("http://0.0.0.0:8080");

// Habilitar Swagger en cualquier entorno
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PeliculasAPI V1");
    c.RoutePrefix = "swagger"; // Asegurar que Swagger se sirva en /swagger
});

// Configurar middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
