import { Box, Typography, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

export default function NotFoundPage() {
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
        src="https://media0.giphy.com/media/v1.Y2lkPTc5MGI3NjExc2t1eWV6a2pncW5penV2MHFqeTJ4bGFrY2U5NDRyaWcxc2dnZ2pkcCZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/g01ZnwAUvutuK8GIQn/giphy.gif"
        alt="Page introuvable"
        style={{ width: "500px", maxWidth: "100%", borderRadius: "8px" }}
      />

      <Typography variant="h3" color="primary">
        🤔 404 – Page introuvable
      </Typography>
      <Typography variant="body1" fontSize={18}>
        On dirait bien que cette page n'existe pas...
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
