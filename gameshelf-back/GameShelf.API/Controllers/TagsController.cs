using Asp.Versioning;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    /// <summary>
    /// Gère les tags utilisés pour catégoriser les jeux.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TagsController(ITagService tagService) : ControllerBase
    {
        /// <summary>
        /// Récupère la liste de tous les tags.
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Liste des tags disponibles.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TagDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<TagDto> tags = await tagService.GetAllAsync(cancellationToken);
            return Ok(tags);
        }

        /// <summary>
        /// Crée un nouveau tag (réservé aux administrateurs).
        /// </summary>
        /// <param name="dto">Données du tag à créer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Le tag nouvellement créé.</returns>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(TagDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create(TagDto dto, CancellationToken cancellationToken)
        {
            TagDto tag = await tagService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = tag.Id }, tag);
        }

        /// <summary>
        /// Met à jour un tag existant (admin uniquement).
        /// </summary>
        /// <param name="id">Identifiant du tag.</param>
        /// <param name="dto">Données mises à jour.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Le tag mis à jour.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(TagDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(int id, TagDto dto, CancellationToken cancellationToken)
        {
            TagDto tag = await tagService.UpdateAsync(id, dto, cancellationToken);
            return Ok(tag);
        }

        /// <summary>
        /// Supprime un tag existant (admin uniquement).
        /// </summary>
        /// <param name="id">Identifiant du tag.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await tagService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}