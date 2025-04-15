using GameShelf.API.Configuration;
using GameShelf.Infrastructure.Configuration;
using GameShelf.Application.Configuration;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddApiVersioningConfiguration();
builder.Services.AddSwaggerConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);

// Add custom services and configurations
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

// 🔐 Redirection HTTPS
app.UseHttpsRedirection();

// 🔑 Auth
app.UseAuthentication();
app.UseAuthorization();

// 🧩 Serveur de fichiers statiques pour ton frontend
app.UseDefaultFiles(); // Sert automatiquement index.html s'il existe
app.UseStaticFiles(new StaticFileOptions // Sert les fichiers statiques (CSS, JS, images, etc.)
{
    OnPrepareResponse = ctx =>
    {
        string path = ctx.File.PhysicalPath ?? string.Empty;
        if (path.EndsWith(".json") || path.EndsWith(".js") || path.EndsWith(".css"))
        {
            ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            ctx.Context.Response.Headers["Pragma"] = "no-cache";
            ctx.Context.Response.Headers["Expires"] = "0";
        }
    }
});

// 🌐 Contrôleurs API (doivent passer AVANT fallback)
app.MapControllers();

// 🎯 Fallback pour le routage SPA (React Router par ex) important pour React Router : fallback vers index.html
app.MapWhen(context =>
    !context.Request.Path.StartsWithSegments("/api"),
    branch =>
    {
        branch.UseRouting();
        branch.UseEndpoints(endpoints =>
        {
            endpoints.MapFallbackToFile("index.html");
        });
    }
);
app.Run();