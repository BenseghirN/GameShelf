import { Button, Chip } from "@mui/material";

export default function TestPage() {
  return (
    <>
      <h1>TEST</h1>
      <Button color="primary" variant="contained">
        Ajouter un jeu
      </Button>
      <Chip label="Terminé" color="success" />
      <Chip label="En cours" color="warning" />
      <Chip label="Backlog" color="error" />
    </>
  );
}
