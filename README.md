# GameShelf

## ✨ About The Project
GameShelf est une application web complète de gestion de bibliothèque de jeux vidéo, permettant aux utilisateurs de créer, suivre et organiser leur collection de jeux. Elle inclut une authentification Azure B2C, une base de données PostgreSQL dockerisée, un backend .NET en Clean Architecture, un frontend React/Next.js/Tailwind, et un exécutable pour automatiser la mise en place de l'environnement de développement.

## 🎓 Public cible
Cette application est un projet éducatif destiné à être présenté dans le cadre d'un examen de fin de semestre.

---

### 🔗 Built With

  * [![.Net][.Net]][.Net-url]
  * [![EntityFramework][EF]][EF-url]
  * [![AutoMapper][AutoMapper]][AutoMapper-url]
  * [![OAuth2][OAuth2]][OAuth2-url]
  * [![React][React.js]][React-url]
  * [![Redux][Redux]][Redux-url]
  * [![Vite][Vite]][Vite-url]
  * [![Mui][Mui]][Mui-url]

---

## ⚙️ Structure du projet
```
GameShelf/
├── 📂 gameshelf-back/                  # Backend .NET 9 Clean Architecture
│   ├── 📂 GameShelf.API/               
│   ├── 📂 GameShelf.Application/       
│   ├── 📂 GameShelf.Domain/       
│   ├── 📂 GameShelf.Infrastructure/
│   ├── 📂 GameShelf.DevSetup/          # Setup automatique (console)
│   └── 📂 GameShelf.UnitTests/    
├── 📂 gameshelf-front/                 # Frontend React + Redux + Tailwind (Vite)     
```

---

## ⚡ Setup rapide avec l'exécutable

> Depuis la racine du projet :

```bash
./GameShelf.DevSetup.exe
```

Cela va :
1. Demander l'`AzureB2C ClientId` (Fournie dans le document d'analyse)
2. Générer le fichier `appsettings.Development.json`
3. Compiler tous les projets .NET
4. Lancer Docker Compose pour créer la base PostgreSQL
5. Appliquer les migrations EF Core
6. Générer le fichier `.env` pour le frontend
7. Installer les dépendances NPM et builder le frontend

---

## 🔧 Setup manuel (en cas de souci)

1. Ajouter manuellement `appsettings.Development.json` dans `GameShelf.API`
2. Lancer `docker-compose up -d`
3. Appliquer les migrations EF :
```bash
dotnet ef database update --project ./gameshelf-back/GameShelf.Infrastructure --startup-project ./gameshelf-back/GameShelf.API
```
4. Créer `.env` dans `gameShelf-front` :
```
VITE_API_BASE_URL=http://localhost:5187
```
5. Builder le frontend :
```bash
cd gameShelf-front
npm install
npm run build
```

---

## 🔑 Authentification
- Azure B2C
- Token géré en cookies
- Middleware de synchronisation des utilisateurs dans la base locale

---

## 🔐 Accès admin
- `/admin/tags` : gestion des tags
- `/admin/platforms` : gestion des plateformes
- `/admin/games` : gestion des jeux
- `/admin/proposals` : validation des propositions utilisateur
- `/admin/users` : promotion/rétrogradation des utilisateurs


---

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Redux]: https://img.shields.io/badge/Redux-764ABC?style=for-the-badge&logo=redux&logoColor=white
[Redux-url]: https://redux.js.org/
[.Net]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[.Net-url]: https://dotnet.microsoft.com/
[Vite]: https://img.shields.io/badge/Vite-646CFF?style=for-the-badge&logo=vite&logoColor=white
[Vite-url]: https://vitejs.dev/
[EF]: https://img.shields.io/badge/Entity%20Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white
[EF-url]: https://learn.microsoft.com/en-us/ef/core/
[AutoMapper]: https://img.shields.io/badge/AutoMapper-FF6F61?style=for-the-badge&logo=automapper&logoColor=white
[AutoMapper-url]: https://automapper.org/
[OAuth2]: https://img.shields.io/badge/OAuth2-000000?style=for-the-badge&logo=oauth&logoColor=white
[OAuth2-url]: https://oauth.net/2/
[Mui]: https://img.shields.io/badge/Material%20UI-007FFF?style=for-the-badge&logo=mui&logoColor=white
[Mui-url]: https://mui.com/
