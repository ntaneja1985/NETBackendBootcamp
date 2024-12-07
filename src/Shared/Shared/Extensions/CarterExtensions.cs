using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class CarterExtensions
    {
        public static IServiceCollection AddCarterWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCarter(configurator: config =>
            {
                //Get all implementations of ICarterModule and register them to expose the HTTP methods.
                foreach (var assembly in assemblies)
                {
                    var modules = assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();
                    
                    config.WithModules(modules);
                }
            });
            return services;
        }
    }
}
