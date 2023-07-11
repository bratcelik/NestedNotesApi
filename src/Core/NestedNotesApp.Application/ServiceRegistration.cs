using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NestedNotesApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assm);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assm));
        }
    }
}
