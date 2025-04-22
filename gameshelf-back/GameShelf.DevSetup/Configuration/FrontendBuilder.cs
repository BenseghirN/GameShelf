using System.Diagnostics;

public static class FrontendBuilder
{
    public static void Run()
    {
        var root = Directory.GetCurrentDirectory();
        var frontPath = Path.Combine(root, "gameshelf-front");

        Console.WriteLine("\n==========================================");
        Console.WriteLine("  Étape : Build du frontend React");
        Console.WriteLine("==========================================\n");

        // Vérification que npm.cmd est disponible
        if (!IsCommandAvailable("npm.cmd"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERREUR] npm.cmd introuvable !");
            Console.WriteLine("Assurez-vous que Node.js est installé et que 'npm' est dans le PATH système.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[INFO] Installation des dépendances npm (peut prendre du temps) ...");
        Console.ResetColor();
        RunCommand("npm", "install", frontPath);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[INFO] Lancement du build frontend...");
        Console.ResetColor();
        RunCommand("npm", "run build", frontPath);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[OK] Frontend compilé avec succès !");
        Console.ResetColor();
    }

    private static void RunCommand(string command, string args, string workingDirectory)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command} {args}",
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
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
            Console.WriteLine("[ERREUR] Impossible d'exécuter la commande npm.");
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }

    private static bool IsCommandAvailable(string command)
    {
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "where",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return !string.IsNullOrWhiteSpace(output);
        }
        catch
        {
            return false;
        }
    }
}
