namespace Blogger.Data;

public class Post
{
    public int PostId { get; set; }
    
    public string Title { get; set; }
    public DateTime PublishDate { get; set; }
    public string Body { get; set; }
    
    public virtual User Author { get; set; }
}