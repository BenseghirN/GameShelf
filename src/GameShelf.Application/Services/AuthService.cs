
using System.Security.Claims;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GameShelf.Application.Services
{
    public class AuthService(IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        public UserDto CurrentUser
        {
            get
            {
                ClaimsPrincipal user = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
                if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                {
                    return new UserDto
                    {
                        Id = Guid.Empty,
                        Email = string.Empty,
                        Pseudo = string.Empty
                    };
                }

                string externalId = user.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? string.Empty;
                string email = user.FindFirst("emails")?.Value ?? string.Empty;
                string pseudo = user.FindFirst("name")?.Value ?? string.Empty;
                string givenName = user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value ?? string.Empty;
                string surname = user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value ?? string.Empty;

                return new UserDto
                {
                    ExternalId = externalId,
                    Email = email ?? string.Empty,
                    Pseudo = pseudo ?? string.Empty,
                    GivenName = givenName ?? string.Empty,
                    Surname = surname ?? string.Empty,
                };
            }
        }
    }
}