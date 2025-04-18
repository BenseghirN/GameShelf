namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Données envoyées par un administrateur pour ajouter ou editer un tag.
    /// </summary>
    public class NewTagDto
    {
        /// <summary>Nom du tag.</summary>
        public string Nom { get; set; } = string.Empty;
    }
}