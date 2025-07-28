using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnippetBox_API.Data;
using SnippetBox_API.DTOs;
using SnippetBox_API.Models;

namespace SnippetBox_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SnippetsController : ControllerBase
    {
        private readonly SnippetContext _context;
        public SnippetsController(SnippetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSnippets()
        {
            return Ok(_context.Snippets.ToList());
        }

        // GET: api/Snippets/5
        // Cette méthode permet de récupérer UN SEUL snippet par son ID
        [HttpGet("{id}")]
        public IActionResult GetSnippet(int id)
        {
            var snippet = _context.Snippets.Find(id);

            if (snippet == null)
            {
                return NotFound();
            }

            return Ok(snippet);
        }

        // POST: api/Snippets
        // Cette méthode permet de créer un nouveau snippet
        [HttpPost]
        public IActionResult PostSnippet(Snippet snippet)
        {
            // On met la date de création au moment de l'ajout
            snippet.CreatedAt = DateTime.UtcNow;

            _context.Snippets.Add(snippet);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSnippet), new { id = snippet.Id }, snippet);
        }

        // PUT: api/Snippets/5
        // Cette méthode permet de mettre à jour un snippet existant
        [HttpPut("{id}")]
        public IActionResult PutSnippet(int id, SnippetUpdateDto snippetDto)
        {
            var snippetInDb = _context.Snippets.Find(id);

            if (snippetInDb == null)
            {
                return NotFound();
            }

            // On met à jour les propriétés du snippet trouvé en base
            // avec les données du DTO reçu.
            snippetInDb.Title = snippetDto.Title;
            snippetInDb.Code = snippetDto.Code;
            snippetInDb.Language = snippetDto.Language;
            snippetInDb.Description = snippetDto.Description;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Snippets/5
        // Cette méthode permet de supprimer un snippet
        [HttpDelete("{id}")]
        public IActionResult DeleteSnippet(int id)
        {
            var snippet = _context.Snippets.Find(id);

            if (snippet == null)
            {
                return NotFound();
            }

            _context.Snippets.Remove(snippet);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
