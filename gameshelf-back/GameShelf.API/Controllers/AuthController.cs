using Asp.Versioning;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    /// <summary>
    /// Gère l'authentification de l'utilisateur via Azure B2C.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthController(IConfiguration configuration, IUserSynchronizationService userSynchronizationService) : ControllerBase
    {
        /// <summary>
        /// Redirige l’utilisateur vers Azure B2C pour l’authentification.
        /// </summary>
        /// <param name="returnUrl">URL de redirection après connexion.</param>
        /// <response code="302">Redirection vers le fournisseur d'identité.</response>
        [HttpGet("connect")]
        public IActionResult Connect(string returnUrl = "/")
        {
            string OpenIdScheme = configuration["AzureAdB2C:OpenIdScheme"] ?? throw new InvalidOperationException("AzureAdB2C:OpenIdScheme configuration is missing.");
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = returnUrl,
            }, OpenIdScheme);
        }

        /// <summary>
        /// Retourne les informations de l’utilisateur actuellement authentifié.
        /// </summary>
        /// <returns>Liste des claims de l’utilisateur.</returns>
        /// <response code="200">Utilisateur authentifié, informations retournées.</response>
        /// <response code="401">Aucun utilisateur connecté.</response>
        [HttpGet("me")]
        public IActionResult Me()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Ok(new { claims = User.Claims.Select(c => new { c.Type, c.Value }).ToArray() });
            }
            else
            {
                return Unauthorized(new { message = "User is not authenticated" });
            }
        }

        /// <summary>
        /// Retourne les informations de l’utilisateur actuellement authentifié.
        /// </summary>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Informations de l'utilisateur actuellement authentifié</returns>
        /// <response code="200">>Utilisateur authentifié, informations retournées.</response>
        /// <response code="404">Utilisateur introuvable.</response>
        /// <response code="401">Aucun utilisateur connecté.</response>
        [HttpGet("user-info")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInfo(CancellationToken cancellationToken)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await userSynchronizationService.GetCurrentUserInfosAsync(cancellationToken);
                return user == null ? NotFound() : Ok(user);
            }
            else
            {
                return Unauthorized(new { message = "User is not authenticated" });
            }
        }

        /// <summary>
        /// Déconnecte l’utilisateur en supprimant le cookie d’authentification.
        /// </summary>
        /// <param name="returnUrl">URL de redirection après déconnexion.</param>
        /// <returns>Redirection vers l’URL indiquée.</returns>
        /// <response code="302">Redirection après déconnexion.</response>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout([FromQuery] string? returnUrl = "/")
        {
            // Supprime le cookie d'authentification
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect(returnUrl ?? "/");
        }
    }
}