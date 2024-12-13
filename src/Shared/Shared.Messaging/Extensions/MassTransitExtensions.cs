

using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Messaging.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitWithAssemblies (this IServiceCollection services ,params Assembly[] assemblies)
        {
            //Implement Mass Transit configuration
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                config.SetInMemorySagaRepositoryProvider();
                //consumer registration.
                config.AddConsumers(assemblies);
                config.AddSagaStateMachines(assemblies);
                config.AddSagas(assemblies);
                config.AddActivities(assemblies);
                //use in-memory message broker suitable for initial setup
                config.UsingInMemory((context, configurator) =>
                {
                    configurator.ConfigureEndpoints(context);
                });


            });
            return services;
        }
    }
}
