import { Box, Typography, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

export default function UnauthorizedPage() {
  const navigate = useNavigate();

  return (
    <Box
      textAlign="center"
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
      height="80vh"
      gap={3}
    >
      <img
        src="https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExcWh4cnVtOW55ZXlnNjNwMGVhNDIwbGVmOGs4NTdqbWtmZnlybmUyZSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/3ohzdQ1IynzclJldUQ/giphy.gif"
        alt="Accès refusé"
        style={{ width: "500px", maxWidth: "100%", borderRadius: "8px" }}
      />
      <Typography variant="h3" color="error">
        🚫 Accès refusé
      </Typography>
      <Typography variant="body1" fontSize={18}>
        Vous n’avez pas les droits nécessaires pour accéder à cette page.
      </Typography>

      <Button
        variant="contained"
        onClick={() => navigate("/home")}
        sx={{
          backgroundColor: "#7c3aed",
          color: "white",
          fontWeight: "bold",
          "&:hover": {
            backgroundColor: "#6b21a8",
            boxShadow: 4,
          },
        }}
      >
        Retour à l'accueil
      </Button>
    </Box>
  );
}
