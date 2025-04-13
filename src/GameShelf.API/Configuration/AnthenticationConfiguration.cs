
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace GameShelf.API.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
                        {
                            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                        })
                    .AddCookie(options =>
                        {
                            options.LoginPath = "/api/v1/Auth/connect";     // Triggered when an unauthenticated user hits a protected route
                            options.Cookie.HttpOnly = true;                 // Prevent JS access (XSS protection)
                            options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                            options.Cookie.SameSite = SameSiteMode.Strict;
                            options.Cookie.Name = "bff_auth";               // Custom cookie name
                        }
                    )
                    .AddOpenIdConnect("AzureADB2C", options =>
                        {
                            options.Authority = "https://IramGameShelf.b2clogin.com/IramGameShelf.onmicrosoft.com/B2C_1_signupsignin/v2.0/";
                            options.ClientId = "3cefd93d-1ca0-4e3d-bc3b-031f43b09a5e";
                            options.ResponseType = OpenIdConnectResponseType.Code;
                            options.UsePkce = true;
                            options.SaveTokens = true;
                            options.Scope.Clear();
                            options.Scope.Add("openid");
                            options.Scope.Add("profile");
                            options.Scope.Add("offline_access");
                            options.Scope.Add(options.ClientId);
                            options.CallbackPath = "/signin-oidc";
                        });
            services.AddScoped<IAuthorizationHandler, RoleAuthorizationHandler>();

            services.AddAuthorization(options =>
                options.AddPolicy(name: "Admin", policyBuilder =>
                {
                    policyBuilder.AddRequirements(new RoleRequirement(UserRole.Admin));
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