using Asp.Versioning;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    /// <summary>
    /// Gère les utilisateurs de la plateforme (réservé aux administrateurs).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = "Admin")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        /// <summary>
        /// Récupère tous les utilisateurs.
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Liste des utilisateurs.</returns>
        /// <response code="200">Liste des utilisateurs retournée avec succès.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<UserDto> users = await userService.GetAllAsync(cancellationToken);
            return Ok(users);
        }

        /// <summary>
        /// Récupère un utilisateur par son identifiant.
        /// </summary>
        /// <param name="id">ID de l'utilisateur.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Détails de l'utilisateur.</returns>
        /// <response code="200">Utilisateur trouvé.</response>
        /// <response code="404">Utilisateur introuvable.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            UserDto? user = await userService.GetByIdAsync(id, cancellationToken);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Promeut un utilisateur au rôle administrateur.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à promouvoir.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>L'utilisateur mis à jour.</returns>
        [HttpPut("{id}/promote")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PromoteToAdmin(Guid id, CancellationToken cancellationToken)
        {
            UserDto user = await userService.PromoteToAdminAsync(id, cancellationToken);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Rétrograde un administrateur vers le rôle utilisateur.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à rétrograder.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>L'utilisateur mis à jour.</returns>
        [HttpPut("{id}/demote")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DemoteToUser(Guid id, CancellationToken cancellationToken)
        {
            UserDto user = await userService.DemoteToUserAsync(id, cancellationToken);
            return user == null ? NotFound() : Ok(user);
        }
    }
}