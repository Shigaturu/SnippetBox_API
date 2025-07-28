﻿namespace SnippetBox_API.Models
{
    public class Snippet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
