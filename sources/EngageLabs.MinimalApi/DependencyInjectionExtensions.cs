using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace EngageLabs.MinimalApi;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Scans the given assemblies and registers all the endpoints of any <see cref="ApiModule"/> it founds.
    /// </summary>
    public static void MapApi(this WebApplication app, Assembly[] assembly, Action<IEndpointConventionBuilder>? configure = null)
    {
        var api = app.MapGroup("api");

        configure?.Invoke(api);

        var modules = assembly.SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && !type.IsAbstract && type.IsAssignableTo(typeof(ApiModule)))
            .Select(Activator.CreateInstance)
            .Cast<ApiModule>()
            .ToArray();

        foreach (var module in modules)
        {
            var group = api.MapGroup($"/{module.Prefix}");
            module.Configure(group);
            module.RegisterEndpoints(group);
        }
    }

    /// <summary>
    /// Scans the given assembly and registers all the endpoints of any <see cref="ApiModule"/> it founds.
    /// </summary>
    public static void MapApi(this WebApplication app, Assembly assembly, Action<IEndpointConventionBuilder>? configure = null)
    {
        app.MapApi([assembly], configure);
    }
}
