

# MinimalApi

A way to pack minimal API into configurable modules.

# Table of Contents
- [MinimalApi](#minimalapi)
- [Table of Contents](#table-of-contents)
- [Description](#description)
- [Usage](#usage)
    - [Create your module](#create-your-module)
    - [Register your module](#register-your-module)
    - [Customize your module](#customize-your-module)
- [Sample code](#sample-code)

# Description

This project aims to provide a minimal API framework that allows you to easily configure and organize your API endpoints into modules. It provides a simple and lightweight solution for building APIs with minimal overhead.


# Usage

## Create your module
To define a new module just create a class inheriting from `ApiModule` and provide a `Prefix` for your endpoints and a body for the `RegisterEndpoints`method.

*REMEMBER: All modules alre already prefixed with `api/` when are registered in the application*

```csharp
using EngageLabs.MinimalApi;

public class MyModule : ApiModule
{
    public override string Prefix => "my-module";

    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("", async (PostsRepository repository) =>
        {
            var posts = await repository.GetPostsAsync().ConfigureAwait(false);
            return Results.Ok(posts);
        });

        // ...
    }
}
```

## Register your module
To register all of your modules, gust call the `MapApi()` extension method in `Program.cs`.

```csharp

using EngageLabs.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

// Your service registrations

var app = builder.Build();

// Your middleware

app.MapApi(typeof(Program).Assembly); // <-- Register api modules

app.Run();
```

The `MapApi()` extension method accepts 2 parameters:

1. An assembly (or an array of assemblies) to scan to register all the modules ant the related endpoints

2. A configuration action to define the behavior of the entire api. This is a good spot, for example, to configure application-wide logics like authentication, rate limiting, etc.

## Customize your module
If you need to define some behaviors common to all the endpoints of the module, you can override the `Configure` method of `ApiModule` base class. You will be provided with a `IEndpointConventionBuilder` to define your conventions for the module.

The `Configure` method is a good spot, for example, to configure Open Api informations like name, description, tags (also used in swagger page), etc or some behaviors that differ from the application-wide ones defined in the [global configuration](#register-your-module).

```csharp
using EngageLabs.MinimalApi;

public class MyModule : ApiModule
{
    // ... Other module code ...

    public override void Configure(IEndpointConventionBuilder group)
    {
        // Your conventions here
        group.RequireAuthorization()
            .RequireRateLimiting.RequireRateLimiting(10, TimeSpan.FromMinutes(1));

        group.WithName("My Module")
            .WithDescription("My awesome API module")
            .WithTags("API Module");
    }
}
```

*NOTE: Conventions defined in the `Configure`method can be overridden by a single endpoint. For example you can configure your module with `RequireAuthentication`but have one or more `MapGet().AllowAnonymous()` in the `RegisterEndpoint` method*

# Sample code
See the [showcase app readme](sources/ShowcaseApp/README.md) for an overview of the showcase app with some examples

