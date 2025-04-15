Write-Host "🚀 Lancement de l'environnement Docker..." -ForegroundColor Cyan
docker compose up -d

Write-Host "⌛ Attente de la disponibilité de la base de données..." -ForegroundColor Yellow
Start-Sleep -Seconds 5  # Attendre 5 secondes pour que la base de données soit prète

Write-Host "🔧 Application des migrations EF Core..." -ForegroundColor Cyan
dotnet ef database update --project ./gameshelf-back/GameShelf.Infrastructure --startup-project ./gameshelf-back/GameShelf.API

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Tout est prèt, l'environnement est en place." -ForegroundColor Green
} else {
    Write-Host "❌ Une erreur est survenue pendant la migration." -ForegroundColor Red
}
