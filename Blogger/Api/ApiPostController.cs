using Blogger.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Api;

[ApiController]
[Route("/api/posts")]
public class ApiPostController : ControllerBase
{
    private PostService _service { get; set; }
    public ApiPostController(PostService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _service.GetAllPosts();
        return Ok(posts.Select( x => new
        {
            PostId = x.PostId,
            Title = x.Title,
            PublishDate = x.PublishDate,
            Body = x.Body
        }));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPostById(int id)
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