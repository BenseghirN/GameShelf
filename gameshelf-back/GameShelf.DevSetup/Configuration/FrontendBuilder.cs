using System.Diagnostics;

public static class FrontendBuilder
{
    public static void Run()
    {
        var root = AppContext.BaseDirectory;
        var frontPath = Path.Combine(root, "gameshelf-front");

        Console.WriteLine("\n🎨 Build du frontend...");

        RunCommand("npm", "install", frontPath);
        RunCommand("npm", "run build", frontPath);

        Console.WriteLine("✅ Frontend compilé avec succès !");
    }

    private static void RunCommand(string command, string args, string workingDirectory)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                WorkingDirectory = workingDirectory,
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
            Console.WriteLine("⚠️ Erreur npm :");
            Console.WriteLine(error);
        }
    }
}
