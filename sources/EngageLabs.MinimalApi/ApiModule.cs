using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EngageLabs.MinimalApi;

/// <summary>
/// A base class to define an API module.
/// </summary>
public abstract class ApiModule
{
    /// <summary>
    /// The prefix of the module.
    /// </summary>
    /// <remarks>
    /// All the registered modules will be prefixed with <code>/api</code>.
    /// Therefore, if the prefix is <code>users</code>, the final route will be <code>/api/users</code>.
    /// </remarks>
    public abstract string Prefix { get; }

    /// <summary>
    /// This method is called to register the endpoints of the module.
    /// </summary>
    public abstract void RegisterEndpoints(IEndpointRouteBuilder app);

    /// <summary>
    /// This method is called to configure the group of the module.
    /// All the conventions applied here will be applied to all the endpoints of the module.
    /// </summary>
    public virtual void Configure(IEndpointConventionBuilder group)
    {
        // No default operations!
    }
}
