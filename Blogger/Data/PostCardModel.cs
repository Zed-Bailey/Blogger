namespace Blogger.Data;

public class PostCardModel
{
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime Date { get; set; }
    public string Author { get; set; }
    public int PostId { get; set; }
    public bool RedirectToEditPage { get; set; } =  false;
}