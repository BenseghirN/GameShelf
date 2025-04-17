namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Représente les statistiques d'un jeu.
    /// </summary>
    public class StatsDto
    {
        /// <summary>Nombre total de jeux.</summary>
        public int NbTotalGames { get; set; }

        /// <summary>>Nombre total de jeux en cours.</summary>
        public int NbOngoingGames { get; set; }
    }
}