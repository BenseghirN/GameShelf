namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Données envoyées par un administrateur pour ajouter ou editer un jeu.
    /// </summary>
    public class NewGameDto
    {
        /// <summary>Titre du jeu.</summary>
        public string Titre { get; set; } = string.Empty;

        /// <summary>Description du jeu (facultatif).</summary>
        public string? Description { get; set; }

        /// <summary>Date de sortie du jeu (facultatif).</summary>
        public DateTime? DateSortie { get; set; }

        /// <summary>Nom de l'éditeur (facultatif).</summary>
        public string? Editeur { get; set; }

        /// <summary>Chemin vers l'image principale du jeu.</summary>
        public string? ImagePath { get; set; } = string.Empty;

        /// <summary>Liste des tags associés.</summary>
        public List<Guid> TagIds { get; set; } = new();

        /// <summary>Liste des plateformes disponibles pour ce jeu.</summary>
        public List<Guid> PlatformIds { get; set; } = new();
    }
}