using API.Errores;
using BLL.Servicios;
using BLL.Servicios.Interfaces;
using Data;
using Data.Interfaces;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;
using Data.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Utilidades;

namespace API.Extensiones
{
    public static class ServicioAplicacionExtension
    {
        // En esta clase se va a agregar los servicios que se encontraban en el Program.cs
        public static IServiceCollection AgregarServiciosAplicacion(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ingrese Bearer [espacio] token \r\n\r\n " +
                                    "Ejemplo: Bearer ejoy89089900999",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            // Agregar servicio para configurar conexion a la base de datos
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ConexionSQL"));
            });
            // Agregar servicio CORS para integrar aplicaciones
            services.AddCors();

            // Agregar nuevo servicio creado
            services.AddScoped<ITokenServicio, TokenSercicio>();

            // Agregar nuevo servicio para cambiar el comportamiento de las validaciones
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errores = ActionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidacionErrorResponse
                    {
                        Errores = errores
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            // Agregar la unidad de trabajo como un servicio, para poder inyectar
            services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();

            // Agregar la especialidad servicio como un servicio, para poder inyectar
            services.AddScoped<IEspecialidadServicio, EspecialidadServicio>();

            // Agregar el medico servicio como un servicio, para poder inyectar
            services.AddScoped<IMedicoServicio, MedicoServicio>();

            // Agregar servicio para AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}