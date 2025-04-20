public static class EnvConfigurator
{
    public static void Run()
    {
        var root = AppContext.BaseDirectory;
        var envPath = Path.Combine(root, "gameshelf-front", ".env");

        if (File.Exists(envPath))
        {
            Console.WriteLine("🗑️ Suppression de l'ancien fichier .env...");
            File.Delete(envPath);
        }

        var viteLine = "VITE_API_BASE_URL=http://localhost:5187";

        File.WriteAllText(envPath, viteLine);
        Console.WriteLine("✅ Fichier .env généré avec succès !");
    }
}
