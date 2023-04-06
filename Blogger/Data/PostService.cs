using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blogger.Data;

public class PostService
{
    private ApplicationDbContext _context;
    
    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Post>> GetAllPosts()
    {
        return await _context.Posts.ToListAsync();
    }

    /// <summary>
    /// Takes the 10 most recent posts
    /// </summary>
    /// <returns></returns>
    public async Task<List<Post>> GetRecentPosts()
    {
        return await _context.Posts
            .OrderByDescending(x => x.PublishDate)
            .Take(10)
            .ToListAsync();
    }


    public async Task<Post?> GetPostById(int withId)
    {
        return await _context.Posts.FindAsync(withId);
    }

    public async Task AddPost(Post post)
    {
        // var user = await _context.Users.FindAsync(userId);
        // user?.Posts.Add(post);
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    
}