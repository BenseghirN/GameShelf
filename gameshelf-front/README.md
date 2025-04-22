
# GameShelf - Frontend (React + Vite)

## ✨ À propos

Ce dossier contient l’interface utilisateur de l’application **GameShelf**. Elle est construite avec :

- **React 19** + **Redux Toolkit**
- **Vite** pour le bundling rapide
- La librairie **MUI** pour le style
- Un découpage clair entre les pages publiques, utilisateur et administration

---

## ⚙️ Installation

```bash
cd gameShelf-front
npm install
```

> ⚠️ Assurez-vous d’avoir Node.js (v18+ conseillé) et npm installés

---

## 🌍 Configuration

Créer un fichier `.env` à la racine avec :

```
VITE_API_BASE_URL=http://localhost:5187
```

---

## 🚀 Build & lancement

### En développement :

```bash
npm run dev
```

### En production :

```bash
npm run build
```

Les fichiers statiques sont automatiquement placés dans `/wwwroot` bon un hebergement côté .NET.

---

## 🗂️ Structure du projet

```
gameShelf-front/
├── 📂 src/
│   ├── 📂 app/              ← Configuration Redux
│   ├── 📂 components/       ← Composants réutilisables (modale, header, cards...)
│   ├── 📂 hooks/            ← Hooks personalisés réutilisables
│   ├── 📂 pages/            ← Pages (MyLibrary, Games, Admin, etc.)
│   ├── 📂 router/           ← Définition des routes / URL pour accéder aux pages
│   ├── 📂 store/            ← Slices + composants liés (ex: games, tags, user)
│   ├── 📂 styles/           ← Fichiers css + globals
│   ├── 📂 types/            ← Types TS represantant les DTOs transitant entre le front et le back
│   └── 📂 utils/            ← Fonctions utilitaires, constantes
├── 📂 public/
└── .env
```

---

## 🔐 Authentification

- Login / signup via Azure B2C
- Token JWT stocké dans les cookies
- Décodage local pour affichage conditionnel des rôles
- Routes protégées via `RequireAuth` wrapper

---

## 🧠 State management

- Utilise `@reduxjs/toolkit`
- Slices :
  - `gameSlice`, `userSlice`, `tagSlice`, `platformSlice`, `userGameSlice`, `proposalSlice`, `admin*Slice`
- Toaster global (`useToast()`) intégré

---

## 🧪 Tests

> (Non inclus dans cette version, mais structure prête à accueillir Jest + Testing Library)

