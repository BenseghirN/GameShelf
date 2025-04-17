import { useEffect, useState } from "react";
import {
  Box,
  TextField,
  CircularProgress,
  Typography,
  Alert,
  Snackbar,
} from "@mui/material";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { loadAllGames } from "@/store/slices/gameSlice";
import GameCard from "@/components/GameCard";
import { Game } from "@/types/Game";
import { useUserGame } from "@/hooks/useUserGame";
import { fetchData } from "@/utils/fetchData";
import GameDetailsModal from "@/components/modals/GameDetails/GameDetailsModal";
import { AddToLibraryDto } from "@/types/AddToLibraryDto";

export default function GamesPage() {
  const dispatch = useAppDispatch();
  const { games, loading, error } = useAppSelector((state) => state.games);
  const [search, setSearch] = useState("");
  const [selectedGame, setSelectedGame] = useState<Game | null>(null);
  const [isModalOpen, setModalOpen] = useState(false);
  const [toast, setToast] = useState<{
    open: boolean;
    message: string;
    severity: "success" | "error" | "info" | "warning";
  }>({
    open: false,
    message: "",
    severity: "info",
  });

  useEffect(() => {
    dispatch(loadAllGames());
  }, [dispatch]);

  const filteredGames = games.filter((game) =>
    game.titre.toLowerCase().includes(search.toLowerCase())
  );

  const {
    userGame,
    loading: userGameLoading,
    error: userGameError,
  } = useUserGame(isModalOpen, selectedGame?.id);

  const handleOpenModal = (game: Game) => {
    setSelectedGame(game);
    setModalOpen(true);
  };

  const handleCloseModal = () => {
    setModalOpen(false);
    setSelectedGame(null);
  };

  const handleAddToLibrary = async () => {
    if (!selectedGame) return;

    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Library/${
      selectedGame.id
    }`;

    const dto: AddToLibraryDto = {
      statut: "Possede",
      note: null,
      imagePersoPath: null,
    };
    const result = await fetchData(url, "POST", dto);

    if (result) {
      showToast("Jeu ajouté à votre bibliothèque !", "success");
      refreshModal();
    } else {
      showToast("Échec de l'ajout", "error");
    }
  };

  const handleRemoveFromLibrary = async () => {
    if (!selectedGame) return;

    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Library/${
      selectedGame.id
    }`;
    const result = await fetchData(url, "DELETE");

    if (result !== null || result === null) {
      showToast("Jeu supprimé de votre bibliothèque !", "success");
      refreshModal();
    } else {
      showToast("Échec de la suppression", "error");
    }
  };

  const refreshModal = () => {
    setModalOpen(false);
    setTimeout(() => setModalOpen(true), 10);
  };

  const showToast = (
    message: string,
    severity: "success" | "error" | "info" | "warning" = "info"
  ) => {
    setToast({ open: true, message, severity });
  };

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
                  <GameCard game={game} onClick={() => handleOpenModal(game)} />
                </Box>
              ))}
            </Box>
          )}
        </>
      )}

      {selectedGame && (
        <GameDetailsModal
          open={isModalOpen}
          onClose={handleCloseModal}
          game={selectedGame}
          userGame={userGame}
          loading={userGameLoading}
          error={userGameError}
          onAddToLibrary={handleAddToLibrary}
          onRemoveFromLibrary={handleRemoveFromLibrary}
          onSave={refreshModal}
        />
      )}
      <Snackbar
        open={toast.open}
        autoHideDuration={3000}
        onClose={() => setToast({ ...toast, open: false })}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        <Alert
          onClose={() => setToast({ ...toast, open: false })}
          severity={toast.severity}
          sx={{ width: "100%" }}
        >
          {toast.message}
        </Alert>
      </Snackbar>
    </Box>
  );
}
