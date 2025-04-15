using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace GameShelf.API.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection azureB2CSection = configuration.GetSection("AzureAdB2C");
            string authority = azureB2CSection["Authority"] ?? throw new ArgumentNullException("Authority");
            string clientId = azureB2CSection["ClientId"] ?? throw new ArgumentNullException("ClientId");
            string callbackPath = azureB2CSection["CallbackPath"] ?? throw new ArgumentNullException("CallbackPath");

            services.AddAuthentication(options =>
                        {
                            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                        })
                    .AddCookie(options =>
                        {
                            options.Cookie.Name = "GameShelf_auth";         // Custom cookie name
                            options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Use Secure cookies in production (HTTPS)
                            options.Cookie.SameSite = SameSiteMode.Strict;  // Prevent CSRF attacks
                            options.Cookie.HttpOnly = true;                 // Prevent JS access (XSS protection)
                            options.LoginPath = "/api/v1/Auth/connect";     // Triggered when an unauthenticated user hits a protected route
                            options.Events.OnRedirectToLogin = context =>
                            {
                                // When an unauthenticated user tries to access a protected route, it returns a 401 Unauthorized status instead of redirecting to the login page.
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                return Task.CompletedTask;
                            };
                            options.Events.OnRedirectToAccessDenied = context =>
                            {
                                // When a user is denied access due to insufficient permissions, it returns a 403 Forbidden status instead of redirecting to an access-denied page.
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                return Task.CompletedTask;
                            };
                        }
                    )
                    .AddOpenIdConnect("GameShelf_OAuth2_B2C", options =>
                        {
                            options.Authority = authority;
                            options.ClientId = clientId;
                            options.ResponseType = OpenIdConnectResponseType.Code;
                            options.UsePkce = true;
                            options.SaveTokens = true;
                            options.Scope.Clear();
                            options.Scope.Add("openid");
                            options.Scope.Add("profile");
                            options.Scope.Add("offline_access");
                            options.Scope.Add(clientId);
                            options.CallbackPath = callbackPath;
                        });

            services.AddScoped<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddAuthorization(options =>
                options.AddPolicy(name: "Admin", policyBuilder =>
                {
                    policyBuilder
                        .RequireAuthenticatedUser()
                        .AddRequirements(new RoleRequirement(UserRole.Admin));
                }
            ));

            return services;
        }
    }
}

public record RoleRequirement(UserRole Role) : IAuthorizationRequirement;
public class RoleAuthorizationHandler(IGameShelfDbContext dbContext) : AuthorizationHandler<RoleRequirement>()
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        string userEmail = context.User.FindFirst("email")?.Value ?? string.Empty;
        User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user != null && user.Role == requirement.Role)
        {
            context.Succeed(requirement);
        }
    }
}