using Asp.Versioning;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
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
        /// <response code="200">Liste récupérée avec succès.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlatformDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<PlatformDto> platforms = await platformService.GetAllAsync(cancellationToken);
            return Ok(platforms);
        }

        /// <summary>
        /// Récupère une plateforme par son ID.
        /// </summary>
        /// <param name="id">Identifiant de la plateforme.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>La plateforme correspondante.</returns>
        /// <response code="200">Plateforme trouvée.</response>
        /// <response code="404">Plateforme introuvable.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            PlatformDto? platform = await platformService.GetByIdAsync(id, cancellationToken);
            return platform == null ? NotFound() : Ok(platform);
        }

        /// <summary>
        /// Crée une nouvelle plateforme.
        /// </summary>
        /// <param name="dto">Données de la plateforme à créer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>La plateforme créée.</returns>
        /// <response code="201">Création réussie.</response>
        [HttpPost]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status201Created)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] PlatformDto dto, CancellationToken cancellationToken)
        {
            PlatformDto created = await platformService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Met à jour une plateforme existante.
        /// </summary>
        /// <param name="id">ID de la plateforme à mettre à jour.</param>
        /// <param name="dto">Données mises à jour.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>La plateforme mise à jour.</returns>
        /// <response code="200">Mise à jour réussie.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status200OK)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlatformDto dto, CancellationToken cancellationToken)
        {
            PlatformDto updated = await platformService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Supprime une plateforme par son ID.
        /// </summary>
        /// <param name="id">ID de la plateforme à supprimer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <response code="204">Suppression réussie.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await platformService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}