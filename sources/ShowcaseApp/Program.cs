using EngageLabs.MinimalApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag;
using ShowcaseApp.Blogs;
using ShowcaseApp.Posts;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerDocument(settings =>
    {
        settings.Title = "Showcase API";
        settings.Version = "v1";
        settings.Description = "A showcase API for EngageLabs.MinimalApi NuGet package.";
        settings.DocumentName = "Showcase API v1";
        settings.AddSecurity(JwtBearerDefaults.AuthenticationScheme, Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        In = OpenApiSecurityApiKeyLocation.Header
                    });
    })
    .AddEndpointsApiExplorer()
    .AddSingleton<PostsRepository>()
    .AddSingleton<BlogsRepository>()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi();

app.MapApi(typeof(Program).Assembly);

app.Run();
