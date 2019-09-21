using AutoMapper;
using CrossCutting.Core.Bus;
using CrossCutting.Core.Filters;
using CrossCutting.Core.IoC;
using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Eventos.IO.Infra.CrossCutting.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Core.Presentation
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // AutoMapper
            services.AddAutoMapper();
            // Registrar todos os DI
            //services.AddDIConfiguration();
            RegisterServices(services);

            services.AddElmahIo(o =>
            {
                o.ApiKey = "db89eadb778d4de08a85296cb48b84e4";
                o.LogId = new Guid("5e299c42-e14a-4ebe-9d38-19f7cf650266");
            });

            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor accessor,
              ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();



            //get http container copy and use on memmory BUS
            CoreMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;

            #region Logging

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddElmahIo( "db89eadb778d4de08a85296cb48b84e4",new Guid("5e299c42-e14a-4ebe-9d38-19f7cf650266"));
            var elmahSts = new ElmahIoSettings
            {
                OnMessage = message =>
                {
                    message.Version = "v1.0";
                    message.Application = "Diretiva Tech";
                    message.User = accessor.HttpContext.User.Identity.Name;
                },
            };
            app.UseElmahIo("db89eadb778d4de08a85296cb48b84e4", new Guid("5e299c42-e14a-4ebe-9d38-19f7cf650266"), elmahSts);
            app.UseElmahIo();

            #endregion
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeCoreInjector.RegisterServices(services);
        }
    }
}
