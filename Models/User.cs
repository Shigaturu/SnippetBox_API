namespace SnippetBox_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // DANS UNE VRAIE APPLI NE JAMAIS STOCKÉ EN CLAIR LE MPD MAIS LE HASHÉ
    }
}
