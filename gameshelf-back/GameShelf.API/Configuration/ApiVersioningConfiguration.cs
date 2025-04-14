using Asp.Versioning;

namespace GameShelf.API.Configuration
{
    public static class ApiVersioningConfiguration
    {
        public static void AddApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
    }
}
