using Microsoft.AspNetCore.Identity;

namespace Blogger.Data;

public class User: IdentityUser
{
    public List<Post> Posts { get; set; }
}