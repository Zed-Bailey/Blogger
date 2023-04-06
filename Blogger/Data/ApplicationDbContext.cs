using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}