using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Redirige l’utilisateur vers Azure B2C pour l’authentification.
        /// </summary>
        /// <param name="returnUrl">URL de retour après connexion</param>
        /// <response code="302">Redirection vers Azure B2C</response>
        [HttpGet("connect")]
        public IActionResult Connect(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = returnUrl,
            }, "AzureADB2C");
        }

        /// <summary>
        /// Retourne les informations de l’utilisateur connecté.
        /// </summary>
        /// <response code="200">Utilisateur authentifié</response>
        /// <response code="401">Utilisateur non connecté</response>
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

        [HttpGet("logout")]
        public async Task<IActionResult> Logout([FromQuery] string? returnUrl = "/")
        {
            // Supprime le cookie d'authentification
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect(returnUrl ?? "/");
        }
    }
}