using System.Text.Json;

public static class AppSettingsConfigurator
{
    public static void Run()
    {
        var root = Directory.GetCurrentDirectory();
        var settingsPath = Path.Combine(root, "gameshelf-back", "GameShelf.API", "appsettings.Development.json");

        Console.WriteLine("\n==========================================");
        Console.WriteLine("  Étape : Configuration du appsettings");
        Console.WriteLine("==========================================\n");

        try
        {
            if (File.Exists(settingsPath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Suppression de l'ancien appsettings...");
                Console.ResetColor();
                File.Delete(settingsPath);
            }

            Console.Write("[INFO] Entrez le ClientId Azure B2C : ");
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[OK] Fichier appsettings.Development.json généré avec succès !");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERREUR] Une erreur est survenue lors de la génération du fichier appsettings.");
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }
}
