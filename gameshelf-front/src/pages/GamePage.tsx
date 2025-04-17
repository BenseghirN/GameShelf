import { useEffect, useState } from "react";
import {
  Box,
  TextField,
  CircularProgress,
  Typography,
  Alert,
} from "@mui/material";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { loadAllGames } from "@/store/slices/gameSlice";
import GameCard from "@/components/GameCard";

export default function GamesPage() {
  const dispatch = useAppDispatch();
  const { games, loading, error } = useAppSelector((state) => state.games);
  const [search, setSearch] = useState("");

  useEffect(() => {
    dispatch(loadAllGames());
  }, [dispatch]);

  const filteredGames = games.filter((game) =>
    game.titre.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <Box px={2} py={4}>
      {/* Champ de recherche pour filtrer les jeux */}
      <Box maxWidth="600px" mx="auto" mb={4}>
        <TextField
          fullWidth
          label="Rechercher un jeu"
          variant="outlined"
          size="small"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          sx={{ mb: 3 }}
        />
      </Box>

      {loading && <CircularProgress />}
      {error && <Alert severity="error">{error}</Alert>}

      {!loading && !error && (
        <>
          {filteredGames.length === 0 ? (
            <Typography>Aucun jeu trouvé.</Typography>
          ) : (
            <Box
              display="grid"
              gridTemplateColumns={{
                xs: "repeat(1, 1fr)",
                sm: "repeat(2, 1fr)",
                md: "repeat(3, 1fr)",
                lg: "repeat(4, 1fr)",
              }}
              gap={2}
            >
              {filteredGames.map((game) => (
                <Box key={game.id}>
                  <GameCard game={game} />
                </Box>
              ))}
            </Box>
          )}
        </>
      )}
    </Box>
  );
}
