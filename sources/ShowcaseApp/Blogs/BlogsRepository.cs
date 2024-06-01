namespace ShowcaseApp.Blogs;

public class BlogsRepository
{
    private readonly List<Blog> _blogs = new()
    {
        new Blog { Id = 1, Title = "First Blog", Content = "This is the first blog." },
        new Blog { Id = 2, Title = "Second Blog", Content = "This is the second blog." },
        new Blog { Id = 3, Title = "Third Blog", Content = "This is the third blog." }
    };

    public Task<Blog> CreateBlogAsync(Blog blog)
    {
        blog.Id = _blogs.Max(p => p.Id) + 1;
        _blogs.Add(blog);
        return Task.FromResult(blog);
    }

    public Task DeleteBlogAsync(int id)
    {
        var blog = _blogs.FirstOrDefault(p => p.Id == id);
        if (blog is not null)
        {
            _blogs.Remove(blog);
        }

        return Task.CompletedTask;
    }

    public Task<Blog?> GetBlogAsync(int id)
    {
        return Task.FromResult(_blogs.FirstOrDefault(p => p.Id == id));
    }

    public Task<IEnumerable<Blog>> GetBlogsAsync()
    {
        return Task.FromResult<IEnumerable<Blog>>(_blogs);
    }

    public Task<Blog?> UpdateBlogAsync(int id, Blog blog)
    {
        var existingBlog = _blogs.FirstOrDefault(p => p.Id == id);
        if (existingBlog is not null)
        {
            existingBlog.Title = blog.Title;
            existingBlog.Content = blog.Content;
        }

        return Task.FromResult(existingBlog);
    }
}
