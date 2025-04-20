Console.WriteLine("═══════════════════════════════════════════════");
Console.WriteLine("Bienvenue sur GameShelf - Outil de Setup Dev !");
Console.WriteLine("═══════════════════════════════════════════════\n");

Console.WriteLine("Appuyez sur ENTRÉE pour démarrer la configuration...");
Console.ReadLine();

AppSettingsConfigurator.Run();
BuildAndDatabaseConfigurator.Run();
EnvConfigurator.Run();
FrontendBuilder.Run();





