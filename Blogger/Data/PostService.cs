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
        var posts = await _context.Posts
            .Include(x => x.Author)
            .OrderByDescending(x => x.PublishDate)
            .Take(10)
            .ToListAsync();
        return posts;
    }


    public async Task<Post?> GetPostById(int withId)
    {
        return await _context.Posts.FindAsync(withId);
    }

    public async Task AddPost(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePost(Post post)
    {
        var first =_context.Posts.FirstOrDefault(x => x.PostId == post.PostId);
        if (first != null)
        {
            // create an entry
            var entry = _context.Entry(first);
            // update it's current values with the new values from the post object
            entry.CurrentValues.SetValues(post);
            
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Could not update post, was null!");
        }
        
        
    }

    public async Task DeletePost(Post post)
    {
        await _context.Posts.FindAsync(post.PostId);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }



    public async Task<List<Post>> SearchPosts(string query)
    {
        return await _context.Posts
            .Where(x => x.Title.Contains(query))
            .ToListAsync();
    }
}