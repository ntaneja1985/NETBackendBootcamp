using Basket.Data.Processors;
using Basket.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Interceptors;


namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            // 1. Api Endpoint services

            // 2. Application Use Case services
            services.AddScoped<IBasketRepository,BasketRepository>();

           //Manual Decoration of Cached Basket Repository
           //This approach is not maintainable and scalable.
           //Solve this by using Scrutor Library
            //services.AddScoped<IBasketRepository>(provider =>
            //{
            //  var basketRepository = provider.GetRequiredService<IBasketRepository>();
            //    return new CachedBasketRepository(basketRepository,provider.GetRequiredService<IDistributedCache>());
            //});


            //Using scrutor library
           services.Decorate<IBasketRepository,CachedBasketRepository>();

            // 3. Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            services.AddHostedService<OutboxProcessor>();

            return services;
        }

        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline.
            // 1. Use Api Endpoint services

            // 2. Use Application Use Case services

            // 3. Use Data - Infrastructure services

            app.UseMigration<BasketDbContext>();
            return app;
        }
    }
}
