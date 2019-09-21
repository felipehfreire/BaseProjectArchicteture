using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.WebApi.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "first base project",
                    Description = "API do site ",
                    TermsOfService = "Nenhum",
                    Contact = new Contact { Name = "Desenvolvedor X", Email = "email@12.34", Url = "http://meusite.com" },
                    License = new License { Name = "MIT", Url = "http://meusite.com/licensa" }
                });

                //s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

           
        }
    }
}
