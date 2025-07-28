document.addEventListener('DOMContentLoaded', () => {

    // IMPORTANT : Vérifiez que ce port correspond bien au port HTTPS de votre API
    const API_BASE_URL = 'https://localhost:7235';

    // Références aux éléments HTML
    const loginSection = document.getElementById('login-section');
    const snippetsSection = document.getElementById('snippets-section');
    const loginForm = document.getElementById('login-form');
    const loginError = document.getElementById('login-error');
    const snippetListContainer = document.getElementById('snippet-list-container');
    const logoutButton = document.getElementById('logout-button');

    // Vérifie au chargement si un token existe déjà
    const checkLoginStatus = () => {
        const token = localStorage.getItem('jwtToken');
        if (token) {
            loginSection.classList.add('d-none');
            snippetsSection.classList.remove('d-none');
            loadSnippets();
        }
    };

    // Gestion de la soumission du formulaire de login
    loginForm.addEventListener('submit', async (event) => {
        event.preventDefault();
        const username = event.target.username.value;
        const password = event.target.password.value;

        try {
            const response = await fetch(`${API_BASE_URL}/api/auth/login`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ username, password })
            });

            if (response.ok) {
                const data = await response.json();
                localStorage.setItem('jwtToken', data.token);
                loginError.classList.add('d-none');
                checkLoginStatus(); // Affiche la section des snippets
            } else {
                loginError.classList.remove('d-none');
            }
        } catch (error) {
            console.error('Erreur de connexion:', error);
            loginError.classList.remove('d-none');
        }
    });
    
    // Fonction pour charger et afficher les snippets
    const loadSnippets = async () => {
        const token = localStorage.getItem('jwtToken');
        if (!token) return;

        try {
            const response = await fetch(`${API_BASE_URL}/api/Snippets`, {
                headers: { 'Authorization': `Bearer ${token}` }
            });

            if (!response.ok) {
                throw new Error('Erreur lors du chargement des snippets.');
            }

            const snippets = await response.json();
            snippetListContainer.innerHTML = ''; // Vide la liste actuelle

            if (snippets.length === 0) {
                snippetListContainer.innerHTML = '<p class="text-center">Aucun snippet trouvé.</p>';
                return;
            }

            snippets.forEach(snippet => {
                const card = document.createElement('div');
                card.className = 'snippet-card';
                card.innerHTML = `
                    <div class="snippet-card-header d-flex justify-content-between">
                        <strong>${snippet.title}</strong>
                        <span class="badge bg-info text-dark">${snippet.language}</span>
                    </div>
                    <div class="snippet-card-body">
                        <p>${snippet.description || 'Pas de description.'}</p>
                        <pre><code>${escapeHtml(snippet.code)}</code></pre>
                    </div>
                `;
                snippetListContainer.appendChild(card);
            });

        } catch (error) {
            console.error(error);
            // Si le token est invalide, on déconnecte l'utilisateur
            logout();
        }
    };

    // Gestion de la déconnexion
    const logout = () => {
        localStorage.removeItem('jwtToken');
        loginSection.classList.remove('d-none');
        snippetsSection.classList.add('d-none');
        loginError.classList.add('d-none');
    };

    logoutButton.addEventListener('click', logout);

    // Petite fonction pour éviter que le HTML dans le code ne soit interprété
    const escapeHtml = (unsafe) => {
        return unsafe
             .replace(/&/g, "&amp;")
             .replace(/</g, "&lt;")
             .replace(/>/g, "&gt;")
             .replace(/"/g, "&quot;")
             .replace(/'/g, "&#039;");
     }

    // Lancement initial
    checkLoginStatus();
});