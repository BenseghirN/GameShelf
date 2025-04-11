using Asp.Versioning;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
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
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        /// <summary>
        /// Enregistre un nouvel utilisateur et retourne un token JWT.
        /// </summary>
        /// <param name="dto">Les informations d'enregistrement de l'utilisateur.</param>
        /// <returns>Un objet contenant l'ID, l'email et le token JWT.</returns>
        /// <response code="200">Utilisateur enregistré avec succès.</response>
        /// <response code="400">Requête invalide ou email déjà utilisé.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            try
            {
                AuthResultDto result = await _authService.RegisterAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Connecte un utilisateur et retourne un token JWT.
        /// </summary>
        /// <param name="dto">Les informations de connexion de l'utilisateur.</param>
        /// <returns>Un objet contenant l'ID, l'email et le token JWT.</returns>
        /// <response code="200">Connexion réussie.</response>
        /// <response code="401">Email ou mot de passe incorrect.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                AuthResultDto result = await _authService.LoginAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}