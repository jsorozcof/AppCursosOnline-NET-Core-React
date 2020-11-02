using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.Cursos;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;
using Seguridad;
using WebAPI.Middleware;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;
using Persistencia.DapperConexion;
using Persistencia.DapperConexion.Instructor;
using Microsoft.OpenApi.Models;
using Persistencia.DapperConexion.Paginacion;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("cursoApp", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddDbContext<CurosOnlineContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddOptions();
            services.Configure<ConexionConfiguracion>(Configuration.GetSection("ConnectionStrings"));


            services.AddMediatR(typeof(Consulta.Manejador).Assembly);
            
            services.AddControllers(opt => {

                // Para crear la implementacion de autorizacion generica para los
                // controladores
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);

            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario,IdentityRole>>();

            identityBuilder.AddEntityFrameworkStores<CurosOnlineContext>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            services.TryAddSingleton<ISystemClock,SystemClock>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
               // Se pueden incluir configuraciones para restringir
               //las IP que van hacer peticiones al API
               var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
                opt.TokenValidationParameters = new  Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false, // Aqui se puede configurar la ip de la compañia que consumira el WEB API
                    ValidateIssuer = false,  //
                };
            });


            services.AddScoped<IJwtGenerador, JwtGenerador>();
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();
            services.AddAutoMapper(typeof(Consulta.Manejador));

            services.AddTransient<IFactoryConnection, FactoryConnection>();
            services.AddScoped<IInstructor, InstructorRepositorio>();
            services.AddScoped<IPaginacion, PaginacionRepositorio>();
            

              services.AddSwaggerGen( c => {
               c.SwaggerDoc("v1", new OpenApiInfo{
                Title = "Services para mantenimiento de cursos",
                Version = "v1"
               });     
               c.CustomSchemaIds(c=>c.FullName);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("cursoApp");

            app.UseMiddleware<ManejadorErrorMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
             
            app.UseSwaggerUI( c=>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Cursos Online v1");
                
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                );

                
                endpoints.MapFallbackToController("Index", "Home");
            });
        }
    }
}
