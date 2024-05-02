using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstatePlatform.Application.Contracts;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Infrastructure.Repositories;

namespace RealEstatePlatform.Infrastructure
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastructureToDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RealEstatePlatformContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("RealEstatePlatformConnectionString"),
                builder => builder.MigrationsAssembly(typeof(RealEstatePlatformContext).Assembly.FullName)
            )
        );
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IListingTypeRepository, ListingTypeRepository>();
            return services;
        }
    }
}

