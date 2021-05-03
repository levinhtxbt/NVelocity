using Microsoft.Extensions.DependencyInjection;

namespace NVelocity
{
    public class NVelocityBuilder
    {
	    public IServiceCollection Services;

	    public NVelocityBuilder(IServiceCollection services)
	    {
		    Services = services;
	    }
    }
}
