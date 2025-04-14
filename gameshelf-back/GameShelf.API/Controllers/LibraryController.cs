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
    public class LibraryController(ILibraryService libraryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMyLibrary(CancellationToken cancellationToken)
        {
            List<UserGameDto> games = await libraryService.GetUserLibraryAsync(cancellationToken);
            return Ok(games);
        }

        [HttpPost("{gameId}")]
        public async Task<IActionResult> Add(Guid gameId, [FromBody] AddToLibraryDto dto, CancellationToken cancellationToken)
        {
            UserGameDto result = await libraryService.AddGameToLibraryAsync(gameId, dto.Statut, dto.Note, dto.ImagePersoPath, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{gameId}")]
        public async Task<IActionResult> Update(Guid gameId, [FromBody] UpdateLibraryDto dto, CancellationToken cancellationToken)
        {
            UserGameDto result = await libraryService.UpdateGameStatusAsync(gameId, dto.Statut, dto.Note, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{gameId}")]
        public async Task<IActionResult> Remove(Guid gameId, CancellationToken cancellationToken)
        {
            await libraryService.RemoveGameFromLibraryAsync(gameId, cancellationToken);
            return NoContent();
        }
    }
}