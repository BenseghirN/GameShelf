using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {

        [HttpGet("connect")]
        public IActionResult Connect(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = returnUrl,
            }, "AzureADB2C");
        }


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
    }
}