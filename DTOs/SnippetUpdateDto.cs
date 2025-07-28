namespace SnippetBox_API.DTOs
{
    public class SnippetUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
