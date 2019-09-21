using CrossCutting.Core.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Presentation.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeCoreInjector.RegisterServices(services);
        }
    }
}
