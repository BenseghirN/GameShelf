import { GameStats } from "@/types/GameStats";
import { fetchData } from "@/utils/fetchData";
import { Alert, Box, CircularProgress, Paper, Typography } from "@mui/material";
import { motion } from "framer-motion";
import { useEffect, useState } from "react";
import Logo from "@/assets/GameShelf.svg";

export default function HomePage() {
  const [stats, setStats] = useState<GameStats | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchStats = async () => {
      try {
        setLoading(true);
        const result = await fetchData<GameStats>(
          `${import.meta.env.VITE_API_BASE_URL}/api/v1/library/stats`
        );
        if (result) {
          setStats(result);
        } else {
          setError("Impossible de charger les statistiques.");
        }
      } catch (err) {
        setError(
          "Une erreur s'est produite lors du chargement des statistiques: " +
            err
        );
      } finally {
        setLoading(false);
      }
    };
    fetchStats();
  }, []);

  return (
    <Box
      textAlign="center"
      mt={4}
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
      gap={4}
    >
      <motion.img
        src={Logo}
        alt="Logo GameShelf"
        style={{ width: "200px", height: "auto" }}
        initial={{ opacity: 0, scale: 0.9 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.5 }}
      />

      <motion.div
        initial={{ opacity: 0, y: 10 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ delay: 0.3, duration: 0.6 }}
      >
        <Typography variant="h4" fontWeight="bold">
          Bienvenue sur{" "}
          <Box component="span" color="primary.main">
            GameShelf 🎮
          </Box>
        </Typography>
      </motion.div>

      {loading && <CircularProgress />}
      {error && <Alert severity="warning">{error}</Alert>}

      {!loading && stats && !error && (
        <motion.div
          initial={{ opacity: 0, y: 15 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.5 }}
        >
          <Paper elevation={3} sx={{ p: 3, mt: 2 }}>
            <Typography variant="body1" fontSize={18}>
              Vous avez <strong>{stats.nbTotalGames}</strong> jeux dans votre
              bibliothèque, dont <strong>{stats.nbOngoingGames}</strong> en
              cours.
            </Typography>
          </Paper>
        </motion.div>
      )}
    </Box>
  );
}
