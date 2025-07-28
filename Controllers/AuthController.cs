using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SnippetBox_API.DTOs;
using SnippetBox_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SnippetBox_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Pour ce projet, on utilise une simple liste en mémoire pour les utilisateurs.
        private static List<User> _users = new List<User>();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto request)
        {
            if (_users.Any(u => u.Username == request.Username))
            {
                return BadRequest("Cet utilisateur existe déjà.");
            }

            var newUser = new User
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                Username = request.Username,
                Password = request.Password // Rappel : à ne pas faire en production !
            };

            _users.Add(newUser);

            return Ok(new { Message = "Utilisateur créé avec succès !" });
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto request)
        {
            var user = _users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized("Identifiants invalides.");
            }

            string token = GenerateJwtToken(user);
            return Ok(new { token = token });
        }

        private string GenerateJwtToken(User user)
        {
            // La liste des "claims" (informations sur l'utilisateur que l'on met dans le token)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            // Récupération de la clé secrète depuis appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Création des identifiants de signature
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Création du token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Le token expirera dans 1 heure
                signingCredentials: creds
            );

            // Sérialisation du token en une chaîne de caractères
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}