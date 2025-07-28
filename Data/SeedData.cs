using Microsoft.EntityFrameworkCore;
using SnippetBox_API.Models;

namespace SnippetBox_API.Data;

public static class SeedData
{ // On passe l'objet IApplicationBuilder (l'application "app")
    public static void Initialize(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {

            var context = scope.ServiceProvider.GetRequiredService<SnippetContext>();

            if (context.Snippets.Any())
            {
                return;
            }

            context.Snippets.AddRange(
                new Snippet
                {
                    Title = "Requête LINQ pour filtrer une liste",
                    Code = "var evenNumbers = numbers.Where(n => n % 2 == 0);",
                    Language = "C#",
                    Description = "Un exemple simple de requête LINQ pour obtenir les nombres pairs d'une collection.",
                    CreatedAt = DateTime.UtcNow
                },
                new Snippet
                {
                    Title = "Appel Fetch API en JavaScript",
                    Code = "fetch('https://api.example.com/data').then(res => res.json()).then(data => console.log(data));",
                    Language = "JavaScript",
                    Description = "Modèle de base pour effectuer une requête GET avec l'API Fetch.",
                    CreatedAt = DateTime.UtcNow
                },
                new Snippet
                {
                    Title = "Centrer un div avec Flexbox",
                    Code = ".container {\n  display: flex;\n  justify-content: center;\n  align-items: center;\n}",
                    Language = "CSS",
                    Description = "La méthode la plus simple pour centrer parfaitement un élément dans son conteneur.",
                    CreatedAt = DateTime.UtcNow
                },
                new Snippet
                {
                    Title = "Requête SQL avec INNER JOIN",
                    Code = "SELECT o.OrderID, c.CustomerName FROM Orders o INNER JOIN Customers c ON o.CustomerID = c.CustomerID;",
                    Language = "SQL",
                    Description = "Jointure classique entre une table de commandes et une table de clients.",
                    CreatedAt = DateTime.UtcNow
                },
                new Snippet
                {
                    Title = "List Comprehension en Python",
                    Code = "squares = [x**2 for x in range(10)]",
                    Language = "Python",
                    Description = "Une manière concise en Python de créer une liste des carrés des nombres de 0 à 9.",
                    CreatedAt = DateTime.UtcNow
                }
            );

            context.SaveChanges();
        }
    }
}