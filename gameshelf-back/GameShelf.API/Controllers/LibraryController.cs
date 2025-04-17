using Asp.Versioning;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    /// <summary>
    /// Gère la bibliothèque personnelle de l'utilisateur.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LibraryController(ILibraryService libraryService) : ControllerBase
    {
        /// <summary>
        /// Récupère la bibliothèque de jeux de l'utilisateur actuellement connecté.
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Liste des jeux ajoutés par l'utilisateur à sa bibliothèque.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyLibrary(CancellationToken cancellationToken)
        {
            List<UserGameDto> games = await libraryService.GetUserLibraryAsync(cancellationToken);
            return Ok(games);
        }

        /// <summary>
        /// Ajoute un jeu à la bibliothèque de l'utilisateur.
        /// </summary>
        /// <param name="gameId">Identifiant du jeu à ajouter.</param>
        /// <param name="dto">Données de statut, note et image personnalisée.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Le jeu ajouté dans la bibliothèque de l'utilisateur.</returns>
        [HttpPost("{gameId}")]
        [ProducesResponseType(typeof(UserGameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Add(Guid gameId, [FromBody] AddToLibraryDto dto, CancellationToken cancellationToken)
        {
            UserGameDto result = await libraryService.AddGameToLibraryAsync(gameId, dto.Statut, dto.Note, dto.ImagePersoPath, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Met à jour le statut ou la note d’un jeu dans la bibliothèque de l'utilisateur.
        /// </summary>
        /// <param name="gameId">Identifiant du jeu à mettre à jour.</param>
        /// <param name="dto">Nouvelles valeurs de statut et/ou de note.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Le jeu mis à jour dans la bibliothèque.</returns>
        [HttpPut("{gameId}")]
        [ProducesResponseType(typeof(UserGameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid gameId, [FromBody] UpdateLibraryDto dto, CancellationToken cancellationToken)
        {
            UserGameDto result = await libraryService.UpdateGameStatusAsync(gameId, dto.Statut, dto.Note, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Supprime un jeu de la bibliothèque de l'utilisateur.
        /// </summary>
        /// <param name="gameId">Identifiant du jeu à retirer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        [HttpDelete("{gameId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(Guid gameId, CancellationToken cancellationToken)
        {
            await libraryService.RemoveGameFromLibraryAsync(gameId, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Récupère des statistiques sur la bibliothèque de l'utilisateur connecté.
        /// </summary>
        /// <remarks>
        /// Ce point retourne le nombre total de jeux ainsi que le nombre de jeux en cours pour l'utilisateur connecté.
        /// </remarks>
        /// <returns>Un objet contenant le nombre total de jeux et le nombre de jeux en cours.</returns>
        /// <response code="200">Statistiques retournées avec succès</response>
        /// <response code="401">Utilisateur non authentifié</response>
        [HttpGet("stats")]
        [ProducesResponseType(typeof(StatsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetLibraryStats(CancellationToken cancellationToken)
        {
            var stats = await libraryService.GetLibraryStatsAsync(cancellationToken);
            return Ok(stats);
        }
    }
}