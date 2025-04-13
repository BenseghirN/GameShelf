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
    public class TagsController(ITagService tagService) : ControllerBase
    {
        /// <summary>
        /// Récupère tous les tags.
        /// </summary>
        /// <response code="200">Liste des tags retournée avec succès</response>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<TagDto> tags = await tagService.GetAllAsync(cancellationToken);
            return Ok(tags);
        }

        /// <summary>
        /// Crée un nouveau tag (admin uniquement).
        /// </summary>
        /// <response code="201">Tag créé avec succès</response>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(TagDto dto, CancellationToken cancellationToken)
        {
            TagDto tag = await tagService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = tag.Id }, tag);
        }

        /// <summary>
        /// Met à jour un tag (admin uniquement).
        /// </summary>
        /// <param name="id">ID du tag</param>
        /// <response code="200">Tag mis à jour avec succès</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(int id, TagDto dto, CancellationToken cancellationToken)
        {
            TagDto tag = await tagService.UpdateAsync(id, dto, cancellationToken);
            return Ok(tag);
        }

        /// <summary>
        /// Supprime un tag (admin uniquement).
        /// </summary>
        /// <param name="id">ID du tag</param>
        /// <response code="204">Tag supprimé avec succès</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await tagService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}