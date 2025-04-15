Write-Host "🚀 Lancement de l'environnement Docker..." -ForegroundColor Cyan
docker compose up -d

Write-Host "⏳ Attente de la disponibilité de la base de données..." -ForegroundColor Yellow
Start-Sleep -Seconds 10  # Attendre 10 secondes pour que la base de données soit prête

Write-Host "🔧 Application des migrations EF Core..." -ForegroundColor Cyan
dotnet ef database update --project ./src/GameShelf.Infrastructure --startup-project ./src/GameShelf.API

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Tout est prêt, l'environnement est en place." -ForegroundColor Green
} else {
    Write-Host "❌ Une erreur est survenue pendant la migration." -ForegroundColor Red
}
