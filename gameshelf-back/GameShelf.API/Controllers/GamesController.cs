using Asp.Versioning;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    /// <summary>
    /// Gère les jeux disponibles dans le système.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class GamesController(IGameService gameService) : ControllerBase
    {
        /// <summary>
        /// Récupère tous les jeux disponibles.
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Liste des jeux.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<GameDto> games = await gameService.GetAllAsync(cancellationToken);
            return Ok(games);
        }

        /// <summary>
        /// Récupère un jeu par son identifiant.
        /// </summary>
        /// <param name="id">ID du jeu.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Détails du jeu.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            GameDto? game = await gameService.GetByIdAsync(id, cancellationToken);
            return game == null ? NotFound() : Ok(game);
        }

        /// <summary>
        /// Crée un nouveau jeu (admin uniquement).
        /// </summary>
        /// <param name="dto">Données du jeu à créer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Le jeu nouvellement créé.</returns>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create(GameDto dto, CancellationToken cancellationToken)
        {
            GameDto created = await gameService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Met à jour un jeu existant (admin uniquement).
        /// </summary>
        /// <param name="id">ID du jeu à modifier.</param>
        /// <param name="dto">Données mises à jour.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Le jeu mis à jour.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, GameDto dto, CancellationToken cancellationToken)
        {
            GameDto updated = await gameService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Supprime un jeu (admin uniquement).
        /// </summary>
        /// <param name="id">ID du jeu à supprimer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await gameService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}