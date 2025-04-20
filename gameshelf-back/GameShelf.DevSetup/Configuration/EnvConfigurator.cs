public static class EnvConfigurator
{
    public static void Run()
    {
        var root = Directory.GetCurrentDirectory();
        var envPath = Path.Combine(root, "gameshelf-front", ".env");

        Console.WriteLine("\n==========================================");
        Console.WriteLine("  Étape : Génération du fichier .env");
        Console.WriteLine("==========================================\n");


        try
        {
            if (File.Exists(envPath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Suppression de l'ancien fichier .env...");
                Console.ResetColor();
                File.Delete(envPath);
            }

            var viteLine = "VITE_API_BASE_URL=http://localhost:5187";

            File.WriteAllText(envPath, viteLine);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[OK] Fichier .env généré avec succès !");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERREUR] Impossible de générer le fichier .env.");
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }
}
