using Blogger.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Api;

[ApiController]
[Route("/api/posts")]
public class ApiPostController : ControllerBase
{
    private readonly PostService _service;
    public ApiPostController(PostService service)
    {
        _service = service;
    }


    /// <summary>
    /// returns all posts
    /// eg. /api/posts?page=1
    /// </summary>
    /// <param name="page">the page to get, defaults to 0, which returns first page</param>
    /// <returns></returns>
    [HttpGet("{page:int?}")]
    public async Task<IActionResult> GetAllPosts(int page=0)
    {
        const int pageSize = 5;
        int start = page * pageSize;
        int end = start + pageSize;
        var range = new Range(start, end);
        
        var postsInPage = await _service.GetPostRange(range);
        return Ok(
            new
            {
                page = page,
                start = start,
                end = end,
                posts = postsInPage.Select( x => new
                {
                    PostId = x.PostId,
                    Title = x.Title,
                    PublishDate = x.PublishDate,
                    Body = x.Body
                })
            }
        );
        
    }

    /// <summary>
    /// Fetches a post by it's id
    /// eg. /api/posts/id/2
    /// </summary>
    /// <param name="id">the post id</param>
    /// <returns></returns>
    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetPostById([FromRoute] int id)
    {
        var post = await _service.GetPostById(id);
        if (post == null)
        {
            return NotFound(new
            {
                mesage = $"post with id = {id} was not found"
            });
        }

        return Ok(new
        {
            PostId = post.PostId,
            Title = post.Title,
            PublishDate = post.PublishDate,
            Body = post.Body
        });
    }
}   