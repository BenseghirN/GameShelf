using System.Diagnostics;

public static class BuildAndDatabaseConfigurator
{
    public static void Run()
    {
        var root = AppContext.BaseDirectory;

        var backendPath = Path.Combine(root, "gameshelf-back");
        Console.WriteLine("🛠️ Compilation du projet .NET...");
        RunCommand("dotnet", $"build \"{backendPath}\"");

        var dockerComposePath = Path.Combine(root, "docker-compose.yml");
        Console.WriteLine("\n🐳 Démarrage des containers Docker...");
        RunCommand("docker", $"compose -f \"{dockerComposePath}\" up -d");

        Console.WriteLine("\n🗃️ Application des migrations EF Core...");
        RunCommand("dotnet", "ef database update " +
                              "--project gameshelf-back/GameShelf.Infrastructure " +
                              "--startup-project gameshelf-back/GameShelf.API");

        Console.WriteLine("\n✅ Build, containers et migrations terminés !");
    }

    private static void RunCommand(string command, string args)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        Console.WriteLine(output);
        if (!string.IsNullOrWhiteSpace(error))
        {
            Console.WriteLine("⚠️ Erreur pendant l'exécution de la commande :");
            Console.WriteLine(error);
        }
    }
}
