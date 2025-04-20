public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public AzureAdB2C AzureAdB2C { get; set; } = new();
    public Logging Logging { get; set; } = new();
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; } = "Host=localhost;Port=5432;Database=gameshelf;Username=postgres;Password=admin";
}

public class AzureAdB2C
{
    public string Authority { get; set; } = "https://IramGameShelf.b2clogin.com/IramGameShelf.onmicrosoft.com/B2C_1_signupsignin/v2.0/";
    public string ClientId { get; set; } = "";
    public string CallbackPath { get; set; } = "/signin-oidc";
    public string OpenIdScheme { get; set; } = "GameShelf_OAuth2_B2C";
}

public class Logging
{
    public LogLevel LogLevel { get; set; } = new();
}

public class LogLevel
{
    public string Default { get; set; } = "Information";
    public string MicrosoftAspNetCore { get; set; } = "Warning";
}
