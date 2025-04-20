using System.Text.Json;

public static class AppSettingsConfigurator
{
    public static void Run()
    {
        var root = AppContext.BaseDirectory;
        var settingsPath = Path.Combine(root, "gameshelf-back", "GameShelf.API", "appsettings.Development.json");

        if (File.Exists(settingsPath))
        {
            Console.WriteLine("🗑️ Suppression de l'ancien appsettings...");
            File.Delete(settingsPath);
        }

        Console.Write("🔑 Entrez le ClientId Azure B2C : ");
        string clientId = Console.ReadLine()?.Trim() ?? "";

        var settings = new AppSettings
        {
            AzureAdB2C = new AzureAdB2C
            {
                ClientId = clientId
            }
        };

        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        File.WriteAllText(settingsPath, json);
        Console.WriteLine("✅ Fichier appsettings.dev.json généré avec succès !\n");
    }
}
