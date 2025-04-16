namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Données nécessaires à l'acceptation d'une proposition utilisateur.
    /// </summary>
    public class AcceptProposalDto
    {
        /// <summary>Description du jeu tel qu'il sera enregistré.</summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>Liste des guids de tags associés au jeu accepté.</summary>
        public List<Guid> TagIds { get; set; } = new();
    }
}