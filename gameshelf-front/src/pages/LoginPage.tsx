import { Button, Container, Typography, Box, useTheme } from "@mui/material";
import GamepadIcon from "@mui/icons-material/SportsEsports";
import Logo from "@/assets/GameShelf.svg";
import { motion } from "framer-motion";

export default function LoginPage() {
  const theme = useTheme();
  const handleLogin = () => {
    const returnUrl = encodeURIComponent("/home");
    const loginUrl = `${
      import.meta.env.VITE_API_BASE_URL
    }/Auth/connect?returnUrl=${returnUrl}`;
    window.location.href = loginUrl;
  };
  return (
    <Container maxWidth="sm">
      <Box mt={5} textAlign="center">
        <motion.div
          initial={{ opacity: 0, y: 30 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 1 }}
        >
          {/* Logo */}
          <img
            src={Logo}
            alt="GameShelf logo"
            style={{
              width: 300,
            }}
          />{" "}
          {/* Titre */}
          <Typography variant="h3" fontWeight={600} gutterBottom>
            Bienvenue sur{" "}
            <span style={{ color: theme.palette.primary.main }}>GameShelf</span>
          </Typography>
        </motion.div>

        {/* Sous-titre */}
        <Typography variant="subtitle1" color="textSecondary" paragraph>
          Connectez-vous pour accéder à votre bibliothèque de jeux.
        </Typography>

        {/* Bouton */}
        <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.95 }}>
          <Button
            variant="contained"
            color="primary"
            onClick={handleLogin}
            size="large"
            startIcon={<GamepadIcon />}
            sx={{ px: 4, py: 1.5, borderRadius: 2, boxShadow: 3 }}
          >
            Se connecter / Créer un compte
          </Button>
        </motion.div>
      </Box>
    </Container>
  );
}
