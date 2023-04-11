namespace Blogger.Data;

public record ImageInsertModel(
    string url,
    string altText,
    int width,
    int height
);