using System.Text;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.Entidades;

namespace API.Extensiones
{
    public static class ServicioIdentidadExtension
    {
        public static IServiceCollection AgregarServiciosIdentidad(this IServiceCollection services, IConfiguration config)
        {
            // Agregar servicio para trabajar con Identity Core
            services.AddIdentityCore<UsuarioAplicacion>(opt => {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<RolAplicacion>()
                .AddRoleManager<RoleManager<RolAplicacion>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Agregar servicio para trabajar con Bearer (Token)
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey
                                                        (Encoding.UTF8.GetBytes(config["TokenKey"])),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };
                            });

            // Agregar servicio para crear politicas
            services.AddAuthorization(opt => {
                opt.AddPolicy("AdminRol", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("AdminAgendadorRol", policy => policy.RequireRole("Admin", "Agendador"));
                opt.AddPolicy("AdminDoctorRol", policy => policy.RequireRole("Admin", "Doctor"));
                
            });

            return services;
        }
    }
}