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
    public class UserProposalController(IUserProposalService userProposalService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewProposalDto dto, CancellationToken cancellationToken)
        {
            UserProposalDto created = await userProposalService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<UserProposalDto> proposals = await userProposalService.GetAllAsync(cancellationToken);
            return Ok(proposals);
        }

        [HttpPut("{id}/accept")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Accept(Guid id, [FromBody] AcceptProposalDto dto, CancellationToken cancellationToken)
        {
            await userProposalService.AcceptAsync(id, dto.Description, dto.Tags, cancellationToken);
            return NoContent();
        }

        [HttpPut("{id}/reject")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Reject(Guid id, CancellationToken cancellationToken)
        {
            await userProposalService.RejectAsync(id, cancellationToken);
            return NoContent();
        }
    }
}