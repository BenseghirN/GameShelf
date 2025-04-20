using System.Diagnostics;

public static class BuildAndDatabaseConfigurator
{
    public static void Run()
    {
        var root = Directory.GetCurrentDirectory();
        try
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("  Étape : Build .NET, Docker et Migrations");
            Console.WriteLine("==========================================\n");

            var backendPath = Path.Combine(root, "gameshelf-back");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[INFO] Compilation des projets .NET...");
            Console.ResetColor();
            RunCommand("dotnet", $"build \"{backendPath}\"");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[INFO] Démarrage des containers Docker...");
            Console.ResetColor();
            RunDockerDirect("docker", "compose up -d");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[INFO] Application des migrations EF Core...");
            Console.ResetColor();
            RunCommand("dotnet", "ef database update " +
                                 "--project gameshelf-back/GameShelf.Infrastructure " +
                                 "--startup-project gameshelf-back/GameShelf.API");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[OK] Build, Docker et migrations terminés !");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERREUR] Une erreur est survenue pendant le setup backend.");
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }

    private static void RunDockerDirect(string command, string args)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command} {args}",
                UseShellExecute = true,
                CreateNoWindow = false
            }
        };

        process.Start();
        process.WaitForExit();
    }

    private static void RunCommand(string command, string args)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command} {args}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false
            }
        };

        try
        {
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                Console.WriteLine(line);
            }
            while (!process.StandardError.EndOfStream)
            {
                var errLine = process.StandardError.ReadLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errLine);
                Console.ResetColor();
            }
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERREUR] La commande a échoué : {command} {args}");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERREUR] Impossible d'exécuter : {command} {args}");
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }
}
