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
        /// <response code="200">Liste retournée avec succès.</response>
        [HttpGet]
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
        /// <response code="200">Jeu trouvé.</response>
        /// <response code="404">Jeu introuvable.</response>
        [HttpGet("{id}")]
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
        /// <response code="201">Jeu créé avec succès.</response>
        [HttpPost]
        [Authorize(Policy = "Admin")]
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
        /// <response code="200">Mise à jour réussie.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
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
        /// <response code="204">Jeu supprimé avec succès.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await gameService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}