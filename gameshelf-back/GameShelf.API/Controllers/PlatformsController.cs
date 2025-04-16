using Asp.Versioning;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    /// <summary>
    /// Gère les plateformes sur lesquelles les jeux peuvent être joués.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PlatformsController(IPlatformService platformService) : ControllerBase
    {
        /// <summary>
        /// Récupère la liste de toutes les plateformes.
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Liste des plateformes disponibles.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlatformDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<PlatformDto> platforms = await platformService.GetAllAsync(cancellationToken);
            return Ok(platforms);
        }

        /// <summary>
        /// Récupère une plateforme par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la plateforme.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Détails de la plateforme.</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            PlatformDto? platform = await platformService.GetByIdAsync(id, cancellationToken);
            return platform == null ? NotFound() : Ok(platform);
        }

        /// <summary>
        /// Crée une nouvelle plateforme (admin uniquement).
        /// </summary>
        /// <param name="dto">Données de la plateforme à créer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Plateforme créée.</returns>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] PlatformDto dto, CancellationToken cancellationToken)
        {
            PlatformDto created = await platformService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Met à jour une plateforme existante (admin uniquement).
        /// </summary>
        /// <param name="id">ID de la plateforme à mettre à jour.</param>
        /// <param name="dto">Données mises à jour.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Plateforme mise à jour.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlatformDto dto, CancellationToken cancellationToken)
        {
            PlatformDto updated = await platformService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }


        /// <summary>
        /// Supprime une plateforme par son identifiant (admin uniquement).
        /// </summary>
        /// <param name="id">ID de la plateforme à supprimer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await platformService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}