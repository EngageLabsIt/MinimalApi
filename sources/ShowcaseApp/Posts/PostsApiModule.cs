using EngageLabs.MinimalApi;

namespace ShowcaseApp;

public class PostsApiModule : ApiModule
{
    public override string Prefix => "posts";

    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("", async (PostsRepository repository) =>
        {
            var posts = await repository.GetPostsAsync().ConfigureAwait(false);
            return Results.Ok(posts);
        });

        app.MapGet("{id}", async (PostsRepository repository, int id) =>
        {
            var post = await repository.GetPostAsync(id).ConfigureAwait(false);
            return post is not null ? Results.Ok(post) : Results.NotFound();
        });

        app.MapPost("", async (PostsRepository repository, Post post) =>
        {
            var createdPost = await repository.CreatePostAsync(post).ConfigureAwait(false);
            return Results.Created($"/posts/{createdPost.Id}", createdPost);
        });

        app.MapPut("{id}", async (PostsRepository repository, int id, Post post) =>
        {
            var updatedPost = await repository.UpdatePostAsync(id, post).ConfigureAwait(false);
            return updatedPost is not null ? Results.Ok(updatedPost) : Results.NotFound();
        });

        app.MapDelete("{id}", async (PostsRepository repository, int id) =>
        {
            await repository.DeletePostAsync(id).ConfigureAwait(false);
            return Results.NoContent();
        });
    }

    public override void Configure(IEndpointConventionBuilder group)
    {
        group.RequireRateLimiting(10, TimeSpan.FromMinutes(1));
    }
}
