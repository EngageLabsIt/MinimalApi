using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EngageLabs.MinimalApi;

public abstract class ApiModule
{
    public abstract string Prefix { get; }

    public abstract void RegisterEndpoints(IEndpointRouteBuilder app);

    public virtual void Configure(IEndpointConventionBuilder group)
    {
        // No default operations!
    }
}
