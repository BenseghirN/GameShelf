namespace GameShelf.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Pseudo { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        public ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();
        public ICollection<UserProposal> Propositions { get; set; } = new List<UserProposal>();
    }
}