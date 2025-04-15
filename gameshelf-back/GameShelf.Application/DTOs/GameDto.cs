namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Représente un jeu enregistré dans la base.
    /// </summary>
    public class GameDto
    {
        /// <summary>Identifiant unique du jeu.</summary>
        public Guid Id { get; set; }

        /// <summary>Titre du jeu.</summary>
        public string Titre { get; set; } = string.Empty;

        /// <summary>Description du jeu (facultatif).</summary>
        public string? Description { get; set; }

        /// <summary>Date de sortie du jeu (facultatif).</summary>
        public DateTime? DateSortie { get; set; }

        /// <summary>Nom de l'éditeur (facultatif).</summary>
        public string? Editeur { get; set; }

        /// <summary>Chemin vers l'image principale du jeu.</summary>
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>Liste des tags associés.</summary>
        public List<string> Tags { get; set; } = new();

        /// <summary>Liste des plateformes disponibles pour ce jeu.</summary>
        public List<string> Platforms { get; set; } = new();
    }
}