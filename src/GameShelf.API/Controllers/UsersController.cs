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
    [Authorize(Policy = "Admin")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        /// <summary>
        /// Récupère tous les utilisateurs.
        /// </summary>
        /// <response code="200">Liste des utilisateurs</response>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<UserDto> users = await userService.GetAllAsync(cancellationToken);
            return Ok(users);
        }

        /// <summary>
        /// Récupère un utilisateur par ID.
        /// </summary>
        /// <param name="id">ID utilisateur</param>
        /// <response code="200">Utilisateur trouvé</response>
        /// <response code="404">Utilisateur introuvable</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            UserDto? user = await userService.GetByIdAsync(id, cancellationToken);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Promeut un utilisateur au rôle Admin.
        /// </summary>
        /// <param name="id">ID utilisateur</param>
        /// <response code="204">Promotion effectuée</response>
        [HttpPut("{id}/promote")]
        public async Task<IActionResult> PromoteToAdmin(Guid id, CancellationToken cancellationToken)
        {
            await userService.PromoteToAdminAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Rétrograde un utilisateur au rôle User.
        /// </summary>
        /// <param name="id">ID utilisateur</param>
        /// <response code="204">Rétrogradation effectuée</response>
        [HttpPut("{id}/demote")]
        public async Task<IActionResult> DemoteToUser(Guid id, CancellationToken cancellationToken)
        {
            await userService.DemoteToUserAsync(id, cancellationToken);
            return NoContent();
        }
    }
}