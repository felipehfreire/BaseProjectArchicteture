using AutoMapper;
using Core.Application.AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Services;
using Core.Domain.Bus;
using Core.Domain.Events;
using Core.Domain.Notifications;
using Core.Infra.Data.Context;
using Core.Infra.Data.EventSourcing;
using Core.Infra.Data.Repositorys;
using Core.Infra.Data.UoW;
using CrossCutting.Core.Bus;
using CrossCutting.Core.Filters;
using CrossCutting.Core.Identity.Models;
using Domain.CommandHandlers;
using Domain.Interfaces;
using Domain.Products.Commands;
using Domain.Products.Events;
using Domain.Products.Repository;
using Eventos.IO.Infra.CrossCutting.Identity.Models;
using Eventos.IO.Infra.CrossCutting.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CrossCutting.Core.IoC
{
    public class NativeCoreInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {

            #region Application
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton(Mapper.Configuration);
            services.AddSingleton<IConfigurationProvider>(AutoMapperConfiguration.RegisterMappings());
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IProductAppService, ProductAppService>();
            #endregion


            #region INgra.BUS
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            #region Domain
            #region Bus
            #endregion

            #region Commands
            services.AddScoped<IRequestHandler<CreateNewProductCommand>, ProductCommandHandler>();
            #endregion

            #region Events
            
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            //services.AddScoped<IHandler<ProductCreatedEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<ProductCreatedEvent>, ProductEventHandler>();
            #endregion

            #endregion

            #region Infra.Data
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped<CoreContext>();
            services.AddScoped<IEventStore, SqlEventStore>();
            #endregion




            #endregion

            #region Infra - Data EventSourcing
            #endregion

            #region Infra - Identity
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IUser, AspNetUser>();
            #endregion

            #region Infra - Filters
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
            services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            services.AddScoped<GlobalActionLogger>();
            #endregion



        }
    }
}
