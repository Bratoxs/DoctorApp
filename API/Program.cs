using API.Extensiones;
using API.Middleware;
using Data.Inicializador;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Llamar a los servicios de la clase ServicioAplicacionExtension
builder.Services.AgregarServiciosAplicacion(builder.Configuration);

// Llamar a los servicios de la clase ServicioIdentidadExtension
builder.Services.AgregarServiciosIdentidad(builder.Configuration);

// Activar a la interfaz y clase
builder.Services.AddScoped<IdbInicializador, DbInicializardor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>(); // Agregar pipeline para controlar lo errores de Middleware
app.UseStatusCodePagesWithReExecute("/errores/{0}");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Agregar pipeline CORS para integrar aplicaciones
app.UseCors(x => x.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

app.UseHttpsRedirection();

// Agregar pipeline para trabaja con Bearer (Token)
app.UseAuthentication();

app.UseAuthorization();

// Activar Inicializador
using(var scope = app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var inicializador = services.GetRequiredService<IdbInicializador>();
        inicializador.Inicializar();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Un Error ocurrio al ejecutar la migración");
    }
}

app.MapControllers();

app.Run();
