# 🧩 Entités - Modèle métier GameShelf

Ce fichier décrit les principales entités métier utilisées dans GameShelf, ainsi que leurs relations.

---

## 👤 User

- `Id` (Guid)
- `ExternalId` (string)
- `Email` (string)
- `Pseudo` (string)
- `GivenName` / `Surname` (string)
- `Role` : enum (`User`, `Admin`)

🔗 Relations :
- Possède plusieurs `UserGame`
- Possède plusieurs `UserProposal`

---

## 🎮 Game

- `Id` (Guid)
- `Titre` (string)
- `Description` (string)
- `DateSortie` (DateTime)
- `Editeur` (string)
- `ImagePath` (string)

🔗 Relations :
- Plusieurs `Tags` (via `GameTag`)
- Plusieurs `Platforms` (via `GamePlatform`)

---

## 📚 UserGame

- `Id` (Guid)
- `UserId` / `GameId` (Guid)
- `Note` (int?)
- `Statut` (enum) → Ex: À jouer, Terminé, Abandonné, etc.
- `DateAjout` (DateTime)

🔗 Relations :
- Appartient à un `User`
- Référence un `Game`

---

## 🏷️ Tag

- `Id` (Guid)
- `Nom` (string)

🔗 Relations :
- Lié à plusieurs `Game` via `GameTag`

---

## 🕹️ Platform

- `Id` (Guid)
- `Nom` (string)

🔗 Relations :
- Liée à plusieurs `Game` via `GamePlatform`

---

## 🔁 GameTag (table de jointure)

- `GameId` (Guid)
- `TagId` (Guid)

---

## 🔁 GamePlatform (table de jointure)

- `GameId` (Guid)
- `PlatformId` (Guid)

---

## 📨 UserProposal

- `Id` (Guid)
- `Titre` (string)
- `DateSoumission` (DateTime)
- `PlatformId` (Guid)
- `UserId` (Guid)

🔗 Relations :
- Proposée par un `User`
- Ciblée vers une `Platform`
