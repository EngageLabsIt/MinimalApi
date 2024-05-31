using System.Reflection;
using EngageLabs.MinimalApi;
using Microsoft.AspNetCore.Builder;

namespace EngageLabs.Extensions.MinimalApi;

public static class DependencyInjectionExtensions
{
    public static void MapApi(this WebApplication app, Assembly[] assembly, Action<IEndpointConventionBuilder>? configure = null)
    {
        var api = app.MapGroup("api");

        if (configure != null)
        {
            configure(api);
        }

        var modules = assembly.SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && !type.IsAbstract && type.IsAssignableTo(typeof(ApiModule)))
            .Select(Activator.CreateInstance)
            .Cast<ApiModule>()
            .ToArray();

        foreach (var module in modules){
            var group = api.MapGroup($"/{module.Prefix}");
            module.Configure(group);
            module.RegisterEndpoints(group);
        }
    }

    public static void MapApi(this WebApplication app, Assembly assembly, Action<IEndpointConventionBuilder>? configure = null)
    {
        app.MapApi([assembly], configure);
    }
}
