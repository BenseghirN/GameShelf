
# 🎮 GameShelf - Backend (.NET)

## ✨ À propos

Ce dossier contient l'ensemble de la logique backend de l'application **GameShelf**, basée sur :
- .NET 9
- Une architecture **Clean Architecture / Domain-Driven Design**
- Entity Framework Core
- AutoMapper
- Authentification via Azure B2C

---

### 📁 Structure du dossier `gameShelf-back`

```
gameShelf-back/
├── 📂 GameShelf.API/             ← API principale exposée via controllers
├── 📂 GameShelf.Application/     ← Logique applicative, interfaces, services, DTOs
├── 📂 GameShelf.Domain/          ← Entités métier, règles, ValueObjects
├── 📂 GameShelf.Infrastructure/  ← Accès base de données, Migrations EF Core
├── 📂 GameShelf.DevSetup/        ← Outil console de setup automatique
└── 📂 GameShelf.Tests/           ← Tests unitaires (xUnit)
```

---

## ⚙️ Configuration initiale

### 🔧 appsettings.Development.json

Le fichier doit être placé dans `GameShelf.API/` avec ce contenu typique :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=gameshelf;Username=postgres;Password=<MDP_DU_DOCKER-COMPOSE_FILE>"
  },
  "AzureAdB2C": {
    "Authority": <LIEN_AUTHORITY>,
    "ClientId": <CLIENT_ID>,
    "CallbackPath": "/signin-oidc",
    "OpenIdScheme": "GameShelf_OAuth2_B2C"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

---

## 🐘 Base de données PostgreSQL

La configuration Docker est fournie à la racine du projet avec ce fichier `docker-compose.yml` :

```yaml
services:
  db:
    image: postgres:15
    restart: always
    container_name: gameshelf-db
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: <PASSWORD>
      POSTGRES_DB: gameshelf
    ports:
      - "5432:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data

volumes:
  pgdata:
```

Lancer avec :

```bash
docker compose up -d
```

---

## 📦 Migrations EF Core

Pour appliquer les migrations :

```bash
dotnet ef database update --project ./gameshelf-back/GameShelf.Infrastructure --startup-project ./gameshelf-back/GameShelf.API
```

Pour en créer une nouvelle :

```bash
dotnet ef migrations add NomDeMigration --project ./gameshelf-back/GameShelf.Infrastructure --startup-project ./gameshelf-back/GameShelf.API
```

---

## 📘 Documentation technique

> Pour plus de détails sur l'API (endpoints, modèles), run du projet API et voir :

- [Swagger UI](http://localhost:5187/swagger)
- [📄 Liste des entités principales](docs/entities.md)
- [📄 Liste des endpoints REST](docs/endpoints.md)

---

 ## 🔐 Authentification

- Basée sur Azure B2C (OpenID Connect)
- Les utilisateurs sont synchronisés automatiquement dans la base locale
- Token JWT vérifiés via middleware et géré dans les cookies

---

## 🔄 Mapper les données

AutoMapper est configuré dans le projet Application :
- MappingProfile centralise tous les mappages DTO <-> Entités

---

## 🧪 Tests

Des tests unitaires sont placés dans le projet `GameShelf.Tests/`  
Utilise `xUnit` + `FluentAssertions`.

---

## 🏗️ Build

```bash
dotnet build gameshelf-back/GameShelf.API
```

---
