using Microsoft.EntityFrameworkCore;
using SnippetBox_API.Models;
using System.Collections.Generic;


namespace SnippetBox_API.Data
{
    public class SnippetContext : DbContext
    {
        public SnippetContext(DbContextOptions<SnippetContext> options)
       : base(options)
        {
        }

        public DbSet<Snippet> Snippets { get; set; }
    }
}
