using Asp.Versioning;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    /// <summary>
    /// Gère les propositions de jeux soumises par les utilisateurs.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserProposalController(IUserProposalService userProposalService) : ControllerBase
    {
        /// <summary>
        /// Soumet une nouvelle proposition de jeu.
        /// </summary>
        /// <param name="dto">Données de la proposition.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>La proposition créée.</returns>
        /// <response code="201">Proposition créée avec succès.</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewProposalDto dto, CancellationToken cancellationToken)
        {
            UserProposalDto created = await userProposalService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        /// <summary>
        /// Récupère toutes les propositions soumises (admin uniquement).
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Liste des propositions.</returns>
        /// <response code="200">Liste récupérée avec succès.</response>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<UserProposalDto> proposals = await userProposalService.GetAllAsync(cancellationToken);
            return Ok(proposals);
        }

        /// <summary>
        /// Accepte une proposition utilisateur (admin uniquement).
        /// </summary>
        /// <param name="id">Identifiant de la proposition à accepter.</param>
        /// <param name="dto">Données complémentaires (description, tags).</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <response code="204">Proposition acceptée avec succès.</response>
        [HttpPut("{id}/accept")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Accept(Guid id, [FromBody] AcceptProposalDto dto, CancellationToken cancellationToken)
        {
            await userProposalService.AcceptAsync(id, dto.Description, dto.Tags, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Rejette une proposition utilisateur (admin uniquement).
        /// </summary>
        /// <param name="id">Identifiant de la proposition à rejeter.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <response code="204">Proposition rejetée avec succès.</response>
        [HttpPut("{id}/reject")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Reject(Guid id, CancellationToken cancellationToken)
        {
            await userProposalService.RejectAsync(id, cancellationToken);
            return NoContent();
        }
    }
}