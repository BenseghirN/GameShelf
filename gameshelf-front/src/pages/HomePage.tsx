import { useAppSelector } from "@/store/hooks";
import { Button, Chip } from "@mui/material";
import { useNavigate } from "react-router-dom";

export default function HomePage() {
  const user = useAppSelector((state) => state.auth.user);
  const navigate = useNavigate();
  const handleLogout = () => {
    window.location.href = `${
      import.meta.env.VITE_API_BASE_URL
    }/Auth/logout?returnUrl=/`;
  };

  return (
    <>
      <h1>Bienvenue sur GameShelf 🎮</h1>
      {user?.pseudo}
      {""}
      {user?.givenName}
      {""}
      {user?.surname}
      {""}
      {user?.email}
      {""}
      {user?.role}
      {""}
      {user?.isAdmin}
      <Button color="inherit" onClick={handleLogout}>
        Se déconnecter
      </Button>
      <Button
        color="primary"
        variant="contained"
        onClick={() => navigate("/test")}
      >
        Goto Test
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
