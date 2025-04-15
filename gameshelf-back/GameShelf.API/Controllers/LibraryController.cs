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
        /// Récupère la bibliothèque de l'utilisateur connecté.
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Liste des jeux de la bibliothèque.</returns>
        /// <response code="200">Liste récupérée avec succès.</response>
        [HttpGet]
        public async Task<IActionResult> GetMyLibrary(CancellationToken cancellationToken)
        {
            List<UserGameDto> games = await libraryService.GetUserLibraryAsync(cancellationToken);
            return Ok(games);
        }

        /// <summary>
        /// Ajoute un jeu à la bibliothèque de l'utilisateur.
        /// </summary>
        /// <param name="gameId">Identifiant du jeu à ajouter.</param>
        /// <param name="dto">Données liées à l'ajout du jeu.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Jeu ajouté à la bibliothèque.</returns>
        /// <response code="200">Jeu ajouté avec succès.</response>
        [HttpPost("{gameId}")]
        public async Task<IActionResult> Add(Guid gameId, [FromBody] AddToLibraryDto dto, CancellationToken cancellationToken)
        {
            UserGameDto result = await libraryService.AddGameToLibraryAsync(gameId, dto.Statut, dto.Note, dto.ImagePersoPath, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Met à jour le statut ou la note d’un jeu dans la bibliothèque.
        /// </summary>
        /// <param name="gameId">Identifiant du jeu.</param>
        /// <param name="dto">Données mises à jour.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Jeu mis à jour.</returns>
        /// <response code="200">Mise à jour réussie.</response>
        [HttpPut("{gameId}")]
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
        /// <response code="204">Suppression réussie.</response>
        [HttpDelete("{gameId}")]
        public async Task<IActionResult> Remove(Guid gameId, CancellationToken cancellationToken)
        {
            await libraryService.RemoveGameFromLibraryAsync(gameId, cancellationToken);
            return NoContent();
        }
    }
}