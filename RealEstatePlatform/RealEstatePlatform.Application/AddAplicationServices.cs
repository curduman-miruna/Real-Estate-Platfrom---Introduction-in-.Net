using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RealEstatePlatform.Application
{
    public static class ApplicationServiceRegistrationDI
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
                );
        }
    }
}
