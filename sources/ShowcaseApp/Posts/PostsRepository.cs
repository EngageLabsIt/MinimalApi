
namespace ShowcaseApp;

public class PostsRepository
{
    private readonly List<Post> _posts = new()
    {
        new Post { Id = 1, Title = "First Post", Content = "This is the first post." },
        new Post { Id = 2, Title = "Second Post", Content = "This is the second post." },
        new Post { Id = 3, Title = "Third Post", Content = "This is the third post." }
    };

    internal Task<Post> CreatePostAsync(Post post)
    {
        post.Id = _posts.Max(p => p.Id) + 1;
        _posts.Add(post);
        return Task.FromResult(post);
    }

    internal Task DeletePostAsync(int id)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post is not null)
        {
            _posts.Remove(post);
        }

        return Task.CompletedTask;
    }

    internal Task<Post?> GetPostAsync(int id)
    {
        return Task.FromResult(_posts.FirstOrDefault(p => p.Id == id));
    }

    internal Task<IEnumerable<Post>> GetPostsAsync()
    {
        return Task.FromResult<IEnumerable<Post>>(_posts);
    }

    internal Task<Post?> UpdatePostAsync(int id, Post post)
    {
        var existingPost = _posts.FirstOrDefault(p => p.Id == id);
        if (existingPost is not null)
        {
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
        }

        return Task.FromResult(existingPost);
    }
}
