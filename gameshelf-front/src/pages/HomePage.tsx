import { Button, Chip } from "@mui/material";

// console.log("✅ React a bien démarré");
const handleLogout = () => {
  window.location.href = `${
    import.meta.env.VITE_API_BASE_URL
  }/Auth/logout?returnUrl=/`;
};
export default function HomePage() {
  return (
    <>
      <h1>Bienvenue sur GameShelf 🎮</h1>
      <Button color="inherit" onClick={handleLogout}>
        Se déconnecter
      </Button>
      <Button color="primary" variant="contained">
        Ajouter un jeu
      </Button>
      <Chip label="Terminé" color="success" />
      <Chip label="En cours" color="warning" />
      <Chip label="Backlog" color="error" />
    </>
  );
}
