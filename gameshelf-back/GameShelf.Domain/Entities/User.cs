using GameShelf.Domain.Enums;

namespace GameShelf.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }  // locale
        public string ExternalId { get; set; } = string.Empty; // Azure ID
        public string Email { get; set; } = string.Empty;
        public string Pseudo { get; set; } = string.Empty;
        public string GivenName { get; set; } = string.Empty; // prénom
        public string Surname { get; set; } = string.Empty; // nom de famille
        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();
        public ICollection<UserProposal> Propositions { get; set; } = new List<UserProposal>();

        public void PromoteToAdmin() => Role = UserRole.Admin;
        public void DemoteToUser() => Role = UserRole.User;
        public User Create(string externalId, string email, string pseudo, string givenName, string surName, UserRole role = UserRole.User)
        {
            if (string.IsNullOrWhiteSpace(pseudo)) throw new ArgumentException("Pseudo invalide.");
            if (string.IsNullOrWhiteSpace(givenName)) throw new ArgumentException("Prénom invalide.");
            if (string.IsNullOrWhiteSpace(surName)) throw new ArgumentException("Nom invalide.");
            if (string.IsNullOrWhiteSpace(externalId)) throw new ArgumentException("ID externe invalide.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email invalide.");


            return new User
            {
                ExternalId = externalId,
                Email = email,
                Pseudo = pseudo,
                GivenName = givenName,
                Surname = surName,
                Role = role
            };
        }

        public void AddGame(Guid gameId, GameStatus statut = GameStatus.Possede, int? note = null)
        {
            if (UserGames.Any(g => g.GameId == gameId))
                throw new InvalidOperationException("Ce jeu est déjà présent dans la collection.");

            UserGames.Add(new UserGame
            {
                UserId = Id,
                GameId = gameId,
                Statut = statut,
                Note = note
            });
        }

        public void UpdateGameStatus(Guid gameId, GameStatus newStatut, int? newNote)
        {
            UserGame? userGame = UserGames.FirstOrDefault(ug => ug.GameId == gameId);
            if (userGame == null)
                throw new InvalidOperationException("Ce jeu n'est pas dans la collection.");

            userGame.Statut = newStatut;
            userGame.Note = newNote;
        }

        public void RemoveGame(Guid gameId)
        {
            UserGame? userGame = UserGames.FirstOrDefault(ug => ug.GameId == gameId);
            if (userGame != null)
            {
                UserGames.Remove(userGame);
            }
        }

        public UserProposal ProposeGame(Guid userId, Guid platformId, string titre, string? imagePath = null)
        {
            if (Propositions.Any(p => p.Titre == titre && p.PlatformId == platformId))
                throw new InvalidOperationException("Cette proposition existe déjà.");

            if (string.IsNullOrWhiteSpace(titre)) throw new ArgumentException("Titre invalide.");
            UserProposal newProposal = new UserProposal
            {
                UserId = userId,
                PlatformId = platformId,
                Titre = titre,
                ImagePath = string.IsNullOrWhiteSpace(imagePath) ? string.Empty : imagePath,
                DateSoumission = DateTime.UtcNow,
                Statut = ProposalStatus.EnAttente
            };
            Propositions.Add(newProposal);
            return newProposal;
        }
    }
}