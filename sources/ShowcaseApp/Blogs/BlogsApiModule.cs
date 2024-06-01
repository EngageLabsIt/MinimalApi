using EngageLabs.MinimalApi;

namespace ShowcaseApp.Blogs;

public class BlogsApiModule : ApiModule
{
    public override string Prefix => "blogs";

    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("", async (BlogsRepository repository) =>
        {
            var blogs = await repository.GetBlogsAsync().ConfigureAwait(false);
            return Results.Ok(blogs);
        });

        app.MapGet("{id}", async (BlogsRepository repository, int id) =>
        {
            var blog = await repository.GetBlogAsync(id).ConfigureAwait(false);
            return blog is not null ? Results.Ok(blog) : Results.NotFound();
        });

        app.MapPost("", async (BlogsRepository repository, Blog blog) =>
        {
            var createdBlog = await repository.CreateBlogAsync(blog).ConfigureAwait(false);
            return Results.Created($"/blogs/{createdBlog.Id}", createdBlog);
        });

        app.MapPut("{id}", async (BlogsRepository repository, int id, Blog blog) =>
        {
            var updatedBlog = await repository.UpdateBlogAsync(id, blog).ConfigureAwait(false);
            return updatedBlog is not null ? Results.Ok(updatedBlog) : Results.NotFound();
        });

        app.MapDelete("{id}", async (BlogsRepository repository, int id) =>
        {
            await repository.DeleteBlogAsync(id).ConfigureAwait(false);
            return Results.NoContent();
        })
        .RequireAuthorization(builder =>
        {
            builder.RequireAssertion(_ => false); // Add your authorization logic here
        });
    }

    public override void Configure(IEndpointConventionBuilder group)
    {
        group.WithTags("Blogs");
        group.WithDescription("Endpoints for managing blogs.");
        group.RequireAuthorization(builder =>
        {
            builder.RequireAssertion(_ => true); // Add your authorization logic here
        });
    }
}
