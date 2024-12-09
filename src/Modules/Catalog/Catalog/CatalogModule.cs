using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviors;
using Shared.Data.Interceptors;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            //Add services to the container

            //Api Endpoint services

            //Application Use Case services
            //services.AddMediatR(config =>
            //{
            //    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            //    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //});
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor,DispatchDomainEventsInterceptor>();

            services.AddDbContext<CatalogDbContext>((sp,options) =>
            {
                //options.AddInterceptors(new AuditableEntityInterceptor(), 
                //    new DispatchDomainEventsInterceptor());
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IDataSeeder, CatalogDataSeeder>();

            return services;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder builder)
        {
            //Configure the HTTP request pipeline

            //1.Use Api Endpoint services

            //2.Use Application Use Case services

            //3.Use Data - Infrastructure services
            builder.UseMigration<CatalogDbContext>();

            //InitializeDatabaseAsync(builder).GetAwaiter().GetResult();  
            return builder;
        }

        //private static async Task InitializeDatabaseAsync(IApplicationBuilder builder)
        //{
        //    using var scope = builder.ApplicationServices.CreateScope();
        //    var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        //    //no need to call update-database command while running migrations
        //    await context.Database.MigrateAsync();
        //}
    }
}
