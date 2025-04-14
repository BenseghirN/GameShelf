using GameShelf.API.Configuration;
using GameShelf.Infrastructure.Configuration;
using GameShelf.Application.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddApiVersioningConfiguration();
builder.Services.AddSwaggerConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();
builder.Services.AddAuthenticationConfiguration();

// Add custom services and configurations
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseDefaultFiles(); // Sert automatiquement index.html s'il existe
app.UseStaticFiles(new StaticFileOptions
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
// Important pour React Router : fallback vers index.html
app.MapWhen(ctx => !ctx.Request.Path.StartsWithSegments("/api"), subApp =>
{
    subApp.UseRouting();
    subApp.UseEndpoints(endpoints =>
    {
        endpoints.MapFallbackToFile("index.html");
    });
});
app.Run();