# SnippetBox API 🚀

Votre bibliothèque de code personnelle, centralisée et accessible partout. Ne perdez plus jamais un extrait de code utile !

## Le Problème

En tant que développeurs, nous réutilisons constamment des bouts de code : des fonctions astucieuses, des expressions régulières complexes, des configurations, etc. Trop souvent, ces "snippets" finissent perdus dans des fichiers notes aléatoires ou d'anciens projets, nous forçant à réinventer la roue et à perdre un temps précieux.

## La Solution

**SnippetBox API** est un service RESTful sécurisé qui offre un moyen simple et efficace de sauvegarder, taguer et retrouver vos extraits de code. L'API est conçue pour être la base de n'importe quel outil de gestion de snippets, que ce soit une application web, mobile ou même une extension d'éditeur de code.

## Fonctionnalités Principales

* **CRUD complet** pour la gestion des snippets.
* **Authentification sécurisée** par JSON Web Tokens (JWT).
* **Protection des routes** : seuls les utilisateurs authentifiés peuvent gérer leurs snippets.
* **Données de démarrage** : Une sélection de snippets utiles est chargée au lancement pour une démo immédiate.
* **Documentation interactive** via Swagger.

## Technologies Utilisées

* ASP.NET Core 8
* Entity Framework Core (In-Memory Database)
* JWT Bearer Authentication
* Swagger / OpenAPI

---

## Démarrage Rapide

### Prérequis

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### 1. Lancer le Backend (API)

L'API sera disponible sur `https://localhost:7235` (vérifiez le port dans `launchSettings.json`).

### 2. Lancer le Frontend (POC)

Le POC est un simple client web statique.
1.  Naviguez dans le dossier `POC_Frontend`.
2.  Ouvrez le fichier **`index.html`** directement dans votre navigateur web (Chrome, Firefox, etc.).

---

## Utilisation de l'API

### Documentation Swagger

Une fois l'API lancée, la documentation interactive Swagger est accessible ici :
[https://localhost:7235/swagger](https://localhost:7235/swagger)

### Compte de Test

Pour tester l'application et l'API, vous devez vous enregistrer avec n'importe quel identifiant (Register with request API),

**Workflow de test :**
1.  Utilisez `POST /api/auth/register` pour créer un compte.
2.  Utilisez `POST /api/auth/login` pour obtenir un token JWT.
3.  Dans Swagger, cliquez sur le bouton `Authorize` et collez le token sous la forme `Bearer [votre_token]`.
4.  Vous pouvez maintenant utiliser les endpoints protégés de `/api/Snippets`.
