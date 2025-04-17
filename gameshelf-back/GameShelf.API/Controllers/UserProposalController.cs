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
        /// Récupère toutes les propositions soumises (admin uniquement).
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Liste des propositions.</returns>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(List<UserProposalDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<UserProposalDto> proposals = await userProposalService.GetAllAsync(cancellationToken);
            return Ok(proposals);
        }

        /// <summary>
        /// Récupère les propositions de jeu de l'utilisateur connecté.
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Liste des propositions.</returns>
        [HttpGet("mine")]
        [ProducesResponseType(typeof(List<UserProposalDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyProposals(CancellationToken cancellationToken)
        {
            List<UserProposalDto> result = await userProposalService.GetProposalsForCurrentUserAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Récupère une proposition par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la proposition.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Détails de la proposition.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserProposalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            UserProposalDto? proposal = await userProposalService.GetByIdAsync(id, cancellationToken);
            return proposal == null ? NotFound() : Ok(proposal);
        }

        /// <summary>
        /// Soumet une nouvelle proposition de jeu.
        /// </summary>
        /// <param name="dto">Données de la proposition.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>La proposition créée.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserProposalDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] NewProposalDto dto, CancellationToken cancellationToken)
        {
            UserProposalDto created = await userProposalService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        /// <summary>
        /// Met à jour la proposition de jeu de l'utilisateur.
        /// </summary>
        /// <param name="proposalId">Identifiant du jeu à mettre à jour.</param>
        /// <param name="dto">Nouvelles valeurs de titre et ou plateforme.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Le jeu mis à jour dans la bibliothèque.</returns>
        [HttpPut("{proposalId}")]
        [ProducesResponseType(typeof(UserProposalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid proposalId, [FromBody] NewProposalDto dto, CancellationToken cancellationToken)
        {
            UserProposalDto result = await userProposalService.UpdateProposalInfoAsync(proposalId, dto, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Supprime une proposition de jeu appartenant à l'utilisateur connecté.
        /// </summary>
        /// <param name="proposalId">Identifiant de la proposition à supprimer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        [HttpDelete("{proposalId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProposal(Guid proposalId, CancellationToken cancellationToken)
        {
            await userProposalService.DeleteProposalAsync(proposalId, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Accepte une proposition utilisateur (admin uniquement).
        /// </summary>
        /// <param name="id">Identifiant de la proposition à accepter.</param>
        /// <param name="dto">Données complémentaires (description, tags).</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        [HttpPut("{id}/accept")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Accept(Guid id, [FromBody] AcceptProposalDto dto, CancellationToken cancellationToken)
        {
            await userProposalService.AcceptAsync(id, dto.Description, dto.TagIds, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Rejette une proposition utilisateur (admin uniquement).
        /// </summary>
        /// <param name="id">Identifiant de la proposition à rejeter.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        [HttpPut("{id}/reject")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Reject(Guid id, CancellationToken cancellationToken)
        {
            await userProposalService.RejectAsync(id, cancellationToken);
            return NoContent();
        }
    }
}