using System;
using Microsoft.Extensions.DependencyInjection;

namespace NVelocity.Extensions
{
    public static class ServiceCollectionExtensions
    {
		public static IServiceCollection AddNVelocity(
			this IServiceCollection services,
			Action<NVelocityBuilder> configure = null)
		{
			var configuration = new NVelocityBuilder(services);
			//configuration.AddTemplateLoader();

			configure?.Invoke(configuration);

			return services;
		}
		
	}
}
