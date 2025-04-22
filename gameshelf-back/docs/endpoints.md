
# 📚 API Endpoints - GameShelf

Voici la liste des endpoints REST exposés par l'API GameShelf, classés par domaine.

---

## 🔐 Authentification

| Méthode | Endpoint             | Description                  |
|---------|----------------------|------------------------------|
| POST    | /api/auth/login      | Connexion utilisateur        |

---

## 👤 Utilisateurs

| Méthode | Endpoint             | Description                            |
|---------|----------------------|----------------------------------------|
| GET     | /api/users           | Liste des utilisateurs                 |
| GET     | /api/users/{id}      | Détails d’un utilisateur               |
| PUT     | /api/users/promote/{id} | Promouvoir en administrateur        |
| PUT     | /api/users/demote/{id}  | Rétrograder en utilisateur standard |

---

## 🎮 Jeux

| Méthode | Endpoint             | Description                            |
|---------|----------------------|----------------------------------------|
| GET     | /api/games           | Liste des jeux                         |
| GET     | /api/games/{id}      | Détails d’un jeu                       |
| POST    | /api/games           | Création d’un jeu (admin)              |
| PUT     | /api/games/{id}      | Modification d’un jeu (admin)          |
| DELETE  | /api/games/{id}      | Suppression d’un jeu (admin)           |

---

## 📚 Bibliothèque utilisateur

| Méthode | Endpoint                 | Description                         |
|---------|--------------------------|-------------------------------------|
| GET     | /api/library             | Récupérer la bibliothèque actuelle  |
| POST    | /api/library             | Ajouter un jeu à la bibliothèque    |
| PUT     | /api/library/{id}        | Modifier l’état (note, statut)      |
| DELETE  | /api/library/{id}        | Retirer un jeu de la bibliothèque   |

---

## 🏷️ Tags

| Méthode | Endpoint             | Description              |
|---------|----------------------|--------------------------|
| GET     | /api/tags            | Liste des tags           |
| POST    | /api/tags            | Créer un tag (admin)     |
| PUT     | /api/tags/{id}       | Modifier un tag (admin)  |
| DELETE  | /api/tags/{id}       | Supprimer un tag (admin) |

---

## 🕹️ Plateformes

| Méthode | Endpoint             | Description                  |
|---------|----------------------|------------------------------|
| GET     | /api/platforms       | Liste des plateformes        |
| POST    | /api/platforms       | Créer une plateforme (admin) |
| PUT     | /api/platforms/{id}  | Modifier une plateforme      |
| DELETE  | /api/platforms/{id}  | Supprimer une plateforme     |

---

## 📨 Propositions utilisateur

| Méthode | Endpoint                 | Description                                |
|---------|--------------------------|--------------------------------------------|
| GET     | /api/userproposals       | Liste des propositions (admin)             |
| GET     | /api/userproposals/mine  | Mes propositions                          |
| POST    | /api/userproposals       | Soumettre une proposition                  |
| DELETE  | /api/userproposals/{id}  | Supprimer une proposition personnelle      |
| PUT     | /api/userproposals/{id}/approve | Valider une proposition (admin)     |
| PUT     | /api/userproposals/{id}/reject  | Rejeter une proposition (admin)     |
