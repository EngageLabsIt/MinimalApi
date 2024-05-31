using EngageLabs.MinimalApi;
using ShowcaseApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerDocument()
    .AddEndpointsApiExplorer()
    .AddSingleton<PostsRepository>();

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi();

app.MapApi(typeof(Program).Assembly);

app.Run();
