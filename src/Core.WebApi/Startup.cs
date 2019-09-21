using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Core.WebApi.Configurations;
using Core.WebApi.Middlewares;
using CrossCutting.Core.Filters;
using Eventos.IO.Infra.CrossCutting.Identity.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.WebApi
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
            services.AddMvc();
            // Contexto do EF para o Identity
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configurações de Autenticação, Autorização e JWT.
            services.AddMvcSecurity(Configuration);

            services.AddAutoMapper();
            // Options to custom configurations configurações customizadas
            services.AddOptions();
            // MVC com restrição de XML e adição de filtro de ações.
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
            });

            
            //services.AddApiVersioning("api/v{version}");
            // register ioc
            services.AddDIConfiguration();

            // MediatR
            services.AddMediatR(typeof(Startup));
            
            //services.AddSingleton(typeof(IMediator), typeof(Mediator));

            // Configurações do Swagger
            services.AddSwaggerConfig();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvc();
           

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerAuthorized();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "web api base project");
            });
            #endregion


            #region Configurações MVC

            //CORS - Cross origin Request
            app.UseCors(c =>
            {
                c.AllowAnyHeader();//any header
                c.AllowAnyMethod();//any method
                c.AllowAnyOrigin();//
                //c.WithOrigins("www.meudominio.com, www.site.com")
                //c.WithMethods("GET, POST, DELETE, PUT")
                
            });

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default_route",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Account", action = "Register" }
                );
            });




            #endregion

            
            

        }
    }
}
